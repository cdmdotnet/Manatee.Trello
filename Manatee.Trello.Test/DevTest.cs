using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Contracts;
using Manatee.Trello.Extensions;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Json;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		public enum TestEnum
		{
			[System.ComponentModel.Description("value1")]
			Value1,
			[System.ComponentModel.Description("value2")]
			Value2,
			[System.ComponentModel.Description("value3")]
			Value3
		}

		[TestMethod]
		public void Test()
		{
			var serializer = new JsonSerializer();
			serializer.Options.EnumSerializationFormat = EnumSerializationFormat.AsName;
			var json = serializer.Serialize(TestEnum.Value2);
			Console.WriteLine(json);
		}

		[TestMethod]
		public void TestMethod1()
		{
			Run(() =>
				{
					var board = Member.Me.Boards.FirstOrDefault(b => b.Organization != null);
					if (board == null) return;
					Console.WriteLine(board.Name);
					Console.WriteLine(board.Organization.DisplayName);
				});
		}

		private static void Run(System.Action action)
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new RestSharpClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			TrelloConfiguration.ThrowOnTrelloError = true;

			action();

			SpinWait.SpinUntil(() => !RestRequestProcessor.HasRequests);
		}
	}
}

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
		[TestMethod]
		public void Test()
		{
			foreach (var color in Enum.GetValues(typeof(LabelColor)))
			{
				var desc = ((Enum) color).ToDescription();
				Console.WriteLine(desc);
				var parsed = desc.ToEnum<LabelColor>();
				Console.WriteLine(parsed);
			}
		}

		[TestMethod]
		public void TestMethod1()
		{
			Card card = null;
			Run(() =>
				{
					card = new Card(TrelloIds.CardId) {Position = Position.Bottom};
				});

			Console.WriteLine(card.Position);
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

			Thread.Sleep(100);

			SpinWait.SpinUntil(() => !RestRequestProcessor.HasRequests);
		}
	}

	public static class Extensions
	{
		public static string ToDescription(this Enum value)
		{
			var type = value.GetType();
			var field = type.GetField(value.ToString());
			var da = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			return da.Length > 0 ? da[0].Description : value.ToString();
		}
		public static T ToEnum<T>(this string stringValue, T defaultValue = default (T))
		{
			foreach (T enumValue in Enum.GetValues(typeof(T)))
			{
				var type = typeof(T);
				var field = type.GetField(enumValue.ToString());
				var da = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (da.Length > 0 && da[0].Description == stringValue)
					return enumValue;
			}
			return defaultValue;
		}
	}
}

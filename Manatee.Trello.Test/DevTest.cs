using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Contracts;
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
		public void TestMethod1()
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new RestSharpClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			var checkList = new CheckList("53d5c89dbf82d6dfed20ca94");

			Console.WriteLine(checkList);
			foreach (var checkItem in checkList.CheckItems)
			{
				Console.WriteLine(checkItem);
				Console.WriteLine(checkItem.State);
			}
			checkList.CheckItems.First().State = CheckItemState.Complete;
			Console.WriteLine(checkList.CheckItems.First().State);

			Thread.Sleep(200);

			SpinWait.SpinUntil(() => !RestRequestProcessor.HasRequests);
		}
	}
}

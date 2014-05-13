using System;
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
			TrelloServiceConfiguration.ThrowOnTrelloError = false;
			var serializer = new ManateeSerializer();
			TrelloServiceConfiguration.Serializer = serializer;
			TrelloServiceConfiguration.Deserializer = serializer;
			TrelloServiceConfiguration.RestClientProvider = new RestSharpClientProvider();

			var auth = new TrelloAuthorization(TrelloIds.AppKey, "asfqwrqewqg3524qwerarb");
			var service = new TrelloService(auth);

			var card = service.Retrieve<Card>(TrelloIds.CardId);

			Console.WriteLine(card);
		}
	}
}

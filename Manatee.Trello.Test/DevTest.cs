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
			TrelloServiceConfiguration.ThrowOnTrelloError = true;
			var serializer = new ManateeSerializer();
			TrelloServiceConfiguration.Serializer = serializer;
			TrelloServiceConfiguration.Deserializer = serializer;
			TrelloServiceConfiguration.RestClientProvider = new RestSharpClientProvider();

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(auth);

			var card = service.Retrieve<Card>(TrelloIds.CardId);

			var description = card.Description;

			Console.WriteLine(description);

			card.Description = "this is a **new** description";
			card.Refresh();

			Console.WriteLine(card.Description);

			card.Description = description;
			card.Refresh();

			Console.WriteLine(card.Description);
		}
	}
}

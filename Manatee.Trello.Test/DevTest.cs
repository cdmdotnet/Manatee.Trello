using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Json.Newtonsoft;
using Manatee.Trello.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			//Options.UseNewtonsoftJson();
			var service = new TrelloService(TrelloIds.AppKey, TrelloIds.UserToken);

			var list = service.Retrieve<List>(TrelloIds.ListId);
			var card = list.AddCard("card to delete");
			var duplicate = list.Cards.Last();
			
			Assert.AreSame(card, duplicate);
			card.Delete();

			card.MarkForUpdate();
			Console.WriteLine(card);
		}
	}
}

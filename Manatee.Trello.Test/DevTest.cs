using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Json.Extensions;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;
using Manatee.Trello.Test.FunctionalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			var service = new TrelloService(TrelloIds.Key, TrelloIds.Token);

			var board = service.Retrieve<Board>(TrelloIds.BoardId);
			var cards = board.Lists.SelectMany(l => l.Cards);
			var card = cards.FirstOrDefault();
			
			Console.WriteLine(card.ToJson().GetIndentedString());
		}
	}
}

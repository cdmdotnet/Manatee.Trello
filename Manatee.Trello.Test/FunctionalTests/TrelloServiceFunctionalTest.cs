using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.FunctionalTests
{
	[TestClass]
	public class TrelloServiceFunctionalTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			var sut = new TrelloService(TrelloIds.Key, TrelloIds.Token);

			var card = sut.Retrieve<Card>(TrelloIds.CardId);
			var list = card.List;
			var board = card.Board;
			var lists = board.Lists;

			Assert.AreEqual(3, lists.Count());
			Assert.IsTrue(lists.Contains(list));
		}
	}
}

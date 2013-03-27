using System;
using System.Linq;
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
			var list = board.Lists.First();

			list.IsClosed = true;

			var archivedList = board.ArchivedLists.First();

			Assert.AreEqual(list.Id, archivedList.Id);

			list.IsClosed = false;
		}
	}
}

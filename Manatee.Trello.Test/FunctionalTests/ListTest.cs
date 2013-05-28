using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.FunctionalTests
{
	[TestClass]
	public class ListTest
	{
		[TestMethod]
		public void RetrieveAndModify()
		{
			var service = new TrelloService(TrelloIds.AppKey, TrelloIds.UserToken);
			var list = service.Retrieve<List>(TrelloIds.ListId);
			var oldName = list.Name;
			var oldPos = list.Position;
			try
			{
				const string name = "Manatee.Trello.ListTest.RetrieveAndModify";
				var board = list.Board;
				
				Assert.AreNotEqual(0, list.Actions.Count());
				Assert.AreEqual(TrelloIds.BoardId, board.Id);
				Assert.AreNotEqual(0, list.Cards);
				Assert.AreEqual(false, list.IsClosed);
				Assert.AreEqual(false, list.IsSubscribed);

				list.Name = name;
				list.Position = Position.Bottom;
				list.IsSubscribed = true;
				list.IsClosed = true;

				Assert.AreEqual(name, list.Name);
				Assert.AreEqual(list, board.Lists.OrderBy(l => l.Position).Last());
				Assert.AreEqual(true, list.IsSubscribed);
				Assert.AreEqual(true, list.IsClosed);
			}
			finally
			{
				list.Name = oldName;
				list.Position = oldPos;
				list.IsSubscribed = false;
				list.IsClosed = false;

				Assert.AreEqual(oldName, list.Name);
				Assert.AreEqual(oldPos, list.Position);
				Assert.AreEqual(false, list.IsSubscribed);
				Assert.AreEqual(false, list.IsClosed);
			}
		}
	}
}

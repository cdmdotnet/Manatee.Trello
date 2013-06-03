using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.FunctionalTests
{
	[TestClass]
	public class CheckListTest
	{
		[TestMethod]
		public void CreateModifyAndDestroy()
		{
			CheckList checklist = null;
			try
			{
				const string checklistName = "Manatee.Trello.FunctionalTests.CheckListTest";
				const string newCheckListName = checklistName + " updated";
				var service = new TrelloService(TrelloIds.AppKey, TrelloIds.UserToken);
				var card = service.Retrieve<Card>(TrelloIds.CardId);
				if (card == null)
					Assert.Inconclusive(string.Format("Could not find card with ID = {{{0}}}", TrelloIds.CardId));
				var list = service.Retrieve<List>(TrelloIds.ListId);
				if (list == null)
					Assert.Inconclusive(string.Format("Could not find card with ID = {{{0}}}", TrelloIds.CardId));
				var newCard = list.Cards.First(c => c != card);

				checklist = card.AddCheckList(checklistName, Position.Top);

				Assert.IsNotNull(checklist);
				Assert.AreEqual(checklistName, checklist.Name);
				Assert.AreEqual(TrelloIds.BoardId, card.Board.Id);
				Assert.AreEqual(TrelloIds.CardId, checklist.Card.Id);
				Assert.AreEqual(0, checklist.CheckItems.Count());
				Assert.AreNotEqual(0, card.CheckLists.Count());
				Assert.AreEqual(card.CheckLists.OrderBy(c => c.Position).First(), checklist);

				VerifyCheckItem(checklist);

				checklist.Name = newCheckListName;
				checklist.Position = Position.Bottom;

				Assert.AreEqual(newCheckListName, checklist.Name);
				Assert.AreEqual(card.CheckLists.OrderBy(c => c.Position).Last(), checklist);

				checklist.Card = newCard;

				Assert.AreEqual(newCard, checklist.Card);
			}
			finally
			{
				if (checklist != null)
					checklist.Delete();
			}
		}

		private void VerifyCheckItem(CheckList checklist)
		{
			const string checkItemName = "Manatee.Trello.FunctionalTests.VerifyCheckItem";
			const string newCheckItemName = checkItemName + " updated";
			checklist.AddCheckItem(checkItemName + " temp");
			var checkItem = checklist.AddCheckItem(checkItemName, CheckItemStateType.Incomplete, Position.Top);
			checklist.AddCheckItem(checkItemName + " temp 2");

			Assert.IsNotNull(checkItem);
			Assert.AreEqual(checkItemName, checkItem.Name);
			Assert.AreNotEqual(0, checklist.CheckItems.Count());
			Assert.AreEqual(checklist.CheckItems.OrderBy(c => c.Position).First(), checkItem);

			checkItem.Name = newCheckItemName;
			checkItem.Position = Position.Bottom;
			checkItem.State = CheckItemStateType.Complete;

			Assert.AreEqual(newCheckItemName, checkItem.Name);
			Assert.AreEqual(checklist.CheckItems.OrderBy(c => c.Position).Last(), checkItem);

			checkItem.Delete();

			Assert.AreEqual(2, checklist.CheckItems.Count());
		}
	}
}

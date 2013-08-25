using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.FunctionalTests
{
	[TestClass]
	public class CardTest
	{
		[TestMethod]
		public void CreateModifyAndDestroy()
		{
			Card card = null;
			List list = null;
			try
			{
				const string cardName = "Manatee.Trello.FunctionalTests.CardTest";
				const string cardDescription =
					"This is a card created by the functional test method CardTest.CreateModifyAndDestroy() in Manatee.Trello.";
				const string newCardName = cardName + " renamed";
				const string newCardDescription = cardDescription + " updated";
				const string attachmentName = "attachment";
				const string checkListName = "checkList";
				const string comment = "comment";
				const LabelColor label = LabelColor.Orange;
				var dueDate = DateTime.Now.TruncateToMilliSeconds();
				var options = new TrelloServiceConfiguration();
				var serializer = new ManateeSerializer();
				options.Serializer = serializer;
				options.Deserializer = serializer;
				options.RestClientProvider = new RestSharpClientProvider(options);
				var service = new TrelloService(options, TrelloIds.AppKey, TrelloIds.UserToken);
				list = service.Retrieve<List>(TrelloIds.ListId);
				if (list == null)
					Assert.Inconclusive("Could not find list with ID = {{{0}}}", TrelloIds.ListId);
				var member = service.Me;

				card = list.AddCard(cardName, cardDescription, Position.Top);

				Assert.IsNotNull(card);
				Assert.AreEqual(cardName, card.Name);
				Assert.AreEqual(TrelloIds.BoardId, card.Board.Id);
				Assert.AreEqual(TrelloIds.ListId, card.List.Id);
				Assert.AreEqual(list.Cards.OrderBy(c => c.Position).First(), card);
				Assert.AreEqual(cardDescription, card.Description);
				Assert.AreEqual(0, card.Actions.Count());
				Assert.AreEqual(0, card.Attachments.Count());
				Assert.AreEqual(0, card.CheckLists.Count());
				Assert.AreEqual(0, card.Comments.Count());
				Assert.AreEqual(0, card.Members.Count());
				Assert.IsNull(card.DueDate);
				Assert.IsNotNull(card.IsClosed);
				Assert.IsFalse(card.IsClosed.Value);
				Assert.IsNotNull(card.IsSubscribed);
				Assert.IsFalse(card.IsSubscribed.Value);
				Assert.AreEqual(0, card.Labels.Count());
				Assert.AreEqual(0, card.VotingMembers.Count());

				card.Name = newCardName;
				card.Description = newCardDescription;
				card.AddAttachment(attachmentName, TrelloIds.AttachmentUrl);
				card.AddCheckList(checkListName);
				card.AddComment(comment);
				card.ApplyLabel(label);
				card.AssignMember(member);
				card.IsClosed = true;
				card.IsSubscribed = true;
				card.DueDate = dueDate;

				Assert.IsNotNull(card.IsClosed);
				Assert.IsTrue(card.IsClosed.Value);

				card.IsClosed = false;
				card.Position = Position.Bottom;

				Assert.AreEqual(newCardName, card.Name);
				Assert.AreEqual(list.Cards.OrderBy(c => c.Position).Last(), card);
				Assert.AreEqual(newCardDescription, card.Description);
				Assert.AreNotEqual(0, card.Actions.Count());
				Assert.AreNotEqual(0, card.Attachments.Count());
				Assert.AreEqual(attachmentName, card.Attachments.First().Name);
				Assert.AreNotEqual(0, card.CheckLists.Count());
				Assert.AreEqual(checkListName, card.CheckLists.First().Name);
				Assert.AreNotEqual(0, card.Comments.Count());
				//Assert.AreEqual(comment, card.Comments.First().Text);
				Assert.AreEqual(1, card.Members.Count());
				Assert.AreEqual(member, card.Members.First());
				Assert.IsNotNull(card.DueDate);
				Assert.AreEqual(dueDate, card.DueDate.Value);
				Assert.IsNotNull(card.IsClosed);
				Assert.IsFalse(card.IsClosed.Value);
				Assert.IsNotNull(card.IsSubscribed);
				Assert.IsTrue(card.IsSubscribed.Value);
				Assert.AreEqual(1, card.Labels.Count());
				Assert.AreEqual(label, card.Labels.First().Color);

				card.RemoveLabel(label);
				card.RemoveMember(member);

				card.MarkForUpdate();
				Assert.IsNotNull(card.LastActivityDate);

				Assert.AreEqual(0, card.Labels.Count());
				Assert.AreEqual(0, card.Members.Count());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			finally
			{
				if (card != null)
				{
					card.Delete();
					Assert.AreNotEqual(list.Cards.FirstOrDefault(), card);
				}
			}
		}
	}
}

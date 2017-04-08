using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Json;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.ManateeJson.Entities;
using Manatee.Trello.RestSharp;
using Manatee.Trello.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class ClientTests
	{
		[TestMethod]
		public void Issue26_NotificationTypeCardDueSoonNotDeserializing()
		{
			var text =
				"{\"id\":\"571ca99c1aa4fb7e9e30bb0b\",\"unread\":false,\"type\":\"cardDueSoon\",\"date\":\"2016-04-24T11:10:19.997Z\",\"data\":{\"board\":{\"name\":\"Team\",\"id\":\"5718d772857c2a4b2a2befb8\"},\"card\":{\"due\":\"2016-04-25T11:00:00.000Z\",\"shortLink\":\"f5sdWFLT\",\"idShort\":19,\"name\":\"AS MRC Training\",\"id\":\"570e55eb131202e342f205ad\"}},\"idMemberCreator\":null}";
			var serializer = new ManateeSerializer();
			var expected = new ManateeNotification
				{
					Id = "571ca99c1aa4fb7e9e30bb0b",
					Unread = false,
					Type = NotificationType.CardDueSoon,
					Date = DateTime.Parse("2016-04-24T11:10:19.997Z"),
					Data = new ManateeNotificationData
						{
							Board = new ManateeBoard
								{
									Name = "Team",
									Id = "5718d772857c2a4b2a2befb8"
								},
							Card = new ManateeCard
								{
									Due = DateTime.Parse("2016-04-25T11:00:00.000Z"),
									ShortUrl = "f5sdWFLT",
									IdShort = 19,
									Name = "AS MRC Training",
									Id = "570e55eb131202e342f205ad"
								}
						},
					MemberCreator = null
				};

			var actual = serializer.Deserialize<IJsonNotification>(text);

			Assert.IsTrue(TheGodComparer.Instance.Equals(expected, actual));
		}

		[TestMethod]
		public void Issue30_PartialSearch_True()
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new WebApiClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			var board = new Board(TrelloIds.BoardId);
			var searchText = "car";
			var search = new Search(SearchFor.Text(searchText), modelTypes: SearchModelType.Cards, context: new[] {board}, isPartial: true);

			// search will include archived cards as well as matches in card descriptions.
			Assert.AreEqual(6, search.Cards.Count());

			TrelloProcessor.Flush();
		}

		[TestMethod]
		public void Issue30_PartialSearch_False()
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new WebApiClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			var board = new Board(TrelloIds.BoardId);
			var searchText = "car";
			var search = new Search(SearchFor.Text(searchText), modelTypes: SearchModelType.Cards, context: new[] {board});

			Assert.AreEqual(0, search.Cards.Count());

			TrelloProcessor.Flush();
		}

		[TestMethod]
		public async Task Issue32_CancelPendingRequests()
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new WebApiClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			TrelloProcessor.ConcurrentCallCount = 1;

			var cards = new List<Card>
				{
					new Card("KHKms82H"),
					new Card("AgTd8qhF"),
					new Card("R1Kc5KHd"),
					new Card("vlgbqJES"),
					new Card("uVD9TAIK"),
					new Card("prSr36Ny"),
					new Card("hBoTLb9V"),
				};

			var nameTasks = cards.Select(c => Task.Run(() => c.Name)).ToList();

			TrelloProcessor.CancelPendingRequests();

			var names = await Task.WhenAll(nameTasks);

			Assert.AreEqual(0, names.Count(n => n != null));
		}

		[TestMethod]
		public async Task Issue33_CardsNotDownloading()
		{
			//json, REST and trello setup
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new RestSharpClientProvider();

			//app key and token, user required to enter token
			TrelloAuthorization.Default.AppKey = "440a184b181002cf00f63713a7f51191";
			TrelloAuthorization.Default.UserToken = "dfd8dd877fa1775db502f891370fb26882a4d8bad41a1cc8cf1a58874b21322b";

			TrelloConfiguration.ThrowOnTrelloError = true;

			Console.WriteLine(Member.Me);
			var boardID = "574e95edd8a4fc16207f7079";
			var board = new Board(boardID);
			Console.WriteLine(board);

			//here is where it calls the exception with 'invalid id'
			foreach (var card in board.Cards)
			{
				Console.WriteLine(card);
			}
		}

		[TestMethod]
		public void Issue35_DatesReturningAs1DayBefore()
		{
			Card card = null;
			try
			{
				var serializer = new ManateeSerializer();
				TrelloConfiguration.Serializer = serializer;
				TrelloConfiguration.Deserializer = serializer;
				TrelloConfiguration.JsonFactory = new ManateeFactory();
				TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
				TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
				TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;
				var learningBoard = new Board(TrelloIds.BoardId);
				string listId = learningBoard.Lists.First().Id;
				var list = new List(listId);
				var member = list.Board.Members.First();
				card = list.Cards.Add("test card 2");
				card.DueDate = new DateTime(2016, 07, 21);

				TrelloProcessor.Flush();

				var cardCopy = new Card(card.Id);
				Assert.AreEqual(new DateTime(2016, 07, 21), cardCopy.DueDate);
			}
			finally
			{
				card?.Delete();
			}
		}

		[TestMethod]
		public void Issue36_CardAttachmentByUrlThrows()
		{
			Card card = null;
			try
			{
				var serializer = new ManateeSerializer();
				TrelloConfiguration.Serializer = serializer;
				TrelloConfiguration.Deserializer = serializer;
				TrelloConfiguration.JsonFactory = new ManateeFactory();
				TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
				TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
				TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

				var list = new List(TrelloIds.ListId);
				card = list.Cards.Add("attachment test");
				card.Attachments.Add("http://i.imgur.com/eKgKEOn.jpg", "me");
			}
			finally
			{
				card?.Delete();
			}
		}

		[TestMethod]
		public void Issue37_InconsistentDateEncoding()
		{
			Card card = null;
			try
			{
				var serializer = new ManateeSerializer();
				TrelloConfiguration.Serializer = serializer;
				TrelloConfiguration.Deserializer = serializer;
				TrelloConfiguration.JsonFactory = new ManateeFactory();
				TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
				TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
				TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

				var list = new List(TrelloIds.ListId);
				card = list.Cards.Add("date encoding test");
				// 2016-12-08T04:45:00.000Z
				var date = Convert.ToDateTime("8/12/2016 5:45:00PM");
				card.DueDate = date;

				TrelloProcessor.Flush();
			}
			finally
			{
				card?.Delete();
			}
		}

		[TestMethod]
		public void Issue45_DueDateAsMinValue()
		{
			Card card = null;
			try
			{
				var serializer = new ManateeSerializer();
				TrelloConfiguration.Serializer = serializer;
				TrelloConfiguration.Deserializer = serializer;
				TrelloConfiguration.JsonFactory = new ManateeFactory();
				TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
				TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
				TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

				var list = new List(TrelloIds.ListId);
				card = list.Cards.Add("min date test");
				card.Description = "a description";
				card.DueDate = DateTime.MinValue;

				TrelloProcessor.Flush();
			}
			finally
			{
				card?.Delete();
			}
		}

		[TestMethod]
		public void Issue47_AddCardWithDetails()
		{
			Card card = null;
			var name = "card detailed creation test";
			var description = "this is a description";
			var position = Position.Top;
			var dueDate = new DateTime(2014, 1, 1);
			try
			{
				var serializer = new ManateeSerializer();
				TrelloConfiguration.Serializer = serializer;
				TrelloConfiguration.Deserializer = serializer;
				TrelloConfiguration.JsonFactory = new ManateeFactory();
				TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
				TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
				TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;
				var members = new Member[] {Member.Me};
				var board = new Board(TrelloIds.BoardId);
				var labels = new[] {board.Labels.FirstOrDefault(l => l.Color == LabelColor.Blue)};

				var list = new List(TrelloIds.ListId);
				card = list.Cards.Add(name, description, position, dueDate, true, members, labels);

				var recard = new Card(card.Id);

				Assert.AreEqual(name, recard.Name);
				Assert.AreEqual(description, recard.Description);
				Assert.AreEqual(card.Id, list.Cards.First().Id);
				Assert.AreEqual(dueDate, recard.DueDate);
				Assert.IsTrue(recard.IsComplete.Value, "card not complete");
				Assert.IsTrue(recard.Members.Contains(Member.Me), "member not found");
				Assert.IsTrue(recard.Labels.Contains(labels[0]), "label not found");

				TrelloProcessor.Flush();
			}
			finally
			{
				card?.Delete();
			}
		}

		[TestMethod]
		public void Issue46_DueComplete()
		{
			Card card = null;
			var name = "due complete test";
			var description = "this is a description";
			var position = Position.Top;
			var dueDate = new DateTime(2014, 1, 1);
			try
			{
				var serializer = new ManateeSerializer();
				TrelloConfiguration.Serializer = serializer;
				TrelloConfiguration.Deserializer = serializer;
				TrelloConfiguration.JsonFactory = new ManateeFactory();
				TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
				TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
				TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

				var list = new List(TrelloIds.ListId);
				card = list.Cards.Add(name, description, position, dueDate);

				Assert.AreEqual(false, card.IsComplete);

				card.IsComplete = true;

				TrelloProcessor.Flush();

				var recard = new Card(card.Id);

				Assert.AreEqual(true, recard.IsComplete);

				TrelloProcessor.Flush();
			}
			finally
			{
				card?.Delete();
			}
		}

		[TestMethod]
		public void Issue59_EditComments()
		{
			Card card = null;
			var name = "edit comment test";
			try
			{
				var serializer = new ManateeSerializer();
				TrelloConfiguration.Serializer = serializer;
				TrelloConfiguration.Deserializer = serializer;
				TrelloConfiguration.JsonFactory = new ManateeFactory();
				TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
				TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
				TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;
				TrelloConfiguration.ExpiryTime = TimeSpan.FromSeconds(1);

				var list = new List(TrelloIds.ListId);
				card = list.Cards.Add(name);
				var comment = card.Comments.Add("This is a comment");
				comment.Data.Text = "This comment was changed.";

				Thread.Sleep(5);

				TrelloProcessor.Flush();
			}
			finally
			{
				card?.Delete();
			}
		}

		[TestMethod]
		public void Issue60_BoardPreferencesFromSearch()
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;
			TrelloConfiguration.ExpiryTime = TimeSpan.FromSeconds(1);

			var search = new Search(SearchFor.TextInName("Sandbox"), 1, SearchModelType.Boards);
			var board = search.Boards.FirstOrDefault();

			Assert.IsNotNull(board.Preferences.Background.Color);
		}

		[TestMethod]
		public void Issue84_ListNameNotDownloading()
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;
			TrelloConfiguration.ExpiryTime = TimeSpan.FromSeconds(1);

			var list = new List(TrelloIds.ListId);

			Assert.IsNotNull(list.Name);
		}
	}
}

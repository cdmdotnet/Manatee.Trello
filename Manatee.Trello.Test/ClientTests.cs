using System;
using System.Collections.Generic;
using System.Linq;
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
		public void NotificationTypeCardDueSoonNotDeserializing_Issue26()
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
		public void PartialSearch_True_Issue30()
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
		public void PartialSearch_False_Issue30()
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
		public async Task CancelPendingRequests_Issue32()
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
		public async Task CardsNotDownloading_Issue33()
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
	}
}

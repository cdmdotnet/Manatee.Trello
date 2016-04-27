using System;
using Manatee.Trello.Json;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.ManateeJson.Entities;
using Manatee.Trello.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class ClientTests
	{
		public static void Run(System.Action action)
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new WebApiClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			action();

			TrelloProcessor.Shutdown();
		}

		[TestMethod]
		public void NotificationTypeCardDueSoonNotDeserializing_Issue26()
		{
			var text = "{\"id\":\"571ca99c1aa4fb7e9e30bb0b\",\"unread\":false,\"type\":\"cardDueSoon\",\"date\":\"2016-04-24T11:10:19.997Z\",\"data\":{\"board\":{\"name\":\"Team\",\"id\":\"5718d772857c2a4b2a2befb8\"},\"card\":{\"due\":\"2016-04-25T11:00:00.000Z\",\"shortLink\":\"f5sdWFLT\",\"idShort\":19,\"name\":\"AS MRC Training\",\"id\":\"570e55eb131202e342f205ad\"}},\"idMemberCreator\":null}";
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
	}
}

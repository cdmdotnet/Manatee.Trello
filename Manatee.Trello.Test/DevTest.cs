using System;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			var serializer = new ManateeSerializer();
			TrelloServiceConfiguration.Serializer = serializer;
			TrelloServiceConfiguration.Deserializer = serializer;
			TrelloServiceConfiguration.RestClientProvider = new RestSharpClientProvider();

			var auth = new TrelloAuthorization(TrelloIds.AppKey, "4a691187d87ea9d47723f74b9af4cbe466f8cc08dd8fbd9568e431803cd21abf");
			var service = new TrelloService(auth);

			var me = service.Me;

			var actions = Task.Factory.StartNew(() => GetActions(me));
			var notifications = Task.Factory.StartNew(() => GetNotifications(me));
			var cards = Task.Factory.StartNew(() => GetCards(me));

			SpinWait.SpinUntil(() => actions.IsCompleted && notifications.IsCompleted && cards.IsCompleted);

			Console.WriteLine("Done");
		}

		private void GetActions(Me me)
		{
			foreach (var action in me.Actions)
			{
				Console.WriteLine("Action: {0}", action);
			}
		}

		private void GetNotifications(Me me)
		{
			foreach (var notification in me.Notifications)
			{
				Console.WriteLine("Notification: {0}", notification);
			}
		}

		private void GetCards(Me me)
		{
			foreach (var board in me.Boards)
			{
				foreach (var list in board.Lists)
				{
					foreach (var card in list.Cards)
					{
						Console.WriteLine("Card: {0}", card);
					}
				}
			}
		}
	}
}

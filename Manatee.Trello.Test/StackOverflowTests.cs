using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class StackOverflowTests
	{
		private static void Run(System.Action action)
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new WebApiClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			action();

			TrelloProcessor.Flush();
		}

		#region http://stackoverflow.com/q/39926431/878701

		private static void Move(Card card, int position, List list = null)
		{
			if (list != null && list != card.List)
			{
				card.List = list;
			}

			card.Position = position;
		}

		[TestMethod]
		public void MovingCards()
		{
			Run(() =>
				    {
						var list = new List(TrelloIds.ListId);
					    var cards = new List<Card>();
					    for (int i = 0; i < 10; i++)
					    {
						    cards.Add(list.Cards.Add("test card " + i));
					    }

					    var otherList = list.Board.Lists.Last();

						cards.AsParallel().ForAll(c => Move(c, 1, otherList));

					    foreach (var card in cards)
					    {
						    card.Delete();
					    }
					});
		}

		#endregion
	}
}

using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.Tests.Common;
using Manatee.Trello.WebApi;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	[Ignore("These tests need to work.")]
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

		private static void Move(ICard card, int position, IList list = null)
		{
			if (list != null && list != card.List)
			{
				card.List = list;
			}

			card.Position = (Position) position;
		}

		[Test]
		public void MovingCards()
		{
			Run(() =>
				    {
						IList list = new List(TrelloIds.ListId);
					    var cards = new List<ICard>();
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

		#region http://stackoverflow.com/q/43667744/878701

		[Test]
		public void LabelNamesAndCards()
		{
			Run(() =>
				{
					var card = new Card(TrelloIds.CardId);

					foreach (var label in card.Labels)
					{
						Assert.IsNotNull(label.Name);
						// I can't always ensure Color isn't null.  Colorless labels are a thing.
						//Assert.IsNotNull(label.Color);
					}
				});
		}

		#endregion
	}
}

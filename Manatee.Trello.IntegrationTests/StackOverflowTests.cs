using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manatee.Trello.Tests.Common;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	[Ignore("These tests need to work.")]
	public class StackOverflowTests
	{
		private static async Task Run(Func<Task> action)
		{
			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;

			await action();

			await TrelloProcessor.Flush();
		}

		#region http://stackoverflow.com/q/39926431/878701

		private static void Move(ICard card, int position, IList list = null)
		{
			if (list != null && list != card.List)
			{
				card.List = list;
			}

			card.Position = position;
		}

		[Test]
		public async Task MovingCards()
		{
			await Run(async () =>
				{
					IList list = new List(TrelloIds.ListId);
					var cards = new List<ICard>();
					for (int i = 0; i < 10; i++)
					{
						cards.Add(await list.Cards.Add("test card " + i));
					}

					var otherList = list.Board.Lists.Last();

					cards.AsParallel().ForAll(c => Move(c, 1, otherList));

					foreach (var card in cards)
					{
						await card.Delete();
					}
				});
		}

		#endregion

		#region http://stackoverflow.com/q/43667744/878701

		[Test]
		public async Task LabelNamesAndCards()
		{
			await Run(async () =>
				{
					var card = new Card(TrelloIds.CardId);
					await card.Refresh();

					foreach (var label in card.Labels)
					{
						Assert.IsNotNull(label.Name);
						Assert.IsNotNull(label.Color);
					}
				});
		}

		#endregion
	}
}

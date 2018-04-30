using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class CardTests
	{
		[Test]
		public async Task UpdateCardData()
		{
			var card = await TestEnvironment.Current.BuildCard();

			var date = DateTime.Now;

			card.Name = "changed";
			card.Description = "changed";
			card.DueDate = date;
			card.IsArchived = true;
			card.IsComplete = true;
			card.Position = 157;

			await TrelloProcessor.Flush();

			TrelloConfiguration.Cache.Clear();

			var reCard = TestEnvironment.Current.Factory.Card(card.Id);

			Assert.AreNotSame(card, reCard);

			await reCard.Refresh();

			reCard.Name.Should().Be(card.Name);
			reCard.Description.Should().Be(card.Description);
			reCard.DueDate.Should().Be(card.DueDate?.Truncate(TimeSpan.FromMilliseconds(1)));
			reCard.IsArchived.Should().Be(card.IsArchived);
			reCard.IsComplete.Should().Be(card.IsComplete);
			reCard.Position.Should().Be(card.Position);
		}
	}
}
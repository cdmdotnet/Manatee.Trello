using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class ClientTestsOfTheNew
	{
		[Test]
		public async Task Issue205_CopyCardWithAttachment()
		{
			var board = TestEnvironment.Current.Board;
			await board.Lists.Refresh();
			var list = board.Lists.Last();
			var cards = list.Cards;
			var sourceCard = await TestEnvironment.Current.BuildCard();
			var jpeg = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Files/smallest-jpeg.jpg");
			await sourceCard.Attachments.Add(File.ReadAllBytes(jpeg), "smallest-jpeg.jpg");

			TrelloConfiguration.Cache.Remove(sourceCard);

			sourceCard = new Card(sourceCard.Id);

			// make sure that we only have the ID
			Assert.IsNull(sourceCard.Name);

			var card = await cards.Add(sourceCard);

			await sourceCard.Refresh();

			Assert.AreNotEqual(sourceCard.Id, card.Id);
			Assert.AreEqual(sourceCard.Name, card.Name);
		}

		[Test]
		[SetCulture("de-DE")]
		public async Task Issue203_CustomNumberFieldsInAlternateCulture()
		{
			await TestEnvironment.Current.Board.EnsurePowerUp(new CustomFieldsPowerUp());

			var numberField = await TestEnvironment.Current.Board.CustomFields.Add(nameof(Issue203_CustomNumberFieldsInAlternateCulture), CustomFieldType.Number);
			var card = await TestEnvironment.Current.BuildCard();

			await numberField.SetValueForCard(card, 9.6);
		}

		[Test]
		public async Task Issue254_CardListIsNull()
		{
			var card = await TestEnvironment.Current.BuildCard();

			await TestEnvironment.Current.RunClean(async () =>
				{
					var board = TestEnvironment.Current.Factory.Board(TestEnvironment.Current.Board.Id);

					await board.Refresh();
					await board.CustomFields.Refresh();
					await board.Lists.Refresh();
					await board.Cards.Refresh();

					board.Cards[0].List.Should().NotBeNull();
				});
		}
	}
}

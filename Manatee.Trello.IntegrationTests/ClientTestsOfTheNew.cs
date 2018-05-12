using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class ClientTestsOfTheNew
	{
		[Test]
		public async Task Issue205_CopyCard()
		{
			var board = TestEnvironment.Current.Board;
			await board.Lists.Refresh();
			var list = board.Lists.Last();
			var cards = list.Cards;
			var sourceCard = await TestEnvironment.Current.BuildCard();

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
			await TestEnvironment.Current.Board.PowerUps.EnablePowerUp(new CustomFieldsPowerUp());

			var numberField = await TestEnvironment.Current.Board.CustomFields.Add("NumberField", CustomFieldType.Number);
			var card = await TestEnvironment.Current.BuildCard();

			await numberField.SetValueForCard(card, 9.6);
		}
	}
}

using System;
using System.Linq;
using System.Threading;
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

			TrelloConfiguration.Cache.Remove(card);

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

		[Test]
		public async Task ChangeList()
		{
			var card = await TestEnvironment.Current.BuildCard();
			var list = await TestEnvironment.Current.BuildList();

			await card.Refresh();

			card.List = list;

			await TrelloProcessor.Flush();

			TrelloConfiguration.Cache.Remove(card);

			var reCard = TestEnvironment.Current.Factory.Card(card.Id);

			Assert.AreNotSame(card, reCard);

			await reCard.Refresh();

			reCard.List.Id.Should().Be(card.List.Id);
		}

		[Test]
		[Ignore("Need to determine how to enable custom fields on the board before this will run.")]
		public async Task CustomFields()
		{
			var numberField = await TestEnvironment.Current.Board.CustomFields.Add("NumberField", CustomFieldType.Number);
			var textField = await TestEnvironment.Current.Board.CustomFields.Add("TextField", CustomFieldType.Text);
			var dateField = await TestEnvironment.Current.Board.CustomFields.Add("DateField", CustomFieldType.DateTime);
			var dropDownField = await TestEnvironment.Current.Board.CustomFields.Add("DropDownField", CustomFieldType.DropDown, CancellationToken.None,
			                                                                         TestEnvironment.Current.Factory.DropDownOption("one"),
			                                                                         TestEnvironment.Current.Factory.DropDownOption("two"));
			var checkBoxField = await TestEnvironment.Current.Board.CustomFields.Add("CheckBoxField", CustomFieldType.CheckBox);

			var card = await TestEnvironment.Current.BuildCard();

			var today = DateTime.Today;
			var two = dropDownField.Options.FirstOrDefault(o => o.Text == "two");

			await numberField.SetValueForCard(card, 9);
			await textField.SetValueForCard(card, "text");
			await dateField.SetValueForCard(card, today);
			await dropDownField.SetValueForCard(card, two);
			await checkBoxField.SetValueForCard(card, true);

			await card.Refresh();

			card.CustomFields.OfType<NumberField>().First().Value.Should().Be(9);
			card.CustomFields.OfType<TextField>().First().Value.Should().Be("text");
			card.CustomFields.OfType<DateTimeField>().First().Value.Should().Be(today);
			card.CustomFields.OfType<DropDownField>().First().Value.Text.Should().Be("two");
			card.CustomFields.OfType<CheckBoxField>().First().Value.Should().Be(true);
		}
	}
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

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
			card.Description = "changed also";
			card.DueDate = date;
			card.IsArchived = true;
			card.IsComplete = true;
			card.Position = 157;

			await TrelloProcessor.Flush();

			TrelloConfiguration.Cache.Remove(card);
			var reCard = TestEnvironment.Current.Factory.Card(card.Id);

			await reCard.Refresh();

			reCard.Name.Should().Be("changed");
			reCard.Description.Should().Be("changed also");
			reCard.DueDate.Should().Be(date.Truncate(TimeSpan.FromMilliseconds(1)));
			reCard.IsArchived.Should().Be(true);
			reCard.IsComplete.Should().Be(true);
			reCard.Position.Should().Be(157);
			reCard.ShortId.Should().NotBeNull();
			reCard.Url.Should().NotBeNullOrEmpty();
			reCard.ShortUrl.Should().NotBeNullOrEmpty();
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

			await reCard.Refresh();

			reCard.List.Id.Should().Be(list.Id);
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

		[Test]
		public async Task CardCanBeDeleted()
		{
			var card = await TestEnvironment.Current.BuildCard();
			var id = card.Id;

			await card.Refresh();

			await card.Delete();

			TrelloConfiguration.Cache.Remove(card);

			var reCard = TestEnvironment.Current.Factory.Card(id);

			Assert.ThrowsAsync<TrelloInteractionException>(async () => await reCard.Refresh());
		}

		[Test]
		public async Task RemovingAllFieldsDownloadsNothing()
		{
			try
			{
				Card.DownloadedFields = 0;

				var card = await TestEnvironment.Current.BuildCard();

				card.Description = "something new";

				await TrelloProcessor.Flush();

				await card.Refresh();

				TestEnvironment.Current.LastResponse.Content.Should().MatchRegex(@"^\{""id"":""[a-f0-9]{24}""\}$");
			}
			finally
			{
				Card.DownloadedFields = (Card.Fields) Enum.GetValues(typeof(Card.Fields)).Cast<int>().Sum() &
				                        ~Card.Fields.Comments;
			}
		}

		[Test]
		public async Task MemberCanBeAdded()
		{
			var card = await TestEnvironment.Current.BuildCard();

			await card.Members.Add(TestEnvironment.Current.Me);

			await card.Refresh();

			card.Members.Count().Should().Be(1);
			card.Members[0].Should().Be(TestEnvironment.Current.Me);
		}
	}
}
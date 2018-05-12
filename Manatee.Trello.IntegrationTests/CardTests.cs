using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class CardTests
	{
		[Test]
		public async Task BasicCardData()
		{
			var card = await TestEnvironment.Current.BuildCard();

			var date = DateTime.Now;

			card.Name = "changed";
			card.Description = "changed also";
			card.DueDate = date;
			card.IsArchived = true;
			card.IsComplete = true;
			card.Position = 157;
			card.IsSubscribed = true;

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
			reCard.IsSubscribed.Should().Be(true);
			reCard.ShortId.Should().NotBeNull();
			reCard.Url.Should().NotBeNullOrEmpty();
			reCard.ShortUrl.Should().NotBeNullOrEmpty();
			reCard.LastActivity.Should().NotBeNull();
			reCard.Board.Should().Be(TestEnvironment.Current.Board);
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
		public async Task CustomFields()
		{
			await TestEnvironment.Current.Board.PowerUps.EnablePowerUp(new CustomFieldsPowerUp());

			var numberField = await TestEnvironment.Current.Board.CustomFields.Add("NumberField", CustomFieldType.Number);
			var textField = await TestEnvironment.Current.Board.CustomFields.Add("TextField", CustomFieldType.Text);
			var dateField = await TestEnvironment.Current.Board.CustomFields.Add("DateField", CustomFieldType.DateTime);
			var dropDownField = await TestEnvironment.Current.Board.CustomFields.Add("DropDownField", CustomFieldType.DropDown, CancellationToken.None,
			                                                                         TestEnvironment.Current.Factory.DropDownOption("one"),
			                                                                         TestEnvironment.Current.Factory.DropDownOption("two"));
			var checkBoxField = await TestEnvironment.Current.Board.CustomFields.Add("CheckBoxField", CustomFieldType.CheckBox);

			var card = await TestEnvironment.Current.BuildCard();

			await dropDownField.Refresh();

			var today = DateTime.Today;
			var two = dropDownField.Options.FirstOrDefault(o => o.Text == "two");
			Assert.NotNull(two);

			await numberField.SetValueForCard(card, 9.6);
			await textField.SetValueForCard(card, "text");
			await dateField.SetValueForCard(card, today);
			await dropDownField.SetValueForCard(card, two);
			await checkBoxField.SetValueForCard(card, true);

			await card.Refresh();

			card.CustomFields.OfType<NumberField>().First().Value.Should().Be(9.6);
			card.CustomFields.OfType<TextField>().First().Value.Should().Be("text");
			card.CustomFields.OfType<DateTimeField>().First().Value.Should().Be(today);
			card.CustomFields.OfType<DropDownField>().First().Value.Text.Should().Be("two");
			card.CustomFields.OfType<CheckBoxField>().First().Value.Should().Be(true);
		}

		[Test]
		public async Task CanDelete()
		{
			var card = await TestEnvironment.Current.BuildCard();
			var id = card.Id;

			await card.Refresh();
			await card.Delete();

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
		public async Task MemberCanBeAddedAndRemoved()
		{
			var card = await TestEnvironment.Current.BuildCard();

			await card.Members.Add(TestEnvironment.Current.Me);

			await card.Refresh();

			card.Members.Count().Should().Be(1);
			card.Members[0].Should().Be(TestEnvironment.Current.Me);

			await card.Members.Remove(TestEnvironment.Current.Me);

			await card.Refresh();

			card.Members.Count().Should().Be(0);
		}

		[Test]
		public async Task LabelCanBeAddedAndRemoved()
		{
			var card = await TestEnvironment.Current.BuildCard();
			await TestEnvironment.Current.Board.Refresh();
			var label = TestEnvironment.Current.Board.Labels.FirstOrDefault();

			await card.Labels.Add(label);
			await card.Refresh();

			card.Labels.Count().Should().Be(1);
			card.Labels[0].Should().Be(label);

			await card.Labels.Remove(label);

			await card.Refresh();

			card.Labels.Count().Should().Be(0);
		}

		[Test]
		public async Task CanComment()
		{
			try
			{
				TrelloConfiguration.RefreshThrottle = TimeSpan.Zero;
				Card.DownloadedFields |= Card.Fields.Comments;

				var card = await TestEnvironment.Current.BuildCard();

				var comment = await card.Comments.Add("a comment");
				await card.Refresh();

				card.Comments.Count().Should().Be(1);

				await comment.Delete();
				await card.Refresh();

				card.Comments.Count().Should().Be(0);
			}
			finally
			{
				Card.DownloadedFields &= ~Card.Fields.Comments;
				TrelloConfiguration.RefreshThrottle = TimeSpan.FromSeconds(5);
			}
		}

		[Test]
		public async Task CreatingWithShortId()
		{
			var card = await TestEnvironment.Current.BuildCard();
			var shortId = card.ShortUrl.Split('/').Last();

			TrelloConfiguration.Cache.Remove(card);

			var otherCard = TestEnvironment.Current.Factory.Card(shortId);

			await card.Refresh();

			Assert.AreEqual(card.Id, otherCard.Id);
		}

		[Test]
		public async Task CanCopy()
		{
			var card = await TestEnvironment.Current.BuildCard();
			var otherCard = await card.List.Cards.Add(card);

			Assert.AreNotEqual(card.Id, otherCard.Id);
			Assert.AreEqual(card.Name, otherCard.Name);
		}
	}
}
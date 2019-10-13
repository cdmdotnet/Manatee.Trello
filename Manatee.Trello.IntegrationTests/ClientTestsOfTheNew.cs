using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

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

			await TestEnvironment.RunClean(async () =>
				{
					sourceCard = new Card(sourceCard.Id);

					// make sure that we only have the ID
					Assert.IsNull(sourceCard.Name);

					var card = await cards.Add(sourceCard);

					await sourceCard.Refresh();

					Assert.AreNotEqual(sourceCard.Id, card.Id);
					Assert.AreEqual(sourceCard.Name, card.Name);
				});
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
		public async Task Issue249_CommentCreator()
		{
			var card = await TestEnvironment.Current.BuildCard();
			var comment = await card.Comments.Add(nameof(Issue249_CommentCreator) + " test comment");

			await TestEnvironment.RunClean(async () =>
				{
					try
					{
						Card.DownloadedFields |= Card.Fields.Comments;
						Card.DownloadedFields &= ~Card.Fields.Actions;

						var cardCopy = TestEnvironment.Current.Factory.Card(card.Id);
						await cardCopy.Refresh();

						cardCopy.Comments.Count().Should().Be(1);

						var commentCopy = cardCopy.Comments[0];
						commentCopy.Id.Should().Be(comment.Id);
						commentCopy.Creator.Should().NotBeNull();
						commentCopy.Creator.UserName.Should().Be(TestEnvironment.Current.Me.UserName);
					}
					finally
					{
						Card.DownloadedFields |= Card.Fields.Actions;
						Card.DownloadedFields &= ~Card.Fields.Comments;
					}
				});
		}

		[Test]
		public async Task Issue254a_CardListIsNull()
		{
			var card = await TestEnvironment.Current.BuildCard();

			await TestEnvironment.RunClean(async () =>
				{
					var board = TestEnvironment.Current.Factory.Board(TestEnvironment.Current.Board.Id);

					await board.Refresh();
					await board.CustomFields.Refresh();
					await board.Lists.Refresh();
					await board.Cards.Refresh();

					board.Cards[0].List.Should().NotBeNull();
				});
		}

		[Test]
		[Ignore("This doesn't test anything.  Cannot reproduce reported results.")]
		public async Task Issue254b_ConsistencyThrowsStackOverflow()
		{
			var card = await TestEnvironment.Current.BuildCard();

			await TestEnvironment.RunClean(async () =>
				{
					TrelloConfiguration.EnableConsistencyProcessing = true;

					var board = TestEnvironment.Current.Factory.Board(TestEnvironment.Current.Board.Id);

					await board.Refresh();
				});
		}

		[Test]
		public async Task DuplicatedData()
		{
			await TestEnvironment.RunClean(async () =>
				{
					TrelloConfiguration.EnableConsistencyProcessing = true;
					TrelloConfiguration.EnableDeepDownloads = true;

					Board.DownloadedFields |= Board.Fields.Cards;

					var board = TestEnvironment.Current.Factory.Board(TestEnvironment.Current.Board.Id);
					await board.Refresh();
					await board.Lists.Refresh(true);

					board.Lists.Count(l => l.Name == "Done").Should().Be(1);
				});
		}

		[Test]
		public async Task Issue277_NumericCustomFields()
		{
			var card = await TestEnvironment.Current.BuildCard();
			await TestEnvironment.Current.Board.EnsurePowerUp(new CustomFieldsPowerUp());
			var definition = await TestEnvironment.Current.Board.CustomFields.Add("planned time", CustomFieldType.Number);
			await TestEnvironment.Current.Board.CustomFields.Refresh(true);
			await definition.SetValueForCard(card, 0.5);
			await card.CustomFields.Refresh(true);

			var field = card.CustomFields.First(x => x.Definition.Name.ToLower() == "planned time");
			Console.WriteLine("field as string: {0}", field);
			var numberField = field as NumberField;
			Console.WriteLine("number field value: {0}", numberField?.Value);
			Assert.AreEqual(0.5, numberField?.Value);

			decimal.TryParse(card.CustomFields.First(x => x.Definition.Name.ToLower() == "planned time").ToString()
			                     .Split('-')[1].Trim(), out var plannedTime);

			Console.WriteLine("parsed value: {0}", plannedTime);

			Assert.AreEqual(0.5m, plannedTime);
		}

		[Test]
		public async Task Issue277_NumericCustomFields_GreekCulture()
		{
			var card = await TestEnvironment.Current.BuildCard();
			await TestEnvironment.Current.Board.EnsurePowerUp(new CustomFieldsPowerUp());
			var definition = await TestEnvironment.Current.Board.CustomFields.Add("planned time (greek)", CustomFieldType.Number);
			await TestEnvironment.Current.Board.CustomFields.Refresh(true);
			await definition.SetValueForCard(card, 0.5);
			var currentCulture = CultureInfo.CurrentCulture;

			try
			{
				CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("el-GR");

				await card.CustomFields.Refresh(true);
				var field = card.CustomFields.First(x => x.Definition.Name.ToLower() == "planned time (greek)");
				Console.WriteLine("field as string: {0}", field);
				var numberField = field as NumberField;
				Console.WriteLine("number field value: {0}", numberField?.Value);
				Assert.AreEqual(0.5, numberField?.Value);

				decimal.TryParse(card.CustomFields.First(x => x.Definition.Name.ToLower() == "planned time (greek)").ToString()
				                     .Split('-')[1].Trim(), out var plannedTime);

				Console.WriteLine("parsed value: {0}", plannedTime);

				Assert.AreEqual(0.5m, plannedTime);
			}
			finally
			{
				CultureInfo.CurrentCulture = currentCulture;
			}
		}

		[Test]
		public async Task Issue277_SetCustomFieldInBrowser()
		{
			await TestEnvironment.RunClean(async () =>
				{
					var card = TestEnvironment.Current.Factory.Card("KU54lK54");
					await card.Refresh();
					await card.Board.Refresh();

					// client way
					decimal.TryParse(card.CustomFields.First(x => x.Definition.Name.ToLower() == "field #1")
					                     .ToString()
					                     .Split('-')[1].Trim(), out var fieldValue);

					fieldValue.Should().Be(5.8m);

					// via field
					var field = card.CustomFields.First(x => x.Definition.Name.ToLower() == "field #1") as NumberField;
					field.Value.Should().Be(5.8);
				});
		}

		[Test]
		public async Task Slack_MovingCardToNewListRemovesHistory()
		{
			await TestEnvironment.RunClean(async () =>
				{
					var card = await TestEnvironment.Current.BuildCard();
					var newList = await TestEnvironment.Current.BuildList("TargetList");
					await card.Refresh(true);

					card.List.Id.Should().NotBe(newList.Id);
					card.Actions.Should().NotBeEmpty();

					var actionCount = card.Actions.Count();

					await card.Refresh(true);
					card.List = newList;
					await TrelloProcessor.Flush();

					await card.Refresh(true);
					card.List.Id.Should().Be(newList.Id);
					card.Actions.Count().Should().BeGreaterThan(actionCount);
				});
		}

		[Test]
		public async Task MovingACard()
		{
			var board = TestEnvironment.Current.Board;

			async Task MoveCard(string cardId, string listId)
			{
				await board.Refresh(true);
				var card = TestEnvironment.Current.Factory.Card(cardId);
				var list = board.Lists.FirstOrDefault(x => x.Id == listId);

				if (card == null || list == null)
					return;

				card.List = list;
			}

			var testCard = await TestEnvironment.Current.BuildCard();
			var destinationList = await TestEnvironment.Current.BuildList(nameof(MovingACard) + " Destination");

			await MoveCard(testCard.Id, destinationList.Id);

			await TrelloProcessor.Flush();
		}
	}
}

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class BoardTests
	{
		[Test]
		public async Task BasicData()
		{
			try
			{
				var board = TestEnvironment.Current.Board;

				board.Description = "changed";
				board.Name = "changed also";
				board.IsClosed = true;
				board.IsSubscribed = true;

				await TrelloProcessor.Flush();

				await TestEnvironment.RunClean(async () =>
					{
						var reBoard = TestEnvironment.Current.Factory.Board(board.Id);

						await reBoard.Refresh();

						reBoard.Description.Should().Be("changed");
						reBoard.Name.Should().Be("changed also");
						reBoard.IsClosed.Should().Be(true);
						reBoard.IsSubscribed.Should().Be(true);
					});
			}
			finally
			{
				TestEnvironment.Current.Board.IsClosed = false;
				await TrelloProcessor.Flush();
			}
		}

		[Test]
		public async Task CanChangeOrg()
		{
			try
			{
				var board = TestEnvironment.Current.Board;
				var org = TestEnvironment.Current.Factory.Organization("littlecrabsolutions");

				await org.Refresh();

				board.Organization = org;

				await TrelloProcessor.Flush();

				TrelloConfiguration.Cache.Remove(board);

				var reBoard = TestEnvironment.Current.Factory.Board(board.Id);

				await reBoard.Refresh();

				TrelloConfiguration.Cache.Remove(reBoard);

				reBoard.Organization.Should().Be(org);
			}
			finally
			{
				TestEnvironment.Current.Board.Organization = TestEnvironment.Current.Organization;

				await TrelloProcessor.Flush();

				TrelloConfiguration.Cache.Add(TestEnvironment.Current.Board);
			}
		}

		[Test]
		public async Task DownloadsEverything()
		{
			await TestEnvironment.RunClean(async () =>
				{
					try
					{
						TrelloConfiguration.EnableConsistencyProcessing = false;

						Board.DownloadedFields |= Board.Fields.Cards;

						var board = TestEnvironment.Current.Board;
						var card = await TestEnvironment.Current.BuildCard();
						var listName = card.List.Name;

						board.Lists[listName].Should().BeNull();
						TrelloConfiguration.EnableConsistencyProcessing = true;
						TrelloConfiguration.Cache.Remove(card);

						await board.Refresh(true);

						board.Lists[listName].Cards.Should().NotBeEmpty();
						board.Memberships.Should().NotBeEmpty();
					}
					finally
					{
						Board.DownloadedFields &= ~Board.Fields.Cards;
					}
				});
		}

		[Test]
		public async Task DeleteCustomField()
		{
			await TestEnvironment.RunClean(async () =>
				{
					var board = TestEnvironment.Current.Board;
					try
					{
						await board.PowerUps.EnablePowerUp(new CustomFieldsPowerUp());
					}
					catch (Exception e)
					{
						Console.WriteLine("Powerup probably already enabled.");
						Console.WriteLine(e.Message);
					}

					var field = await board.CustomFields.Add(nameof(DeleteCustomField), CustomFieldType.Text);
					var card = await board.Lists[0].Cards.Add(nameof(DeleteCustomField) + "Card");

					await field.SetValueForCard(card, "a value");

					await card.Refresh(true);

					((TextField) card.CustomFields[0]).Value.Should().Be("a value");

					await field.Delete();

					Board.DownloadedFields |= Board.Fields.Actions;
					await board.Refresh();

					Card.DownloadedFields |= Card.Fields.Actions;
					Card.DownloadedFields &= ~Card.Fields.Comments;

					Console.WriteLine(string.Join("\n", board.Actions.Select(a => a.Type)));
				});
		}
	}
}

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
			}
			finally
			{
				Board.DownloadedFields &= ~Board.Fields.Cards;
				TrelloConfiguration.EnableConsistencyProcessing = false;
			}
		}
	}
}

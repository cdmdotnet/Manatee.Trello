using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

				TrelloConfiguration.Cache.Remove(board);

				var reBoard = TestEnvironment.Current.Factory.Board(board.Id);

				await reBoard.Refresh();

				TrelloConfiguration.Cache.Remove(reBoard);

				reBoard.Description.Should().Be("changed");
				reBoard.Name.Should().Be("changed also");
				reBoard.IsClosed.Should().Be(true);
				reBoard.IsSubscribed.Should().Be(true);
			}
			finally
			{
				TestEnvironment.Current.Board.IsClosed = false;

				await TrelloProcessor.Flush();

				TrelloConfiguration.Cache.Add(TestEnvironment.Current.Board);
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
	}
}

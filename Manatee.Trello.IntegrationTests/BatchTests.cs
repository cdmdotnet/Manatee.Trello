using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class BatchTests
	{
		[Test]
		public async Task BatchDownloadsAndAppliesDifferentTypesOfData()
		{
			var board = TestEnvironment.Current.Board;
			var list = board.Lists.First();

			await TestEnvironment.RunClean(async () =>
				{
					var otherBoard = new Board(board.Id);
					var otherList = new List(list.Id);

					otherBoard.Name.Should().BeNullOrEmpty();
					otherList.Name.Should().BeNullOrEmpty();

					await TrelloProcessor.Refresh(new IBatchRefreshable[] {otherBoard, otherList});

					otherBoard.Name.Should().Be(board.Name);
					otherList.Name.Should().Be(list.Name);
				});
		}
	}
}
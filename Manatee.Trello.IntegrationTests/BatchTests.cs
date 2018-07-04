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

			var otherBoard = new Board(board.Id);
			var otherList = new List(list.Id);

			TrelloConfiguration.Cache.Remove(otherBoard);
			TrelloConfiguration.Cache.Remove(otherList);

			otherBoard.Name.Should().BeNullOrEmpty();
			otherList.Name.Should().BeNullOrEmpty();

			await TrelloProcessor.Refresh(new IRefreshable[] {otherBoard, otherList});

			otherBoard.Name.Should().Be(board.Name);
			otherList.Name.Should().Be(list.Name);
		}
	}
}
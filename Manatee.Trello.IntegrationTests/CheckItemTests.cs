using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class CheckItemTests
	{
		[Test]
		public async Task BasicData()
		{
			var checkItem = await TestEnvironment.Current.BuildCheckItem();
			var checkList = checkItem.CheckList;

			checkItem.Name = "changed";
			checkItem.State = CheckItemState.Complete;
			checkItem.Position = 157;

			await TrelloProcessor.Flush();

			TrelloConfiguration.Cache.Remove(checkItem);

			await checkList.CheckItems.Refresh();

			var reCheckItem = checkList.CheckItems[0];

			reCheckItem.Name.Should().Be("changed");
			reCheckItem.State.Should().Be(CheckItemState.Complete);
			reCheckItem.Position.Should().Be(157);
		}

		[Test]
		public async Task MoveCheckItem()
		{
			var checkItem = await TestEnvironment.Current.BuildCheckItem();
			var source = checkItem.CheckList;
			var destination = await checkItem.CheckList.Card.CheckLists.Add("other checklist");

			checkItem.CheckList = destination;

			await TrelloProcessor.Flush();

			await source.Refresh();
			await destination.Refresh();

			source.CheckItems.Should().NotContain(checkItem);
			destination.CheckItems.Should().Contain(checkItem);
		}

		[Test]
		public async Task CanDelete()
		{
			var checkItem = await TestEnvironment.Current.BuildCheckItem();
			var checkList = checkItem.CheckList;

			await checkItem.Delete();
			await checkList.Refresh();

			checkList.CheckItems.Count().Should().Be(0);

			// can't run the exception check because we can't just create a check item
		}
	}
}

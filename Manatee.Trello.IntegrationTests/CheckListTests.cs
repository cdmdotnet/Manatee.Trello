using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class CheckListTests
	{
		[Test]
		public async Task BasicData()
		{
			var checkList = await TestEnvironment.Current.BuildCheckList();

			checkList.Name = "changed";
			checkList.Position = 157;

			await TrelloProcessor.Flush();

			TrelloConfiguration.Cache.Remove(checkList);

			var reCheckList = TestEnvironment.Current.Factory.CheckList(checkList.Id);

			await reCheckList.Refresh();

			reCheckList.Name.Should().Be("changed");
			reCheckList.Position.Should().Be(157);
		}

		[Test]
		public async Task CanDelete()
		{
			var checkList = await TestEnvironment.Current.BuildCheckList();
			var id = checkList.Id;

			await checkList.Refresh();
			await checkList.Delete();

			var reCheckList = TestEnvironment.Current.Factory.CheckList(id);

			Assert.ThrowsAsync<TrelloInteractionException>(async () => await reCheckList.Refresh());
		}

		[Test]
		public async Task CanCopy()
		{
			var checkList = await TestEnvironment.Current.BuildCheckList();
			var card = await TestEnvironment.Current.BuildCard();
			await checkList.CheckItems.Add("one");
			var checkItem = await checkList.CheckItems.Add("two");
			checkItem.State = CheckItemState.Complete;

			await TrelloProcessor.Flush();
			await checkItem.Refresh();

			checkItem.State.Should().Be(CheckItemState.Complete);

			var newCheckList = await card.CheckLists.Add("copied", checkList);

			newCheckList.Name.Should().Be("copied");
			newCheckList.CheckItems.Count().Should().Be(2);
			newCheckList.CheckItems[0].Name.Should().Be("one");
			newCheckList.CheckItems[1].Name.Should().Be("two");
			// Trello does not copy item state
			newCheckList.CheckItems[1].State.Should().Be(CheckItemState.Incomplete);
		}
	}
}
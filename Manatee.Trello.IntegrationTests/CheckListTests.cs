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
	}
}
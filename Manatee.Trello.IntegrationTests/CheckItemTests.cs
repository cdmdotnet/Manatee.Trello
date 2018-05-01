using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class CheckItemTests
	{
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
	}
}

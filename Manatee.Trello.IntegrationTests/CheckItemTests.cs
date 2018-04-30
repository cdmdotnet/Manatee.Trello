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
			var destination = await TestEnvironment.Current.BuildCheckList();

			await checkItem.Refresh();

			checkItem.CheckList = destination;

			await TrelloProcessor.Flush();

			await checkItem.Refresh();

			checkItem.CheckList.Id.Should().Be(destination.Id);
		}
	}
}

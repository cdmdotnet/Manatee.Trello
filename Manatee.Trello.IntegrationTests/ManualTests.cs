using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	[Category("Manual")]
	[Ignore("This should only be run manually to clean up after halted tests (occurs if debugging a test and debugger is halted).")]
	public class ManualTests
	{

		[Test]
		public async Task CleanUpLeftOverTestRuns()
		{
			var factory = new TrelloFactory();
			var me = await factory.Me();

			await me.Refresh();

			await Task.WhenAll(me.Boards.Where(b => b.Name.StartsWith("TestBoard_")).Select(async b =>
				{
					Console.WriteLine($"Deleting board {b}");
					await b.Delete();
				}));
			await Task.WhenAll(me.Organizations.Where(o => o.Name.StartsWith("TestOrg_")).Select(async o =>
				{
					Console.WriteLine($"Deleting org {o}");
					await o.Delete();
				}));
		}
	}
}
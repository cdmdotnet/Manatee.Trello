using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class TokenTests
	{
		[Test]
		public async Task OwnedTokenReturnsSelf()
		{
			var me = await TestEnvironment.Current.Factory.Me();
			var token = TestEnvironment.Current.Factory.Token(TrelloAuthorization.Default.UserToken);
			await token.Refresh();

			token.Member.Should().Be(me);
		}
	}
}

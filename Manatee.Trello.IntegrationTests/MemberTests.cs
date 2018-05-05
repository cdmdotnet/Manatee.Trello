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
	public class MemberTests
	{
		[Test]
		public async Task BasicData()
		{
			var member = TestEnvironment.Current.Factory.Member("s_littlecrabsolutions");

			await member.Refresh();

			member.AvatarSource.Should().BeNull();
			member.AvatarUrl.Should().NotBeNullOrEmpty();
			member.Bio.Should().NotBeNull();
			member.FullName.Should().Be("Little Crab Solutions");
			member.Initials.Should().Be("MOS");
			member.IsConfirmed.Should().Be(true);
			member.Mention.Should().Be("@s_littlecrabsolutions");
			member.Status.Should().NotBeNull();
			member.Url.Should().Be("https://trello.com/s_littlecrabsolutions");
			member.UserName.Should().Be("s_littlecrabsolutions");

			Console.WriteLine(member.AvatarUrl);
		}
	}
}

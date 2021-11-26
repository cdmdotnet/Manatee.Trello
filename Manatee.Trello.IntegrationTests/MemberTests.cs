using System;
using System.IO;
using System.Reflection;
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
			member.Initials.Should().Be("LS");
			member.IsConfirmed.Should().Be(true);
			member.Mention.Should().Be("@s_littlecrabsolutions");
			member.Status.Should().NotBeNull();
			member.Url.Should().Be("https://trello.com/s_littlecrabsolutions");
			member.UserName.Should().Be("s_littlecrabsolutions");

			Console.WriteLine(member.AvatarUrl);
		}

		[Test]
		public async Task StarredBoards()
		{
			var member = TestEnvironment.Current.Me;
			var board = TestEnvironment.Current.Board;

			await member.Refresh();
			await board.Refresh();

			board.IsStarred.Should().Be(false);

			await member.StarredBoards.Add(board);
			await board.Refresh(true);
			await member.Refresh(true);

			board.IsStarred.Should().Be(true);
			member.StarredBoards.Should().Contain(s => s.Board == board);
		}

		[Test]
		public async Task BoardBackgrounds()
		{
			var me = await TestEnvironment.Current.Factory.Me();
			await me.BoardBackgrounds.Refresh(true);

			var imagePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Files/logo.png");
			var data = File.ReadAllBytes(imagePath);
			var newBackground = await me.BoardBackgrounds.Add(data);

			newBackground.Should().NotBeNull();
			newBackground.Type.Should().Be(BoardBackgroundType.Custom);

			await newBackground.Delete();
		}
	}
}

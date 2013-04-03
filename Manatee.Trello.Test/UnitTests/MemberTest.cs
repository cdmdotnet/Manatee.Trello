using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class MemberTest : EntityTestBase<Member>
	{
		[TestMethod]
		public void Actions()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void AvatarHash()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void AvatarSource()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Bio()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Boards()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Confirmed()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Email()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void FullName()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void GravatarHash()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Initials()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void InvitedBoardIds()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void InvitedOrganizations()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void LoginTypes()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void MemberType()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Notifications()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Organizations()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void PinnedBoards()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Preferences()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void PremiumOrganizations()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Status()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Trophies()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void UploadedAvatarHash()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void MarkAllNotificationsAsRead()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void PinBoard()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void RescindVoteForCard()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void UnpinBoard()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void VoteForCard()
		{
			throw new NotImplementedException();
		}

		#region Given

		private void ABoardMembership()
		{
			_systemUnderTest = new SystemUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
		}

		#endregion

		#region When


		#endregion
	}
}
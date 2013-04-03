using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class OrganizationTest : EntityTestBase<Organization>
	{
		[TestMethod]
		public void Actions()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Boards()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Description()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void DisplayName()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void LogoHash()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Members()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Name()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void PowerUps()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Preferences()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Website()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void AddBoard()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void AddOrUpdateMember()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Delete()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void InviteMember()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void RemoveMember()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void RescindInvitation()
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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class OrganizationPreferencesTest : EntityTestBase<OrganizationPreferences>
	{
		[TestMethod]
		public void BoardVisibilityRestrict()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void ExternalMembersDisabled()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void OrgInviteRestrict()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void PermissionLevel()
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
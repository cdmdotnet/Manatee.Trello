using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class BoardPreferencesTest : EntityTestBase<BoardPreferences>
	{
		[TestMethod]
		public void AllowsSelfJoin()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Comments()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Invitations()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void PermissionLevel()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void ShowCardCovers()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Voting()
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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class BoardPersonalPreferencesTest : EntityTestBase<BoardPersonalPreferences>
	{
		[TestMethod]
		public void ShowListGuide()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void ShowSidebar()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void ShowSidebarActivity()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void ShowSidebarBoardActions()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void ShowSidebarMembers()
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
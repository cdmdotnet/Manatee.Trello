using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class MemberPreferencesTest : EntityTestBase<MemberPreferences>
	{
		[TestMethod]
		public void ColorBlind()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void MinutesBetweenSummaries()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void SendSummaries()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void MinutesBeforeDeadlineToNotify()
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
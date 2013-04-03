using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class NotificationTest : EntityTestBase<Notification>
	{
		[TestMethod]
		public void IsUnread()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void MemberCreator()
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
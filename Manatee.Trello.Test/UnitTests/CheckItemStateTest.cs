using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class CheckItemStateTest : EntityTestBase<CheckItemState>
	{
		[TestMethod]
		public void State()
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
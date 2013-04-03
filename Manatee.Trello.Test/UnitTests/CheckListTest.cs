using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class CheckListTest : EntityTestBase<CheckList>
	{
		[TestMethod]
		public void Board()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Card()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void CheckItems()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Name()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Position()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void AddCheckItem()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Delete()
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
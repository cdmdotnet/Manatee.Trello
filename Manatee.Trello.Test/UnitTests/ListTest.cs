using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class ListTest : EntityTestBase<List>
	{
		[TestMethod]
		public void Actions()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Board()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Cards()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void IsClosed()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void IsSubscribed()
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
		public void AddCard()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Delete()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Move()
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
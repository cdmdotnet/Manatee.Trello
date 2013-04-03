using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class LabelNamesTest : EntityTestBase<LabelNames>
	{
		[TestMethod]
		public void Red()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Orange()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Yellow()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Green()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Blue()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Purple()
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
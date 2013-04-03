using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class CardTest : EntityTestBase<Card>
	{
		[TestMethod]
		public void Actions()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void AttachmentCoverId()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Attachments()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Badges()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Board()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void CheckItemStates()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void CheckLists()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Comments()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Description()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void DueDate()
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
		public void Labels()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void List()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void ManualCoverAttachment()
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
		public void Position()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void VotingMembers()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void AddAttachment()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void AddCheckList()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void AddComment()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void ApplyLabel()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void AssignMember()
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
		[TestMethod]
		public void RemoveLabel()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void RemoveMember()
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
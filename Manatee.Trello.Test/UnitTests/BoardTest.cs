using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class BoardTest : EntityTestBase<Board>
	{
		[TestMethod]
		public void Actions()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void ArchivedCards()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void ArchivedLists()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Description()
		{
			var story = new Story("Description");

			var feature = story.InOrderTo("control a board's description")
				.AsA("developer")
				.IWant("to get and set the Description");

			feature.WithScenario("Get")
				.Given(ABoard)
				.When(DescriptionIsAccessed)
				.Then(ValueIsReturned, "description")
				.And(ExceptionIsNotThrown)

				.WithScenario("Set")
				.Given(ABoard)
				.When(DescriptionIsSet)
				.Then(MockApiPutIsCalled<Board>, 1)
				.And(PropertyIsSet, "new description", _systemUnderTest.Sut.Description)

				.Execute();
		}
		[TestMethod]
		public void IsClosed()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void IsPinned()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void IsSubscribed()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void LabelNames()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Lists()
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
		public void Organization()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void PersonalPreferences()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Preferences()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Url()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void AddList()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void AddOrUpdateMember()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void MarkAsViewed()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void InviteMember()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void RemoveMember()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void RescindInvitation()
		{
			throw new NotImplementedException();
		}

		#region Given

		private void ABoard()
		{
			_systemUnderTest = new SystemUnderTest();
			_systemUnderTest.Sut.Description = "description";
			_systemUnderTest.Sut.IsClosed = true;
			_systemUnderTest.Sut.IsPinned = true;
			_systemUnderTest.Sut.IsSubscribed = true;
			_systemUnderTest.Sut.Name = "name";
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
			_systemUnderTest.Dependencies.Api.Setup(a => a.Get(It.IsAny<IRestRequest<Organization>>()))
				.Returns(new Organization());
		}

		#endregion

		#region When

		private void DescriptionIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Description);
		}
		private void DescriptionIsSet()
		{
			Execute(() => _systemUnderTest.Sut.Description = "new description");
		}

		#endregion
	}
}

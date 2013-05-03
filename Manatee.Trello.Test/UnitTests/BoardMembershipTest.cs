using System;
using Manatee.Trello.Contracts;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class BoardMembershipTest : EntityTestBase<BoardMembership>
	{
		[TestMethod]
		public void IsDeactivated()
		{
			var story = new Story("IsDeactivated");

			var feature = story.InOrderTo("determine if the member is activated")
				.AsA("developer")
				.IWant("to get the members activation status.");

			feature.WithScenario("Access IsDeactivated property")
				.Given(ABoardMembership)
				.And(EntityIsRefreshed)
				.When(IsDeactivatedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardMembership>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsDeactivated property when expired")
				.Given(ABoardMembership)
				.And(EntityIsExpired)
				.When(IsDeactivatedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardMembership>, 0)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Member()
		{
			var story = new Story("Member");

			var feature = story.InOrderTo("access the details of the member who added an attachment")
				.AsA("developer")
				.IWant("to get the member object.");

			feature.WithScenario("Access Member property")
				.Given(ABoardMembership)
				.And(EntityIsRefreshed)
				.When(MemberIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardMembership>, 0)
				.And(MockSvcRetrieveIsCalled<Member>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Member property when expired")
				.Given(ABoardMembership)
				.And(EntityIsExpired)
				.When(MemberIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardMembership>, 0)
				.And(MockSvcRetrieveIsCalled<Member>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ABoardMembership()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Svc.Object;
			OwnedBy<Board>();
			SetupMockRetrieve<Member>();
			SetupMockGet<IJsonBoardMembership>();
		}

		#endregion

		#region When

		private void IsDeactivatedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.IsDeactivated);
		}
		private void MemberIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Member);
		}

		#endregion
	}
}
using System;
using Manatee.Trello.Contracts;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class OrganizationMembershipTest : EntityTestBase<OrganizationMembership>
	{
		[TestMethod]
		public void IsUnconfirmed()
		{
			var story = new Story("IsUnconfirmed");

			var feature = story.InOrderTo("determine if the member is confirmed")
				.AsA("developer")
				.IWant("to get the members confirmation status.");

			feature.WithScenario("Access IsUnconfirmed property")
				.Given(AOrganizationMembership)
				.And(EntityIsRefreshed)
				.When(IsUnconfirmedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganizationMembership>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsUnconfirmed property when expired")
				.Given(AOrganizationMembership)
				.And(EntityIsExpired)
				.When(IsUnconfirmedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganizationMembership>, 0)
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
				.Given(AOrganizationMembership)
				.And(EntityIsRefreshed)
				.When(MemberIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganizationMembership>, 0)
				.And(MockSvcRetrieveIsCalled<Member>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Member property when expired")
				.Given(AOrganizationMembership)
				.And(EntityIsExpired)
				.When(MemberIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganizationMembership>, 0)
				.And(MockSvcRetrieveIsCalled<Member>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void AOrganizationMembership()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Svc.Object;
			OwnedBy<Board>();
			SetupMockRetrieve<Member>();
			SetupMockGet<IJsonOrganizationMembership>();
		}

		#endregion

		#region When

		private void IsUnconfirmedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.IsUnconfirmed);
		}
		private void MemberIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Member);
		}

		#endregion
	}
}
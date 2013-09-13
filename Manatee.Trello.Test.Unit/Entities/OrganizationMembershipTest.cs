using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class OrganizationMembershipTest : EntityTestBase<OrganizationMembership, IJsonOrganizationMembership>
	{
		[TestMethod]
		public void IsUnconfirmed()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access IsUnconfirmed property when not expired")
				.Given(AOrganizationMembership)
				.When(IsUnconfirmedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<OrganizationMembership>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsUnconfirmed property when expired")
				.Given(AOrganizationMembership)
				.And(EntityIsExpired)
				.When(IsUnconfirmedIsAccessed)
				.Then(RepositoryRefreshIsCalled<OrganizationMembership>, EntityRequestType.OrganizationMembership_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Member()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Member property when not expired")
				.Given(AOrganizationMembership)
				.When(MemberIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<OrganizationMembership>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Member property when expired")
				.Given(AOrganizationMembership)
				.And(EntityIsExpired)
				.When(MemberIsAccessed)
				.Then(RepositoryRefreshIsCalled<OrganizationMembership>, EntityRequestType.OrganizationMembership_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void AOrganizationMembership()
		{
			_test = new EntityUnderTest();
			OwnedBy<Board>();
		}

		#endregion

		#region When

		private void IsUnconfirmedIsAccessed()
		{
			Execute(() => _test.Sut.IsUnconfirmed);
		}
		private void MemberIsAccessed()
		{
			Execute(() => _test.Sut.Member);
		}

		#endregion
	}
}
using System;
using Manatee.Trello.Internal;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class OrganizationPreferencesTest : EntityTestBase<OrganizationPreferences, IJsonOrganizationPreferences>
	{
		[TestMethod]
		public void AssociatedDomain()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access AssociatedDomain property when not expired")
				.Given(AnOrganizationPreferencesObject)
				.When(AssociatedDomainIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<OrganizationPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access AssociatedDomain property when expired")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsExpired)
				.When(AssociatedDomainIsAccessed)
				.Then(RepositoryRefreshIsCalled<OrganizationPreferences>, EntityRequestType.OrganizationPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AssociatedDomain property")
				.Given(AnOrganizationPreferencesObject)
				.When(AssociatedDomainIsSet, "domain")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.OrganizationPreferences_Write_AssociatedDomain)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AssociatedDomain property to same")
				.Given(AnOrganizationPreferencesObject)
				.And(AssociatedDomainIs, "domain")
				.When(AssociatedDomainIsSet, "domain")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ExternalMembersDisabled()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access ExternalMembersDisabled property when not expired")
				.Given(AnOrganizationPreferencesObject)
				.When(ExternalMembersDisabledIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<OrganizationPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ExternalMembersDisabled property when expired")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsExpired)
				.When(ExternalMembersDisabledIsAccessed)
				.Then(RepositoryRefreshIsCalled<OrganizationPreferences>, EntityRequestType.OrganizationPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ExternalMembersDisabled property")
				.Given(AnOrganizationPreferencesObject)
				.When(ExternalMembersDisabledIsSet, (bool?)true)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.OrganizationPreferences_Write_ExternalMembersDisabled)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ExternalMembersDisabled property to same")
				.Given(AnOrganizationPreferencesObject)
				.And(ExternalMembersDisabledIs, (bool?)true)
				.When(ExternalMembersDisabledIsSet, (bool?)true)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		[Ignore]
		public void OrgInviteRestrict()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void OrgVisibleBoardVisibility()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access OrgVisibleBoardVisibility property when not expired")
				.Given(AnOrganizationPreferencesObject)
				.When(OrgVisibleBoardVisibilityIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<OrganizationPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access OrgVisibleBoardVisibility property when expired")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsExpired)
				.When(OrgVisibleBoardVisibilityIsAccessed)
				.Then(RepositoryRefreshIsCalled<OrganizationPreferences>, EntityRequestType.OrganizationPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set OrgVisibleBoardVisibility property")
				.Given(AnOrganizationPreferencesObject)
				.When(OrgVisibleBoardVisibilityIsSet, BoardPermissionLevelType.Org)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.OrganizationPreferences_Write_OrgVisibleBoardVisibility)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set OrgVisibleBoardVisibility property to same")
				.Given(AnOrganizationPreferencesObject)
				.And(OrgVisibleBoardVisibilityIs, BoardPermissionLevelType.Org)
				.When(OrgVisibleBoardVisibilityIsSet, BoardPermissionLevelType.Org)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void PermissionLevel()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access PermissionLevel property when not expired")
				.Given(AnOrganizationPreferencesObject)
				.When(PermissionLevelIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<OrganizationPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access PermissionLevel property when expired")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsExpired)
				.When(PermissionLevelIsAccessed)
				.Then(RepositoryRefreshIsCalled<OrganizationPreferences>, EntityRequestType.OrganizationPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Voting property")
				.Given(AnOrganizationPreferencesObject)
				.When(PermissionLevelIsSet, OrganizationPermissionLevelType.Public)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.OrganizationPreferences_Write_PermissionLevel)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PermissionLevel property to same")
				.Given(AnOrganizationPreferencesObject)
				.And(PermissionLevelIs, OrganizationPermissionLevelType.Public)
				.When(PermissionLevelIsSet, OrganizationPermissionLevelType.Public)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void PrivateBoardVisibility()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access PrivateBoardVisibility property when not expired")
				.Given(AnOrganizationPreferencesObject)
				.When(PrivateBoardVisibilityIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<OrganizationPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access PrivateBoardVisibility property when expired")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsExpired)
				.When(PrivateBoardVisibilityIsAccessed)
				.Then(RepositoryRefreshIsCalled<OrganizationPreferences>, EntityRequestType.OrganizationPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PrivateBoardVisibility property")
				.Given(AnOrganizationPreferencesObject)
				.When(PrivateBoardVisibilityIsSet, BoardPermissionLevelType.Private)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.OrganizationPreferences_Write_PrivateBoardVisibility)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PrivateBoardVisibility property to same")
				.Given(AnOrganizationPreferencesObject)
				.And(PrivateBoardVisibilityIs, BoardPermissionLevelType.Private)
				.When(PrivateBoardVisibilityIsSet, BoardPermissionLevelType.Private)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void PublicBoardVisibility()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access PublicBoardVisibility property when not expired")
				.Given(AnOrganizationPreferencesObject)
				.When(PublicBoardVisibilityIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<OrganizationPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access PublicBoardVisibility property when expired")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsExpired)
				.When(PublicBoardVisibilityIsAccessed)
				.Then(RepositoryRefreshIsCalled<OrganizationPreferences>, EntityRequestType.OrganizationPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PublicBoardVisibility property")
				.Given(AnOrganizationPreferencesObject)
				.When(PublicBoardVisibilityIsSet, BoardPermissionLevelType.Public)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.OrganizationPreferences_Write_PublicBoardVisibility)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PublicBoardVisibility property to same")
				.Given(AnOrganizationPreferencesObject)
				.And(PublicBoardVisibilityIs, BoardPermissionLevelType.Public)
				.When(PublicBoardVisibilityIsSet, BoardPermissionLevelType.Public)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void AnOrganizationPreferencesObject()
		{
			_test = new EntityUnderTest();
			OwnedBy<Organization>();
		}
		private void AssociatedDomainIs(string value)
		{
			_test.Json.SetupGet(j => j.AssociatedDomain)
				.Returns(value);
		}
		private void ExternalMembersDisabledIs(bool? value)
		{
			_test.Json.SetupGet(j => j.ExternalMembersDisabled)
				.Returns(value);
		}
		//private void OrgInviteRestrictIs(List<object> value)
		//{
		//	_test.Json.SetupGet(j => j.OrgInviteRestrict)
		//		.Returns(value);
		//}
		private void OrgVisibleBoardVisibilityIs(BoardPermissionLevelType value)
		{
			var restrict = new Mock<IJsonBoardVisibilityRestrict>();
			restrict.SetupGet(r => r.Org)
					.Returns(value.ToLowerString());
			_test.Json.SetupGet(j => j.BoardVisibilityRestrict)
				 .Returns(restrict.Object);
			ReapplyJson();
		}
		private void PermissionLevelIs(OrganizationPermissionLevelType value)
		{
			_test.Json.SetupGet(j => j.PermissionLevel)
				.Returns(value.ToLowerString());
			ReapplyJson();
		}
		private void PrivateBoardVisibilityIs(BoardPermissionLevelType value)
		{
			var restrict = new Mock<IJsonBoardVisibilityRestrict>();
			restrict.SetupGet(r => r.Private)
					.Returns(value.ToLowerString());
			_test.Json.SetupGet(j => j.BoardVisibilityRestrict)
				 .Returns(restrict.Object);
			ReapplyJson();
		}
		private void PublicBoardVisibilityIs(BoardPermissionLevelType value)
		{
			var restrict = new Mock<IJsonBoardVisibilityRestrict>();
			restrict.SetupGet(r => r.Public)
					.Returns(value.ToLowerString());
			_test.Json.SetupGet(j => j.BoardVisibilityRestrict)
				 .Returns(restrict.Object);
			ReapplyJson();
		}

		#endregion

		#region When

		private void AssociatedDomainIsAccessed()
		{
			Execute(() => _test.Sut.AssociatedDomain);
		}
		private void AssociatedDomainIsSet(string value)
		{
			Execute(() => _test.Sut.AssociatedDomain = value);
		}
		private void ExternalMembersDisabledIsAccessed()
		{
			Execute(() => _test.Sut.ExternalMembersDisabled);
		}
		private void ExternalMembersDisabledIsSet(bool? value)
		{
			Execute(() => _test.Sut.ExternalMembersDisabled = value);
		}
		//private void OrgInviteRestrictIsAccessed()
		//{
		//	Execute(() => _test.Sut.OrgInviteRestrict);
		//}
		//private void OrgInviteRestrictIsSet(List<object> value)
		//{
		//	Execute(() => _test.Sut.OrgInviteRestrict = value);
		//}
		private void OrgVisibleBoardVisibilityIsAccessed()
		{
			Execute(() => _test.Sut.OrgVisibleBoardVisibility);
		}
		private void OrgVisibleBoardVisibilityIsSet(BoardPermissionLevelType value)
		{
			Execute(() => _test.Sut.OrgVisibleBoardVisibility = value);
		}
		private void PermissionLevelIsAccessed()
		{
			Execute(() => _test.Sut.PermissionLevel);
		}
		private void PermissionLevelIsSet(OrganizationPermissionLevelType value)
		{
			Execute(() => _test.Sut.PermissionLevel = value);
		}
		private void PrivateBoardVisibilityIsAccessed()
		{
			Execute(() => _test.Sut.PrivateBoardVisibility);
		}
		private void PrivateBoardVisibilityIsSet(BoardPermissionLevelType value)
		{
			Execute(() => _test.Sut.PrivateBoardVisibility = value);
		}
		private void PublicBoardVisibilityIsAccessed()
		{
			Execute(() => _test.Sut.PublicBoardVisibility);
		}
		private void PublicBoardVisibilityIsSet(BoardPermissionLevelType value)
		{
			Execute(() => _test.Sut.PublicBoardVisibility = value);
		}

		#endregion
	}
}
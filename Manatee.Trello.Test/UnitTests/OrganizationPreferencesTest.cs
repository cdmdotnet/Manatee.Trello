using System;
using System.Collections.Generic;
using Manatee.Trello.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class OrganizationPreferencesTest : EntityTestBase<OrganizationPreferences>
	{
		[TestMethod]
		[Ignore]
		public void BoardVisibilityRestrict()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void ExternalMembersDisabled()
		{
			var story = new Story("ExternalMembersDisabled");

			var feature = story.InOrderTo("???")
				.AsA("developer")
				.IWant("to get and set the ExternalMembersDisabled property value.");

			feature.WithScenario("Access ExternalMembersDisabled property")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsNotExpired)
				.When(ExternalMembersDisabledIsAccessed)
				.Then(MockApiGetIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ExternalMembersDisabled property when expired")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsExpired)
				.When(ExternalMembersDisabledIsAccessed)
				.Then(MockApiGetIsCalled<OrganizationPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ExternalMembersDisabled property")
				.Given(AnOrganizationPreferencesObject)
				.When(ExternalMembersDisabledIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ExternalMembersDisabled property to null")
				.Given(AnOrganizationPreferencesObject)
				.And(ExternalMembersDisabledIs, (bool?) true)
				.When(ExternalMembersDisabledIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set ExternalMembersDisabled property to same")
				.Given(AnOrganizationPreferencesObject)
				.And(ExternalMembersDisabledIs, (bool?) true)
				.When(ExternalMembersDisabledIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ExternalMembersDisabled property without AuthToken")
				.Given(AnOrganizationPreferencesObject)
				.And(TokenNotSupplied)
				.When(ExternalMembersDisabledIsSet, (bool?)true)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		[Ignore]
		public void OrgInviteRestrict()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void PermissionLevel()
		{
			var story = new Story("PermissionLevel");

			var feature = story.InOrderTo("control who is allowed to view the organization and its boards")
				.AsA("developer")
				.IWant("to get the PermissionLevel property value.");

			feature.WithScenario("Access PermissionLevel property")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsNotExpired)
				.When(PermissionLevelIsAccessed)
				.Then(MockApiGetIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access PermissionLevel property when expired")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsExpired)
				.When(PermissionLevelIsAccessed)
				.Then(MockApiGetIsCalled<OrganizationPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Voting property")
				.Given(AnOrganizationPreferencesObject)
				.When(PermissionLevelIsSet, OrganizationPermissionLevelType.Public)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PermissionLevel property to same")
				.Given(AnOrganizationPreferencesObject)
				.And(PermissionLevelIs, OrganizationPermissionLevelType.Public)
				.When(PermissionLevelIsSet, OrganizationPermissionLevelType.Public)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PermissionLevel property without AuthToken")
				.Given(AnOrganizationPreferencesObject)
				.And(TokenNotSupplied)
				.When(PermissionLevelIsSet, OrganizationPermissionLevelType.Public)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		#region Given

		private void AnOrganizationPreferencesObject()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
		}
		private void BoardVisibilityRestrictIs(object value)
		{
			SetupProperty(() => _systemUnderTest.Sut.BoardVisibilityRestrict = value);
		}
		private void ExternalMembersDisabledIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.ExternalMembersDisabled = value);
		}
		private void OrgInviteRestrictIs(List<object> value)
		{
			SetupProperty(() => _systemUnderTest.Sut.OrgInviteRestrict = value);
		}
		private void PermissionLevelIs(OrganizationPermissionLevelType value)
		{
			SetupProperty(() => _systemUnderTest.Sut.PermissionLevel = value);
		}

		#endregion

		#region When

		private void BoardVisibilityRestrictIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.BoardVisibilityRestrict);
		}
		private void BoardVisibilityRestrictIsSet(object value)
		{
			Execute(() => _systemUnderTest.Sut.BoardVisibilityRestrict = value);
		}
		private void ExternalMembersDisabledIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ExternalMembersDisabled);
		}
		private void ExternalMembersDisabledIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.ExternalMembersDisabled = value);
		}
		private void OrgInviteRestrictIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.OrgInviteRestrict);
		}
		private void OrgInviteRestrictIsSet(List<object> value)
		{
			Execute(() => _systemUnderTest.Sut.OrgInviteRestrict = value);
		}
		private void PermissionLevelIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.PermissionLevel);
		}
		private void PermissionLevelIsSet(OrganizationPermissionLevelType value)
		{
			Execute(() => _systemUnderTest.Sut.PermissionLevel = value);
		}

		#endregion
	}
}
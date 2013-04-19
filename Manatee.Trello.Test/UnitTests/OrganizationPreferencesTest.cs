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
		public void OrgVisibleBoardVisibility()
		{
			var story = new Story("OrgVisibleBoardVisibility");

			var feature = story.InOrderTo("control the exposure of owned boards")
				.AsA("developer")
				.IWant("to get the OrgVisibleBoardVisibility property value.");

			feature.WithScenario("Access OrgVisibleBoardVisibility property")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsNotExpired)
				.When(OrgVisibleBoardVisibilityIsAccessed)
				.Then(MockApiGetIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access OrgVisibleBoardVisibility property when expired")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsExpired)
				.When(OrgVisibleBoardVisibilityIsAccessed)
				.Then(MockApiGetIsCalled<OrganizationPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set OrgVisibleBoardVisibility property")
				.Given(AnOrganizationPreferencesObject)
				.When(OrgVisibleBoardVisibilityIsSet, BoardPermissionLevelType.Org)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set OrgVisibleBoardVisibility property to same")
				.Given(AnOrganizationPreferencesObject)
				.And(OrgVisibleBoardVisibilityIs, BoardPermissionLevelType.Org)
				.When(OrgVisibleBoardVisibilityIsSet, BoardPermissionLevelType.Org)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set OrgVisibleBoardVisibility property without AuthToken")
				.Given(AnOrganizationPreferencesObject)
				.And(TokenNotSupplied)
				.When(OrgVisibleBoardVisibilityIsSet, BoardPermissionLevelType.Org)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
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
		[TestMethod]
		public void PrivateBoardVisibility()
		{
			var story = new Story("PrivateBoardVisibility");

			var feature = story.InOrderTo("control the exposure of owned boards")
				.AsA("developer")
				.IWant("to get the PrivateBoardVisibility property value.");

			feature.WithScenario("Access PrivateBoardVisibility property")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsNotExpired)
				.When(PrivateBoardVisibilityIsAccessed)
				.Then(MockApiGetIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access PrivateBoardVisibility property when expired")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsExpired)
				.When(PrivateBoardVisibilityIsAccessed)
				.Then(MockApiGetIsCalled<OrganizationPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PrivateBoardVisibility property")
				.Given(AnOrganizationPreferencesObject)
				.When(PrivateBoardVisibilityIsSet, BoardPermissionLevelType.Private)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PrivateBoardVisibility property to same")
				.Given(AnOrganizationPreferencesObject)
				.And(PrivateBoardVisibilityIs, BoardPermissionLevelType.Private)
				.When(PrivateBoardVisibilityIsSet, BoardPermissionLevelType.Private)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PrivateBoardVisibility property without AuthToken")
				.Given(AnOrganizationPreferencesObject)
				.And(TokenNotSupplied)
				.When(PrivateBoardVisibilityIsSet, BoardPermissionLevelType.Private)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void PublicBoardVisibility()
		{
			var story = new Story("PublicBoardVisibility");

			var feature = story.InOrderTo("control the exposure of owned boards")
				.AsA("developer")
				.IWant("to get the PublicBoardVisibility property value.");

			feature.WithScenario("Access PublicBoardVisibility property")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsNotExpired)
				.When(PublicBoardVisibilityIsAccessed)
				.Then(MockApiGetIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access PublicBoardVisibility property when expired")
				.Given(AnOrganizationPreferencesObject)
				.And(EntityIsExpired)
				.When(PublicBoardVisibilityIsAccessed)
				.Then(MockApiGetIsCalled<OrganizationPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PublicBoardVisibility property")
				.Given(AnOrganizationPreferencesObject)
				.When(PublicBoardVisibilityIsSet, BoardPermissionLevelType.Public)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PublicBoardVisibility property to same")
				.Given(AnOrganizationPreferencesObject)
				.And(PublicBoardVisibilityIs, BoardPermissionLevelType.Public)
				.When(PublicBoardVisibilityIsSet, BoardPermissionLevelType.Public)
				.Then(MockApiPutIsCalled<OrganizationPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PublicBoardVisibility property without AuthToken")
				.Given(AnOrganizationPreferencesObject)
				.And(TokenNotSupplied)
				.When(PublicBoardVisibilityIsSet, BoardPermissionLevelType.Public)
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
		private void ExternalMembersDisabledIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.ExternalMembersDisabled = value);
		}
		private void OrgInviteRestrictIs(List<object> value)
		{
			SetupProperty(() => _systemUnderTest.Sut.OrgInviteRestrict = value);
		}
		private void OrgVisibleBoardVisibilityIs(BoardPermissionLevelType value)
		{
			SetupProperty(() => _systemUnderTest.Sut.OrgVisibleBoardVisibility = value);
		}
		private void PermissionLevelIs(OrganizationPermissionLevelType value)
		{
			SetupProperty(() => _systemUnderTest.Sut.PermissionLevel = value);
		}
		private void PrivateBoardVisibilityIs(BoardPermissionLevelType value)
		{
			SetupProperty(() => _systemUnderTest.Sut.PrivateBoardVisibility = value);
		}
		private void PublicBoardVisibilityIs(BoardPermissionLevelType value)
		{
			SetupProperty(() => _systemUnderTest.Sut.PublicBoardVisibility = value);
		}

		#endregion

		#region When

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
		private void OrgVisibleBoardVisibilityIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.OrgVisibleBoardVisibility);
		}
		private void OrgVisibleBoardVisibilityIsSet(BoardPermissionLevelType value)
		{
			Execute(() => _systemUnderTest.Sut.OrgVisibleBoardVisibility = value);
		}
		private void PermissionLevelIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.PermissionLevel);
		}
		private void PermissionLevelIsSet(OrganizationPermissionLevelType value)
		{
			Execute(() => _systemUnderTest.Sut.PermissionLevel = value);
		}
		private void PrivateBoardVisibilityIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.PrivateBoardVisibility);
		}
		private void PrivateBoardVisibilityIsSet(BoardPermissionLevelType value)
		{
			Execute(() => _systemUnderTest.Sut.PrivateBoardVisibility = value);
		}
		private void PublicBoardVisibilityIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.PublicBoardVisibility);
		}
		private void PublicBoardVisibilityIsSet(BoardPermissionLevelType value)
		{
			Execute(() => _systemUnderTest.Sut.PublicBoardVisibility = value);
		}

		#endregion
	}
}
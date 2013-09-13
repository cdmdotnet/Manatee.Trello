using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class BoardPersonalPreferencesTest : EntityTestBase<BoardPersonalPreferences, IJsonBoardPersonalPreferences>
	{
		[TestMethod]
		public void ShowListGuide()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access ShowListGuide property when not expired")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowListGuideIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<BoardPersonalPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ShowListGuide property when expired")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsExpired)
				.When(ShowListGuideIsAccessed)
				.Then(RepositoryRefreshIsCalled<BoardPersonalPreferences>, EntityRequestType.BoardPersonalPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowListGuide property")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowListGuideIsSet, (bool?) true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.BoardPersonalPreferences_Write_ShowListGuide)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowListGuide property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowListGuideIs, (bool?)true)
				.When(ShowListGuideIsSet, (bool?) true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ShowSidebar()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access ShowSidebar property when not expired")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowSidebarIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<BoardPersonalPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ShowSidebar property when expired")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsExpired)
				.When(ShowSidebarIsAccessed)
				.Then(RepositoryRefreshIsCalled<BoardPersonalPreferences>, EntityRequestType.BoardPersonalPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebar property")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowSidebarIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.BoardPersonalPreferences_Write_ShowSidebar)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebar property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowSidebarIs, (bool?)true)
				.When(ShowSidebarIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ShowSidebarActivity()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access ShowSidebarActivity property when not expired")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowSidebarActivityIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<BoardPersonalPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ShowSidebarActivity property when expired")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsExpired)
				.When(ShowSidebarActivityIsAccessed)
				.Then(RepositoryRefreshIsCalled<BoardPersonalPreferences>, EntityRequestType.BoardPersonalPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarActivity property")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowSidebarActivityIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.BoardPersonalPreferences_Write_ShowSidebarActivity)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarActivity property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowSidebarActivityIs, (bool?)true)
				.When(ShowSidebarActivityIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ShowSidebarBoardActions()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access ShowSidebarBoardActions property when not expired")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowSidebarBoardActionsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<BoardPersonalPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ShowSidebarBoardActions property when expired")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsExpired)
				.When(ShowSidebarBoardActionsIsAccessed)
				.Then(RepositoryRefreshIsCalled<BoardPersonalPreferences>, EntityRequestType.BoardPersonalPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarBoardActions property")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowSidebarBoardActionsIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.BoardPersonalPreferences_Write_ShowSidebarBoardActions)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarBoardActions property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowSidebarBoardActionsIs, (bool?)true)
				.When(ShowSidebarBoardActionsIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ShowSidebarMembers()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access ShowSidebarMembers property when not expired")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowSidebarMembersIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<BoardPersonalPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ShowSidebarMembers property when expired")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsExpired)
				.When(ShowSidebarMembersIsAccessed)
				.Then(RepositoryRefreshIsCalled<BoardPersonalPreferences>, EntityRequestType.BoardPersonalPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarMembers property")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowSidebarMembersIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.BoardPersonalPreferences_Write_ShowSidebarMembers)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarMembers property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowSidebarMembersIs, (bool?)true)
				.When(ShowSidebarMembersIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ABoardPersonalPreferencesObject()
		{
			_test = new EntityUnderTest();
			OwnedBy<Board>();
		}
		private void ShowListGuideIs(bool? value)
		{
			_test.Json.SetupGet(j => j.ShowListGuide)
							.Returns(value);
		}
		private void ShowSidebarIs(bool? value)
		{
			_test.Json.SetupGet(j => j.ShowSidebar)
							.Returns(value);
		}
		private void ShowSidebarActivityIs(bool? value)
		{
			_test.Json.SetupGet(j => j.ShowSidebarActivity)
							.Returns(value);
		}
		private void ShowSidebarBoardActionsIs(bool? value)
		{
			_test.Json.SetupGet(j => j.ShowSidebarBoardActions)
							.Returns(value);
		}
		private void ShowSidebarMembersIs(bool? value)
		{
			_test.Json.SetupGet(j => j.ShowSidebarMembers)
							.Returns(value);
		}

		#endregion

		#region When

		private void ShowListGuideIsAccessed()
		{
			Execute(() => _test.Sut.ShowListGuide);
		}
		private void ShowListGuideIsSet(bool? value)
		{
			Execute(() => _test.Sut.ShowListGuide = value);
		}
		private void ShowSidebarIsAccessed()
		{
			Execute(() => _test.Sut.ShowSidebar);
		}
		private void ShowSidebarIsSet(bool? value)
		{
			Execute(() => _test.Sut.ShowSidebar = value);
		}
		private void ShowSidebarActivityIsAccessed()
		{
			Execute(() => _test.Sut.ShowSidebarActivity);
		}
		private void ShowSidebarActivityIsSet(bool? value)
		{
			Execute(() => _test.Sut.ShowSidebarActivity = value);
		}
		private void ShowSidebarBoardActionsIsAccessed()
		{
			Execute(() => _test.Sut.ShowSidebarBoardActions);
		}
		private void ShowSidebarBoardActionsIsSet(bool? value)
		{
			Execute(() => _test.Sut.ShowSidebarBoardActions = value);
		}
		private void ShowSidebarMembersIsAccessed()
		{
			Execute(() => _test.Sut.ShowSidebarMembers);
		}
		private void ShowSidebarMembersIsSet(bool? value)
		{
			Execute(() => _test.Sut.ShowSidebarMembers = value);
		}

		#endregion
	}
}
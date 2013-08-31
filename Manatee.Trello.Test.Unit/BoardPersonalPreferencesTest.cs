using System;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test.Unit
{
	[TestClass]
	public class BoardPersonalPreferencesTest : EntityTestBase<BoardPersonalPreferences>
	{
		[TestMethod]
		public void ShowListGuide()
		{
			var story = new Story("ShowListGuide");

			var feature = story.InOrderTo("control whether the list guide is shown")
				.AsA("developer")
				.IWant("to get the Attachments property value.");

			feature.WithScenario("Access ShowListGuide property")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.When(ShowListGuideIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ShowListGuide property when expired")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsExpired)
				.When(ShowListGuideIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowListGuide property")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.When(ShowListGuideIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowListGuide property to null")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.And(ShowListGuideIs, (bool?)true)
				.When(ShowListGuideIsSet, (bool?) null)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set ShowListGuide property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.And(ShowListGuideIs, (bool?)true)
				.When(ShowListGuideIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowListGuide property without UserToken")
				.Given(ABoardPersonalPreferencesObject)
				.And(TokenNotSupplied)
				.When(ShowListGuideIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void ShowSidebar()
		{
			var story = new Story("ShowSidebar");

			var feature = story.InOrderTo("control whether the side bar is shown")
				.AsA("developer")
				.IWant("to get the ShowSidebar property value.");

			feature.WithScenario("Access ShowSidebar property")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.When(ShowSidebarIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ShowSidebar property when expired")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsExpired)
				.When(ShowSidebarIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebar property")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.When(ShowSidebarIsSet, (bool?)true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebar property to null")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.And(ShowSidebarIs, (bool?)true)
				.When(ShowSidebarIsSet, (bool?) null)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set ShowSidebar property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.And(ShowSidebarIs, (bool?)true)
				.When(ShowSidebarIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebar property without UserToken")
				.Given(ABoardPersonalPreferencesObject)
				.And(TokenNotSupplied)
				.When(ShowSidebarIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void ShowSidebarActivity()
		{
			var story = new Story("ShowSidebarActivity");

			var feature = story.InOrderTo("control whether the activity section of the side bar is expanded")
				.AsA("developer")
				.IWant("to get the ShowSidebarActivity property value.");

			feature.WithScenario("Access ShowSidebarActivity property")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.When(ShowSidebarActivityIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ShowSidebarActivity property when expired")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsExpired)
				.When(ShowSidebarActivityIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarActivity property")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.When(ShowSidebarActivityIsSet, (bool?)true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarActivity property to null")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.And(ShowSidebarActivityIs, (bool?)true)
				.When(ShowSidebarActivityIsSet, (bool?) null)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set ShowSidebarActivity property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.And(ShowSidebarActivityIs, (bool?)true)
				.When(ShowSidebarActivityIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarActivity property without UserToken")
				.Given(ABoardPersonalPreferencesObject)
				.And(TokenNotSupplied)
				.When(ShowSidebarActivityIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void ShowSidebarBoardActions()
		{
			var story = new Story("ShowSidebarBoardActions");

			var feature = story.InOrderTo("control whether the actions section of the side bar is expanded")
				.AsA("developer")
				.IWant("to get the ShowSidebarBoardActions property value.");

			feature.WithScenario("Access ShowSidebarBoardActions property")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.When(ShowSidebarBoardActionsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ShowSidebarBoardActions property when expired")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsExpired)
				.When(ShowSidebarBoardActionsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarBoardActions property")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.When(ShowSidebarBoardActionsIsSet, (bool?)true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarBoardActions property to null")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.And(ShowSidebarBoardActionsIs, (bool?)true)
				.When(ShowSidebarBoardActionsIsSet, (bool?) null)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set ShowSidebarBoardActions property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.And(ShowSidebarBoardActionsIs, (bool?)true)
				.When(ShowSidebarBoardActionsIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarBoardActions property without UserToken")
				.Given(ABoardPersonalPreferencesObject)
				.And(TokenNotSupplied)
				.When(ShowSidebarBoardActionsIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void ShowSidebarMembers()
		{
			var story = new Story("ShowSidebarMembers");

			var feature = story.InOrderTo("control whether the members section of the side bar is expanded")
				.AsA("developer")
				.IWant("to get the ShowSidebarMembers property value.");

			feature.WithScenario("Access ShowSidebarMembers property")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.When(ShowSidebarMembersIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ShowSidebarMembers property when expired")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsExpired)
				.When(ShowSidebarMembersIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarMembers property")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.When(ShowSidebarMembersIsSet, (bool?)true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarMembers property to null")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.And(ShowSidebarMembersIs, (bool?)true)
				.When(ShowSidebarMembersIsSet, (bool?) null)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set ShowSidebarMembers property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(EntityIsRefreshed)
				.And(ShowSidebarMembersIs, (bool?)true)
				.When(ShowSidebarMembersIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarMembers property without UserToken")
				.Given(ABoardPersonalPreferencesObject)
				.And(TokenNotSupplied)
				.When(ShowSidebarMembersIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		#region Given

		private void ABoardPersonalPreferencesObject()
		{
			_systemUnderTest = new EntityUnderTest();
			OwnedBy<Board>();
			SetupMockGet<IJsonBoardPersonalPreferences>();
			SetupMockPost<IJsonBoardPersonalPreferences>();
		}
		private void ShowListGuideIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.ShowListGuide = value);
		}
		private void ShowSidebarIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.ShowSidebar = value);
		}
		private void ShowSidebarActivityIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.ShowSidebarActivity = value);
		}
		private void ShowSidebarBoardActionsIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.ShowSidebarBoardActions = value);
		}
		private void ShowSidebarMembersIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.ShowSidebarMembers = value);
		}

		#endregion

		#region When

		private void ShowListGuideIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ShowListGuide);
		}
		private void ShowListGuideIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.ShowListGuide = value);
		}
		private void ShowSidebarIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ShowSidebar);
		}
		private void ShowSidebarIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.ShowSidebar = value);
		}
		private void ShowSidebarActivityIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ShowSidebarActivity);
		}
		private void ShowSidebarActivityIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.ShowSidebarActivity = value);
		}
		private void ShowSidebarBoardActionsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ShowSidebarBoardActions);
		}
		private void ShowSidebarBoardActionsIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.ShowSidebarBoardActions = value);
		}
		private void ShowSidebarMembersIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ShowSidebarMembers);
		}
		private void ShowSidebarMembersIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.ShowSidebarMembers = value);
		}

		#endregion
	}
}
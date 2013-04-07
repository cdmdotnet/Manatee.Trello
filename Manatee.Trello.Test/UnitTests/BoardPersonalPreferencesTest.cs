using System;
using Manatee.Trello.Test.FunctionalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
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
				.And(EntityIsExpired)
				.When(ShowListGuideIsAccessed)
				.Then(MockApiGetIsCalled<BoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowListGuide property")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowListGuideIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowListGuide property to null")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowListGuideIs, (bool?) true)
				.When(ShowListGuideIsSet, (bool?) null)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set ShowListGuide property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowListGuideIs, (bool?) true)
				.When(ShowListGuideIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 0)
				.And(ExceptionIsNotThrown)

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
				.And(EntityIsExpired)
				.When(ShowSidebarIsAccessed)
				.Then(MockApiGetIsCalled<BoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebar property")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowSidebarIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebar property to null")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowSidebarIs, (bool?) true)
				.When(ShowSidebarIsSet, (bool?) null)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set ShowSidebar property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowSidebarIs, (bool?) true)
				.When(ShowSidebarIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 0)
				.And(ExceptionIsNotThrown)

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
				.And(EntityIsExpired)
				.When(ShowSidebarActivityIsAccessed)
				.Then(MockApiGetIsCalled<BoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarActivity property")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowSidebarActivityIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarActivity property to null")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowSidebarActivityIs, (bool?) true)
				.When(ShowSidebarActivityIsSet, (bool?) null)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set ShowSidebarActivity property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowSidebarActivityIs, (bool?) true)
				.When(ShowSidebarActivityIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 0)
				.And(ExceptionIsNotThrown)

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
				.And(EntityIsExpired)
				.When(ShowSidebarBoardActionsIsAccessed)
				.Then(MockApiGetIsCalled<BoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarBoardActions property")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowSidebarBoardActionsIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarBoardActions property to null")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowSidebarBoardActionsIs, (bool?) true)
				.When(ShowSidebarBoardActionsIsSet, (bool?) null)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set ShowSidebarBoardActions property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowSidebarBoardActionsIs, (bool?) true)
				.When(ShowSidebarBoardActionsIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 0)
				.And(ExceptionIsNotThrown)

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
				.And(EntityIsExpired)
				.When(ShowSidebarMembersIsAccessed)
				.Then(MockApiGetIsCalled<BoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarMembers property")
				.Given(ABoardPersonalPreferencesObject)
				.When(ShowSidebarMembersIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowSidebarMembers property to null")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowSidebarMembersIs, (bool?) true)
				.When(ShowSidebarMembersIsSet, (bool?) null)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set ShowSidebarMembers property to same")
				.Given(ABoardPersonalPreferencesObject)
				.And(ShowSidebarMembersIs, (bool?) true)
				.When(ShowSidebarMembersIsSet, (bool?) true)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ABoardPersonalPreferencesObject()
		{
			_systemUnderTest = new SystemUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
			_systemUnderTest.Sut.Owner = new Board {Id = TrelloIds.Invalid};
			SetupMockGet<BoardPersonalPreferences>();
			SetupMockPost<BoardPersonalPreferences>();
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
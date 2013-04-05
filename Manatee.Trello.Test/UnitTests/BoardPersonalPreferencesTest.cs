using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
				.When(ShowListGuideIsSet)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 1)
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
				.When(ShowSidebarIsSet)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 1)
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
				.When(ShowSidebarActivityIsSet)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 1)
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
				.When(ShowSidebarBoardActionsIsSet)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 1)
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
				.When(ShowSidebarMembersIsSet)
				.Then(MockApiPostIsCalled<BoardPersonalPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ABoardPersonalPreferencesObject()
		{
			_systemUnderTest = new SystemUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
			SetupMockGet<BoardPersonalPreferences>();
			SetupMockPost<BoardPersonalPreferences>();
		}

		#endregion

		#region When

		private void ShowListGuideIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ShowListGuide);
		}
		private void ShowListGuideIsSet()
		{
			Execute(() => _systemUnderTest.Sut.ShowListGuide = true);
		}
		private void ShowSidebarIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ShowListGuide);
		}
		private void ShowSidebarIsSet()
		{
			Execute(() => _systemUnderTest.Sut.ShowListGuide = true);
		}
		private void ShowSidebarActivityIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ShowListGuide);
		}
		private void ShowSidebarActivityIsSet()
		{
			Execute(() => _systemUnderTest.Sut.ShowListGuide = true);
		}
		private void ShowSidebarBoardActionsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ShowListGuide);
		}
		private void ShowSidebarBoardActionsIsSet()
		{
			Execute(() => _systemUnderTest.Sut.ShowListGuide = true);
		}
		private void ShowSidebarMembersIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ShowListGuide);
		}
		private void ShowSidebarMembersIsSet()
		{
			Execute(() => _systemUnderTest.Sut.ShowListGuide = true);
		}

		#endregion
	}
}
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class BadgesTest : EntityTestBase<Badges>
	{
		[TestMethod]
		public void Attachments()
		{
			var story = new Story("Attachments");

			var feature = story.InOrderTo("how many attachments a card has")
				.AsA("developer")
				.IWant("to get the Attachments property value.");

			feature.WithScenario("Access Attachments property")
				.Given(ABadgesObject)
				.And(EntityIsRefreshed)
				.When(AttachmentsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Attachments property when expired")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(AttachmentsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void CheckItems()
		{
			var story = new Story("CheckItems");

			var feature = story.InOrderTo("how many checklist items a card has")
				.AsA("developer")
				.IWant("to get the CheckItems property value.");

			feature.WithScenario("Access CheckItems property")
				.Given(ABadgesObject)
				.And(EntityIsRefreshed)
				.When(CheckItemsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access CheckItems property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(CheckItemsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void CheckItemsChecked()
		{
			var story = new Story("CheckItemsChecked");

			var feature = story.InOrderTo("how many checklist items on a card are checked")
				.AsA("developer")
				.IWant("to get the CheckItemsChecked property value.");

			feature.WithScenario("Access CheckItemsChecked property")
				.Given(ABadgesObject)
				.And(EntityIsRefreshed)
				.When(CheckItemsCheckedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access CheckItemsChecked property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(CheckItemsCheckedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Comments()
		{
			var story = new Story("Comments");

			var feature = story.InOrderTo("how many comments a card has")
				.AsA("developer")
				.IWant("to get the Comments property value.");

			feature.WithScenario("Access Comments property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(CommentsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Comments property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(CommentsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void DueDate()
		{
			var story = new Story("DueDate");

			var feature = story.InOrderTo("how what due date, if any, a card has")
				.AsA("developer")
				.IWant("to get the DueDate property value.");

			feature.WithScenario("Access DueDate property")
				.Given(ABadgesObject)
				.And(EntityIsRefreshed)
				.When(DueDateIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access DueDate property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(DueDateIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void FogBugz()
		{
			var story = new Story("FogBugz");

			var feature = story.InOrderTo("if a card has a FogBugz link")
				.AsA("developer")
				.IWant("to get the FogBugz property value.");

			feature.WithScenario("Access FogBugz property")
				.Given(ABadgesObject)
				.And(EntityIsRefreshed)
				.When(FogBugzIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access FogBugz property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(FogBugzIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void HasDescription()
		{
			var story = new Story("HasDescription");

			var feature = story.InOrderTo("if a card has a description")
				.AsA("developer")
				.IWant("to get the HasDescription property value.");

			feature.WithScenario("Access HasDescription property")
				.Given(ABadgesObject)
				.And(EntityIsRefreshed)
				.When(HasDescriptionIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access HasDescription property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(HasDescriptionIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void IsSubscribed()
		{
			var story = new Story("IsSubscribed");

			var feature = story.InOrderTo("if the current member is subscribed to a card")
				.AsA("developer")
				.IWant("to get the IsSubscribed property value.");

			feature.WithScenario("Access IsSubscribed property")
				.Given(ABadgesObject)
				.And(EntityIsRefreshed)
				.When(IsSubscribedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsSubscribed property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(IsSubscribedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ViewingMemberVoted()
		{
			var story = new Story("ViewingMemberVoted");

			var feature = story.InOrderTo("if the current member has voted on a card")
				.AsA("developer")
				.IWant("to get the ViewingMemberVoted property value.");

			feature.WithScenario("Access ViewingMemberVoted property")
				.Given(ABadgesObject)
				.And(EntityIsRefreshed)
				.When(ViewingMemberVotedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ViewingMemberVoted property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(ViewingMemberVotedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Votes()
		{
			var story = new Story("Attachments");

			var feature = story.InOrderTo("how many votes a card has")
				.AsA("developer")
				.IWant("to get the Votes property value.");

			feature.WithScenario("Access Votes property")
				.Given(ABadgesObject)
				.And(EntityIsRefreshed)
				.When(VotesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Votes property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(VotesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBadges>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ABadgesObject()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Svc.Object;
			OwnedBy<Card>();
			SetupMockGet<IJsonBadges>();
		}

		#endregion

		#region When

		private void AttachmentsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Attachments);
		}
		private void CheckItemsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.CheckItems);
		}
		private void CheckItemsCheckedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.CheckItemsChecked);
		}
		private void CommentsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Comments);
		}
		private void DueDateIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.DueDate);
		}
		private void FogBugzIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.FogBugz);
		}
		private void HasDescriptionIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.HasDescription);
		}
		private void IsSubscribedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.IsSubscribed);
		}
		private void ViewingMemberVotedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ViewingMemberVoted);
		}
		private void VotesIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Votes);
		}

		#endregion
	}
}

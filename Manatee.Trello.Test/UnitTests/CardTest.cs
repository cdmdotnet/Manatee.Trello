using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Test.FunctionalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class CardTest : EntityTestBase<Card>
	{
		[TestMethod]
		public void Actions()
		{
			var story = new Story("Actions");

			var feature = story.InOrderTo("get all actions for a card")
				.AsA("developer")
				.IWant("to get Actions");

			feature.WithScenario("Access Actions property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(ActionsIsAccessed)
				.Then(MockApiGetCollectionIsCalled<Action>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Action>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AttachmentCoverId()
		{
			var story = new Story("AttachmentCoverId");

			var feature = story.InOrderTo("get a card's attachment cover ID")
				.AsA("developer")
				.IWant("to get the AttachmentCoverId");

			feature.WithScenario("Access AttachmentCoverId property")
				.Given(ACard)
				.And(EntityIsNotExpired)
				.When(AttachmentCoverIdIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access AttachmentCoverId property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(AttachmentCoverIdIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Attachments()
		{
			var story = new Story("Attachments");

			var feature = story.InOrderTo("get all attachments for a card")
				.AsA("developer")
				.IWant("to get Attachments");

			feature.WithScenario("Access Attachments property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(AttachmentsIsAccessed)
				.Then(MockApiGetIsCalled<Attachment>, 0)
				.And(MockApiGetCollectionIsCalled<Attachment>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Attachment>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Badges()
		{
			var story = new Story("Badges");

			var feature = story.InOrderTo("get a card's badge set")
				.AsA("developer")
				.IWant("to get the Badges");

			feature.WithScenario("Access Badges property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(BadgesIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(NonNullValueOfTypeIsReturned<Badges>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Board()
		{
			var story = new Story("Board");

			var feature = story.InOrderTo("get the board which contains a card")
				.AsA("developer")
				.IWant("to get the Board");

			feature.WithScenario("Access Board property")
				.Given(ACard)
				.And(EntityIsNotExpired)
				.When(BoardIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)
				
				.WithScenario("Access Board property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(BoardIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void CheckLists()
		{
			var story = new Story("CheckLists");

			var feature = story.InOrderTo("get the check lists contained within a card")
				.AsA("developer")
				.IWant("to get CheckLists");

			feature.WithScenario("Access CheckLists property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(CheckListsIsAccessed)
				.Then(MockApiGetCollectionIsCalled<CheckList>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<CheckList>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Comments()
		{
			var story = new Story("Comments");

			var feature = story.InOrderTo("get all comments for a card")
				.AsA("developer")
				.IWant("to get Comments");

			feature.WithScenario("Access Comments property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(CommentsIsAccessed)
				.Then(MockApiGetCollectionIsCalled<Action>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Action>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Description()
		{
			var story = new Story("Description");

			var feature = story.InOrderTo("control a card's description")
				.AsA("developer")
				.IWant("to get and set the Description");

			feature.WithScenario("Access Description property")
				.Given(ACard)
				.And(EntityIsNotExpired)
				.When(DescriptionIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Description property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(DescriptionIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property")
				.Given(ACard)
				.When(DescriptionIsSet, "description")
				.Then(MockApiPutIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property to null")
				.Given(ACard)
				.And(DescriptionIs, "not description")
				.When(DescriptionIsSet, (string) null)
				.Then(MockApiPutIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property to empty")
				.Given(ACard)
				.And(DescriptionIs, "not description")
				.When(DescriptionIsSet, string.Empty)
				.Then(MockApiPutIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property to same")
				.Given(ACard)
				.And(DescriptionIs, "description")
				.When(DescriptionIsSet, "description")
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(DescriptionIsSet, "description")
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void DueDate()
		{
			var story = new Story("DueDate");

			var feature = story.InOrderTo("control a card's due date")
				.AsA("developer")
				.IWant("to get and set the DueDate");

			feature.WithScenario("Access DueDate property")
				.Given(ACard)
				.And(EntityIsNotExpired)
				.When(DueDateIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access DueDate property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(DueDateIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set DueDate property")
				.Given(ACard)
				.When(DueDateIsSet, (DateTime?) DateTime.Now.AddDays(1))
				.Then(MockApiPutIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set DueDate property to null")
				.Given(ACard)
				.And(DueDateIs, (DateTime?) DateTime.Now.AddDays(1))
				.When(DueDateIsSet, (DateTime?) null)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set DueDate property to same")
				.Given(ACard)
				.And(DueDateIs, (DateTime?) DateTime.Now.AddDays(1))
				.When(DueDateIsSet, (DateTime?) DateTime.Now.AddDays(1))
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set DueDate property without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(DueDateIsSet, (DateTime?) DateTime.Now.AddDays(1))
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void IsClosed()
		{
			var story = new Story("IsClosed");

			var feature = story.InOrderTo("control a card is closed")
				.AsA("developer")
				.IWant("to get and set IsClosed");

			feature.WithScenario("Access IsClosed property")
				.Given(ACard)
				.And(EntityIsNotExpired)
				.When(IsClosedIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsClosed property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(IsClosedIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property")
				.Given(ACard)
				.When(IsClosedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property to null")
				.Given(ACard)
				.And(IsClosedIs, (bool?) true)
				.When(IsClosedIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set IsClosed property to same")
				.Given(ACard)
				.And(IsClosedIs, (bool?) true)
				.When(IsClosedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(IsClosedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void IsSubscribed()
		{
			var story = new Story("IsSubscribed");

			var feature = story.InOrderTo("control whether the current member is subscribed to a card")
				.AsA("developer")
				.IWant("to get and set IsSubscribed");

			feature.WithScenario("Access IsSubscribed property")
				.Given(ACard)
				.And(EntityIsNotExpired)
				.When(IsSubscribedIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsSubscribed property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(IsSubscribedIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property")
				.Given(ACard)
				.When(IsSubscribedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property to null")
				.Given(ACard)
				.And(IsSubscribedIs, (bool?) true)
				.When(IsSubscribedIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set IsSubscribed property to same")
				.Given(ACard)
				.And(IsSubscribedIs, (bool?) true)
				.When(IsSubscribedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(IsSubscribedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Labels()
		{
			var story = new Story("Labels");

			var feature = story.InOrderTo("get all labels applied to a card")
				.AsA("developer")
				.IWant("to get Labels");

			feature.WithScenario("Access Labels property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(LabelsIsAccessed)
				.Then(MockApiGetIsCalled<Label>, 0)
				.And(MockApiGetCollectionIsCalled<Label>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Label>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void List()
		{
			var story = new Story("List");

			var feature = story.InOrderTo("get the list which contains a card")
				.AsA("developer")
				.IWant("to get the List");

			feature.WithScenario("Access List property")
				.Given(ACard)
				.And(EntityIsNotExpired)
				.When(ListIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access List property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(ListIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ManualCoverAttachment()
		{
			var story = new Story("ManualCoverAttachment");

			var feature = story.InOrderTo("get if a card's attachment cover was manually set")
				.AsA("developer")
				.IWant("to get the ManualCoverAttachment");

			feature.WithScenario("Access ManualCoverAttachment property")
				.Given(ACard)
				.And(EntityIsNotExpired)
				.When(ManualCoverAttachmentIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ManualCoverAttachment property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(ManualCoverAttachmentIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Members()
		{
			var story = new Story("Members");

			var feature = story.InOrderTo("get all members assigned to a card")
				.AsA("developer")
				.IWant("to get Members");

			feature.WithScenario("Access Members property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(MembersIsAccessed)
				.Then(MockApiGetIsCalled<Member>, 0)
				.And(MockApiGetCollectionIsCalled<Member>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Member>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Name()
		{
			var story = new Story("Name");

			var feature = story.InOrderTo("control a card's name")
				.AsA("developer")
				.IWant("to get and set the Name");

			feature.WithScenario("Access Name property")
				.Given(ACard)
				.And(EntityIsNotExpired)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Name property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property")
				.Given(ACard)
				.When(NameIsSet, "name")
				.Then(MockApiPutIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to null")
				.Given(ACard)
				.And(NameIs, "name")
				.When(NameIsSet, (string) null)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Name property to empty")
				.Given(ACard)
				.And(NameIs, "not description")
				.When(NameIsSet, string.Empty)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Name property to same")
				.Given(ACard)
				.And(NameIs, "description")
				.When(NameIsSet, "description")
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(NameIsSet, "description")
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Position()
		{
			var story = new Story("Position");

			var feature = story.InOrderTo("control the postition of a card within its list")
				.AsA("developer")
				.IWant("to get and set Position");

			feature.WithScenario("Access Position property")
				.Given(ACard)
				.And(EntityIsNotExpired)
				.When(PositionIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Position property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(PositionIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property")
				.Given(ACard)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(MockApiPutIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property to null")
				.Given(ACard)
				.And(PositionIs, Trello.Position.Bottom)
				.When(PositionIsSet, (Position) null)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Position property to same")
				.Given(ACard)
				.And(PositionIs, Trello.Position.Bottom)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void ShortId()
		{
			var story = new Story("ShortId");

			var feature = story.InOrderTo("get a card's ShortId")
				.AsA("developer")
				.IWant("to get the ShortId");

			feature.WithScenario("Access ShortId property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(ShortIdIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Url()
		{
			var story = new Story("Url");

			var feature = story.InOrderTo("get a card's URL")
				.AsA("developer")
				.IWant("to get the Url");

			feature.WithScenario("Access Url property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(UrlIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void VotingMembers()
		{
			var story = new Story("VotingMembers");

			var feature = story.InOrderTo("get all members who voted on a card")
				.AsA("developer")
				.IWant("to get VotingMembers");

			feature.WithScenario("Access VotingMembers property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(VotingMembersIsAccessed)
				.Then(MockApiGetIsCalled<Member>, 0)
				.And(MockApiGetCollectionIsCalled<Member>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Member>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		[Ignore]
		public void AddAttachment()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void AddCheckList()
		{
			var story = new Story("AddCheckList");

			var feature = story.InOrderTo("add a checklist to a card")
				.AsA("developer")
				.IWant("to call AddCheckList");

			feature.WithScenario("AddCheckList is called")
				.Given(ACard)
				.When(AddCheckListIsCalled, "checklist")
				.Then(MockApiPostIsCalled<CheckList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("AddCheckList is called with null name")
				.Given(ACard)
				.When(AddCheckListIsCalled, (string) null)
				.Then(MockApiPostIsCalled<CheckList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddCheckList is called with empty name")
				.Given(ACard)
				.When(AddCheckListIsCalled, string.Empty)
				.Then(MockApiPostIsCalled<CheckList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddCheckList is called without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(AddCheckListIsCalled, string.Empty)
				.Then(MockApiPutIsCalled<CheckList>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void AddComment()
		{
			var story = new Story("AddComment");

			var feature = story.InOrderTo("add a comment to a card")
				.AsA("developer")
				.IWant("to call AddComment");

			feature.WithScenario("AddComment is called")
				.Given(ACard)
				.When(AddCommentIsCalled, "checklist")
				.Then(MockApiPostIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("AddComment is called with null name")
				.Given(ACard)
				.When(AddCommentIsCalled, (string)null)
				.Then(MockApiPostIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddComment is called with empty name")
				.Given(ACard)
				.When(AddCommentIsCalled, string.Empty)
				.Then(MockApiPostIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddComment is called without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(AddCommentIsCalled, string.Empty)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void ApplyLabel()
		{
			var story = new Story("ApplyLabel");

			var feature = story.InOrderTo("apply a label to a card")
				.AsA("developer")
				.IWant("to call ApplyLabel");

			feature.WithScenario("ApplyLabel is called")
				.Given(ACard)
				.When(ApplyLabelIsCalled, LabelColor.Red)
				.Then(MockApiPostIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("ApplyLabel is called without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(ApplyLabelIsCalled, LabelColor.Red)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void AssignMember()
		{
			var story = new Story("AssignMember");

			var feature = story.InOrderTo("assign a member to a card")
				.AsA("developer")
				.IWant("to call AssignMember");

			feature.WithScenario("AssignMember is called")
				.Given(ACard)
				.When(AssignMemberIsCalled, new Member {Id = TrelloIds.Invalid})
				.Then(MockApiPostIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("AssignMember is called with null member")
				.Given(ACard)
				.When(AssignMemberIsCalled, (Member) null)
				.Then(MockApiPostIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AssignMember is called with local member")
				.Given(ACard)
				.When(AssignMemberIsCalled, new Member())
				.Then(MockApiPostIsCalled<Card>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Member>>)

				.WithScenario("AssignMember is called without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(AssignMemberIsCalled, new Member())
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void ClearNotifications()
		{
			var story = new Story("ClearNotifications");

			var feature = story.InOrderTo("clear all notifications for a card")
				.AsA("developer")
				.IWant("to call ClearNotifications");

			feature.WithScenario("ClearNotifications is called")
				.Given(ACard)
				.When(ClearNotificationsIsCalled)
				.Then(MockApiPostIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("ClearNotifications is called without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(ClearNotificationsIsCalled)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var story = new Story("Delete");

			var feature = story.InOrderTo("delete a card")
				.AsA("developer")
				.IWant("to call Delete");

			feature.WithScenario("Delete is called")
				.Given(ACard)
				.When(DeleteIsCalled)
				.Then(MockApiDeleteIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(DeleteIsCalled)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		[Ignore]
		public void Move()
		{
			var story = new Story("Move");

			var feature = story.InOrderTo("move a card to a different board or list")
				.AsA("developer")
				.IWant("to call Move");

			feature.WithScenario("Move is called")
				.Given(ACard)
				.And(BoardContainsList)
				// Need to figure this out...
				.When(MoveIsCalled, new Board(_systemUnderTest.Dependencies.Api.Object, TrelloIds.Invalid),
				      new List {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Move is called and board does not contain list")
				.Given(ACard)
				.When(MoveIsCalled, new Board {Id = TrelloIds.Invalid}, new List {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<Card>, 1)
				.And(ExceptionIsThrown<InvalidOperationException>)

				.WithScenario("Move is called with null board")
				.Given(ACard)
				.When(MoveIsCalled, (Board) null, new List {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Move is called with local board")
				.Given(ACard)
				.When(MoveIsCalled, new Board(), new List {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Board>>)

				.WithScenario("Move is called with null list")
				.Given(ACard)
				.When(MoveIsCalled, new Board {Id = TrelloIds.Invalid}, (List) null)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Move is called with local list")
				.Given(ACard)
				.When(MoveIsCalled, new Board {Id = TrelloIds.Invalid}, new List())
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<List>>)

				.WithScenario("Move is called without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(MoveIsCalled, new Board {Id = TrelloIds.Invalid}, new List {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void RemoveLabel()
		{
			var story = new Story("RemoveLabel");

			var feature = story.InOrderTo("remove a label from a card")
				.AsA("developer")
				.IWant("to call RemoveLabel");

			feature.WithScenario("RemoveLabel is called")
				.Given(ACard)
				.When(RemoveLabelIsCalled, LabelColor.Red)
				.Then(MockApiDeleteIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("RemoveLabel is called without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(RemoveLabelIsCalled, LabelColor.Red)
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void RemoveMember()
		{
			var story = new Story("RemoveMember");

			var feature = story.InOrderTo("remove a member from a card")
				.AsA("developer")
				.IWant("to call RemoveMember");

			feature.WithScenario("RemoveMember is called")
				.Given(ACard)
				.When(RemoveMemberIsCalled, new Member {Id = TrelloIds.Invalid})
				.Then(MockApiDeleteIsCalled<Card>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("RemoveMember is called with null member")
				.Given(ACard)
				.When(RemoveMemberIsCalled, (Member) null)
				.Then(MockApiDeleteIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("RemoveMember is called with local member")
				.Given(ACard)
				.When(RemoveMemberIsCalled, new Member())
				.Then(MockApiDeleteIsCalled<Card>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Member>>)

				.WithScenario("RemoveMember is called without AuthToken")
				.Given(ACard)
				.And(TokenNotSupplied)
				.When(RemoveMemberIsCalled, new Member {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<Card>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		#region Given

		private void ACard()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
			SetupMockPost<CheckList>();
		}
		private void DescriptionIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Description = value);
		}
		private void DueDateIs(DateTime? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.DueDate = value);
		}
		private void IsClosedIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.IsClosed = value);
		}
		private void IsSubscribedIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.IsSubscribed = value);
		}
		private void NameIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Name = value);
		}
		private void PositionIs(Position value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Position = value);
		}
		private void BoardContainsList()
		{
			_systemUnderTest.Dependencies.Api.Setup(a => a.Get(It.IsAny<IRestCollectionRequest<List>>()))
				.Returns(new List<List> {new List {Id = TrelloIds.Invalid}});
		}

		#endregion

		#region When

		private void ActionsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Actions);
		}
		private void AttachmentCoverIdIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.AttachmentCoverId);
		}
		private void AttachmentsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Attachments);
		}
		private void BadgesIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Badges);
		}
		private void BoardIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Board);
		}
		private void CheckListsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.CheckLists);
		}
		private void CommentsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Comments);
		}
		private void DescriptionIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Description);
		}
		private void DescriptionIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Description = value);
		}
		private void DueDateIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.DueDate);
		}
		private void DueDateIsSet(DateTime? value)
		{
			Execute(() => _systemUnderTest.Sut.DueDate = value);
		}
		private void IsClosedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.IsClosed);
		}
		private void IsClosedIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.IsClosed = value);
		}
		private void IsSubscribedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.IsSubscribed);
		}
		private void IsSubscribedIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.IsSubscribed = value);
		}
		private void LabelsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Labels);
		}
		private void ListIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.List);
		}
		private void ManualCoverAttachmentIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ManualCoverAttachment);
		}
		private void MembersIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Members);
		}
		private void NameIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Name);
		}
		private void NameIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Name = value);
		}
		private void PositionIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Position);
		}
		private void PositionIsSet(Position value)
		{
			Execute(() => _systemUnderTest.Sut.Position = value);
		}
		private void ShortIdIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ShortId);
		}
		private void UrlIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Url);
		}
		private void VotingMembersIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.VotingMembers);
		}
		private void AddAttachmentIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.AddAttachment());
		}
		private void AddCheckListIsCalled(string value)
		{
			Execute(() => _systemUnderTest.Sut.AddCheckList(value));
		}
		private void AddCommentIsCalled(string value)
		{
			Execute(() => _systemUnderTest.Sut.AddComment(value));
		}
		private void ApplyLabelIsCalled(LabelColor value)
		{
			Execute(() => _systemUnderTest.Sut.ApplyLabel(value));
		}
		private void AssignMemberIsCalled(Member value)
		{
			Execute(() => _systemUnderTest.Sut.AssignMember(value));
		}
		private void ClearNotificationsIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.ClearNotifications());
		}
		private void DeleteIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.Delete());
		}
		private void MoveIsCalled(Board board, List list)
		{
			Execute(() => _systemUnderTest.Sut.Move(board, list));
		}
		private void RemoveLabelIsCalled(LabelColor value)
		{
			Execute(() => _systemUnderTest.Sut.RemoveLabel(value));
		}
		private void RemoveMemberIsCalled(Member value)
		{
			Execute(() => _systemUnderTest.Sut.RemoveMember(value));
		}

		#endregion
	}
}
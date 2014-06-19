using System;
using System.Linq;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class CardTest : EntityTestBase<Card, IJsonCard>
	{
		[TestMethod]
		public void Actions()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Actions property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(ActionsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(RepositoryRefreshCollectionIsNotCalled<Action>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Actions collection enumerates")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(ActionsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Action>, EntityRequestType.Card_Read_Actions)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AttachmentCoverId()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access AttachmentCoverId property when not expired")
				.Given(ACard)
				.When(AttachmentCoverIdIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access AttachmentCoverId property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(AttachmentCoverIdIsAccessed)
				.Then(RepositoryRefreshIsCalled<Card>, EntityRequestType.Card_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Attachments()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Attachments property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(AttachmentsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(RepositoryRefreshCollectionIsNotCalled<Attachment>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Attachments collection enumerates")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(AttachmentsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Attachment>, EntityRequestType.Card_Read_Attachments)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Badges()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Badges property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(BadgesIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Board()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Board property when not expired")
				.Given(ACard)
				.When(BoardIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)
				
				.WithScenario("Access Board property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(BoardIsAccessed)
				.Then(RepositoryRefreshIsCalled<Card>, EntityRequestType.Card_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void CheckLists()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access CheckLists property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(CheckListsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(RepositoryRefreshCollectionIsNotCalled<CheckList>)
				.And(ExceptionIsNotThrown)

				.WithScenario("CheckLists collection enumerates")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(CheckListsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<CheckList>, EntityRequestType.Card_Read_CheckLists)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Comments()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Comments property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(CommentsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Comments collection enumerates")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(CommentsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Action>, EntityRequestType.Card_Read_Actions)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Description()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Description property when not expired")
				.Given(ACard)
				.When(DescriptionIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Description property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(DescriptionIsAccessed)
				.Then(RepositoryRefreshIsCalled<Card>, EntityRequestType.Card_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property")
				.Given(ACard)
				.When(DescriptionIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_Description)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property to same")
				.Given(ACard)
				.And(DescriptionIs, "description")
				.When(DescriptionIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void DueDate()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access DueDate property when not expired")
				.Given(ACard)
				.When(DueDateIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access DueDate property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(DueDateIsAccessed)
				.Then(RepositoryRefreshIsCalled<Card>, EntityRequestType.Card_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set DueDate property")
				.Given(ACard)
				.When(DueDateIsSet, (DateTime?)DateTime.Now.AddDays(1))
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_DueDate)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set DueDate property to same")
				.Given(ACard)
				.And(DueDateIs, (DateTime?)DateTime.Today.AddDays(1))
				.When(DueDateIsSet, (DateTime?)DateTime.Today.AddDays(1))
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void IsClosed()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access IsClosed property when not expired")
				.Given(ACard)
				.When(IsClosedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsClosed property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(IsClosedIsAccessed)
				.Then(RepositoryRefreshIsCalled<Card>, EntityRequestType.Card_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property")
				.Given(ACard)
				.When(IsClosedIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_IsClosed)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property to same")
				.Given(ACard)
				.And(IsClosedIs, (bool?)true)
				.When(IsClosedIsSet, (bool?) true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void IsSubscribed()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access IsSubscribed property when not expired")
				.Given(ACard)
				.When(IsSubscribedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsSubscribed property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(IsSubscribedIsAccessed)
				.Then(RepositoryRefreshIsCalled<Card>, EntityRequestType.Card_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property")
				.Given(ACard)
				.When(IsSubscribedIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_IsSubscribed)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property to same")
				.Given(ACard)
				.And(IsSubscribedIs, (bool?)true)
				.When(IsSubscribedIsSet, (bool?) true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Labels()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Labels property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(LabelsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(RepositoryRefreshCollectionIsNotCalled<Label>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Labels collection enumerates")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(LabelsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Label>, EntityRequestType.Card_Read_Labels)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void List()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access List property when not expired")
				.Given(ACard)
				.When(ListIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access List property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(ListIsAccessed)
				.Then(RepositoryRefreshIsCalled<Card>, EntityRequestType.Card_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ManualCoverAttachment()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access ManualCoverAttachment property when not expired")
				.Given(ACard)
				.When(ManualCoverAttachmentIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ManualCoverAttachment property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(ManualCoverAttachmentIsAccessed)
				.Then(RepositoryRefreshIsCalled<Card>, EntityRequestType.Card_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Members()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Members property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(MembersIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Members collection enumerates")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(MembersIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Member>, EntityRequestType.Card_Read_Members)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Name()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Name property when not expired")
				.Given(ACard)
				.When(NameIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Name property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(NameIsAccessed)
				.Then(RepositoryRefreshIsCalled<Card>, EntityRequestType.Card_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property")
				.Given(ACard)
				.When(NameIsSet, "name")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_Name)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to same")
				.Given(ACard)
				.And(NameIs, "description")
				.When(NameIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Position()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Position propertyy when not expired")
				.Given(ACard)
				.When(PositionIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Position property when expired")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(PositionIsAccessed)
				.Then(RepositoryRefreshIsCalled<Card>, EntityRequestType.Card_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property")
				.Given(ACard)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorPositionIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_Position)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property to same")
				.Given(ACard)
				.And(PositionIs, Trello.Position.Bottom)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorPositionIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ShortId()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access ShortId property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(ShortIdIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Url()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Url property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(UrlIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void VotingMembers()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access VotingMembers property")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(VotingMembersIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("VotingMembers collection enumerates")
				.Given(ACard)
				.And(EntityIsExpired)
				.When(VotingMembersIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Member>, EntityRequestType.Card_Read_VotingMembers)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AddAttachment()
		{
			var feature = CreateFeature();

			feature.WithScenario("AddAttachment is called")
				.Given(ACard)
				.When(AddAttachmentIsCalled, "logo", TrelloIds.AttachmentUrl)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorUrlIsCalled)
				.And(RepositoryDownloadIsCalled<Attachment>, EntityRequestType.Card_Write_AddAttachment)
				.And(ExceptionIsNotThrown)

				.WithScenario("AddAttachment is called with null name")
				.Given(ACard)
				.When(AddAttachmentIsCalled, (string) null, TrelloIds.AttachmentUrl)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorUrlIsCalled)
				.And(RepositoryDownloadIsCalled<Attachment>, EntityRequestType.Card_Write_AddAttachment)
				.And(ExceptionIsNotThrown)

				.WithScenario("AddAttachment is called with empty name")
				.Given(ACard)
				.When(AddAttachmentIsCalled, string.Empty, TrelloIds.AttachmentUrl)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorUrlIsCalled)
				.And(RepositoryDownloadIsCalled<Attachment>, EntityRequestType.Card_Write_AddAttachment)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AddCheckList()
		{
			var feature = CreateFeature();

			feature.WithScenario("AddCheckList is called")
				.Given(ACard)
				.When(AddCheckListIsCalled, "checklist")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryDownloadIsCalled<CheckList>, EntityRequestType.Card_Write_AddChecklist)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AddComment()
		{
			var feature = CreateFeature();

			feature.WithScenario("AddComment is called")
				.Given(ACard)
				.When(AddCommentIsCalled, "checklist")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_AddComment)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ApplyLabel()
		{
			var feature = CreateFeature();

			feature.WithScenario("ApplyLabel is called")
				.Given(ACard)
				.When(ApplyLabelIsCalled, LabelColor.Red)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_ApplyLabel)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AssignMember()
		{
			var feature = CreateFeature();

			feature.WithScenario("AssignMember is called")
				.Given(ACard)
				.When(AssignMemberIsCalled, new Member {Id = TrelloIds.Test})
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Member>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_AssignMember)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ClearNotifications()
		{
			var feature = CreateFeature();

			feature.WithScenario("ClearNotifications is called")
				.Given(ACard)
				.When(ClearNotificationsIsCalled)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_ClearNotifications)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var feature = CreateFeature();

			feature.WithScenario("Delete is called when not deleted")
				.Given(ACard)
				.When(DeleteIsCalled)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_Delete)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called when already deleted")
				.Given(ACard)
				.And(AlreadyDeleted)
				.When(DeleteIsCalled)
				.Then(ValidatorWritableIsNotCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		[Ignore]
		public void Move()
		{
			var feature = CreateFeature();

			feature.WithScenario("Move is called and board contains list")
				.Given(ACard)
				.And(BoardContainsList)
				.When(MoveIsCalled, new Board(), new List {Id = TrelloIds.Test})
				.Then(ValidatorEntityIsCalled<Board>)
				.And(ValidatorEntityIsCalled<List>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_Move)
				.And(ExceptionIsNotThrown)

				.WithScenario("Move is called and board does not contain list")
				.Given(ACard)
				.When(MoveIsCalled, new Board {Id = TrelloIds.Test}, new List {Id = TrelloIds.Test})
				.Then(ValidatorEntityIsCalled<Board>)
				.And(ValidatorEntityIsCalled<List>)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsThrown<InvalidOperationException>)

				.Execute();
		}
		[TestMethod]
		public void RemoveLabel()
		{
			var feature = CreateFeature();

			feature.WithScenario("RemoveLabel is called")
				.Given(ACard)
				.When(RemoveLabelIsCalled, LabelColor.Red)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_RemoveLabel)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void RemoveMember()
		{
			var feature = CreateFeature();

			feature.WithScenario("RemoveMember is called")
				.Given(ACard)
				.When(RemoveMemberIsCalled, new Member {Id = TrelloIds.Test})
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Member>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Card_Write_RemoveMember)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ACard()
		{
			_test = new EntityUnderTest();
			_test.Dependencies.SetupListGeneration<Action>();
			_test.Dependencies.SetupListGeneration<Attachment>();
			_test.Dependencies.SetupListGeneration<CheckList>();
			_test.Dependencies.SetupListGeneration<Label>();
			_test.Dependencies.SetupListGeneration<Member>();
		}
		private void DescriptionIs(string value)
		{
			_test.Json.SetupGet(j => j.Desc)
				.Returns(value);
		}
		private void DueDateIs(DateTime? value)
		{
			_test.Json.SetupGet(j => j.Due)
				.Returns(value);
		}
		private void IsClosedIs(bool? value)
		{
			_test.Json.SetupGet(j => j.Closed)
				.Returns(value);
		}
		private void IsSubscribedIs(bool? value)
		{
			_test.Json.SetupGet(j => j.Subscribed)
				.Returns(value);
		}
		private void NameIs(string value)
		{
			_test.Json.SetupGet(j => j.Name)
				.Returns(value);
		}
		private void PositionIs(Position value)
		{
			_test.Json.SetupGet(j => j.Pos)
				.Returns(value.Value);
			ReapplyJson();
		}
		private void BoardContainsList()
		{
			// TODO ???
		}
		private void AlreadyDeleted()
		{
			_test.Sut.ForceDeleted(true);
		}

		#endregion

		#region When

		private void ActionsIsAccessed()
		{
			Execute(() => _test.Sut.Actions);
		}
		private void ActionsIsEnumerated()
		{
			Execute(() => _test.Sut.Actions.ToList());
		}
		private void AttachmentCoverIdIsAccessed()
		{
			Execute(() => _test.Sut.AttachmentCoverId);
		}
		private void AttachmentsIsAccessed()
		{
			Execute(() => _test.Sut.Attachments);
		}
		private void AttachmentsIsEnumerated()
		{
			Execute(() => _test.Sut.Attachments.ToList());
		}
		private void BadgesIsAccessed()
		{
			Execute(() => _test.Sut.Badges);
		}
		private void BoardIsAccessed()
		{
			Execute(() => _test.Sut.Board);
		}
		private void CheckListsIsAccessed()
		{
			Execute(() => _test.Sut.CheckLists);
		}
		private void CheckListsIsEnumerated()
		{
			Execute(() => _test.Sut.CheckLists.ToList());
		}
		private void CommentsIsAccessed()
		{
			Execute(() => _test.Sut.Comments);
		}
		private void CommentsIsEnumerated()
		{
			Execute(() => _test.Sut.Comments.ToList());
		}
		private void DescriptionIsAccessed()
		{
			Execute(() => _test.Sut.Description);
		}
		private void DescriptionIsSet(string value)
		{
			Execute(() => _test.Sut.Description = value);
		}
		private void DueDateIsAccessed()
		{
			Execute(() => _test.Sut.DueDate);
		}
		private void DueDateIsSet(DateTime? value)
		{
			Execute(() => _test.Sut.DueDate = value);
		}
		private void IsClosedIsAccessed()
		{
			Execute(() => _test.Sut.IsClosed);
		}
		private void IsClosedIsSet(bool? value)
		{
			Execute(() => _test.Sut.IsClosed = value);
		}
		private void IsSubscribedIsAccessed()
		{
			Execute(() => _test.Sut.IsSubscribed);
		}
		private void IsSubscribedIsSet(bool? value)
		{
			Execute(() => _test.Sut.IsSubscribed = value);
		}
		private void LabelsIsAccessed()
		{
			Execute(() => _test.Sut.Labels);
		}
		private void LabelsIsEnumerated()
		{
			Execute(() => _test.Sut.Labels.ToList());
		}
		private void ListIsAccessed()
		{
			Execute(() => _test.Sut.List);
		}
		private void ManualCoverAttachmentIsAccessed()
		{
			Execute(() => _test.Sut.ManualCoverAttachment);
		}
		private void MembersIsAccessed()
		{
			Execute(() => _test.Sut.Members);
		}
		private void MembersIsEnumerated()
		{
			Execute(() => _test.Sut.Members.ToList());
		}
		private void NameIsAccessed()
		{
			Execute(() => _test.Sut.Name);
		}
		private void NameIsSet(string value)
		{
			Execute(() => _test.Sut.Name = value);
		}
		private void PositionIsAccessed()
		{
			Execute(() => _test.Sut.Position);
		}
		private void PositionIsSet(Position value)
		{
			Execute(() => _test.Sut.Position = value);
		}
		private void ShortIdIsAccessed()
		{
			Execute(() => _test.Sut.ShortId);
		}
		private void UrlIsAccessed()
		{
			Execute(() => _test.Sut.Url);
		}
		private void VotingMembersIsAccessed()
		{
			Execute(() => _test.Sut.VotingMembers);
		}
		private void VotingMembersIsEnumerated()
		{
			Execute(() => _test.Sut.VotingMembers.ToList());
		}
		private void AddAttachmentIsCalled(string name, string url)
		{
			SetupRepositoryDownload<Attachment>();
			Execute(() => _test.Sut.AddAttachment(name, url));
		}
		private void AddCheckListIsCalled(string value)
		{
			SetupRepositoryDownload<CheckList>();
			Execute(() => _test.Sut.AddCheckList(value));
		}
		private void AddCommentIsCalled(string value)
		{
			Execute(() => _test.Sut.AddComment(value));
		}
		private void ApplyLabelIsCalled(LabelColor value)
		{
			Execute(() => _test.Sut.ApplyLabel(value));
		}
		private void AssignMemberIsCalled(Member value)
		{
			Execute(() => _test.Sut.AssignMember(value));
		}
		private void ClearNotificationsIsCalled()
		{
			Execute(() => _test.Sut.ClearNotifications());
		}
		private void DeleteIsCalled()
		{
			Execute(() => _test.Sut.Delete());
		}
		private void MoveIsCalled(Board board, List list)
		{
			Execute(() => _test.Sut.Move(board, list));
		}
		private void RemoveLabelIsCalled(LabelColor value)
		{
			Execute(() => _test.Sut.RemoveLabel(value));
		}
		private void RemoveMemberIsCalled(Member value)
		{
			Execute(() => _test.Sut.RemoveMember(value));
		}

		#endregion
	}
}
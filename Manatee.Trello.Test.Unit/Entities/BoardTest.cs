using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class BoardTest : EntityTestBase<Board, IJsonBoard>
	{
		[TestMethod]
		public void Actions()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Actions property")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(ActionsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ArchivedCards()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access ArchivedCards property")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(ArchivedCardsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ArchivedLists()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access ArchivedLists property")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(ArchivedListsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Description()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Description property when not expired")
				.Given(ABoard)
				.When(DescriptionIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Description property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(DescriptionIsAccessed)
				.Then(RepositoryRefreshIsCalled<Board>, EntityRequestType.Board_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property")
				.Given(ABoard)
				.When(DescriptionIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Board_Write_Description)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property to same")
				.Given(ABoard)
				.And(DescriptionIs, "description")
				.When(DescriptionIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void InvitedMembers()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access InvitedMembers property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(InvitedMembersIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void IsClosed()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access IsClosed property when not expired")
				.Given(ABoard)
				.When(IsClosedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsClosed property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(IsClosedIsAccessed)
				.Then(RepositoryRefreshIsCalled<Board>, EntityRequestType.Board_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property")
				.Given(ABoard)
				.When(IsClosedIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Board_Write_IsClosed)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property to same")
				.Given(ABoard)
				.And(IsClosedIs, (bool?)true)
				.When(IsClosedIsSet, (bool?) true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void IsPinned()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access IsPinned property when not expired")
				.Given(ABoard)
				.When(IsPinnedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsPinned property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(IsPinnedIsAccessed)
				.Then(RepositoryRefreshIsCalled<Board>, EntityRequestType.Board_Read_Refresh)
				.And(ExceptionIsNotThrown)

				//.WithScenario("Set IsPinned property")
				//.Given(ABoard)
				//.When(IsPinnedIsSet, (bool?)true)
				//.Then(ValidatorNullableIsCalled<bool>)
				//.And(ValidatorWritableIsCalled)
				//.And(RepositoryUploadIsCalled, EntityRequestType.Board_Write_IsPinned)
				//.And(ExceptionIsNotThrown)

				//.WithScenario("Set IsPinned property to same")
				//.Given(ABoard)
				//.And(IsPinnedIs, (bool?)true)
				//.When(IsPinnedIsSet, (bool?) true)
				//.Then(ValidatorNullableIsCalled<bool>)
				//.And(ValidatorWritableIsCalled)
				//.And(RepositoryUploadIsNotCalled)
				//.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void IsSubscribed()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access IsSubscribed property when not expired")
				.Given(ABoard)
				.When(IsSubscribedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsSubscribed property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(IsSubscribedIsAccessed)
				.Then(RepositoryRefreshIsCalled<Board>, EntityRequestType.Board_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property")
				.Given(ABoard)
				.When(IsSubscribedIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Board_Write_IsSubscribed)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property to same")
				.Given(ABoard)
				.And(IsSubscribedIs, (bool?)true)
				.When(IsSubscribedIsSet, (bool?) true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void LabelNames()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access IsSubscribed property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(LabelNamesIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Lists()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Lists property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(ListsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Memberships()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Memberships property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(MembershipsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Name()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Name property when not expired")
				.Given(ABoard)
				.When(NameIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Name property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(NameIsAccessed)
				.Then(RepositoryRefreshIsCalled<Board>, EntityRequestType.Board_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property")
				.Given(ABoard)
				.When(NameIsSet, "name")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Board_Write_Name)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to same")
				.Given(ABoard)
				.And(NameIs, "description")
				.When(NameIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Organization()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Organization property when not expired")
				.Given(ABoard)
				.When(OrganizationIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Organization property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(OrganizationIsAccessed)
				.Then(RepositoryRefreshIsCalled<Board>, EntityRequestType.Board_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Organization property")
				.Given(ABoard)
				.When(OrganizationIsSet, new Organization {Id = TrelloIds.Invalid})
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Organization>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Board_Write_Organization)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Organization property to same")
				.Given(ABoard)
				.And(OrganizationIs, TrelloIds.Invalid)
				.When(OrganizationIsSet, new Organization {Id = TrelloIds.Invalid})
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Organization>)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void PersonalPreferences()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access PersonalPreferences property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(PersonalPreferencesIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Preferences()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Preferences property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(PreferencesIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Url()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Url property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(UrlIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AddList()
		{
			var feature = CreateFeature();

			feature.WithScenario("AddList is called")
				.Given(ABoard)
				.When(AddListIsCalled, "list")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryDownloadIsCalled<List>, EntityRequestType.Board_Write_AddList)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AddOrUpdateMemberByMember()
		{
			var feature = CreateFeature();

			feature.WithScenario("AddOrUpdateMember is called")
				.Given(ABoard)
				.When(AddOrUpdateMemberIsCalled, new Member {Id = TrelloIds.Invalid})
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Member>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Board_Write_AddOrUpdateMember)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		//[TestMethod]
		//public void AddOrUpdateMemberByEmailAndName()
		//{
		//	var story = new Story("AddOrUpdateMember by email and name");

		//	var feature = story.InOrderTo("add a member to or update permissions for a member on a board")
		//		.AsA("developer")
		//		.IWant("to add or update a member");

		//	feature.WithScenario("AddOrUpdateMember is called")
		//		.Given(ABoard)
		//		.When(AddOrUpdateMemberIsCalled, "some@email.com", "Some Email")
		//		.Then(MockSvcSearchMembersIsCalled, "some@email.com", 1)
		//		.And(MockApiPutIsCalled<IJsonMember>, 1)
		//		.And(ExceptionIsNotThrown)

		//		.Execute();
		//}
		[TestMethod]
		public void MarkAsViewed()
		{
			var feature = CreateFeature();

			feature.WithScenario("MarkAsViewed is called")
				.Given(ABoard)
				.When(MarkAsViewedIsCalled)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Board_Write_MarkAsViewed)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		//[TestMethod]
		//[Ignore]
		//public void InviteMember()
		//{
		//	var feature = CreateFeature();

		//	feature.WithScenario("InviteMember is called")
		//		.Given(ABoard)
		//		.When(InviteMemberIsCalled)
		//		.Then(MockApiPutIsCalled<IJsonBoard>, 1)
		//		.And(ExceptionIsNotThrown)

		//		.Execute();
		//}
		[TestMethod]
		public void RemoveMember()
		{
			var feature = CreateFeature();

			feature.WithScenario("RemoveMember is called")
				.Given(ABoard)
				.When(RemoveMemberIsCalled, new Member {Id = TrelloIds.Invalid})
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Member>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Board_Write_RemoveMember)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		//[TestMethod]
		//[Ignore]
		//public void RescindInvitation()
		//{
		//	var feature = CreateFeature();

		//	feature.WithScenario("RescindInvitation is called")
		//		.Given(ABoard)
		//		.When(RescindInvitationIsCalled)
		//		.Then(MockApiDeleteIsCalled<IJsonBoard>, 1)
		//		.And(ExceptionIsNotThrown)

		//		.Execute();
		//}

		#region Given

		private void ABoard()
		{
			_test = new EntityUnderTest();
		}
		private void DescriptionIs(string value)
		{
			_test.Json.SetupGet(j => j.Desc)
							.Returns(value);
		}
		private void IsClosedIs(bool? value)
		{
			_test.Json.SetupGet(j => j.Closed)
							.Returns(value);
		}
		//private void IsPinnedIs(bool? value)
		//{
		//	_systemUnderTest.Json.SetupGet(j => j.Pinned)
		//					.Returns(value);
		//}
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
		private void OrganizationIs(string value)
		{
			_test.Json.SetupGet(j => j.IdOrganization)
							.Returns(value);
		}

		#endregion

		#region When

		private void ActionsIsAccessed()
		{
			Execute(() => _test.Sut.Actions);
		}
		private void ArchivedCardsIsAccessed()
		{
			Execute(() => _test.Sut.ArchivedCards);
		}
		private void ArchivedListsIsAccessed()
		{
			Execute(() => _test.Sut.ArchivedLists);
		}
		private void DescriptionIsAccessed()
		{
			Execute(() => _test.Sut.Description);
		}
		private void DescriptionIsSet(string value)
		{
			Execute(() => _test.Sut.Description = value);
		}
		private void InvitedMembersIsAccessed()
		{
			Execute(() => _test.Sut.InvitedMembers);
		}
		private void IsClosedIsAccessed()
		{
			Execute(() => _test.Sut.IsClosed);
		}
		private void IsClosedIsSet(bool? value)
		{
			Execute(() => _test.Sut.IsClosed = value);
		}
		private void IsPinnedIsAccessed()
		{
			Execute(() => _test.Sut.IsPinned);
		}
		//private void IsPinnedIsSet(bool? value)
		//{
		//	Execute(() => _systemUnderTest.Sut.IsPinned = value);
		//}
		private void IsSubscribedIsAccessed()
		{
			Execute(() => _test.Sut.IsSubscribed);
		}
		private void IsSubscribedIsSet(bool? value)
		{
			Execute(() => _test.Sut.IsSubscribed = value);
		}
		private void LabelNamesIsAccessed()
		{
			Execute(() => _test.Sut.LabelNames);
		}
		private void ListsIsAccessed()
		{
			Execute(() => _test.Sut.Lists);
		}
		private void MembershipsIsAccessed()
		{
			Execute(() => _test.Sut.Memberships);
		}
		private void NameIsAccessed()
		{
			Execute(() => _test.Sut.Name);
		}
		private void NameIsSet(string value) 
		{
			Execute(() => _test.Sut.Name = value);
		}
		private void OrganizationIsAccessed()
		{
			Execute(() => _test.Sut.Organization);
		}
		private void OrganizationIsSet(Organization value)
		{
			Execute(() => _test.Sut.Organization = value);
		}
		private void PersonalPreferencesIsAccessed()
		{
			Execute(() => _test.Sut.PersonalPreferences);
		}
		private void PreferencesIsAccessed()
		{
			Execute(() => _test.Sut.Preferences);
		}
		private void UrlIsAccessed()
		{
			Execute(() => _test.Sut.Url);
		}
		private void AddListIsCalled(string value)
		{
			SetupRepositoryDownload<List>();
			Execute(() => _test.Sut.AddList(value));
		}
		private void AddOrUpdateMemberIsCalled(Member value)
		{
			Execute(() => _test.Sut.AddOrUpdateMember(value));
		}
		//private void AddOrUpdateMemberIsCalled(string email, string name)
		//{
		//	Execute(() => _systemUnderTest.Sut.AddOrUpdateMember(email, name));
		//}
		private void MarkAsViewedIsCalled()
		{
			Execute(() => _test.Sut.MarkAsViewed());
		}
		//private void InviteMemberIsCalled()
		//{
		//	Execute(() => _systemUnderTest.Sut.InviteMember(new Member { Id = TrelloIds.Invalid }));
		//}
		private void RemoveMemberIsCalled(Member value)
		{
			Execute(() => _test.Sut.RemoveMember(value));
		}
		//private void RescindInvitationIsCalled()
		//{
		//	Execute(() => _systemUnderTest.Sut.RescindInvitation(new Member { Id = TrelloIds.Invalid }));
		//}

		#endregion
	}
}

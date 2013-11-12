using Manatee.Trello.Internal;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class MemberTest : EntityTestBase<Member, IJsonMember>
	{
		[TestMethod]
		public void Actions()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Actions property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(ActionsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(RepositoryRefreshCollectionIsNotCalled<Action>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Actions collection enumerates")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(ActionsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Action>, EntityRequestType.Member_Read_Actions)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AvatarHash()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access AvatarHash property when not expired")
				.Given(AMember)
				.When(AvatarHashIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access AvatarHash property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(AvatarHashIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AvatarSource()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access AvatarSource property when not expired")
				.Given(AMember)
				.When(AvatarSourceIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access AvatarSource property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(AvatarSourceIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AvatarSource property")
				.Given(AMember)
				.When(AvatarSourceIsSet, AvatarSourceType.Gravatar)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEnumerationIsCalled<AvatarSourceType>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Member_Write_AvatarSource)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AvatarSource property to same")
				.Given(AMember)
				.And(AvatarSourceIs, AvatarSourceType.Gravatar)
				.When(AvatarSourceIsSet, AvatarSourceType.Gravatar)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Bio()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Bio property when not expired")
				.Given(AMember)
				.When(BioIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Bio property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(BioIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Bio property")
				.Given(AMember)
				.When(BioIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Member_Write_Bio)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Bio property to same")
				.Given(AMember)
				.And(BioIs, "description")
				.When(BioIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Boards()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Boards property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(BoardsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(RepositoryRefreshCollectionIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Boards collection enumerates")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(BoardsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Board>, EntityRequestType.Member_Read_Boards)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Confirmed()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Confirmed property when not expired")
				.Given(AMember)
				.When(ConfirmedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Confirmed property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(ConfirmedIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Email()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Email property when not expired")
				.Given(AMember)
				.When(EmailIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Email property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(EmailIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void FullName()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access FullName property when not expired")
				.Given(AMember)
				.When(FullNameIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access FullName property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(FullNameIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set FullName property")
				.Given(AMember)
				.When(FullNameIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorMinStringLengthIsCalled, 4)
				.And(RepositoryUploadIsCalled, EntityRequestType.Member_Write_FullName)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set FullName property to same")
				.Given(AMember)
				.And(FullNameIs, "description")
				.When(FullNameIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorMinStringLengthIsNotCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void GravatarHash()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access GravatarHash property when not expired")
				.Given(AMember)
				.When(GravatarHashIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access GravatarHash property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(GravatarHashIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Initials()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Initials property when not expired")
				.Given(AMember)
				.When(InitialsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Initials property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(InitialsIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Initials property")
				.Given(AMember)
				.When(InitialsIsSet, "mt")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorStringLengthRangeIsCalled, 1, 3)
				.And(RepositoryUploadIsCalled, EntityRequestType.Member_Write_Initials)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Initials property to same")
				.Given(AMember)
				.And(InitialsIs, "mt")
				.When(InitialsIsSet, "mt")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorStringLengthRangeIsNotCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void InvitedBoards()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access InvitedBoards property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(InvitedBoardsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(RepositoryRefreshCollectionIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("InvitedBoards collection enumerates")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(InvitedBoardsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Board>, EntityRequestType.Member_Read_InvitedBoards)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void InvitedOrganizations()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access InvitedOrganizations property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(InvitedOrganizationsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(RepositoryRefreshCollectionIsNotCalled<Organization>)
				.And(ExceptionIsNotThrown)

				.WithScenario("InvitedOrganizations collection enumerates")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(InvitedOrganizationsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Organization>, EntityRequestType.Member_Read_InvitedOrganizations)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void LoginTypes()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access LoginTypes property when not expired")
				.Given(AMember)
				.When(LoginTypesIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access LoginTypes property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(LoginTypesIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void MemberType()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access MemberType property when not expired")
				.Given(AMember)
				.When(MemberTypeIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access MemberType property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(MemberTypeIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Notifications()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Notifications property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(NotificationsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(RepositoryRefreshCollectionIsNotCalled<Notification>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Notifications collection enumerates")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(NotificationsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Notification>, EntityRequestType.Member_Read_Notifications)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Organizations()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Organizations property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(OrganizationsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(RepositoryRefreshCollectionIsNotCalled<Organization>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Organizations collection enumerates")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(OrganizationsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Organization>, EntityRequestType.Member_Read_Organizations)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void PinnedBoards()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access PinnedBoards property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(PinnedBoardsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Preferences()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Preferences property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(PreferencesIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Status()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Status property when not expired")
				.Given(AMember)
				.When(StatusIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Status property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(StatusIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Trophies()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Trophies property when not expired")
				.Given(AMember)
				.When(TrophiesIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Trophies property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(TrophiesIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void UploadedAvatarHash()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access UploadedAvatarHash property when not expired")
				.Given(AMember)
				.When(UploadedAvatarHashIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access UploadedAvatarHash property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(UploadedAvatarHashIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Url()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Url property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(UrlIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Username()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Username property when not expired")
				.Given(AMember)
				.When(UsernameIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Username property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(UsernameIsAccessed)
				.Then(RepositoryRefreshIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Username property")
				.Given(AMember)
				.When(UsernameIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorUserNameIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Member_Write_Username)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Username property to same")
				.Given(AMember)
				.And(UsernameIs, "description")
				.When(UsernameIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorUserNameIsNotCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ClearNotifications()
		{
			var feature = CreateFeature();

			feature.WithScenario("ClearNotifications is called")
				.Given(AMember)
				.When(ClearNotificationsIsCalled)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Member_Write_ClearNotifications)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void CreateBoard()
		{
			var feature = CreateFeature();

			feature.WithScenario("CreateBoard is called")
				.Given(AMember)
				.When(CreateBoardIsCalled, "org name")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryDownloadIsCalled<Board>, EntityRequestType.Member_Write_CreateBoard)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void CreateOrganization()
		{
			var feature = CreateFeature();

			feature.WithScenario("CreateOrganization is called")
				.Given(AMember)
				.When(CreateOrganizationIsCalled, "org name")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryDownloadIsCalled<Organization>, EntityRequestType.Member_Write_CreateOrganization)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void PinBoard()
		{
			var feature = CreateFeature();

			feature.WithScenario("PinBoard is called")
				.Given(AMember)
				.When(PinBoardIsCalled, new Board {Id = TrelloIds.Test})
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Board>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Member_Write_PinBoard)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void RescindVoteForCard()
		{
			var feature = CreateFeature();

			feature.WithScenario("RescindVoteForCard is called")
				.Given(AMember)
				.When(RescindVoteForCardIsCalled, new Card {Id = TrelloIds.Test})
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Card>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Member_Write_RescindVoteForCard)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void UnpinBoard()
		{
			var feature = CreateFeature();

			feature.WithScenario("UnpinBoard is called")
				.Given(AMember)
				.When(UnpinBoardIsCalled, new Board {Id = TrelloIds.Test})
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Board>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Member_Write_UnpinBoard)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void VoteForCard()
		{
			var feature = CreateFeature();

			feature.WithScenario("VoteForCard is called")
				.Given(AMember)
				.When(VoteForCardIsCalled, new Card {Id = TrelloIds.Test})
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Card>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Member_Write_VoteForCard)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void AMember()
		{
			_test = new EntityUnderTest();
		}
		private void AvatarSourceIs(AvatarSourceType value)
		{
			_test.Json.SetupGet(j => j.AvatarSource)
				 .Returns(value.ToLowerString());
			ReapplyJson();
		}
		private void BioIs(string value)
		{
			_test.Json.SetupGet(j => j.Bio)
				 .Returns(value.ToLowerString());
		}
		private void FullNameIs(string value)
		{
			_test.Json.SetupGet(j => j.FullName)
				 .Returns(value.ToLowerString());
		}
		private void InitialsIs(string value)
		{
			_test.Json.SetupGet(j => j.Initials)
				 .Returns(value.ToLowerString());
		}
		private void UsernameIs(string value)
		{
			_test.Json.SetupGet(j => j.Username)
				 .Returns(value.ToLowerString());
		}

		#endregion

		#region When

		private void ActionsIsAccessed()
		{
			Execute(() => _test.Sut.Actions);
		}
		private void ActionsIsEnumerated()
		{
			Execute(() => _test.Sut.Actions.GetEnumerator());
		}
		private void AvatarHashIsAccessed()
		{
			Execute(() => _test.Sut.AvatarHash);
		}
		private void AvatarSourceIsAccessed()
		{
			Execute(() => _test.Sut.AvatarSource);
		}
		private void AvatarSourceIsSet(AvatarSourceType value)
		{
			Execute(() => _test.Sut.AvatarSource = value);
		}
		private void BioIsAccessed()
		{
			Execute(() => _test.Sut.Bio);
		}
		private void BioIsSet(string value)
		{
			Execute(() => _test.Sut.Bio = value);
		}
		private void BoardsIsAccessed()
		{
			Execute(() => _test.Sut.Boards);
		}
		private void BoardsIsEnumerated()
		{
			Execute(() => _test.Sut.Boards.GetEnumerator());
		}
		private void ConfirmedIsAccessed()
		{
			Execute(() => _test.Sut.Confirmed);
		}
		private void EmailIsAccessed()
		{
			Execute(() => _test.Sut.Email);
		}
		private void FullNameIsAccessed()
		{
			Execute(() => _test.Sut.FullName);
		}
		private void FullNameIsSet(string value)
		{
			Execute(() => _test.Sut.FullName = value);
		}
		private void GravatarHashIsAccessed()
		{
			Execute(() => _test.Sut.GravatarHash);
		}
		private void InitialsIsAccessed()
		{
			Execute(() => _test.Sut.Initials);
		}
		private void InitialsIsSet(string value)
		{
			Execute(() => _test.Sut.Initials = value);
		}
		private void InvitedBoardsIsAccessed()
		{
			Execute(() => _test.Sut.InvitedBoards);
		}
		private void InvitedBoardsIsEnumerated()
		{
			Execute(() => _test.Sut.InvitedBoards.GetEnumerator());
		}
		private void InvitedOrganizationsIsAccessed()
		{
			Execute(() => _test.Sut.InvitedOrganizations);
		}
		private void InvitedOrganizationsIsEnumerated()
		{
			Execute(() => _test.Sut.InvitedOrganizations.GetEnumerator());
		}
		private void LoginTypesIsAccessed()
		{
			Execute(() => _test.Sut.LoginTypes);
		}
		private void MemberTypeIsAccessed()
		{
			Execute(() => _test.Sut.MemberType);
		}
		private void NotificationsIsAccessed()
		{
			Execute(() => _test.Sut.Notifications);
		}
		private void NotificationsIsEnumerated()
		{
			Execute(() => _test.Sut.Notifications.GetEnumerator());
		}
		private void OrganizationsIsAccessed()
		{
			Execute(() => _test.Sut.Organizations);
		}
		private void OrganizationsIsEnumerated()
		{
			Execute(() => _test.Sut.Organizations.GetEnumerator());
		}
		private void PinnedBoardsIsAccessed()
		{
			Execute(() => _test.Sut.PinnedBoards);
		}
		private void PreferencesIsAccessed()
		{
			Execute(() => _test.Sut.Preferences);
		}
		private void StatusIsAccessed()
		{
			Execute(() => _test.Sut.Status);
		}
		private void TrophiesIsAccessed()
		{
			Execute(() => _test.Sut.Trophies);
		}
		private void UploadedAvatarHashIsAccessed()
		{
			Execute(() => _test.Sut.UploadedAvatarHash);
		}
		private void UrlIsAccessed()
		{
			Execute(() => _test.Sut.Url);
		}
		private void UsernameIsAccessed()
		{
			Execute(() => _test.Sut.Username);
		}
		private void UsernameIsSet(string value)
		{
			Execute(() => _test.Sut.Username = value);
		}
		private void ClearNotificationsIsCalled()
		{
			Execute(() => _test.Sut.ClearNotifications());
		}
		private void CreateBoardIsCalled(string value)
		{
			Execute(() => _test.Sut.CreateBoard(value));
		}
		private void CreateOrganizationIsCalled(string value)
		{
			Execute(() => _test.Sut.CreateOrganization(value));
		}
		private void PinBoardIsCalled(Board value)
		{
			Execute(() => _test.Sut.PinBoard(value));
		}
		private void RescindVoteForCardIsCalled(Card value)
		{
			Execute(() => _test.Sut.RescindVoteForCard(value));
		}
		private void UnpinBoardIsCalled(Board value)
		{
			Execute(() => _test.Sut.UnpinBoard(value));
		}
		private void VoteForCardIsCalled(Card value)
		{
			Execute(() => _test.Sut.VoteForCard(value));
		}

		#endregion
	}
}
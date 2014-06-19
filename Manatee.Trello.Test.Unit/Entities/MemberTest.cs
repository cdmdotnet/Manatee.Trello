using System;
using System.Linq;
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

				.Execute();
		}

		#region Given

		private void AMember()
		{
			_test = new EntityUnderTest();
			_test.Dependencies.SetupListGeneration<Action>();
			_test.Dependencies.SetupListGeneration<Board>();
			_test.Dependencies.SetupListGeneration<Notification>();
			_test.Dependencies.SetupListGeneration<Organization>();
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
			Execute(() => _test.Sut.Actions.ToList());
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
		private void BoardsIsAccessed()
		{
			Execute(() => _test.Sut.Boards);
		}
		private void BoardsIsEnumerated()
		{
			Execute(() => _test.Sut.Boards.ToList());
		}
		private void ConfirmedIsAccessed()
		{
			Execute(() => _test.Sut.Confirmed);
		}
		private void FullNameIsAccessed()
		{
			Execute(() => _test.Sut.FullName);
		}
		private void GravatarHashIsAccessed()
		{
			Execute(() => _test.Sut.GravatarHash);
		}
		private void InitialsIsAccessed()
		{
			Execute(() => _test.Sut.Initials);
		}
		private void InvitedBoardsIsAccessed()
		{
			Execute(() => _test.Sut.InvitedBoards);
		}
		private void InvitedBoardsIsEnumerated()
		{
			Execute(() => _test.Sut.InvitedBoards.ToList());
		}
		private void InvitedOrganizationsIsAccessed()
		{
			Execute(() => _test.Sut.InvitedOrganizations);
		}
		private void InvitedOrganizationsIsEnumerated()
		{
			Execute(() => _test.Sut.InvitedOrganizations.ToList());
		}
		private void LoginTypesIsAccessed()
		{
			Execute(() => _test.Sut.LoginTypes);
		}
		private void MemberTypeIsAccessed()
		{
			Execute(() => _test.Sut.MemberType);
		}
		private void OrganizationsIsAccessed()
		{
			Execute(() => _test.Sut.Organizations);
		}
		private void OrganizationsIsEnumerated()
		{
			Execute(() => _test.Sut.Organizations.ToList());
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

		#endregion
	}
}
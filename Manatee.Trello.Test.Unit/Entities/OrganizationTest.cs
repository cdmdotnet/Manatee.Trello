using System.Linq;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class OrganizationTest : EntityTestBase<Organization, IJsonOrganization>
	{
		[TestMethod]
		public void Actions()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Actions property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(ActionsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(RepositoryRefreshCollectionIsNotCalled<Action>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Actions collection enumerates")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(ActionsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Action>, EntityRequestType.Organization_Read_Actions)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Boards()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Boards property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(BoardsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(RepositoryRefreshCollectionIsNotCalled<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Boards collection enumerates")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(BoardsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Board>, EntityRequestType.Organization_Read_Boards)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Description()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Description property when not expired")
				.Given(AnOrganization)
				.When(DescriptionIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Description property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(DescriptionIsAccessed)
				.Then(RepositoryRefreshIsCalled<Organization>, EntityRequestType.Organization_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property")
				.Given(AnOrganization)
				.When(DescriptionIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Organization_Write_Description)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property to same")
				.Given(AnOrganization)
				.And(DescriptionIs, "description")
				.When(DescriptionIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void DisplayName()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access DisplayName property when not expired")
				.Given(AnOrganization)
				.When(DisplayNameIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access DisplayName property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(DisplayNameIsAccessed)
				.Then(RepositoryRefreshIsCalled<Organization>, EntityRequestType.Organization_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set DisplayName property")
				.Given(AnOrganization)
				.When(DisplayNameIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorMinStringLengthIsCalled, 4)
				.And(RepositoryUploadIsCalled, EntityRequestType.Organization_Write_DisplayName)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set DisplayName property to same")
				.Given(AnOrganization)
				.And(DisplayNameIs, "description")
				.When(DisplayNameIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorMinStringLengthIsNotCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void InvitedMembers()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access InvitedMembers property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(InvitedMembersIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(RepositoryRefreshCollectionIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("InvitedMembers collection enumerates")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(InvitedMembersIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Member>, EntityRequestType.Organization_Read_InvitedMembers)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void IsPaidAccount()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access IsPaidAccount property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(IsPaidAccountIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void LogoHash()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access AvatarHash property when not expired")
				.Given(AnOrganization)
				.When(LogoHashIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access AvatarHash property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(LogoHashIsAccessed)
				.Then(RepositoryRefreshIsCalled<Organization>, EntityRequestType.Organization_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Memberships()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Memberships property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(MembershipsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(RepositoryRefreshCollectionIsNotCalled<OrganizationMembership>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Memberships collection enumerates")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(MembershipsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<OrganizationMembership>, EntityRequestType.Organization_Read_Memberships)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Members()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Members property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(MembersIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(RepositoryRefreshCollectionIsNotCalled<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Members collection enumerates")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(MembersIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<Member>, EntityRequestType.Organization_Read_Members)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Name()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Name property when not expired")
				.Given(AnOrganization)
				.When(NameIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Name property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(NameIsAccessed)
				.Then(RepositoryRefreshIsCalled<Organization>, EntityRequestType.Organization_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property")
				.Given(AnOrganization)
				.When(NameIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorOrgNameIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Organization_Write_Name)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to same")
				.Given(AnOrganization)
				.And(NameIs, "description")
				.When(NameIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorOrgNameIsNotCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void PowerUps()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access PowerUps property when not expired")
				.Given(AnOrganization)
				.When(PowerUpsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access PowerUps property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(PowerUpsIsAccessed)
				.Then(RepositoryRefreshIsCalled<Organization>, EntityRequestType.Organization_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Preferences()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Preferences property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(PreferencesIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void PremiumFeatures()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access PremiumFeatures property when not expired")
				.Given(AnOrganization)
				.When(PremiumFeaturesIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access PremiumFeatures property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(PremiumFeaturesIsAccessed)
				.Then(RepositoryRefreshIsCalled<Organization>, EntityRequestType.Organization_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Website()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Website property when not expired")
				.Given(AnOrganization)
				.When(WebsiteIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Website property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(WebsiteIsAccessed)
				.Then(RepositoryRefreshIsCalled<Organization>, EntityRequestType.Organization_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Website property")
				.Given(AnOrganization)
				.When(WebsiteIsSet, "Website")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorUrlIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Organization_Write_Website)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Website property to same")
				.Given(AnOrganization)
				.And(WebsiteIs, "Website")
				.When(WebsiteIsSet, "Website")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorUrlIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Url()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Url property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(UrlIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Organization>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AddOrUpdateMemberByMember()
		{
			var feature = CreateFeature();

			feature.WithScenario("AddOrUpdateMember is called")
				.Given(AnOrganization)
				.When(AddOrUpdateMemberIsCalled, new Member { Id = TrelloIds.Test })
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Member>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Organization_Write_AddOrUpdateMember)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		//[TestMethod]
		//public void AddOrUpdateMemberByEmailAndName()
		//{
		//	var feature = CreateFeature();

		//	feature.WithScenario("AddOrUpdateMember is called")
		//		.Given(AnOrganization)
		//		.When(AddOrUpdateMemberIsCalled, "some@email.com", "Some Email")
		//		.Then(ValidatorWritableIsCalled)
		//		.And(ValidatorNonEmptyStringIsCalled)
		//		.And(RepositoryUploadIsCalled, EntityRequestType.Organization_Write_AddOrUpdateMember)
		//		.And(ExceptionIsNotThrown)

		//		.Execute();
		//}
		[TestMethod]
		public void CreateBoard()
		{
			var feature = CreateFeature();

			feature.WithScenario("CreateBoard is called")
				.Given(AnOrganization)
				.When(CreateBoardIsCalled, "org name")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryDownloadIsCalled<Board>, EntityRequestType.Organization_Write_CreateBoard)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var feature = CreateFeature();

			feature.WithScenario("Delete is called")
				.Given(AnOrganization)
				.When(DeleteIsCalled)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Organization_Write_Delete)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called when already deleted")
				.Given(AnOrganization)
				.And(AlreadyDeleted)
				.When(DeleteIsCalled)
				.Then(ValidatorWritableIsNotCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void RemoveMember()
		{
			var feature = CreateFeature();

			feature.WithScenario("RemoveMember is called")
				.Given(AnOrganization)
				.When(RemoveMemberIsCalled, new Member {Id = TrelloIds.Test})
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Member>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Organization_Write_RemoveMember)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void AnOrganization()
		{
			_test = new EntityUnderTest();
			_test.Dependencies.SetupListGeneration<Action>();
			_test.Dependencies.SetupListGeneration<Board>();
			_test.Dependencies.SetupListGeneration<Member>();
			_test.Dependencies.SetupListGeneration<OrganizationMembership>();
			_test.Json.Setup(j => j.Url).Returns("http://trello.com/organization-test");
		}
		private void DescriptionIs(string value)
		{
			_test.Json.SetupGet(j => j.Desc)
				 .Returns(value);
		}
		private void DisplayNameIs(string value)
		{
			_test.Json.SetupGet(j => j.DisplayName)
				 .Returns(value);
		}
		private void NameIs(string value)
		{
			_test.Json.SetupGet(j => j.Name)
				 .Returns(value);
		}
		private void WebsiteIs(string value)
		{
			_test.Json.SetupGet(j => j.Website)
				 .Returns(value);
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
		private void BoardsIsAccessed()
		{
			Execute(() => _test.Sut.Boards);
		}
		private void BoardsIsEnumerated()
		{
			Execute(() => _test.Sut.Boards.ToList());
		}
		private void DescriptionIsAccessed()
		{
			Execute(() => _test.Sut.Description);
		}
		private void DescriptionIsSet(string value)
		{
			Execute(() => _test.Sut.Description = value);
		}
		private void DisplayNameIsAccessed()
		{
			Execute(() => _test.Sut.DisplayName);
		}
		private void DisplayNameIsSet(string value)
		{
			Execute(() => _test.Sut.DisplayName = value);
		}
		private void InvitedMembersIsAccessed()
		{
			Execute(() => _test.Sut.InvitedMembers);
		}
		private void InvitedMembersIsEnumerated()
		{
			Execute(() => _test.Sut.InvitedMembers.ToList());
		}
		private void IsPaidAccountIsAccessed()
		{
			Execute(() => _test.Sut.IsPaidAccount);
		}
		private void LogoHashIsAccessed()
		{
			Execute(() => _test.Sut.LogoHash);
		}
		private void MembershipsIsAccessed()
		{
			Execute(() => _test.Sut.Memberships);
		}
		private void MembershipsIsEnumerated()
		{
			Execute(() => _test.Sut.Memberships.ToList());
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
		private void PowerUpsIsAccessed()
		{
			Execute(() => _test.Sut.PowerUps);
		}
		private void PreferencesIsAccessed()
		{
			Execute(() => _test.Sut.Preferences);
		}
		private void PremiumFeaturesIsAccessed()
		{
			Execute(() => _test.Sut.PremiumFeatures);
		}
		private void UrlIsAccessed()
		{
			Execute(() => _test.Sut.Url);
		}
		private void WebsiteIsAccessed()
		{
			Execute(() => _test.Sut.Website);
		}
		private void WebsiteIsSet(string value)
		{
			Execute(() => _test.Sut.Website = value);
		}
		private void AddOrUpdateMemberIsCalled(Member member)
		{
			Execute(() => _test.Sut.AddOrUpdateMember(member));
		}
		//private void AddOrUpdateMemberIsCalled(string email, string name)
		//{
		//	Execute(() => _test.Sut.AddOrUpdateMember(email, name));
		//}
		private void CreateBoardIsCalled(string name)
		{
			Execute(() => _test.Sut.CreateBoard(name));
		}
		private void DeleteIsCalled()
		{
			Execute(() => _test.Sut.Delete());
		}
		private void RemoveMemberIsCalled(Member member)
		{
			Execute(() => _test.Sut.RemoveMember(member));
		}

		#endregion
	}
}
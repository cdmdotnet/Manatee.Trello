using System;
using System.Collections.Generic;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class OrganizationTest : EntityTestBase<Organization>
	{
		[TestMethod]
		public void Actions()
		{
			var story = new Story("Actions");

			var feature = story.InOrderTo("get all actions for an organization")
				.AsA("developer")
				.IWant("to get Actions");

			feature.WithScenario("Access Actions property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(ActionsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonAction>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Action>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Boards()
		{
			var story = new Story("Boards");

			var feature = story.InOrderTo("get the boards owned by an organization")
				.AsA("developer")
				.IWant("to get Boards");

			feature.WithScenario("Access Boards property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(BoardsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonBoard>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Board>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Description()
		{
			var story = new Story("Description");

			var feature = story.InOrderTo("control an organization's description")
				.AsA("developer")
				.IWant("to get and set the Description");

			feature.WithScenario("Access Description property")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.When(DescriptionIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Description property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(DescriptionIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.When(DescriptionIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property to null")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.And(DescriptionIs, "not description")
				.When(DescriptionIsSet, (string) null)
				.Then(MockApiPutIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property to empty")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.And(DescriptionIs, "not description")
				.When(DescriptionIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property to same")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.And(DescriptionIs, "description")
				.When(DescriptionIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property wihtout UserToken")
				.Given(AnOrganization)
				.And(TokenNotSupplied)
				.When(DescriptionIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void DisplayName()
		{
			var story = new Story("DisplayName");

			var feature = story.InOrderTo("control an organization's display name")
				.AsA("developer")
				.IWant("to get and set the DisplayName");

			feature.WithScenario("Access DisplayName property")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.When(DisplayNameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access DisplayName property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(DisplayNameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set DisplayName property")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.When(DisplayNameIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set DisplayName property to null")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.And(DisplayNameIs, "not description")
				.When(DisplayNameIsSet, (string) null)
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set DisplayName property to less than 4 characters")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.When(DisplayNameIsSet, "bad")
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ArgumentException>)

				.WithScenario("Set DisplayName property to same")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.And(DisplayNameIs, "description")
				.When(DisplayNameIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set DisplayName property wihtout UserToken")
				.Given(AnOrganization)
				.And(TokenNotSupplied)
				.When(DisplayNameIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void InvitedMembers()
		{
			var story = new Story("InvitedMembers");

			var feature = story.InOrderTo("get all members invited an organization")
				.AsA("developer")
				.IWant("to get InvitedMembers");

			feature.WithScenario("Access InvitedMembers property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(InvitedMembersIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonMember>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Member>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void IsPaidAccount()
		{
			var story = new Story("IsPaidAccount");

			var feature = story.InOrderTo("get whether an organization has paid features")
				.AsA("developer")
				.IWant("to get IsPaidAccount");

			feature.WithScenario("Access IsPaidAccount property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(IsPaidAccountIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void LogoHash()
		{
			var story = new Story("AvatarHash");

			var feature = story.InOrderTo("get a organization's logo hash")
				.AsA("developer")
				.IWant("to get the AvatarHash");

			feature.WithScenario("Access AvatarHash property")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.When(LogoHashIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access AvatarHash property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(LogoHashIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Memberships()
		{
			var story = new Story("MembershipsIsAccessed");

			var feature = story.InOrderTo("get all memberships of a organization")
				.AsA("developer")
				.IWant("to get MembershipsIsAccessed");

			feature.WithScenario("Access Memberships property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(MembershipsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonOrganizationMembership>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<OrganizationMembership>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Name()
		{
			var story = new Story("Name");

			var feature = story.InOrderTo("control an organiaations's name")
				.AsA("developer")
				.IWant("to get and set the Name");

			feature.WithScenario("Access Name property")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Name property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.When(NameIsSet, "description")
				.Then(MockApiGetIsCalled<IJsonSearchResults>, 1)
				.And(MockApiPutIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to null")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.And(NameIs, "not_description")
				.When(NameIsSet, (string) null)
				.Then(MockApiGetIsCalled<IJsonSearchResults>, 0)
				.And(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Name property to less than 3 characters")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.When(NameIsSet, "un")
				.Then(MockApiGetIsCalled<IJsonSearchResults>, 0)
				.And(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ArgumentException>)

				.WithScenario("Set Name property to same")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.And(NameIs, "description")
				.When(NameIsSet, "description")
				.Then(MockApiGetIsCalled<IJsonSearchResults>, 0)
				.And(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to existing name")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.And(NameExists)
				.When(NameIsSet, "name")
				.Then(MockApiGetIsCalled<IJsonSearchResults>, 1)
				.And(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<OrgNameInUseException>)

				.WithScenario("Set Name property without UserToken")
				.Given(AnOrganization)
				.And(TokenNotSupplied)
				.When(NameIsSet, "description")
				.Then(MockApiGetIsCalled<IJsonSearchResults>, 0)
				.And(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void PowerUps()
		{
			var story = new Story("PowerUps");

			var feature = story.InOrderTo("get an organization's PowerUps")
				.AsA("developer")
				.IWant("to get the PowerUps");

			feature.WithScenario("Access PowerUps property")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.When(PowerUpsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access PowerUps property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(PowerUpsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Preferences()
		{
			var story = new Story("Preferences");

			var feature = story.InOrderTo("get the preferences for a organization")
				.AsA("developer")
				.IWant("to get Preferences");

			feature.WithScenario("Access Preferences property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(PreferencesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganizationPreferences>, 0)
				.And(NonNullValueOfTypeIsReturned<OrganizationPreferences>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void PremiumFeatures()
		{
			var story = new Story("PremiumFeatures");

			var feature = story.InOrderTo("get an organization's Premium Features")
				.AsA("developer")
				.IWant("to get the PremiumFeatures");

			feature.WithScenario("Access PremiumFeatures property")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.When(PremiumFeaturesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access PremiumFeatures property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(PremiumFeaturesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Website()
		{
			var story = new Story("Website");

			var feature = story.InOrderTo("control an organization's website link")
				.AsA("developer")
				.IWant("to get and set the Website");

			feature.WithScenario("Access Website property")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.When(WebsiteIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Website property when expired")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(WebsiteIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Website property")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.When(WebsiteIsSet, "Website")
				.Then(MockApiPutIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Website property to null")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.And(WebsiteIs, "not Website")
				.When(WebsiteIsSet, (string) null)
				.Then(MockApiPutIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Website property to empty")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.And(WebsiteIs, "not Website")
				.When(WebsiteIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Website property to same")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.And(WebsiteIs, "Website")
				.When(WebsiteIsSet, "Website")
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Website property wihtout UserToken")
				.Given(AnOrganization)
				.And(EntityIsRefreshed)
				.And(TokenNotSupplied)
				.When(WebsiteIsSet, "Website")
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Url()
		{
			var story = new Story("Url");

			var feature = story.InOrderTo("get an organization's URL")
				.AsA("developer")
				.IWant("to get the Url");

			feature.WithScenario("Access Url property")
				.Given(AnOrganization)
				.And(EntityIsExpired)
				.When(UrlIsAccessed)
				.Then(MockApiGetIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AddOrUpdateMember()
		{
			var story = new Story("AddOrUpdateMember");

			var feature = story.InOrderTo("add a member to or update permissions for a member on an organization")
				.AsA("developer")
				.IWant("to add or update a member");

			feature.WithScenario("AddOrUpdateMember is called")
				.Given(AnOrganization)
				.When(AddOrUpdateMemberIsCalled, new Member {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("AddOrUpdateMember is called with null")
				.Given(AnOrganization)
				.When(AddOrUpdateMemberIsCalled, (Member) null)
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddOrUpdateMember is called with local member")
				.Given(AnOrganization)
				.When(AddOrUpdateMemberIsCalled, new Member())
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Member>>)

				.WithScenario("AddOrUpdateMember is called wihtout UserToken")
				.Given(AnOrganization)
				.And(TokenNotSupplied)
				.When(AddOrUpdateMemberIsCalled, new Member { Id = TrelloIds.Invalid })
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void CreateBoard()
		{
			var story = new Story("CreateBoard");

			var feature = story.InOrderTo("create a new organization board")
				.AsA("developer")
				.IWant("to call CreateBoard");

			feature.WithScenario("CreateBoard is called")
				.Given(AnOrganization)
				.When(CreateBoardIsCalled, "org name")
				.Then(MockApiPostIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("CreateBoard is called without UserToken")
				.Given(AnOrganization)
				.And(TokenNotSupplied)
				.When(CreateBoardIsCalled, "org name")
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.WithScenario("CreateBoard is called with null name")
				.Given(AnOrganization)
				.When(CreateBoardIsCalled, (string)null)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("CreateBoard is called with empty name")
				.Given(AnOrganization)
				.When(CreateBoardIsCalled, string.Empty)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("CreateBoard is called with whitespace name")
				.Given(AnOrganization)
				.When(CreateBoardIsCalled, "     ")
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var story = new Story("Delete");

			var feature = story.InOrderTo("delete an organization")
				.AsA("developer")
				.IWant("to call Delete");

			feature.WithScenario("Delete is called")
				.Given(AnOrganization)
				.When(DeleteIsCalled)
				.Then(MockApiDeleteIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called wihtout UserToken")
				.Given(AnOrganization)
				.And(TokenNotSupplied)
				.When(DeleteIsCalled)
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		[Ignore]
		public void InviteMember()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void RemoveMember()
		{
			var story = new Story("RemoveMember");

			var feature = story.InOrderTo("remove a member from an organization")
				.AsA("developer")
				.IWant("to call RemoveMember");

			feature.WithScenario("RemoveMember is called")
				.Given(AnOrganization)
				.When(RemoveMemberIsCalled, new Member {Id = TrelloIds.Invalid})
				.Then(MockApiDeleteIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("RemoveMember is called with null member")
				.Given(AnOrganization)
				.When(RemoveMemberIsCalled, (Member) null)
				.Then(MockApiDeleteIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("RemoveMember is called with local member")
				.Given(AnOrganization)
				.When(RemoveMemberIsCalled, new Member())
				.Then(MockApiDeleteIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Member>>)

				.Execute();
		}
		[TestMethod]
		[Ignore]
		public void RescindInvitation()
		{
			throw new NotImplementedException();
		}

		#region Given

		private void AnOrganization()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Svc.Object;
			var obj = SetupMockGet<IJsonSearchResults>();
			obj.SetupGet(s => s.OrganizationIds).Returns(new List<string>());
			SetupMockGet<List<IJsonMember>>();
			SetupMockGet<IJsonOrganization>();
			SetupMockPost<IJsonBoard>();
		}
		private void DescriptionIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Description = value);
		}
		private void DisplayNameIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.DisplayName = value);
		}
		private void NameExists()
		{
			var obj = new Mock<IJsonSearchResults>();
			obj.SetupGet(s => s.OrganizationIds).Returns(new List<string> {TrelloIds.Invalid});
			_systemUnderTest.Dependencies.Rest.Setup(a => a.Get<IJsonSearchResults>(It.IsAny<IRestRequest>()))
				.Returns(obj.Object);
		}
		private void NameIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Name = value);
		}
		private void WebsiteIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Website = value);
		}

		#endregion

		#region When

		private void ActionsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Actions);
		}
		private void BoardsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Boards);
		}
		private void DescriptionIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Description);
		}
		private void DescriptionIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Description = value);
		}
		private void DisplayNameIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.DisplayName);
		}
		private void DisplayNameIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.DisplayName = value);
		}
		private void InvitedMembersIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.InvitedMembers);
		}
		private void IsPaidAccountIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.IsPaidAccount);
		}
		private void LogoHashIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.LogoHash);
		}
		private void MembershipsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Memberships);
		}
		private void NameIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Name);
		}
		private void NameIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Name = value);
		}
		private void PowerUpsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.PowerUps);
		}
		private void PreferencesIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Preferences);
		}
		private void PremiumFeaturesIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.PremiumFeatures);
		}
		private void UrlIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Url);
		}
		private void WebsiteIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Website);
		}
		private void WebsiteIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Website = value);
		}
		private void AddOrUpdateMemberIsCalled(Member member)
		{
			Execute(() => _systemUnderTest.Sut.AddOrUpdateMember(member));
		}
		private void CreateBoardIsCalled(string name)
		{
			Execute(() => _systemUnderTest.Sut.CreateBoard(name));
		}
		private void DeleteIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.Delete());
		}
		private void InviteMemberIsCalled(Member member)
		{
			Execute(() => _systemUnderTest.Sut.InviteMember(member));
		}
		private void RemoveMemberIsCalled(Member member)
		{
			Execute(() => _systemUnderTest.Sut.RemoveMember(member));
		}
		private void RescindInvitationIsCalled(Member member)
		{
			Execute(() => _systemUnderTest.Sut.RescindInvitation(member));
		}

		#endregion
	}
}
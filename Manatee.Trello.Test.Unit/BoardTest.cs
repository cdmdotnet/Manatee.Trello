using System;
using System.Collections.Generic;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.Unit
{
	[TestClass]
	public class BoardTest : EntityTestBase<Board>
	{
		[TestMethod]
		public void Actions()
		{
			var story = new Story("Actions");

			var feature = story.InOrderTo("get all actions for a board")
				.AsA("developer")
				.IWant("to get Actions");

			feature.WithScenario("Access Actions property")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(ActionsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonAction>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Action>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ArchivedCards()
		{
			var story = new Story("ArchivedCards");

			var feature = story.InOrderTo("get all archived cards for a board")
				.AsA("developer")
				.IWant("to get ArchivedCards");

			feature.WithScenario("Access ArchivedCards property")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(ArchivedCardsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 0)
				.And(MockApiGetIsCalled<List<IJsonCard>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Card>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ArchivedLists()
		{
			var story = new Story("ArchivedLists");

			var feature = story.InOrderTo("get all archived lists for a board")
				.AsA("developer")
				.IWant("to get ArchivedLists");

			feature.WithScenario("Access ArchivedLists property")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(ArchivedListsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 0)
				.And(MockApiGetIsCalled<List<IJsonList>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<List>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Description()
		{
			var story = new Story("Description");

			var feature = story.InOrderTo("control a board's description")
				.AsA("developer")
				.IWant("to get and set the Description");

			feature.WithScenario("Access Description property")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.When(DescriptionIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Description property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(DescriptionIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.When(DescriptionIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property to null")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(DescriptionIs, "not description")
				.When(DescriptionIsSet, (string) null)
				.Then(MockApiPutIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property to empty")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(DescriptionIs, "not description")
				.When(DescriptionIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property to same")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(DescriptionIs, "description")
				.When(DescriptionIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Description property without UserToken")
				.Given(ABoard)
				.And(TokenNotSupplied)
				.When(DescriptionIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void InvitedMembers()
		{
			var story = new Story("InvitedMembers");

			var feature = story.InOrderTo("get all members invited a board")
				.AsA("developer")
				.IWant("to get InvitedMembers");

			feature.WithScenario("Access InvitedMembers property")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(InvitedMembersIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonMember>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Member>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void IsClosed()
		{
			var story = new Story("IsClosed");

			var feature = story.InOrderTo("control a board is closed")
				.AsA("developer")
				.IWant("to get and set IsClosed");

			feature.WithScenario("Access IsClosed property")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.When(IsClosedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsClosed property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(IsClosedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.When(IsClosedIsSet, (bool?)true)
				.Then(MockApiPutIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property to null")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(IsClosedIs, (bool?)true)
				.When(IsClosedIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set IsClosed property to same")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(IsClosedIs, (bool?)true)
				.When(IsClosedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property without UserToken")
				.Given(ABoard)
				.And(TokenNotSupplied)
				.When(IsClosedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		[Ignore]
		public void IsPinned()
		{
			var story = new Story("IsPinned");

			var feature = story.InOrderTo("control whether the board is pinned to the current member's boards menu")
				.AsA("developer")
				.IWant("to get and set IsPinned");

			feature.WithScenario("Access IsPinned property")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.When(IsPinnedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsPinned property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(IsPinnedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsPinned property")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.When(IsPinnedIsSet, (bool?)true)
				.Then(MockApiPutIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsPinned property to null")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(IsPinnedIs, (bool?)true)
				.When(IsPinnedIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set IsPinned property to same")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(IsPinnedIs, (bool?)true)
				.When(IsPinnedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsPinned property without UserToken")
				.Given(ABoard)
				.And(TokenNotSupplied)
				.When(IsPinnedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void IsSubscribed()
		{
			var story = new Story("IsSubscribed");

			var feature = story.InOrderTo("control whether the current member is subscribed to a board")
				.AsA("developer")
				.IWant("to get and set IsSubscribed");

			feature.WithScenario("Access IsSubscribed property")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.When(IsSubscribedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsSubscribed property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(IsSubscribedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.When(IsSubscribedIsSet, (bool?)true)
				.Then(MockApiPutIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property to null")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(IsSubscribedIs, (bool?)true)
				.When(IsSubscribedIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set IsSubscribed property to same")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(IsSubscribedIs, (bool?)true)
				.When(IsSubscribedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property without UserToken")
				.Given(ABoard)
				.And(TokenNotSupplied)
				.When(IsSubscribedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void LabelNames()
		{
			var story = new Story("LabelNames");

			var feature = story.InOrderTo("get all label names of a board")
				.AsA("developer")
				.IWant("to get LabelNames");

			feature.WithScenario("Access LabelNames property")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(LabelNamesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonLabelNames>, 0)
				.And(NonNullValueOfTypeIsReturned<LabelNames>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Lists()
		{
			var story = new Story("Lists");

			var feature = story.InOrderTo("get all lists for a board")
				.AsA("developer")
				.IWant("to get Lists");

			feature.WithScenario("Access Lists property")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(ListsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonList>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<List>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Memberships()
		{
			var story = new Story("Memberships");

			var feature = story.InOrderTo("get all members of a board")
				.AsA("developer")
				.IWant("to get Memberships");

			feature.WithScenario("Access Memberships property")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(MembershipsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonBoardMembership>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<BoardMembership>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Name()
		{
			var story = new Story("Name");

			var feature = story.InOrderTo("control a board's name")
				.AsA("developer")
				.IWant("to get and set the Name");

			feature.WithScenario("Access Name property")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Name property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.When(NameIsSet, "name")
				.Then(MockApiPutIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to null")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(NameIs, "name")
				.When(NameIsSet, (string) null)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Name property to empty")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(NameIs, "not description")
				.When(NameIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Name property to same")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(NameIs, "description")
				.When(NameIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property without UserToken")
				.Given(ABoard)
				.And(TokenNotSupplied)
				.When(NameIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Organization()
		{
			var story = new Story("Organization");

			var feature = story.InOrderTo("control the organization to which a board belongs")
				.AsA("developer")
				.IWant("to get and set the Organization");

			feature.WithScenario("Access Organization property")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.When(OrganizationIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 1)
				.And(MockSvcRetrieveIsCalled<Organization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Organization property when expired")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(OrganizationIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 1)
				.And(MockSvcRetrieveIsCalled<Organization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Organization property")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.When(OrganizationIsSet, new Organization {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Organization property to null")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(OrganizationIs, new Organization {Id = TrelloIds.Invalid})
				.When(OrganizationIsSet, (Organization) null)
				.Then(MockApiPutIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Organization property to local organization")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.When(OrganizationIsSet, new Organization())
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Organization>>)

				.WithScenario("Set Organization property to same")
				.Given(ABoard)
				.And(EntityIsRefreshed)
				.And(OrganizationIs, new Organization {Id = TrelloIds.Invalid})
				.When(OrganizationIsSet, new Organization {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Organization property without UserToken")
				.Given(ABoard)
				.And(TokenNotSupplied)
				.When(OrganizationIsSet, new Organization {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void PersonalPreferences()
		{
			var story = new Story("PersonalPreferences");

			var feature = story.InOrderTo("get the preferences of the current user for a board")
				.AsA("developer")
				.IWant("to get PersonalPreferences");

			feature.WithScenario("Access PersonalPreferences property")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(PersonalPreferencesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardPersonalPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Preferences()
		{
			var story = new Story("Preferences");

			var feature = story.InOrderTo("get the preferences for a board")
				.AsA("developer")
				.IWant("to get Preferences");

			feature.WithScenario("Access Preferences property")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(PreferencesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoardPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Url()
		{
			var story = new Story("Url");

			var feature = story.InOrderTo("get a board's URL")
				.AsA("developer")
				.IWant("to get the Url");

			feature.WithScenario("Access Url property")
				.Given(ABoard)
				.And(EntityIsExpired)
				.When(UrlIsAccessed)
				.Then(MockApiGetIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AddList()
		{
			var story = new Story("AddList");

			var feature = story.InOrderTo("add a list to a board")
				.AsA("developer")
				.IWant("to add a list");

			feature.WithScenario("AddList is called")
				.Given(ABoard)
				.When(AddListIsCalled, "list")
				.Then(MockApiPostIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("AddList is called with null")
				.Given(ABoard)
				.When(AddListIsCalled, (string) null)
				.Then(MockApiPostIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddList is called with empty")
				.Given(ABoard)
				.When(AddListIsCalled, string.Empty)
				.Then(MockApiPostIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddList is called without UserToken")
				.Given(ABoard)
				.And(TokenNotSupplied)
				.When(AddListIsCalled, string.Empty)
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void AddOrUpdateMemberByMember()
		{
			var story = new Story("AddOrUpdateMember by member");

			var feature = story.InOrderTo("add a member to or update permissions for a member on a board")
				.AsA("developer")
				.IWant("to add or update a member");

			feature.WithScenario("AddOrUpdateMember is called")
				.Given(ABoard)
				.When(AddOrUpdateMemberIsCalled, new Member {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("AddOrUpdateMember is called with null")
				.Given(ABoard)
				.When(AddOrUpdateMemberIsCalled, (Member) null)
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddOrUpdateMember is called with local member")
				.Given(ABoard)
				.When(AddOrUpdateMemberIsCalled, new Member())
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Member>>)

				.WithScenario("AddOrUpdateMember is called without UserToken")
				.Given(ABoard)
				.And(TokenNotSupplied)
				.When(AddOrUpdateMemberIsCalled, new Member())
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void AddOrUpdateMemberByEmailAndName()
		{
			var story = new Story("AddOrUpdateMember by email and name");

			var feature = story.InOrderTo("add a member to or update permissions for a member on a board")
				.AsA("developer")
				.IWant("to add or update a member");

			feature.WithScenario("AddOrUpdateMember is called")
				.Given(ABoard)
				.When(AddOrUpdateMemberIsCalled, "some@email.com", "Some Email")
				.Then(MockSvcSearchMembersIsCalled, "some@email.com", 1)
				.And(MockApiPutIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("AddOrUpdateMember is called with null email")
				.Given(ABoard)
				.When(AddOrUpdateMemberIsCalled, (string)null, "Some Email")
				.Then(MockSvcSearchMembersIsCalled, "some@email.com", 0)
				.And(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddOrUpdateMember is called with null name")
				.Given(ABoard)
				.When(AddOrUpdateMemberIsCalled, "some@email.com", (string)null)
				.Then(MockSvcSearchMembersIsCalled, "some@email.com", 0)
				.And(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddOrUpdateMember is called without UserToken")
				.Given(ABoard)
				.And(TokenNotSupplied)
				.When(AddOrUpdateMemberIsCalled, "some@email.com", "Some Email")
				.Then(MockSvcSearchMembersIsCalled, "some@email.com", 0)
				.And(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void MarkAsViewed()
		{
			var story = new Story("MarkAsViewed");

			var feature = story.InOrderTo("mark a board as viewewd by the current member")
				.AsA("developer")
				.IWant("to mark a board as viewed");

			feature.WithScenario("MarkAsViewed is called")
				.Given(ABoard)
				.When(MarkAsViewedIsCalled)
				.Then(MockApiPostIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("MarkAsViewed is called without UserToken")
				.Given(ABoard)
				.And(TokenNotSupplied)
				.When(MarkAsViewedIsCalled)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		[Ignore]
		public void InviteMember()
		{
			var story = new Story("InviteMember");

			var feature = story.InOrderTo("invite a member to a board")
				.AsA("developer")
				.IWant("to invite a member");

			feature.WithScenario("InviteMember is called")
				.Given(ABoard)
				.When(InviteMemberIsCalled)
				.Then(MockApiPutIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("InviteMember is called without UserToken")
				.Given(ABoard)
				.And(TokenNotSupplied)
				.When(InviteMemberIsCalled)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void RemoveMember()
		{
			var story = new Story("RemoveMember");

			var feature = story.InOrderTo("remove a member from a board")
				.AsA("developer")
				.IWant("to remove a member");

			feature.WithScenario("RemoveMember is called")
				.Given(ABoard)
				.When(RemoveMemberIsCalled, new Member {Id = TrelloIds.Invalid})
				.Then(MockApiDeleteIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("RemoveMember is called with null")
				.Given(ABoard)
				.When(RemoveMemberIsCalled, (Member) null)
				.Then(MockApiDeleteIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("RemoveMember is called with local member")
				.Given(ABoard)
				.When(RemoveMemberIsCalled, new Member())
				.Then(MockApiDeleteIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Member>>)

				.WithScenario("RemoveMember is called without UserToken")
				.Given(ABoard)
				.And(TokenNotSupplied)
				.When(RemoveMemberIsCalled, new Member())
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		[Ignore]
		public void RescindInvitation()
		{
			var story = new Story("RescindInvitation");

			var feature = story.InOrderTo("uninvite a member for a board")
				.AsA("developer")
				.IWant("to uninvite a member");

			feature.WithScenario("RescindInvitation is called")
				.Given(ABoard)
				.When(RescindInvitationIsCalled)
				.Then(MockApiDeleteIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("RescindInvitation is called without UserToken")
				.Given(ABoard)
				.And(TokenNotSupplied)
				.When(RescindInvitationIsCalled)
				.Then(MockApiDeleteIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		#region Given

		private void ABoard()
		{
			_systemUnderTest = new EntityUnderTest();
			var jsonBoardMock = SetupMockGet<IJsonBoard>();
			jsonBoardMock.SetupGet(b => b.IdOrganization).Returns(TrelloIds.OrganizationId);
			SetupMockPut<IJsonMember>();
			SetupMockPost<IJsonList>();
			SetupMockRetrieve<Organization>();
			var mockMember = new Mock<Member>();
			mockMember.SetupGet(m => m.Id).Returns(TrelloIds.MemberId);
			_systemUnderTest.Dependencies.Svc.Setup(s => s.SearchMembers(It.IsAny<string>(), It.IsAny<int>()))
			                .Returns(new List<Member> {mockMember.Object});
		}
		private void DescriptionIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Description = value);
		}
		private void IsClosedIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.IsClosed = value);
		}
		private void IsPinnedIs(bool? value)
		{
			//SetupProperty(() => _systemUnderTest.Sut.IsPinned = value);
		}
		private void IsSubscribedIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.IsSubscribed = value);
		}
		private void NameIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Name = value);
		}
		private void OrganizationIs(Organization value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Organization = value);
		}

		#endregion

		#region When

		private void ActionsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Actions);
		}
		private void ArchivedCardsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ArchivedCards);
		}
		private void ArchivedListsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ArchivedLists);
		}
		private void DescriptionIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Description);
		}
		private void DescriptionIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Description = value);
		}
		private void InvitedMembersIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.InvitedMembers);
		}
		private void IsClosedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.IsClosed);
		}
		private void IsClosedIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.IsClosed = value);
		}
		private void IsPinnedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.IsPinned);
		}
		private void IsPinnedIsSet(bool? value)
		{
			//Execute(() => _systemUnderTest.Sut.IsPinned = value);
		}
		private void IsSubscribedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.IsSubscribed);
		}
		private void IsSubscribedIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.IsSubscribed = value);
		}
		private void LabelNamesIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.LabelNames);
		}
		private void ListsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Lists);
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
		private void OrganizationIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Organization);
		}
		private void OrganizationIsSet(Organization value)
		{
			Execute(() => _systemUnderTest.Sut.Organization = value);
		}
		private void PersonalPreferencesIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.PersonalPreferences);
		}
		private void PreferencesIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Preferences);
		}
		private void UrlIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Url);
		}
		private void AddListIsCalled(string value)
		{
			Execute(() => _systemUnderTest.Sut.AddList(value));
		}
		private void AddOrUpdateMemberIsCalled(Member value)
		{
			Execute(() => _systemUnderTest.Sut.AddOrUpdateMember(value));
		}
		private void AddOrUpdateMemberIsCalled(string email, string name)
		{
			Execute(() => _systemUnderTest.Sut.AddOrUpdateMember(email, name));
		}
		private void MarkAsViewedIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.MarkAsViewed());
		}
		private void InviteMemberIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.InviteMember(new Member { Id = TrelloIds.Invalid }));
		}
		private void RemoveMemberIsCalled(Member value)
		{
			Execute(() => _systemUnderTest.Sut.RemoveMember(value));
		}
		private void RescindInvitationIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.RescindInvitation(new Member { Id = TrelloIds.Invalid }));
		}

		#endregion

		#region Then

		private void MockSvcSearchMembersIsCalled(string emailAddress, int count)
		{
			_systemUnderTest.Dependencies.Svc.Verify(t => t.SearchMembers(It.Is<string>(s => s == emailAddress), It.IsAny<int>()), Times.Exactly(count));
		}

		#endregion
	}
}

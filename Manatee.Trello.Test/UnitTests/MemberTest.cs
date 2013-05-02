using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Json;
using Manatee.Trello.Json.Manatee.Entities;
using Manatee.Trello.Rest;
using Manatee.Trello.Test.FunctionalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class MemberTest : EntityTestBase<Member>
	{
		[TestMethod]
		public void Actions()
		{
			var story = new Story("Actions");

			var feature = story.InOrderTo("get all actions for a member")
				.AsA("developer")
				.IWant("to get Actions");

			feature.WithScenario("Access Actions property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(ActionsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonAction>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Action>>)
				.And(ExceptionIsNotThrown)

				.WithScenario("All action types are mapped")
				.Given(AMember)
				.And(AllKnownActionTypesExist)
				.When(NotificationsIsAccessed)
				.Then(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AvatarHash()
		{
			var story = new Story("AvatarHash");

			var feature = story.InOrderTo("get a member's avatar hash")
				.AsA("developer")
				.IWant("to get the AvatarHash");

			feature.WithScenario("Access AvatarHash property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(AvatarHashIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access AvatarHash property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(AvatarHashIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AvatarSource()
		{
			var story = new Story("AvatarSource");

			var feature = story.InOrderTo("control a member's avatar source URL")
				.AsA("developer")
				.IWant("to get and set the AvatarSource");

			feature.WithScenario("Access AvatarSource property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(AvatarSourceIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access AvatarSource property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(AvatarSourceIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AvatarSource property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(AvatarSourceIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AvatarSource property to null")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.And(AvatarSourceIs, "not description")
				.When(AvatarSourceIsSet, (string)null)
				.Then(MockApiPutIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AvatarSource property to empty")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.And(AvatarSourceIs, "not description")
				.When(AvatarSourceIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AvatarSource property to same")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.And(AvatarSourceIs, "description")
				.When(AvatarSourceIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AvatarSource property without AuthToken")
				.Given(AMember)
				.And(TokenNotSupplied)
				.When(AvatarSourceIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Bio()
		{
			var story = new Story("Bio");

			var feature = story.InOrderTo("control a member's biographical description")
				.AsA("developer")
				.IWant("to get and set the Bio");

			feature.WithScenario("Access Bio property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(BioIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Bio property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(BioIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Bio property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(BioIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Bio property to null")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.And(BioIs, "not description")
				.When(BioIsSet, (string)null)
				.Then(MockApiPutIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Bio property to empty")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.And(BioIs, "not description")
				.When(BioIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Bio property to same")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.And(BioIs, "description")
				.When(BioIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Bio property without AuthToken")
				.Given(AMember)
				.And(TokenNotSupplied)
				.When(BioIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Boards()
		{
			var story = new Story("Boards");

			var feature = story.InOrderTo("get the boards owned by a member")
				.AsA("developer")
				.IWant("to get Boards");

			feature.WithScenario("Access Boards property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(BoardsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonBoard>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Board>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Confirmed()
		{
			var story = new Story("Confirmed");

			var feature = story.InOrderTo("get a whether a member is a confirmed Trello user")
				.AsA("developer")
				.IWant("to get the Confirmed");

			feature.WithScenario("Access Confirmed property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(ConfirmedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Confirmed property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(ConfirmedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Email()
		{
			var story = new Story("Email");

			var feature = story.InOrderTo("get a members's email address")
				.AsA("developer")
				.IWant("to get the Email");

			feature.WithScenario("Access Email property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(EmailIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Email property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(EmailIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void FullName()
		{
			var story = new Story("FullName");

			var feature = story.InOrderTo("control a member's full name")
				.AsA("developer")
				.IWant("to get and set the FullName");

			feature.WithScenario("Access FullName property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(FullNameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access FullName property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(FullNameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set FullName property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(FullNameIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set FullName property to null")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.And(FullNameIs, "not description")
				.When(FullNameIsSet, (string) null)
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set FullName property to less than 4 characters")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(FullNameIsSet, "bad")
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentException>)

				.WithScenario("Set FullName property to same")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.And(FullNameIs, "description")
				.When(FullNameIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set FullName property without AuthToken")
				.Given(AMember)
				.And(TokenNotSupplied)
				.When(FullNameIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void GravatarHash()
		{
			var story = new Story("GravatarHash");

			var feature = story.InOrderTo("get a card's Gravatar hash")
				.AsA("developer")
				.IWant("to get the GravatarHash");

			feature.WithScenario("Access GravatarHash property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(GravatarHashIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access GravatarHash property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(GravatarHashIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Initials()
		{
			var story = new Story("Initials");

			var feature = story.InOrderTo("control a member's full name")
				.AsA("developer")
				.IWant("to get and set the Initials");

			feature.WithScenario("Access Initials property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(InitialsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Initials property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(InitialsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Initials property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(InitialsIsSet, "mt")
				.Then(MockApiPutIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Initials property to null")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.And(InitialsIs, "mt")
				.When(InitialsIsSet, (string)null)
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Initials property to empty")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(InitialsIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentException>)

				.WithScenario("Set Initials property to greater than 3")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(InitialsIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentException>)

				.WithScenario("Set Initials property to same")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.And(InitialsIs, "mt")
				.When(InitialsIsSet, "mt")
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Initials property without AuthToken")
				.Given(AMember)
				.And(TokenNotSupplied)
				.When(InitialsIsSet, "mt")
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void InvitedBoards()
		{
			var story = new Story("InvitedBoards");

			var feature = story.InOrderTo("get the boards to which a member is invited")
				.AsA("developer")
				.IWant("to get InvitedBoards");

			feature.WithScenario("Access InvitedBoards property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(InvitedBoardsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonBoard>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Board>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void InvitedOrganizations()
		{
			var story = new Story("InvitedOrganizations");

			var feature = story.InOrderTo("get the organizations to which a member is invited")
				.AsA("developer")
				.IWant("to get InvitedOrganizations");

			feature.WithScenario("Access InvitedOrganizations property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(InvitedOrganizationsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonOrganization>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Organization>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void LoginTypes()
		{
			var story = new Story("LoginTypes");

			var feature = story.InOrderTo("get a member's LoginTypes")
				.AsA("developer")
				.IWant("to get the LoginTypes");

			feature.WithScenario("Access LoginTypes property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(LoginTypesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access LoginTypes property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(LoginTypesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void MemberType()
		{
			var story = new Story("MemberType");

			var feature = story.InOrderTo("get a member's membership type")
				.AsA("developer")
				.IWant("to get the MemberType");

			feature.WithScenario("Access MemberType property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(MemberTypeIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access MemberType property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(MemberTypeIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Notifications()
		{
			var story = new Story("Notifications");

			var feature = story.InOrderTo("get the member's recent notifications")
				.AsA("developer")
				.IWant("to get Notifications");

			feature.WithScenario("Access Notifications property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(NotificationsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonNotification>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Notification>>)
				.And(ExceptionIsNotThrown)

				.WithScenario("All notification types are mapped")
				.Given(AMember)
				.And(AllKnownNotificationTypesExist)
				.When(NotificationsIsAccessed)
				.Then(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Organizations()
		{
			var story = new Story("Organizations");

			var feature = story.InOrderTo("get the organizations to which the member belongs")
				.AsA("developer")
				.IWant("to get Organizations");

			feature.WithScenario("Access Organizations property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(OrganizationsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonOrganization>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Organization>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void PinnedBoards()
		{
			var story = new Story("PinnedBoards");

			var feature = story.InOrderTo("get the boards which a member has pinned to their Boards menu")
				.AsA("developer")
				.IWant("to get PinnedBoards");

			feature.WithScenario("Access PinnedBoards property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(PinnedBoardsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonBoard>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Board>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Preferences()
		{
			var story = new Story("Preferences");

			var feature = story.InOrderTo("get a member's preference options")
				.AsA("developer")
				.IWant("to get the Preferences");

			feature.WithScenario("Access Preferences property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(PreferencesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 0)
				.And(NonNullValueOfTypeIsReturned<MemberPreferences>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void PremiumOrganizations()
		{
			var story = new Story("PremiumOrganizations");

			var feature = story.InOrderTo("get the premium organizations to which a member belongs")
				.AsA("developer")
				.IWant("to get PremiumOrganizations");

			feature.WithScenario("Access PremiumOrganizations property")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(PremiumOrganizationsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonOrganization>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Organization>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Status()
		{
			var story = new Story("Status");

			var feature = story.InOrderTo("get a member's online status")
				.AsA("developer")
				.IWant("to get the Status");

			feature.WithScenario("Access Status property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(StatusIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Status property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(StatusIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Trophies()
		{
			var story = new Story("Trophies");

			var feature = story.InOrderTo("get the trophies which a member has earned")
				.AsA("developer")
				.IWant("to get the Trophies");

			feature.WithScenario("Access Trophies property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(TrophiesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Trophies property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(TrophiesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void UploadedAvatarHash()
		{
			var story = new Story("UploadedAvatarHash");

			var feature = story.InOrderTo("get a member's uploaded avatar hash")
				.AsA("developer")
				.IWant("to get the UploadedAvatarHash");

			feature.WithScenario("Access UploadedAvatarHash property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(UploadedAvatarHashIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access UploadedAvatarHash property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(UploadedAvatarHashIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
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
				.Given(AMember)
				.And(EntityIsExpired)
				.When(UrlIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 0)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Username()
		{
			var story = new Story("Username");

			var feature = story.InOrderTo("control a member's username")
				.AsA("developer")
				.IWant("to get and set the Username");

			feature.WithScenario("Access Username property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(UsernameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Username property when expired")
				.Given(AMember)
				.And(EntityIsExpired)
				.When(UsernameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Username property")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(UsernameIsSet, "description")
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(MockApiPutIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Username property to null")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.And(UsernameIs, "not description")
				.When(UsernameIsSet, (string) null)
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Username property to less than 3 characters")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.When(UsernameIsSet, "un")
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentException>)

				.WithScenario("Set Username property to same")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.And(UsernameIs, "description")
				.When(UsernameIsSet, "description")
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Username property to existing username")
				.Given(AMember)
				.And(EntityIsRefreshed)
				.And(UsernameExists)
				.When(UsernameIsSet, "username")
				.Then(MockApiGetIsCalled<IJsonMember>, 1)
				.And(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<UsernameInUseException>)

				.WithScenario("Set Username property without AuthToken")
				.Given(AMember)
				.And(TokenNotSupplied)
				.When(UsernameIsSet, "description")
				.Then(MockApiGetIsCalled<IJsonMember>, 0)
				.And(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void ClearNotifications()
		{
			var story = new Story("ClearNotifications");

			var feature = story.InOrderTo("clear all notifications for a member")
				.AsA("developer")
				.IWant("to call ClearNotifications");

			feature.WithScenario("ClearNotifications is called")
				.Given(AMember)
				.When(ClearNotificationsIsCalled)
				.Then(MockApiPostIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("ClearNotifications is called without AuthToken")
				.Given(AMember)
				.And(TokenNotSupplied)
				.When(ClearNotificationsIsCalled)
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void CreateBoard()
		{
			var story = new Story("CreateBoard");

			var feature = story.InOrderTo("create a new personal board")
				.AsA("developer")
				.IWant("to call CreateBoard");

			feature.WithScenario("CreateBoard is called")
				.Given(AMember)
				.When(CreateBoardIsCalled, "org name")
				.Then(MockApiPostIsCalled<IJsonBoard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("CreateBoard is called without AuthToken")
				.Given(AMember)
				.And(TokenNotSupplied)
				.When(CreateBoardIsCalled, "org name")
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.WithScenario("CreateBoard is called with null name")
				.Given(AMember)
				.When(CreateBoardIsCalled, (string) null)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("CreateBoard is called with empty name")
				.Given(AMember)
				.When(CreateBoardIsCalled, string.Empty)
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("CreateBoard is called with whitespace name")
				.Given(AMember)
				.When(CreateBoardIsCalled, "     ")
				.Then(MockApiPutIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.Execute();
		}
		[TestMethod]
		public void CreateOrganization()
		{
			var story = new Story("CreateOrganization");

			var feature = story.InOrderTo("create a new organization")
				.AsA("developer")
				.IWant("to call CreateOrganization");

			feature.WithScenario("CreateOrganization is called")
				.Given(AMember)
				.When(CreateOrganizationIsCalled, "org name")
				.Then(MockApiPostIsCalled<IJsonOrganization>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("CreateOrganization is called without AuthToken")
				.Given(AMember)
				.And(TokenNotSupplied)
				.When(CreateOrganizationIsCalled, "org name")
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.WithScenario("CreateOrganization is called with null name")
				.Given(AMember)
				.When(CreateOrganizationIsCalled, (string) null)
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("CreateOrganization is called with empty name")
				.Given(AMember)
				.When(CreateOrganizationIsCalled, string.Empty)
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("CreateOrganization is called with whitespace name")
				.Given(AMember)
				.When(CreateOrganizationIsCalled, "     ")
				.Then(MockApiPutIsCalled<IJsonOrganization>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.Execute();
		}
		[TestMethod]
		public void PinBoard()
		{
			var story = new Story("PinBoard");

			var feature = story.InOrderTo("pin a board to a member's boards menu")
				.AsA("developer")
				.IWant("to call PinBoard");

			feature.WithScenario("PinBoard is called")
				.Given(AMember)
				.When(PinBoardIsCalled, new Board {Id = TrelloIds.Invalid})
				.Then(MockApiPostIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("PinBoard is called with null board")
				.Given(AMember)
				.When(PinBoardIsCalled, (Board) null)
				.Then(MockApiPostIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("PinBoard is called with local board")
				.Given(AMember)
				.When(PinBoardIsCalled, new Board())
				.Then(MockApiPostIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Board>>)

				.WithScenario("PinBoard is called without AuthToken")
				.Given(AMember)
				.And(TokenNotSupplied)
				.When(PinBoardIsCalled, new Board {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void RescindVoteForCard()
		{
			var story = new Story("RescindVoteForCard");

			var feature = story.InOrderTo("rescind a member's vote for a card")
				.AsA("developer")
				.IWant("to call RescindVoteForCard");

			feature.WithScenario("RescindVoteForCard is called")
				.Given(AMember)
				.When(RescindVoteForCardIsCalled, new Card {Id = TrelloIds.Invalid})
				.Then(MockApiDeleteIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("RescindVoteForCard is called with null card")
				.Given(AMember)
				.When(RescindVoteForCardIsCalled, (Card) null)
				.Then(MockApiDeleteIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("RescindVoteForCard is called with local card")
				.Given(AMember)
				.When(RescindVoteForCardIsCalled, new Card())
				.Then(MockApiDeleteIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Card>>)

				.WithScenario("RescindVoteForCard is called without AuthToken")
				.Given(AMember)
				.And(TokenNotSupplied)
				.When(RescindVoteForCardIsCalled, new Card {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void UnpinBoard()
		{
			var story = new Story("UnpinBoard");

			var feature = story.InOrderTo("unpin a board from a member's boards menu")
				.AsA("developer")
				.IWant("to call UnpinBoard");

			feature.WithScenario("UnpinBoard is called")
				.Given(AMember)
				.When(UnpinBoardIsCalled, new Board {Id = TrelloIds.Invalid})
				.Then(MockApiDeleteIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("UnpinBoard is called with null board")
				.Given(AMember)
				.When(UnpinBoardIsCalled, (Board) null)
				.Then(MockApiDeleteIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("UnpinBoard is called with local board")
				.Given(AMember)
				.When(UnpinBoardIsCalled, new Board())
				.Then(MockApiDeleteIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Board>>)

				.WithScenario("UnpinBoard is called without AuthToken")
				.Given(AMember)
				.And(TokenNotSupplied)
				.When(UnpinBoardIsCalled, new Board {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void VoteForCard()
		{
			var story = new Story("VoteForCard");

			var feature = story.InOrderTo("apply a member's vote to a card")
				.AsA("developer")
				.IWant("to call VoteForCard");

			feature.WithScenario("VoteForCard is called")
				.Given(AMember)
				.When(VoteForCardIsCalled, new Card {Id = TrelloIds.Invalid})
				.Then(MockApiPostIsCalled<IJsonMember>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("VoteForCard is called with null card")
				.Given(AMember)
				.When(VoteForCardIsCalled, (Card) null)
				.Then(MockApiPostIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("VoteForCard is called with local card")
				.Given(AMember)
				.When(VoteForCardIsCalled, new Card())
				.Then(MockApiPostIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Card>>)

				.WithScenario("VoteForCard is called without AuthToken")
				.Given(AMember)
				.And(TokenNotSupplied)
				.When(VoteForCardIsCalled, new Card {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<IJsonMember>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		#region Given

		private void AMember()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Svc.Object;
			SetupMockGet<IJsonMember>();
			SetupMockPost<IJsonBoard>();
			SetupMockPost<IJsonOrganization>();
		}
		private void AvatarSourceIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.AvatarSource = value);
		}
		private void BioIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Bio = value);
		}
		private void FullNameIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.FullName = value);
		}
		private void InitialsIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Initials = value);
		}
		private void UsernameExists()
		{
			SetupMockGet<Member>();
		}
		private void UsernameIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Username = value);
		}
		private void AllKnownActionTypesExist()
		{
			var types = Enum.GetValues(typeof(ActionType)).Cast<ActionType>();
			var jsonActions = types.Select(t =>
			                               	{
			                               		var mock = new Mock<IJsonAction>();
			                               		mock.SetupGet(a => a.Type).Returns(t.ToLowerString());
			                               		return mock.Object;
			                               	})
								   .ToList();

			_systemUnderTest.Dependencies.Rest.Setup(a => a.Get<List<IJsonAction>>(It.IsAny<IRestRequest>()))
				.Returns(jsonActions);
		}
		private void AllKnownNotificationTypesExist()
		{
			var types = Enum.GetValues(typeof(NotificationType)).Cast<NotificationType>();
			var jsonNotifications = types.Select(t =>
			                               	{
			                               		var mock = new Mock<IJsonNotification>();
			                               		mock.SetupGet(a => a.Type).Returns(t.ToLowerString());
			                               		return mock.Object;
			                               	})
										 .ToList();

			_systemUnderTest.Dependencies.Rest.Setup(a => a.Get<List<IJsonNotification>>(It.IsAny<IRestRequest>()))
				.Returns(jsonNotifications);
		}

		#endregion

		#region When

		private void ActionsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Actions);
		}
		private void AvatarHashIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.AvatarHash);
		}
		private void AvatarSourceIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.AvatarSource);
		}
		private void AvatarSourceIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.AvatarSource = value);
		}
		private void BioIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Bio);
		}
		private void BioIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Bio = value);
		}
		private void BoardsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Boards);
		}
		private void ConfirmedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Confirmed);
		}
		private void EmailIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Email);
		}
		private void FullNameIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.FullName);
		}
		private void FullNameIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.FullName = value);
		}
		private void GravatarHashIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.GravatarHash);
		}
		private void InitialsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Initials);
		}
		private void InitialsIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Initials = value);
		}
		private void InvitedBoardsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.InvitedBoards);
		}
		private void InvitedOrganizationsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.InvitedOrganizations);
		}
		private void LoginTypesIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.LoginTypes);
		}
		private void MemberTypeIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.MemberType);
		}
		private void NotificationsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Notifications);
		}
		private void OrganizationsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Organizations);
		}
		private void PinnedBoardsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.PinnedBoards);
		}
		private void PreferencesIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Preferences);
		}
		private void PremiumOrganizationsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.PremiumOrganizations);
		}
		private void StatusIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Status);
		}
		private void TrophiesIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Trophies);
		}
		private void UploadedAvatarHashIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.UploadedAvatarHash);
		}
		private void UrlIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Url);
		}
		private void UsernameIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Username);
		}
		private void UsernameIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Username = value);
		}
		private void ClearNotificationsIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.ClearNotifications());
		}
		private void CreateBoardIsCalled(string value)
		{
			Execute(() => _systemUnderTest.Sut.CreateBoard(value));
		}
		private void CreateOrganizationIsCalled(string value)
		{
			Execute(() => _systemUnderTest.Sut.CreateOrganization(value));
		}
		private void PinBoardIsCalled(Board value)
		{
			Execute(() => _systemUnderTest.Sut.PinBoard(value));
		}
		private void RescindVoteForCardIsCalled(Card value)
		{
			Execute(() => _systemUnderTest.Sut.RescindVoteForCard(value));
		}
		private void UnpinBoardIsCalled(Board value)
		{
			Execute(() => _systemUnderTest.Sut.UnpinBoard(value));
		}
		private void VoteForCardIsCalled(Card value)
		{
			Execute(() => _systemUnderTest.Sut.VoteForCard(value));
		}

		#endregion
	}
}
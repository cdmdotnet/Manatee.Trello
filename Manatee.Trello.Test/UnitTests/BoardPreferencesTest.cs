using System;
using Manatee.Trello.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class BoardPreferencesTest : EntityTestBase<BoardPreferences>
	{
		[TestMethod]
		public void AllowsSelfJoin()
		{
			var story = new Story("AllowsSelfJoin");

			var feature = story.InOrderTo("control whether the board allows members to join without an invitation")
				.AsA("developer")
				.IWant("to get the AllowsSelfJoin property value.");

			feature.WithScenario("Access AllowsSelfJoin property")
				.Given(ABoardPreferencesObject)
				.And(EntityIsNotExpired)
				.When(AllowsSelfJoinIsAccessed)
				.Then(MockApiGetIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access AllowsSelfJoin property when expired")
				.Given(ABoardPreferencesObject)
				.And(EntityIsExpired)
				.When(AllowsSelfJoinIsAccessed)
				.Then(MockApiGetIsCalled<BoardPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AllowsSelfJoin property")
				.Given(ABoardPreferencesObject)
				.When(AllowsSelfJoinIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<BoardPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AllowsSelfJoin property to null")
				.Given(ABoardPreferencesObject)
				.And(AllowsSelfJoinIs, (bool?) true)
				.When(AllowsSelfJoinIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set AllowsSelfJoin property to same")
				.Given(ABoardPreferencesObject)
				.And(AllowsSelfJoinIs, (bool?) true)
				.When(AllowsSelfJoinIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AllowsSelfJoin property without AuthToken")
				.Given(ABoardPreferencesObject)
				.And(TokenNotSupplied)
				.When(AllowsSelfJoinIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Comments()
		{
			var story = new Story("Comments");

			var feature = story.InOrderTo("control who is allowed to post comments")
				.AsA("developer")
				.IWant("to get the Comments property value.");

			feature.WithScenario("Access Comments property")
				.Given(ABoardPreferencesObject)
				.And(EntityIsNotExpired)
				.When(CommentsIsAccessed)
				.Then(MockApiGetIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Comments property when expired")
				.Given(ABoardPreferencesObject)
				.And(EntityIsExpired)
				.When(CommentsIsAccessed)
				.Then(MockApiGetIsCalled<BoardPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Comments property")
				.Given(ABoardPreferencesObject)
				.When(CommentsIsSet, BoardCommentType.Org)
				.Then(MockApiPutIsCalled<BoardPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Comments property to same")
				.Given(ABoardPreferencesObject)
				.And(CommentsIs, BoardCommentType.Disabled)
				.When(CommentsIsSet, BoardCommentType.Disabled)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Comments property without AuthToken")
				.Given(ABoardPreferencesObject)
				.And(TokenNotSupplied)
				.When(CommentsIsSet, BoardCommentType.Disabled)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Invitations()
		{
			var story = new Story("Invitations");

			var feature = story.InOrderTo("control who is allowed to invite members to the board")
				.AsA("developer")
				.IWant("to get the Invitations property value.");

			feature.WithScenario("Access Invitations property")
				.Given(ABoardPreferencesObject)
				.And(EntityIsNotExpired)
				.When(InvitationsIsAccessed)
				.Then(MockApiGetIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Invitations property when expired")
				.Given(ABoardPreferencesObject)
				.And(EntityIsExpired)
				.When(InvitationsIsAccessed)
				.Then(MockApiGetIsCalled<BoardPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Invitations property")
				.Given(ABoardPreferencesObject)
				.When(InvitationsIsSet, BoardInvitationType.Members)
				.Then(MockApiPutIsCalled<BoardPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Invitations property to same")
				.Given(ABoardPreferencesObject)
				.And(InvitationsIs, BoardInvitationType.Admins)
				.When(InvitationsIsSet, BoardInvitationType.Admins)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Invitations property without AuthToken")
				.Given(ABoardPreferencesObject)
				.And(TokenNotSupplied)
				.When(InvitationsIsSet, BoardInvitationType.Admins)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void PermissionLevel()
		{
			var story = new Story("PermissionLevel");

			var feature = story.InOrderTo("control the board's exposure")
				.AsA("developer")
				.IWant("to get the PermissionLevel property value.");

			feature.WithScenario("Access PermissionLevel property")
				.Given(ABoardPreferencesObject)
				.And(EntityIsNotExpired)
				.When(PermissionLevelIsAccessed)
				.Then(MockApiGetIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access PermissionLevel property when expired")
				.Given(ABoardPreferencesObject)
				.And(EntityIsExpired)
				.When(PermissionLevelIsAccessed)
				.Then(MockApiGetIsCalled<BoardPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PermissionLevel property")
				.Given(ABoardPreferencesObject)
				.When(PermissionLevelIsSet, BoardPermissionLevelType.Public)
				.Then(MockApiPutIsCalled<BoardPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PermissionLevel property to same")
				.Given(ABoardPreferencesObject)
				.And(PermissionLevelIs, BoardPermissionLevelType.Public)
				.When(PermissionLevelIsSet, BoardPermissionLevelType.Public)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PermissionLevel property without AuthToken")
				.Given(ABoardPreferencesObject)
				.And(TokenNotSupplied)
				.When(PermissionLevelIsSet, BoardPermissionLevelType.Public)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void ShowCardCovers()
		{
			var story = new Story("ShowCardCovers");

			var feature = story.InOrderTo("control whether the board shows card covers")
				.AsA("developer")
				.IWant("to get the ShowCardCovers property value.");

			feature.WithScenario("Access ShowCardCovers property")
				.Given(ABoardPreferencesObject)
				.And(EntityIsNotExpired)
				.When(ShowCardCoversIsAccessed)
				.Then(MockApiGetIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ShowCardCovers property when expired")
				.Given(ABoardPreferencesObject)
				.And(EntityIsExpired)
				.When(ShowCardCoversIsAccessed)
				.Then(MockApiGetIsCalled<BoardPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowCardCovers property")
				.Given(ABoardPreferencesObject)
				.When(ShowCardCoversIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<BoardPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowCardCovers property to null")
				.Given(ABoardPreferencesObject)
				.And(ShowCardCoversIs, (bool?) true)
				.When(ShowCardCoversIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set ShowCardCovers property to same")
				.Given(ABoardPreferencesObject)
				.And(ShowCardCoversIs, (bool?) true)
				.When(ShowCardCoversIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowCardCovers property without AuthToken")
				.Given(ABoardPreferencesObject)
				.And(TokenNotSupplied)
				.When(ShowCardCoversIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Voting()
		{
			var story = new Story("Voting");

			var feature = story.InOrderTo("control who is allowed to cast votes")
				.AsA("developer")
				.IWant("to get the Voting property value.");

			feature.WithScenario("Access Voting property")
				.Given(ABoardPreferencesObject)
				.And(EntityIsNotExpired)
				.When(VotingIsAccessed)
				.Then(MockApiGetIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Voting property when expired")
				.Given(ABoardPreferencesObject)
				.And(EntityIsExpired)
				.When(VotingIsAccessed)
				.Then(MockApiGetIsCalled<BoardPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Voting property")
				.Given(ABoardPreferencesObject)
				.When(VotingIsSet, BoardVotingType.Disabled)
				.Then(MockApiPutIsCalled<BoardPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Voting property to same")
				.Given(ABoardPreferencesObject)
				.And(VotingIs, BoardVotingType.Public)
				.When(VotingIsSet, BoardVotingType.Public)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Voting property without AuthToken")
				.Given(ABoardPreferencesObject)
				.And(TokenNotSupplied)
				.When(VotingIsSet, BoardVotingType.Public)
				.Then(MockApiPutIsCalled<BoardPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		#region Given

		private void ABoardPreferencesObject()
		{
			_systemUnderTest = new SystemUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
			SetupMockGet<BoardPreferences>();
			SetupMockPut<BoardPreferences>();
		}
		private void AllowsSelfJoinIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.AllowsSelfJoin = value);
		}
		private void CommentsIs(BoardCommentType value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Comments = value);
		}
		private void InvitationsIs(BoardInvitationType value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Invitations = value);
		}
		private void PermissionLevelIs(BoardPermissionLevelType value)
		{
			SetupProperty(() => _systemUnderTest.Sut.PermissionLevel = value);
		}
		private void ShowCardCoversIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.ShowCardCovers = value);
		}
		private void VotingIs(BoardVotingType value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Voting = value);
		}

		#endregion

		#region When

		private void AllowsSelfJoinIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.AllowsSelfJoin);
		}
		private void AllowsSelfJoinIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.AllowsSelfJoin = value);
		}
		private void CommentsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Comments);
		}
		private void CommentsIsSet(BoardCommentType value)
		{
			Execute(() => _systemUnderTest.Sut.Comments = value);
		}
		private void InvitationsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Invitations);
		}
		private void InvitationsIsSet(BoardInvitationType value)
		{
			Execute(() => _systemUnderTest.Sut.Invitations = value);
		}
		private void PermissionLevelIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.PermissionLevel);
		}
		private void PermissionLevelIsSet(BoardPermissionLevelType value)
		{
			Execute(() => _systemUnderTest.Sut.PermissionLevel = value);
		}
		private void ShowCardCoversIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ShowCardCovers);
		}
		private void ShowCardCoversIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.ShowCardCovers = value);
		}
		private void VotingIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Voting);
		}
		private void VotingIsSet(BoardVotingType value)
		{
			Execute(() => _systemUnderTest.Sut.Voting = value);
		}

		#endregion
	}
}
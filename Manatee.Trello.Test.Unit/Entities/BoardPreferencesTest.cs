using Manatee.Trello.Json;
using Manatee.Trello.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class BoardPreferencesTest : EntityTestBase<BoardPreferences, IJsonBoardPreferences>
	{
		[TestMethod]
		public void AllowsSelfJoin()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access AllowsSelfJoin property when not expired")
				.Given(ABoardPreferencesObject)
				.When(AllowsSelfJoinIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<BoardPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access AllowsSelfJoin property when expired")
				.Given(ABoardPreferencesObject)
				.And(EntityIsExpired)
				.When(AllowsSelfJoinIsAccessed)
				.Then(RepositoryRefreshIsCalled<BoardPreferences>, EntityRequestType.BoardPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AllowsSelfJoin property")
				.Given(ABoardPreferencesObject)
				.When(AllowsSelfJoinIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.BoardPreferences_Write_AllowsSelfJoin)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set AllowsSelfJoin property to same")
				.Given(ABoardPreferencesObject)
				.And(AllowsSelfJoinIs, (bool?)true)
				.When(AllowsSelfJoinIsSet, (bool?) true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Comments()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Comments property when not expired")
				.Given(ABoardPreferencesObject)
				.When(CommentsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<BoardPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Comments property when expired")
				.Given(ABoardPreferencesObject)
				.And(EntityIsExpired)
				.When(CommentsIsAccessed)
				.Then(RepositoryRefreshIsCalled<BoardPreferences>, EntityRequestType.BoardPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Comments property")
				.Given(ABoardPreferencesObject)
				.When(CommentsIsSet, BoardCommentType.Org)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.BoardPreferences_Write_Comments)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Comments property to same")
				.Given(ABoardPreferencesObject)
				.And(CommentsIs, BoardCommentType.Disabled)
				.When(CommentsIsSet, BoardCommentType.Disabled)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Invitations()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Invitations property when not expired")
				.Given(ABoardPreferencesObject)
				.When(InvitationsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<BoardPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Invitations property when expired")
				.Given(ABoardPreferencesObject)
				.And(EntityIsExpired)
				.When(InvitationsIsAccessed)
				.Then(RepositoryRefreshIsCalled<BoardPreferences>, EntityRequestType.BoardPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Invitations property")
				.Given(ABoardPreferencesObject)
				.When(InvitationsIsSet, BoardInvitationType.Members)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.BoardPreferences_Write_Invitations)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Invitations property to same")
				.Given(ABoardPreferencesObject)
				.And(InvitationsIs, BoardInvitationType.Admins)
				.When(InvitationsIsSet, BoardInvitationType.Admins)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void PermissionLevel()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access PermissionLevel property when not expired")
				.Given(ABoardPreferencesObject)
				.When(PermissionLevelIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<BoardPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access PermissionLevel property when expired")
				.Given(ABoardPreferencesObject)
				.And(EntityIsExpired)
				.When(PermissionLevelIsAccessed)
				.Then(RepositoryRefreshIsCalled<BoardPreferences>, EntityRequestType.BoardPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PermissionLevel property")
				.Given(ABoardPreferencesObject)
				.When(PermissionLevelIsSet, BoardPermissionLevelType.Public)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.BoardPreferences_Write_PermissionLevel)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set PermissionLevel property to same")
				.Given(ABoardPreferencesObject)
				.And(PermissionLevelIs, BoardPermissionLevelType.Public)
				.When(PermissionLevelIsSet, BoardPermissionLevelType.Public)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ShowCardCovers()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access ShowCardCovers property when not expired")
				.Given(ABoardPreferencesObject)
				.When(ShowCardCoversIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<BoardPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ShowCardCovers property when expired")
				.Given(ABoardPreferencesObject)
				.And(EntityIsExpired)
				.When(ShowCardCoversIsAccessed)
				.Then(RepositoryRefreshIsCalled<BoardPreferences>, EntityRequestType.BoardPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowCardCovers property")
				.Given(ABoardPreferencesObject)
				.When(ShowCardCoversIsSet, (bool?)true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.BoardPreferences_Write_ShowCardCovers)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ShowCardCovers property to same")
				.Given(ABoardPreferencesObject)
				.And(ShowCardCoversIs, (bool?)true)
				.When(ShowCardCoversIsSet, (bool?) true)
				.Then(ValidatorNullableIsCalled<bool>)
				.And(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Voting()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Voting property when not expired")
				.Given(ABoardPreferencesObject)
				.When(VotingIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<BoardPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Voting property when expired")
				.Given(ABoardPreferencesObject)
				.And(EntityIsExpired)
				.When(VotingIsAccessed)
				.Then(RepositoryRefreshIsCalled<BoardPreferences>, EntityRequestType.BoardPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Voting property")
				.Given(ABoardPreferencesObject)
				.When(VotingIsSet, BoardVotingType.Disabled)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.BoardPreferences_Write_Voting)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Voting property to same")
				.Given(ABoardPreferencesObject)
				.And(VotingIs, BoardVotingType.Public)
				.When(VotingIsSet, BoardVotingType.Public)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ABoardPreferencesObject()
		{
			_test = new EntityUnderTest();
			OwnedBy<Board>();
		}
		private void AllowsSelfJoinIs(bool? value)
		{
			_test.Json.SetupGet(j => j.SelfJoin)
							.Returns(value);
		}
		private void CommentsIs(BoardCommentType value)
		{
			_test.Json.SetupGet(j => j.Comments)
							.Returns(value.ToLowerString());
			ReapplyJson();
		}
		private void InvitationsIs(BoardInvitationType value)
		{
			_test.Json.SetupGet(j => j.Invitations)
							.Returns(value.ToLowerString());
			ReapplyJson();
		}
		private void PermissionLevelIs(BoardPermissionLevelType value)
		{
			_test.Json.SetupGet(j => j.PermissionLevel)
							.Returns(value.ToLowerString());
			ReapplyJson();
		}
		private void ShowCardCoversIs(bool? value)
		{
			_test.Json.SetupGet(j => j.CardCovers)
							.Returns(value);
		}
		private void VotingIs(BoardVotingType value)
		{
			_test.Json.SetupGet(j => j.Voting)
							.Returns(value.ToLowerString());
			ReapplyJson();
		}

		#endregion

		#region When

		private void AllowsSelfJoinIsAccessed()
		{
			Execute(() => _test.Sut.AllowsSelfJoin);
		}
		private void AllowsSelfJoinIsSet(bool? value)
		{
			Execute(() => _test.Sut.AllowsSelfJoin = value);
		}
		private void CommentsIsAccessed()
		{
			Execute(() => _test.Sut.Comments);
		}
		private void CommentsIsSet(BoardCommentType value)
		{
			Execute(() => _test.Sut.Comments = value);
		}
		private void InvitationsIsAccessed()
		{
			Execute(() => _test.Sut.Invitations);
		}
		private void InvitationsIsSet(BoardInvitationType value)
		{
			Execute(() => _test.Sut.Invitations = value);
		}
		private void PermissionLevelIsAccessed()
		{
			Execute(() => _test.Sut.PermissionLevel);
		}
		private void PermissionLevelIsSet(BoardPermissionLevelType value)
		{
			Execute(() => _test.Sut.PermissionLevel = value);
		}
		private void ShowCardCoversIsAccessed()
		{
			Execute(() => _test.Sut.ShowCardCovers);
		}
		private void ShowCardCoversIsSet(bool? value)
		{
			Execute(() => _test.Sut.ShowCardCovers = value);
		}
		private void VotingIsAccessed()
		{
			Execute(() => _test.Sut.Voting);
		}
		private void VotingIsSet(BoardVotingType value)
		{
			Execute(() => _test.Sut.Voting = value);
		}

		#endregion
	}
}
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class BadgesTest : EntityTestBase<Badges, IJsonBadges>
	{
		[TestMethod]
		public void Attachments()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Attachments property when not expired")
				.Given(ABadgesObject)
				.When(AttachmentsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Badges>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Attachments property when expired")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(AttachmentsIsAccessed)
				.Then(RepositoryRefreshIsCalled<Badges>, EntityRequestType.Badges_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void CheckItems()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access CheckItems property when not expired")
				.Given(ABadgesObject)
				.When(CheckItemsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Badges>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access CheckItems property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(CheckItemsIsAccessed)
				.Then(RepositoryRefreshIsCalled<Badges>, EntityRequestType.Badges_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void CheckItemsChecked()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access CheckItemsChecked property when not expired")
				.Given(ABadgesObject)
				.When(CheckItemsCheckedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Badges>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access CheckItemsChecked property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(CheckItemsCheckedIsAccessed)
				.Then(RepositoryRefreshIsCalled<Badges>, EntityRequestType.Badges_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Comments()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Comments property when not expired")
				.Given(ABadgesObject)
				.When(CommentsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Badges>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Comments property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(CommentsIsAccessed)
				.Then(RepositoryRefreshIsCalled<Badges>, EntityRequestType.Badges_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void DueDate()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access DueDate property when not expired")
				.Given(ABadgesObject)
				.When(DueDateIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Badges>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access DueDate property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(DueDateIsAccessed)
				.Then(RepositoryRefreshIsCalled<Badges>, EntityRequestType.Badges_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void FogBugz()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access FogBugz property when not expired")
				.Given(ABadgesObject)
				.When(FogBugzIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Badges>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access FogBugz property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(FogBugzIsAccessed)
				.Then(RepositoryRefreshIsCalled<Badges>, EntityRequestType.Badges_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void HasDescription()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access HasDescription property")
				.Given(ABadgesObject)
				.When(HasDescriptionIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Badges>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access HasDescription property when not expired")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(HasDescriptionIsAccessed)
				.Then(RepositoryRefreshIsCalled<Badges>, EntityRequestType.Badges_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void IsSubscribed()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access IsSubscribed property when not expired")
				.Given(ABadgesObject)
				.When(IsSubscribedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Badges>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsSubscribed property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(IsSubscribedIsAccessed)
				.Then(RepositoryRefreshIsCalled<Badges>, EntityRequestType.Badges_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void ViewingMemberVoted()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access ViewingMemberVoted property when not expired")
				.Given(ABadgesObject)
				.When(ViewingMemberVotedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Badges>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ViewingMemberVoted property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(ViewingMemberVotedIsAccessed)
				.Then(RepositoryRefreshIsCalled<Badges>, EntityRequestType.Badges_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Votes()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Votes property when not expired")
				.Given(ABadgesObject)
				.When(VotesIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Badges>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Votes property")
				.Given(ABadgesObject)
				.And(EntityIsExpired)
				.When(VotesIsAccessed)
				.Then(RepositoryRefreshIsCalled<Badges>, EntityRequestType.Badges_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ABadgesObject()
		{
			_test = new EntityUnderTest();
			OwnedBy<Card>();
		}

		#endregion

		#region When

		private void AttachmentsIsAccessed()
		{
			Execute(() => _test.Sut.Attachments);
		}
		private void CheckItemsIsAccessed()
		{
			Execute(() => _test.Sut.CheckItems);
		}
		private void CheckItemsCheckedIsAccessed()
		{
			Execute(() => _test.Sut.CheckItemsChecked);
		}
		private void CommentsIsAccessed()
		{
			Execute(() => _test.Sut.Comments);
		}
		private void DueDateIsAccessed()
		{
			Execute(() => _test.Sut.DueDate);
		}
		private void FogBugzIsAccessed()
		{
			Execute(() => _test.Sut.FogBugz);
		}
		private void HasDescriptionIsAccessed()
		{
			Execute(() => _test.Sut.HasDescription);
		}
		private void IsSubscribedIsAccessed()
		{
			Execute(() => _test.Sut.IsSubscribed);
		}
		private void ViewingMemberVotedIsAccessed()
		{
			Execute(() => _test.Sut.ViewingMemberVoted);
		}
		private void VotesIsAccessed()
		{
			Execute(() => _test.Sut.Votes);
		}

		#endregion
	}
}

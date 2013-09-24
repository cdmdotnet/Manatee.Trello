using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class ActionTest : EntityTestBase<Action, IJsonAction>
	{
		[TestMethod]
		public void MemberCreator()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access MemberCreator property")
				.Given(AnAction)
				.And(EntityIsExpired)
				.When(MemberCreatorIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Action>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var feature = CreateFeature();

			feature.WithScenario("Delete is called")
				.Given(AnAction)
				.When(DeleteIsCalled)
				.Then(RepositoryUploadIsCalled, EntityRequestType.Action_Write_Delete)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called without UserToken")
				.Given(AnAction)
				.And(AlreadyDeleted)
				.When(DeleteIsCalled)
				.Then(ValidatorWritableIsNotCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		private void AnAction()
		{
			_test = new EntityUnderTest();
		}
		private void AlreadyDeleted()
		{
			_test.Sut.ForceDeleted(true);
		}

		private void MemberCreatorIsAccessed()
		{
			Execute(() => _test.Sut.MemberCreator);
		}
		private void DeleteIsCalled()
		{
			Execute(() => _test.Sut.Delete());
		}
	}
}

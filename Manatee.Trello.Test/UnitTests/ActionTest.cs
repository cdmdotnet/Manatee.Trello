using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class ActionTest : EntityTestBase<Action>
	{
		[TestMethod]
		public void MemberCreator()
		{
			var story = new Story("MemberCreator");

			var feature = story.InOrderTo("access the details of the member who created an action")
				.AsA("developer")
				.IWant("to get the member object.");

			feature.WithScenario("Access MemberCreator property")
				.Given(AnAction)
				.And(EntityIsRefreshed)
				.When(MemberCreatorIsAccessed)
				.Then(MockSvcRetrieveIsCalled<Member>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access MemberCreator property when expired")
				.Given(AnAction)
				.And(EntityIsRefreshed)
				.And(EntityIsExpired)
				.When(MemberCreatorIsAccessed)
				.Then(MockSvcRetrieveIsCalled<Member>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var story = new Story("Delete");

			var feature = story.InOrderTo("delete an action")
				.AsA("developer")
				.IWant("Delete to call the service.");

			feature.WithScenario("Delete is called")
				.Given(AnAction)
				.When(DeleteIsCalled)
				.Then(MockApiDeleteIsCalled<IJsonAction>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called without UserToken")
				.Given(AnAction)
				.And(TokenNotSupplied)
				.When(DeleteIsCalled)
				.Then(MockApiDeleteIsCalled<IJsonAction>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		private void AnAction()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Svc.Object;
			SetupMockGet<IJsonAction>();
			SetupMockRetrieve<Member>();
		}

		private void MemberCreatorIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.MemberCreator);
		}
		private void DeleteIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.Delete());
		}
	}
}

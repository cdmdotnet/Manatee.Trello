using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Implementation;
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
				.When(MemberIsAccessed)
				.Then(MockApiGetIsCalled<Action>, 0)
				.And(NonNullValueOfTypeIsReturned<Member>)
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

			feature.WithScenario("Call Delete")
				.Given(AnAction)
				.When(DeleteIsCalled)
				.Then(MockApiDeleteIsCalled<Action>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Call Delete without AuthToken")
				.Given(AnAction)
				.And(TokenNotSupplied)
				.When(DeleteIsCalled)
				.Then(MockApiDeleteIsCalled<Action>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		private void AnAction()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
			SetupMockGet<Member>();
		}

		private void MemberIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.MemberCreator);
		}
		private void DeleteIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.Delete());
		}
	}
}

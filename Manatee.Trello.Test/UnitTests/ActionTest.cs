using Manatee.Trello.Contracts;
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
				.And(MemberIsReturned)
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

				.Execute();
		}

		private void AnAction()
		{
			_systemUnderTest = new SystemUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
			_systemUnderTest.Dependencies.Api.Setup(a => a.Get(It.IsAny<IRestRequest<Member>>()))
				.Returns(new Member());
		}

		private void MemberIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.MemberCreator);
		}
		private void DeleteIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.Delete());
		}

		private void MemberIsReturned()
		{
			Assert.IsNotNull(_actualResult);
			Assert.IsInstanceOfType(_actualResult, typeof (Member));
		}
	}
}

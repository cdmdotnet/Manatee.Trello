using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
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

			feature.WithScenario("Access Member property")
				.Given(AnAction)
				.When(MemberIsRequested)
				.Then(MockApiIsCalled<Member>, Times.Once())
				.And(MemberIsReturned)
				.And(ExceptionIsNotThrown)
				
				.Execute();
		}

		private void AnAction()
		{
			_serviceGroup = new SystemUnderTest();
			_serviceGroup.Sut.Svc = _serviceGroup.Dependencies.Api.Object;
			_serviceGroup.Dependencies.Api.Setup(a => a.Get(It.IsAny<IRestRequest<Member>>()))
				.Returns(new Member());
		}

		private void MemberIsRequested()
		{
			Execute(() => _serviceGroup.Sut.MemberCreator);
		}

		private void MockApiIsCalled<T>(Times times) where T : ExpiringObject, new()
		{
			_serviceGroup.Dependencies.Api.Verify(a => a.Get(It.IsAny<IRestRequest<T>>()), times);
		}
		private void MemberIsReturned()
		{
			Assert.IsNotNull(_actualResult);
			Assert.IsInstanceOfType(_actualResult, typeof (Member));
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using StoryQ;
using IRestClient = Manatee.Trello.Contracts.IRestClient;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class TrelloServiceUnitTest : UnitTestBase<TrelloService>
	{
		public const string MockAuthKey = "mock auth key";
		public const string MockAuthToken = "mock auth token";
		public const string MockEntityId = "mock entity ID";

		#region Dependencies

		private class DependencyCollection : UnitTestBase<TrelloService>.DependencyCollection
		{
			public Mock<IRestClient> RestClient { get; private set; }

			public DependencyCollection()
			{
				RestClient = new Mock<IRestClient>();

				RestClientProvider.SetupGet(p => p.RequestProvider)
					.Returns(RequestProvider);
				RestClientProvider.Setup(p => p.CreateRestClient(It.IsAny<string>()))
					.Returns(RestClient.Object);
			}
		}
		private class ServiceUnderTest : SystemUnderTest<DependencyCollection>
		{
			public ServiceUnderTest()
			{
				Sut = new TrelloService(MockAuthKey, MockAuthToken)
				{
					RestClientProvider = Dependencies.RestClientProvider.Object
				};
			}
		}

		#endregion

		#region Data

		private ServiceUnderTest _systemUnderTest;
		private string _request;

		#endregion

		#region Scenarios

		[TestMethod]
		public void Retrieve()
		{
			var story = new Story("Retrieve");

			var feature = story.InOrderTo("retrieve data from Trello.com")
				.AsA("TrelloService consumer")
				.IWant("the requested data returned");

			feature.WithScenario("Retrieve a Board")
				.Given(AnEntityExists<Board>)
				.When(RetrieveIsCalled<Board>)
				.Then(MockExecuteIsCalled<Board>, Times.Once())
				.And(RetrieveReturns<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a List")
				.Given(AnEntityExists<List>)
				.When(RetrieveIsCalled<List>)
				.Then(MockExecuteIsCalled<List>, Times.Once())
				.And(RetrieveReturns<List>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Card")
				.Given(AnEntityExists<Card>)
				.When(RetrieveIsCalled<Card>)
				.Then(MockExecuteIsCalled<Card>, Times.Once())
				.And(RetrieveReturns<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a CheckList")
				.Given(AnEntityExists<CheckList>)
				.When(RetrieveIsCalled<CheckList>)
				.Then(MockExecuteIsCalled<CheckList>, Times.Once())
				.And(RetrieveReturns<CheckList>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve an Organization")
				.Given(AnEntityExists<Organization>)
				.When(RetrieveIsCalled<Organization>)
				.Then(MockExecuteIsCalled<Organization>, Times.Once())
				.And(RetrieveReturns<Organization>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Action")
				.Given(AnActionExists)
				.When(RetrieveIsCalled<Action>)
				.Then(MockExecuteIsCalled<Action>, Times.Once())
				.And(RetrieveReturns<Action>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Notification")
				.Given(ANotificationExists)
				.When(RetrieveIsCalled<Notification>)
				.Then(MockExecuteIsCalled<Notification>, Times.Once())
				.And(RetrieveReturns<Notification>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Member")
				.Given(AnEntityExists<Member>)
				.When(RetrieveIsCalled<Member>)
				.Then(MockExecuteIsCalled<Member>, Times.Once())
				.And(RetrieveReturns<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Caches an entity")
				.Given(AnEntityExists<Board>)
				.When(RetrieveIsCalled<Board>)
				.And(RetrieveIsCalled<Board>)
				.Then(MockExecuteIsCalled<Board>, Times.Once())
				.And(RetrieveReturns<Board>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#endregion

		#region Given

		[GenericMethodFormat("A(n) {0} exists")]
		private void AnEntityExists<T>()
			where T : JsonCompatibleExpiringObject, new()
		{
			T entity = new T {Id = MockEntityId};
			_systemUnderTest = new ServiceUnderTest();
			_systemUnderTest.Dependencies.RestClient.Setup(c => c.Execute(It.IsAny<IRestRequest<T>>()))
				.Returns(new RestSharpResponse<T>(new RestResponse<T>()) {Data = entity});
		}
		private void AnActionExists()
		{
			var entity = new Action
			             	{
			             		Id = MockEntityId,
			             		Type = ActionType.CommentCard,
			             		Data = new Manatee.Json.JsonObject()
			             	};
			_systemUnderTest = new ServiceUnderTest();
			_systemUnderTest.Dependencies.RestClient.Setup(c => c.Execute(It.IsAny<IRestRequest<Action>>()))
				.Returns(new RestSharpResponse<Action>(new RestResponse<Action>()) {Data = entity});
		}
		private void ANotificationExists()
		{
			var entity = new Notification
			             	{
			             		Id = MockEntityId,
								Type = NotificationType.CommentCard,
			             		Data = new Manatee.Json.JsonObject()
			             	};
			_systemUnderTest = new ServiceUnderTest();
			_systemUnderTest.Dependencies.RestClient.Setup(c => c.Execute(It.IsAny<IRestRequest<Notification>>()))
				.Returns(new RestSharpResponse<Notification>(new RestResponse<Notification>()) {Data = entity});
		}

		#endregion

		#region When

		[GenericMethodFormat("Retrieve<{0}>() is called")]
		private void RetrieveIsCalled<T>()
			where T : ExpiringObject, new()
		{
			Execute(() => _systemUnderTest.Sut.Retrieve<T>(MockEntityId));
		}

		#endregion

		#region Then

		[GenericMethodFormat("Execute<{0}>() is called")]
		private void MockExecuteIsCalled<T>(Times times)
			where T : ExpiringObject, new()
		{
			_systemUnderTest.Dependencies.RestClient.Verify(c => c.Execute(It.IsAny<IRestRequest<T>>()), times);
		}
		private void RetrieveReturns<T>()
			where T : ExpiringObject, new()
		{
			var actual = _actualResult as T;
			Assert.IsNotNull(actual);
			Assert.AreEqual(MockEntityId, actual.Id);
		}

		#endregion
	}
}

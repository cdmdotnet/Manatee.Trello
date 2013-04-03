using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
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
	public class TrelloServiceUnitTest
	{
		public const string MockAuthKey = "mock auth key";
		public const string MockAuthToken = "mock auth token";
		public const string MockEntityId = "mock entity ID";

		#region Dependencies

		private class DependencyCollection
		{
			public class MockRequestProvider : IRestRequestProvider
			{
				public IRestRequest<T> Create<T>() where T : ExpiringObject, new()
				{
					return new Mock<IRestRequest<T>>().Object;
				}
				public IRestRequest<T> Create<T>(string id) where T : ExpiringObject, new()
				{
					var mock = new Mock<IRestRequest<T>>();
					mock.SetupGet(r => r.Template).Returns(new T {Id = id});
					return mock.Object;
				}
				public IRestRequest<T> Create<T>(ExpiringObject obj) where T : ExpiringObject, new()
				{
					var mock = new Mock<IRestRequest<T>>();
					mock.SetupGet(r => r.Template).Returns(new T { Id = obj.Id });
					return mock.Object;
				}
				public IRestRequest<T> Create<T>(IEnumerable<ExpiringObject> tokens, ExpiringObject entity, string urlExtension) where T : ExpiringObject, new()
				{
					return new Mock<IRestRequest<T>>().Object;
				}
				public IRestCollectionRequest<T> CreateCollectionRequest<T>(IEnumerable<ExpiringObject> tokens, ExpiringObject entity) where T : ExpiringObject, new()
				{
					return new Mock<IRestCollectionRequest<T>>().Object;
				}
			}

			public Mock<IRestClient> RestClient { get; private set; }
			public Mock<IRestClientProvider> RestClientProvider { get; private set; }
			public MockRequestProvider RequestProvider { get; private set; }

			public DependencyCollection()
			{
				RestClient = new Mock<IRestClient>();
				RestClientProvider = new Mock<IRestClientProvider>();
				RequestProvider = new MockRequestProvider();

				RestClientProvider.SetupGet(p => p.RequestProvider)
					.Returns(RequestProvider);
				RestClientProvider.Setup(p => p.CreateRestClient(It.IsAny<string>()))
					.Returns(RestClient.Object);
			}
		}
		private class SystemUnderTest
		{
			public DependencyCollection Dependencies { get; private set; }
			public TrelloService Sut { get; private set; }

			public SystemUnderTest()
			{
				Dependencies = new DependencyCollection();
				Sut = new TrelloService(MockAuthKey, MockAuthToken)
				{
					RestClientProvider = Dependencies.RestClientProvider.Object
				};
			}
		}

		#endregion

		#region Data

		private SystemUnderTest _serviceGroup;
		private Exception _exception;
		private object _actualResult;
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

		private void AnEntityExists<T>()
			where T : JsonCompatibleExpiringObject, new()
		{
			T entity = new T {Id = MockEntityId};
			_serviceGroup = new SystemUnderTest();
			_serviceGroup.Dependencies.RestClient.Setup(c => c.Execute(It.IsAny<IRestRequest<T>>()))
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
			_serviceGroup = new SystemUnderTest();
			_serviceGroup.Dependencies.RestClient.Setup(c => c.Execute(It.IsAny<IRestRequest<Action>>()))
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
			_serviceGroup = new SystemUnderTest();
			_serviceGroup.Dependencies.RestClient.Setup(c => c.Execute(It.IsAny<IRestRequest<Notification>>()))
				.Returns(new RestSharpResponse<Notification>(new RestResponse<Notification>()) {Data = entity});
		}

		#endregion

		#region When

		private void RetrieveIsCalled<T>()
			where T : ExpiringObject, new()
		{
			_request = MockEntityId;
			_exception = null;

			try
			{
				_actualResult = _serviceGroup.Sut.Retrieve<T>(_request);
			}
			catch (Exception e)
			{
				_exception = e;
			}
		}

		#endregion

		#region Then

		private void ExceptionIsNotThrown()
		{
			Assert.IsNull(_exception);
		}
		private void ExceptionIsThrown<T>()
			where T : Exception
		{
			Assert.IsNotNull(_exception);
			Assert.IsTrue(_exception is T);
		}
		private void MockExecuteIsCalled<T>(Times times)
			where T : ExpiringObject, new()
		{
			_serviceGroup.Dependencies.RestClient.Verify(c => c.Execute(It.IsAny<IRestRequest<T>>()), times);
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

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using StoryQ;
using IRestClient = Manatee.Trello.Rest.IRestClient;

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
			public Mock<ICache> EntityCache { get; private set; }

			public DependencyCollection()
			{
				RestClient = new Mock<IRestClient>();
				EntityCache = new Mock<ICache>();

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
					RestClientProvider = Dependencies.RestClientProvider.Object,
					Cache = Dependencies.EntityCache.Object
				};
			}
		}

		#endregion

		#region Data

		private ServiceUnderTest _systemUnderTest;

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
				.Given(AnEntityExists<Board, IJsonBoard>)
				.When(RetrieveIsCalled<Board>)
				.Then(MockExecuteIsCalled<IJsonBoard>, 1)
				.And(MockCacheAddIsCalled<Board>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a List")
				.Given(AnEntityExists<List, IJsonList>)
				.When(RetrieveIsCalled<List>)
				.Then(MockExecuteIsCalled<IJsonList>, 1)
				.And(MockCacheAddIsCalled<List>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<List>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Card")
				.Given(AnEntityExists<Card, IJsonCard>)
				.When(RetrieveIsCalled<Card>)
				.Then(MockExecuteIsCalled<IJsonCard>, 1)
				.And(MockCacheAddIsCalled<Card>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a CheckList")
				.Given(AnEntityExists<CheckList, IJsonCheckList>)
				.When(RetrieveIsCalled<CheckList>)
				.Then(MockExecuteIsCalled<IJsonCheckList>, 1)
				.And(MockCacheAddIsCalled<CheckList>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<CheckList>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve an Organization")
				.Given(AnEntityExists<Organization, IJsonOrganization>)
				.When(RetrieveIsCalled<Organization>)
				.Then(MockExecuteIsCalled<IJsonOrganization>, 1)
				.And(MockCacheAddIsCalled<Organization>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<Organization>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Action")
				.Given(AnEntityExists<Action, IJsonAction>)
				.When(RetrieveIsCalled<Action>)
				.Then(MockExecuteIsCalled<IJsonAction>, 1)
				.And(MockCacheAddIsCalled<Action>, 1)
				.And(ResponseIs<Action>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Notification")
				.Given(AnEntityExists<Notification, IJsonNotification>)
				.When(RetrieveIsCalled<Notification>)
				.Then(MockExecuteIsCalled<IJsonNotification>, 1)
				.And(MockCacheAddIsCalled<Notification>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<Notification>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Member")
				.Given(AnEntityExists<Member, IJsonMember>)
				.When(RetrieveIsCalled<Member>)
				.Then(MockExecuteIsCalled<IJsonMember>, 1)
				.And(MockCacheAddIsCalled<Member>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Returns cached entity")
				.Given(AnEntityExists<Board, IJsonBoard>)
				.And(ItemExistsInCache<Board>)
				.When(RetrieveIsCalled<Board>)
				.Then(MockExecuteIsCalled<IJsonBoard>, 0)
				.And(MockCacheAddIsCalled<Board>, 0)
				.And(ResponseIsNotNull)
				.And(ResponseIs<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Invalid ID returns null")
				.Given(AnEntityDoesNotExist<Board, IJsonBoard>)
				.When(RetrieveIsCalled<Board>)
				.Then(MockExecuteIsCalled<IJsonBoard>, 1)
				.And(ResponseIsNull)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#endregion

		#region Given

		[GenericMethodFormat("A(n) {0} exists")]
		private void AnEntityExists<T, TJ>() where T : class where TJ : class
		{
			_systemUnderTest = new ServiceUnderTest();
			_systemUnderTest.Dependencies.RestClient.Setup(c => c.Execute(It.IsAny<IRestRequest<TJ>>()))
				.Returns(new RestSharpResponse<TJ>(new RestResponse(), new Mock<TJ>().Object));
			_systemUnderTest.Dependencies.EntityCache.Setup(c => c.Add(It.IsAny<T>()))
				.Callback(ItemExistsInCache<T>);
		}
		[GenericMethodFormat("The cache contains a(n) {0}")]
		private void ItemExistsInCache<T>() where T : class
		{
			_systemUnderTest.Dependencies.EntityCache.Setup(c => c.Find(It.IsAny<Func<T, bool>>()))
				.Returns(new Mock<T>().Object);
		}
		[GenericMethodFormat("A(n) {0} does not exist")]
		private void AnEntityDoesNotExist<T, TJ>() where T : class where TJ : class
		{
			_systemUnderTest = new ServiceUnderTest();
			_systemUnderTest.Dependencies.RestClient.Setup(c => c.Execute(It.IsAny<IRestRequest<TJ>>()))
				.Returns(new RestSharpResponse<TJ>(new RestResponse(), null));
			_systemUnderTest.Dependencies.EntityCache.Setup(c => c.Add(It.IsAny<T>()))
				.Callback(ItemExistsInCache<T>);
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

		[GenericMethodFormat("Api.Execute<{0}>() is called {1} time(s)")]
		private void MockExecuteIsCalled<T>(int times)
			where T : class
		{
			_systemUnderTest.Dependencies.RestClient.Verify(c => c.Execute(It.IsAny<IRestRequest<T>>()), Times.Exactly(times));
		}
		[GenericMethodFormat("Cache.Add<{0}>() is called {1} time(s)")]
		private void MockCacheAddIsCalled<T>(int times)
		{
			_systemUnderTest.Dependencies.EntityCache.Verify(c => c.Add(It.IsAny<T>()), Times.Exactly(times));
		}

		#endregion
	}
}

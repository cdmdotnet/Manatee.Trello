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
using IRestRequest = Manatee.Trello.Rest.IRestRequest;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class TrelloServiceUnitTest : UnitTestBase<TrelloService>
	{
		public const string MockAuthKey = "mock auth key";
		public const string MockAuthToken = "mock auth token";
		public const string MockEntityId = "mock entity ID";

		#region Dependencies

		private new class DependencyCollection : UnitTestBase<TrelloService>.DependencyCollection
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
				.When(RetrieveIsCalled<Board>, MockEntityId)
				.Then(MockExecuteIsCalled<IJsonBoard>, 1)
				.And(MockCacheAddIsCalled<Board>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a List")
				.Given(AnEntityExists<List, IJsonList>)
				.When(RetrieveIsCalled<List>, MockEntityId)
				.Then(MockExecuteIsCalled<IJsonList>, 1)
				.And(MockCacheAddIsCalled<List>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<List>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Card")
				.Given(AnEntityExists<Card, IJsonCard>)
				.When(RetrieveIsCalled<Card>, MockEntityId)
				.Then(MockExecuteIsCalled<IJsonCard>, 1)
				.And(MockCacheAddIsCalled<Card>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a CheckList")
				.Given(AnEntityExists<CheckList, IJsonCheckList>)
				.When(RetrieveIsCalled<CheckList>, MockEntityId)
				.Then(MockExecuteIsCalled<IJsonCheckList>, 1)
				.And(MockCacheAddIsCalled<CheckList>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<CheckList>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve an Organization")
				.Given(AnEntityExists<Organization, IJsonOrganization>)
				.When(RetrieveIsCalled<Organization>, MockEntityId)
				.Then(MockExecuteIsCalled<IJsonOrganization>, 1)
				.And(MockCacheAddIsCalled<Organization>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<Organization>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Action")
				.Given(AnEntityExists<Action, IJsonAction>)
				.When(RetrieveIsCalled<Action>, MockEntityId)
				.Then(MockExecuteIsCalled<IJsonAction>, 1)
				.And(MockCacheAddIsCalled<Action>, 1)
				.And(ResponseIs<Action>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Notification")
				.Given(AnEntityExists<Notification, IJsonNotification>)
				.When(RetrieveIsCalled<Notification>, MockEntityId)
				.Then(MockExecuteIsCalled<IJsonNotification>, 1)
				.And(MockCacheAddIsCalled<Notification>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<Notification>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Member")
				.Given(AnEntityExists<Member, IJsonMember>)
				.When(RetrieveIsCalled<Member>, MockEntityId)
				.Then(MockExecuteIsCalled<IJsonMember>, 1)
				.And(MockCacheAddIsCalled<Member>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Returns cached entity")
				.Given(AnEntityExists<Board, IJsonBoard>)
				.And(ItemExistsInCache<Board>)
				.When(RetrieveIsCalled<Board>, MockEntityId)
				.Then(MockExecuteIsCalled<IJsonBoard>, 0)
				.And(MockCacheAddIsCalled<Board>, 0)
				.And(ResponseIsNotNull)
				.And(ResponseIs<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Invalid ID returns null")
				.Given(AnEntityDoesNotExist<Board, IJsonBoard>)
				.When(RetrieveIsCalled<Board>, MockEntityId)
				.Then(MockExecuteIsCalled<IJsonBoard>, 1)
				.And(ResponseIsNull)
				.And(ExceptionIsNotThrown)

				.WithScenario("Null string throws")
				.Given(AnEntityExists<Board, IJsonBoard>)
				.When(SearchIsCalled, (string)null)
				.Then(MockExecuteIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Empty string throws")
				.Given(AnEntityExists<Board, IJsonBoard>)
				.When(SearchIsCalled, string.Empty)
				.Then(MockExecuteIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Whitespace string throws")
				.Given(AnEntityExists<Board, IJsonBoard>)
				.When(SearchIsCalled, "    ")
				.Then(MockExecuteIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.Execute();
		}
		[TestMethod]
		public void Search()
		{
			var story = new Story("Search");

			var feature = story.InOrderTo("search entities on Trello.com")
				.AsA("TrelloService consumer")
				.IWant("all entities which contain data matching my query");

			feature.WithScenario("Search a string")
				.Given(SearchableEntitiesExist)
				.When(SearchIsCalled, "search")
				.Then(MockExecuteIsCalled<IJsonSearchResults>, 1)
				.And(MockCacheAddIsCalled<Action>, 1)
				.And(MockCacheAddIsCalled<Board>, 1)
				.And(MockCacheAddIsCalled<Card>, 1)
				.And(MockCacheAddIsCalled<Member>, 1)
				.And(MockCacheAddIsCalled<Organization>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<SearchResults>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Null string throws")
				.Given(SearchableEntitiesExist)
				.When(SearchIsCalled, (string)null)
				.Then(MockExecuteIsCalled<IJsonSearchResults>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Empty string throws")
				.Given(SearchableEntitiesExist)
				.When(SearchIsCalled, string.Empty)
				.Then(MockExecuteIsCalled<IJsonSearchResults>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Whitespace string throws")
				.Given(SearchableEntitiesExist)
				.When(SearchIsCalled, "    ")
				.Then(MockExecuteIsCalled<IJsonSearchResults>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.Execute();
		}
		[TestMethod]
		public void SearchMembers()
		{
			var story = new Story("Search");

			var feature = story.InOrderTo("search members on Trello.com")
				.AsA("TrelloService consumer")
				.IWant("all members which contain data matching my query");

			feature.WithScenario("Search a string")
				.Given(SearchableMembersExist)
				.When(SearchMembersIsCalled, "search")
				.Then(MockExecuteIsCalled<List<IJsonMember>>, 1)
				.And(MockCacheAddIsCalled<Member>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<IEnumerable<Member>>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Null string throws")
				.Given(SearchableMembersExist)
				.When(SearchMembersIsCalled, (string)null)
				.Then(MockExecuteIsCalled<List<IJsonMember>>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Empty string throws")
				.Given(SearchableMembersExist)
				.When(SearchMembersIsCalled, string.Empty)
				.Then(MockExecuteIsCalled<List<IJsonMember>>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Whitespace string throws")
				.Given(SearchableMembersExist)
				.When(SearchMembersIsCalled, "    ")
				.Then(MockExecuteIsCalled<List<IJsonMember>>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.Execute();
		}

		#endregion

		#region Given

		[GenericMethodFormat("A(n) {0} exists")]
		private void AnEntityExists<T, TJ>() where T : class where TJ : class
		{
			_systemUnderTest = new ServiceUnderTest();
			_systemUnderTest.Dependencies.RestClient.Setup(c => c.Execute<TJ>(It.IsAny<IRestRequest>()))
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
			_systemUnderTest.Dependencies.RestClient.Setup(c => c.Execute<TJ>(It.IsAny<IRestRequest>()))
				.Returns(new RestSharpResponse<TJ>(new RestResponse(), null));
			_systemUnderTest.Dependencies.EntityCache.Setup(c => c.Add(It.IsAny<T>()))
				.Callback(ItemExistsInCache<T>);
		}
		private void SearchableEntitiesExist()
		{
			var searchResults = new Mock<IJsonSearchResults>();
			searchResults.SetupGet(s => s.ActionIds).Returns(new List<string> { TrelloIds.ActionId });
			searchResults.SetupGet(s => s.BoardIds).Returns(new List<string> { TrelloIds.BoardId });
			searchResults.SetupGet(s => s.CardIds).Returns(new List<string> { TrelloIds.CardId });
			searchResults.SetupGet(s => s.MemberIds).Returns(new List<string> { TrelloIds.MemberId });
			searchResults.SetupGet(s => s.OrganizationIds).Returns(new List<string> { TrelloIds.OrganizationId });
			_systemUnderTest = new ServiceUnderTest();
			_systemUnderTest.Dependencies.RestClient.Setup(c => c.Execute<IJsonSearchResults>(It.IsAny<IRestRequest>()))
				.Returns(new RestSharpResponse<IJsonSearchResults>(new RestResponse(), searchResults.Object));
		}
		private void SearchableMembersExist()
		{
			var member = new Mock<IJsonMember>();
			_systemUnderTest = new ServiceUnderTest();
			_systemUnderTest.Dependencies.RestClient.Setup(c => c.Execute<List<IJsonMember>>(It.IsAny<IRestRequest>()))
				.Returns(new RestSharpResponse<List<IJsonMember>>(new RestResponse(), new List<IJsonMember> {member.Object}));
		}

		#endregion

		#region When

		[GenericMethodFormat("Retrieve<{0}>() is called")]
		private void RetrieveIsCalled<T>(string value)
			where T : ExpiringObject, new()
		{
			Execute(() => _systemUnderTest.Sut.Retrieve<T>(value));
		}
		private void SearchIsCalled(string value)
		{
			Execute(() => _systemUnderTest.Sut.Search(value));
		}
		private void SearchMembersIsCalled(string value)
		{
			Execute(() => _systemUnderTest.Sut.SearchMembers(value).ToList());
		}

		#endregion

		#region Then

		[GenericMethodFormat("Api.Execute<{0}>() is called {1} time(s)")]
		private void MockExecuteIsCalled<T>(int times)
			where T : class
		{
			_systemUnderTest.Dependencies.RestClient.Verify(c => c.Execute<T>(It.IsAny<IRestRequest>()), Times.Exactly(times));
		}
		[GenericMethodFormat("Cache.Add<{0}>() is called {1} time(s)")]
		private void MockCacheAddIsCalled<T>(int times)
		{
			_systemUnderTest.Dependencies.EntityCache.Verify(c => c.Add(It.IsAny<T>()), Times.Exactly(times));
		}

		#endregion
	}
}

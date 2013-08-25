using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Internal.RequestProcessing;
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
		public const string MockAppKey = "mock app key";
		public const string MockUserToken = "mock user token";
		public const string MockEntityId = "mock entity ID";

		#region Dependencies

		private class MockEntity : ExpiringObject
		{
			public int RefreshCallCount { get; private set; }

			internal override void ApplyJson(object obj) {}

			public override bool Refresh()
			{
				RefreshCallCount++;
				return true;
			}
			protected override void PropagateService() {}
		}
		private class MockRequestProcessor : IRestRequestProcessor
		{
			public bool IsActive { get; set; }
			public string AppKey { get; private set; }
			public string UserToken { get; set; }
			public void AddRequest<T>(IRestRequest request) where T : class
			{
				if (IsActive)
					request.Response = new RestSharp.RestSharpResponse<T>(null, new Mock<T>().Object);
			}
			public void ShutDown() {}
		}

		private new class DependencyCollection : UnitTestBase<TrelloService>.DependencyCollection 
		{
			public Mock<IEndpointFactory> EndpointFactory { get; private set; }
			public MockRequestProcessor RequestProcessor { get; set; }
			public new Mock<IRestClient> RestClient { get; private set; }
			public MockEntity Entity { get; private set; }

			public DependencyCollection()
			{
				RequestProcessor = new MockRequestProcessor();
				RestClient = new Mock<IRestClient>();
				Entity = new MockEntity();

				//Config.SetupGet(c => c.ItemDuration)
				//	  .Returns(TimeSpan.MaxValue);
				RestClientProvider.Setup(p => p.CreateRestClient(It.IsAny<string>()))
								  .Returns(RestClient.Object);
			}
		}

		private class ServiceUnderTest : SystemUnderTest<DependencyCollection>
		{
			public ServiceUnderTest()
			{
				Sut = new TrelloService(Dependencies.Config.Object,
				                        Dependencies.Validator.Object,
										Dependencies.EntityRepository.Object,
				                        Dependencies.RequestProcessor,
				                        Dependencies.EndpointFactory.Object);
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
				.Then(MockGetIsCalled<IJsonBoard>, 1)
				.And(MockCacheFindIsCalled<Board>, 1)
				.And(ResponseIs<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a List")
				.Given(AnEntityExists<List, IJsonList>)
				.When(RetrieveIsCalled<List>, MockEntityId)
				.Then(MockGetIsCalled<IJsonList>, 1)
				.And(MockCacheFindIsCalled<List>, 1)
				.And(ResponseIs<List>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Card")
				.Given(AnEntityExists<Card, IJsonCard>)
				.When(RetrieveIsCalled<Card>, MockEntityId)
				.Then(MockGetIsCalled<IJsonCard>, 1)
				.And(MockCacheFindIsCalled<Card>, 1)
				.And(ResponseIs<Card>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a CheckList")
				.Given(AnEntityExists<CheckList, IJsonCheckList>)
				.When(RetrieveIsCalled<CheckList>, MockEntityId)
				.Then(MockGetIsCalled<IJsonCheckList>, 1)
				.And(MockCacheFindIsCalled<CheckList>, 1)
				.And(ResponseIs<CheckList>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve an Organization")
				.Given(AnEntityExists<Organization, IJsonOrganization>)
				.When(RetrieveIsCalled<Organization>, MockEntityId)
				.Then(MockGetIsCalled<IJsonOrganization>, 1)
				.And(MockCacheFindIsCalled<Organization>, 1)
				.And(ResponseIs<Organization>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Action")
				.Given(AnEntityExists<Action, IJsonAction>)
				.When(RetrieveIsCalled<Action>, MockEntityId)
				.Then(MockGetIsCalled<IJsonAction>, 1)
				.And(MockCacheFindIsCalled<Action>, 1)
				.And(ResponseIs<Action>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Notification")
				.Given(AnEntityExists<Notification, IJsonNotification>)
				.When(RetrieveIsCalled<Notification>, MockEntityId)
				.Then(MockGetIsCalled<IJsonNotification>, 1)
				.And(MockCacheFindIsCalled<Notification>, 1)
				.And(ResponseIs<Notification>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Member")
				.Given(AnEntityExists<Member, IJsonMember>)
				.When(RetrieveIsCalled<Member>, MockEntityId)
				.Then(MockGetIsCalled<IJsonMember>, 1)
				.And(MockCacheFindIsCalled<Member>, 1)
				.And(ResponseIs<Member>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Token")
				.Given(ATokenExists)
				.When(RetrieveIsCalled<Token>, MockUserToken)
				.Then(MockGetIsCalled<IJsonToken>, 1)
				.And(ResponseIs<Token>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Returns cached entity")
				.Given(AnEntityExists<Board, IJsonBoard>)
				.And(ItemExistsInCache<Board>)
				.When(RetrieveIsCalled<Board>, MockEntityId)
				.Then(MockGetIsCalled<IJsonBoard>, 0)
				.And(MockCacheFindIsCalled<Board>, 1)
				.And(ResponseIs<Board>)
				.And(ExceptionIsNotThrown)

				// TrelloService will currently return an object with an invalid ID, which will in turn
				// throw an exception when trying to refresh.
				//.WithScenario("Invalid ID returns null")
				//.Given(AnEntityDoesNotExist<Board, IJsonBoard>)
				//.When(RetrieveIsCalled<Board>, MockEntityId)
				//.Then(MockGetIsCalled<IJsonBoard>, 1)
				//.And(ResponseIsNull)
				//.And(ExceptionIsThrown<EntityNotOnTrelloException<Board>>)

				.WithScenario("Null string throws")
				.Given(AnEntityExists<Board, IJsonBoard>)
				.When(SearchIsCalled, (string)null)
				.Then(MockGetIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Empty string throws")
				.Given(AnEntityExists<Board, IJsonBoard>)
				.When(SearchIsCalled, string.Empty)
				.Then(MockGetIsCalled<IJsonBoard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Whitespace string throws")
				.Given(AnEntityExists<Board, IJsonBoard>)
				.When(SearchIsCalled, "    ")
				.Then(MockGetIsCalled<IJsonBoard>, 0)
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
				.Then(MockGetIsCalled<IJsonSearchResults>, 1)
				.And(MockCacheFindIsCalled<Action>, 1)
				.And(MockCacheFindIsCalled<Board>, 1)
				.And(MockCacheFindIsCalled<Card>, 1)
				.And(MockCacheFindIsCalled<Member>, 1)
				.And(MockCacheFindIsCalled<Organization>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<SearchResults>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Null string throws")
				.Given(SearchableEntitiesExist)
				.When(SearchIsCalled, (string)null)
				.Then(MockGetIsCalled<IJsonSearchResults>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Empty string throws")
				.Given(SearchableEntitiesExist)
				.When(SearchIsCalled, string.Empty)
				.Then(MockGetIsCalled<IJsonSearchResults>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Whitespace string throws")
				.Given(SearchableEntitiesExist)
				.When(SearchIsCalled, "    ")
				.Then(MockGetIsCalled<IJsonSearchResults>, 0)
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
				.Then(MockGetIsCalled<List<IJsonMember>>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<IEnumerable<Member>>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Null string throws")
				.Given(SearchableMembersExist)
				.When(SearchMembersIsCalled, (string)null)
				.Then(MockGetIsCalled<List<IJsonMember>>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Empty string throws")
				.Given(SearchableMembersExist)
				.When(SearchMembersIsCalled, string.Empty)
				.Then(MockGetIsCalled<List<IJsonMember>>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Whitespace string throws")
				.Given(SearchableMembersExist)
				.When(SearchMembersIsCalled, "    ")
				.Then(MockGetIsCalled<List<IJsonMember>>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.Execute();
		}

		#endregion

		#region Given

		[GenericMethodFormat("A(n) {0} exists")]
		private void AnEntityExists<T, TJ>() where T : class where TJ : class
		{
			_systemUnderTest = new ServiceUnderTest();
			var mockJsonObject = new Mock<TJ>();
			mockJsonObject.SetupAllProperties();
			_systemUnderTest.Dependencies.JsonRepository.Setup(c => c.Get<TJ>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
			                .Returns(mockJsonObject.Object);
			_systemUnderTest.Dependencies.Cache.Setup(c => c.Add(It.IsAny<T>()))
			                .Callback(ItemExistsInCache<T>);
			_systemUnderTest.Dependencies.Cache.Setup(c => c.Find(It.IsAny<Func<T, bool>>(), It.IsAny<Func<T>>()))
			                .Returns((Func<T, bool> match, Func<T> fetch) => fetch());
		}
		private void ATokenExists()
		{
			var mock = new Mock<IJsonToken>();
			mock.SetupAllProperties();
			mock.Object.Permissions = new List<IJsonTokenPermission>();
			_systemUnderTest = new ServiceUnderTest();
			_systemUnderTest.Dependencies.JsonRepository.Setup(c => c.Get<IJsonToken>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
			                .Returns(mock.Object);
			_systemUnderTest.Dependencies.Cache.Setup(c => c.Add(It.IsAny<Token>()))
			                .Callback(ItemExistsInCache<Token>);
			_systemUnderTest.Dependencies.Cache.Setup(c => c.Find(It.IsAny<Func<Token, bool>>(), It.IsAny<Func<Token>>()))
							.Returns((Func<Token, bool> match, Func<Token> fetch) => fetch());
		}
		[GenericMethodFormat("The cache contains a(n) {0}")]
		private void ItemExistsInCache<T>() where T : class
		{
			_systemUnderTest.Dependencies.Cache.Setup(c => c.Find(It.IsAny<Func<T, bool>>(), It.IsAny<Func<T>>()))
			                .Returns(new Mock<T>().Object);
		}
		[GenericMethodFormat("A(n) {0} does not exist")]
		private void AnEntityDoesNotExist<T, TJ>() where T : class where TJ : class
		{
			_systemUnderTest = new ServiceUnderTest();
			_systemUnderTest.Dependencies.JsonRepository.Setup(c => c.Get<TJ>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
							.Returns((TJ)null);
			_systemUnderTest.Dependencies.Cache.Setup(c => c.Add(It.IsAny<T>()))
							.Callback(ItemExistsInCache<T>);
			_systemUnderTest.Dependencies.Cache.Setup(c => c.Find(It.IsAny<Func<T, bool>>(), It.IsAny<Func<T>>()))
							.Returns((Func<T, bool> match, Func<T> fetch) => fetch());
		}
		private void SearchableEntitiesExist()
		{
			var searchResults = new Mock<IJsonSearchResults>();
			searchResults.SetupGet(s => s.ActionIds).Returns(new List<string> {TrelloIds.ActionId});
			searchResults.SetupGet(s => s.BoardIds).Returns(new List<string> {TrelloIds.BoardId});
			searchResults.SetupGet(s => s.CardIds).Returns(new List<string> {TrelloIds.CardId});
			searchResults.SetupGet(s => s.MemberIds).Returns(new List<string> {TrelloIds.MemberId});
			searchResults.SetupGet(s => s.OrganizationIds).Returns(new List<string> {TrelloIds.OrganizationId});
			var mockAction = new Mock<IJsonAction>();
			mockAction.SetupGet(a => a.Id).Returns(TrelloIds.ActionId);
			var mockBoard = new Mock<IJsonBoard>();
			mockBoard.SetupGet(a => a.Id).Returns(TrelloIds.BoardId);
			var mockCard = new Mock<IJsonCard>();
			mockCard.SetupGet(a => a.Id).Returns(TrelloIds.CardId);
			var mockMember = new Mock<IJsonMember>();
			mockMember.SetupGet(a => a.Id).Returns(TrelloIds.MemberId);
			var mockOrganization = new Mock<IJsonOrganization>();
			mockOrganization.SetupGet(a => a.Id).Returns(TrelloIds.OrganizationId);
			_systemUnderTest = new ServiceUnderTest();
			_systemUnderTest.Dependencies.JsonRepository.Setup(c => c.Get<IJsonSearchResults>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
				.Returns(searchResults.Object);
			_systemUnderTest.Dependencies.JsonRepository.Setup(c => c.Get<IJsonAction>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
				.Returns(mockAction.Object);
			_systemUnderTest.Dependencies.JsonRepository.Setup(c => c.Get<IJsonBoard>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
				.Returns(mockBoard.Object);
			_systemUnderTest.Dependencies.JsonRepository.Setup(c => c.Get<IJsonCard>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
				.Returns(mockCard.Object);
			_systemUnderTest.Dependencies.JsonRepository.Setup(c => c.Get<IJsonMember>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
				.Returns(mockMember.Object);
			_systemUnderTest.Dependencies.JsonRepository.Setup(c => c.Get<IJsonOrganization>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
				.Returns(mockOrganization.Object);
		}
		private void SearchableMembersExist()
		{
			var member = new Mock<IJsonMember>();
			_systemUnderTest = new ServiceUnderTest();
			_systemUnderTest.Dependencies.JsonRepository.Setup(c => c.Get<List<IJsonMember>>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
			                .Returns(new List<IJsonMember> {member.Object});
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

		private void MockEntityRefreshIsCalled(int count)
		{
			Assert.AreEqual(count, _systemUnderTest.Dependencies.Entity.RefreshCallCount);
		}
		[GenericMethodFormat("Api.Get<{0}>() is called {1} time(s)")]
		private void MockGetIsCalled<T>(int times)
			where T : class
		{
			_systemUnderTest.Dependencies.JsonRepository.Verify(c => c.Get<T>(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()), Times.Exactly(times));
		}
		[GenericMethodFormat("RestClient.Execute<{0}>() is called {1} time(s)")]
		private void MockExecuteIsCalled<T>(int times)
			where T : class
		{
			_systemUnderTest.Dependencies.RestClient.Verify(c => c.Execute<T>(It.IsAny<IRestRequest>()), Times.Exactly(times));
		}
		[GenericMethodFormat("Cache.Find<{0}>() is called {1} time(s)")]
		private void MockCacheFindIsCalled<T>(int times)
		{
			_systemUnderTest.Dependencies.Cache.Verify(c => c.Find(It.IsAny<Func<T, bool>>(), It.IsAny<Func<T>>()), Times.Exactly(times));
		}

		#endregion
	}
}

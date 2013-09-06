using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Json;
using Manatee.Trello.Test.Unit.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using StoryQ;
using IRestClient = Manatee.Trello.Rest.IRestClient;
using IRestRequest = Manatee.Trello.Rest.IRestRequest;

namespace Manatee.Trello.Test.Unit
{
	[TestClass]
	public class TrelloServiceUnitTest : TrelloTestBase<TrelloService>
	{
		public const string MockAppKey = "mock app key";
		public const string MockUserToken = "mock user token";
		public const string MockEntityId = "mock entity ID";

		#region Dependencies

		private class MockEntity : ExpiringObject
		{
			public int RefreshCallCount { get; private set; }
			public override bool IsStubbed { get { return false; } }
			public override bool Refresh()
			{
				RefreshCallCount++;
				return true;
			}
			internal override void ApplyJson(object obj) { }
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
			public void AddRequest<T>(IRestRequest request, ExpiringObject entity) where T : class
			{
				if (IsActive)
					request.Response = new RestSharp.RestSharpResponse<T>(null, new Mock<T>().Object);
			}
			public void ShutDown() {}
			public void NetworkStatusChanged(object sender, EventArgs e) {}
		}

		private class DependencyCollection
		{
			public Mock<IEndpointFactory> EndpointFactory { get; private set; }
			public MockRequestProcessor RequestProcessor { get; set; }
			public Mock<ITrelloServiceConfiguration> Config { get; private set; }
			public MockValidator Validator { get; private set; }
			public Mock<IEntityRepository> EntityRepository { get; private set; }
			public Mock<ICache> Cache { get; private set; }
			public Mock<ILog> Log { get; private set; }

			public DependencyCollection()
			{
				EndpointFactory = new Mock<IEndpointFactory>();
				RequestProcessor = new MockRequestProcessor();
				Config = new Mock<ITrelloServiceConfiguration>();
				Validator = new MockValidator();
				EntityRepository = new Mock<IEntityRepository>();
				Cache = new Mock<ICache>();
				Log = new Mock<ILog>();

				Config.SetupGet(c => c.Cache).Returns(Cache.Object);
				Config.SetupGet(c => c.Log).Returns(Log.Object);
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
				.Given(AnEntityExists<Board>)
				.When(RetrieveIsCalled<Board>, MockEntityId)
				.Then(RepositoryDownloadIsCalled<Board>, 1)
				.And(CacheFindIsCalled<Board>, 1)
				.And(ResponseIs<Board>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Returns cached entity")
				.Given(AnEntityExists<Board>)
				.And(ItemExistsInCache<Board>)
				.When(RetrieveIsCalled<Board>, MockEntityId)
				.Then(RepositoryDownloadIsCalled<Board>, 0)
				.And(CacheFindIsCalled<Board>, 1)
				.And(ResponseIs<Board>)
				.And(ExceptionIsNotThrown)

				//.WithScenario("Invalid ID returns null")
				//.Given(AnEntityDoesNotExist<Board, IJsonBoard>)
				//.When(RetrieveIsCalled<Board>, MockEntityId)
				//.Then(RepositoryDownloadIsCalled<Board>, 1)
				//.And(ResponseIsNull)
				//.And(ExceptionIsThrown<EntityNotOnTrelloException<Board>>)

				.WithScenario("Null string throws")
				.Given(AnEntityExists<Board>)
				.When(SearchIsCalled, (string)null)
				.Then(RepositoryDownloadIsCalled<Board>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Empty string throws")
				.Given(AnEntityExists<Board>)
				.When(SearchIsCalled, string.Empty)
				.Then(RepositoryDownloadIsCalled<Board>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Whitespace string throws")
				.Given(AnEntityExists<Board>)
				.When(SearchIsCalled, "    ")
				.Then(RepositoryDownloadIsCalled<Board>, 0)
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
				.Then(RepositoryDownloadIsCalled<SearchResults>, 1)
				//.And(CacheFindIsCalled<Action>, 1)
				//.And(CacheFindIsCalled<Board>, 1)
				//.And(CacheFindIsCalled<Card>, 1)
				//.And(CacheFindIsCalled<Member>, 1)
				//.And(CacheFindIsCalled<Organization>, 1)
				.And(ResponseIsNotNull)
				.And(ResponseIs<SearchResults>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Null string throws")
				.Given(SearchableEntitiesExist)
				.When(SearchIsCalled, (string)null)
				.Then(RepositoryDownloadIsCalled<SearchResults>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Empty string throws")
				.Given(SearchableEntitiesExist)
				.When(SearchIsCalled, string.Empty)
				.Then(RepositoryDownloadIsCalled<SearchResults>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Whitespace string throws")
				.Given(SearchableEntitiesExist)
				.When(SearchIsCalled, "    ")
				.Then(RepositoryDownloadIsCalled<SearchResults>, 0)
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

			//feature.WithScenario("Search a string")
			//	.Given(SearchableMembersExist)
			//	.When(SearchMembersIsCalled, "search")
			//	.Then(MockGetIsCalled<List<IJsonMember>>, 1)
			//	.And(ResponseIsNotNull)
			//	.And(ResponseIs<IEnumerable<Member>>)
			//	.And(ExceptionIsNotThrown)

			//	.WithScenario("Null string throws")
			//	.Given(SearchableMembersExist)
			//	.When(SearchMembersIsCalled, (string)null)
			//	.Then(MockGetIsCalled<List<IJsonMember>>, 0)
			//	.And(ExceptionIsThrown<ArgumentNullException>)

			//	.WithScenario("Empty string throws")
			//	.Given(SearchableMembersExist)
			//	.When(SearchMembersIsCalled, string.Empty)
			//	.Then(MockGetIsCalled<List<IJsonMember>>, 0)
			//	.And(ExceptionIsThrown<ArgumentNullException>)

			//	.WithScenario("Whitespace string throws")
			//	.Given(SearchableMembersExist)
			//	.When(SearchMembersIsCalled, "    ")
			//	.Then(MockGetIsCalled<List<IJsonMember>>, 0)
			//	.And(ExceptionIsThrown<ArgumentNullException>)

			//	.Execute();
		}

		#endregion

		#region Given

		[GenericMethodFormat("A(n) {0} exists")]
		private void AnEntityExists<T>()
			where T : ExpiringObject
		{
			_systemUnderTest = new ServiceUnderTest();
			_systemUnderTest.Dependencies.EntityRepository.Setup(r => r.Download<T>(It.IsAny<EntityRequestType>(),
																					It.IsAny<IDictionary<string, object>>()))
							.Returns(new Mock<T>().Object);
			_systemUnderTest.Dependencies.Cache.Setup(c => c.Add(It.IsAny<T>()))
			                .Callback(ItemExistsInCache<T>);
			_systemUnderTest.Dependencies.Cache.Setup(c => c.Find(It.IsAny<Func<T, bool>>(), It.IsAny<Func<T>>()))
			                .Returns((Func<T, bool> match, Func<T> fetch) => fetch());
			_systemUnderTest.Dependencies.EndpointFactory.Setup(f => f.GetRequestType<T>())
							.Returns(EntityRequestType.Badges_Read_Refresh);
		}
		[GenericMethodFormat("The cache contains a(n) {0}")]
		private void ItemExistsInCache<T>()
			where T : class
		{
			_systemUnderTest.Dependencies.Cache.Setup(c => c.Find(It.IsAny<Func<T, bool>>(), It.IsAny<Func<T>>()))
			                .Returns(new Mock<T>().Object);
		}
		[GenericMethodFormat("A(n) {0} does not exist")]
		private void AnEntityDoesNotExist<T, TJ>() where T : class where TJ : class
		{
			_systemUnderTest = new ServiceUnderTest();
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
		}
		private void SearchableMembersExist()
		{
			var member = new Mock<IJsonMember>();
			_systemUnderTest = new ServiceUnderTest();
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

		[GenericMethodFormat("Api.Get<{0}>() is called {1} time(s)")]
		private void RepositoryDownloadIsCalled<T>(int times)
			where T : ExpiringObject
		{
			_systemUnderTest.Dependencies.EntityRepository.Verify(r => r.Download<T>(It.IsAny<EntityRequestType>(), It.IsAny<Dictionary<string, object>>()), Times.Exactly(times));
		}
		[GenericMethodFormat("Cache.Find<{0}>() is called {1} time(s)")]
		private void CacheFindIsCalled<T>(int times)
		{
			_systemUnderTest.Dependencies.Cache.Verify(c => c.Find(It.IsAny<Func<T, bool>>(), It.IsAny<Func<T>>()), Times.Exactly(times));
		}

		#endregion
	}
}

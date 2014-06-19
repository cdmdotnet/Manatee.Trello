using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.Unit
{
	[TestClass]
	public class TrelloServiceTest : UnitTestBase
	{
		#region Dependencies

		private class ServiceUnderTest : SystemUnderTest<TrelloService, DependencyCollection>
		{
			public ServiceUnderTest()
			{
				Sut = new TrelloService(Dependencies.Validator.Object,
				                        Dependencies.EntityRepository.Object);
			}
		}

		#endregion

		#region Data

		private SystemUnderTest<TrelloService, DependencyCollection> _systemUnderTest;

		#endregion

		#region Scenarios

		[TestMethod]
		public void Retrieve()
		{
			var story = new Story("Retrieve");

			var feature = story.InOrderTo("retrieve data from Trello.com")
			                   .AsA("TrelloService consumer")
			                   .IWant("the requested data returned");

			feature.WithScenario("Calls repository")
			       .Given(AService)
			       .And(ABoardExists)
			       .When(RetrieveIsCalled<Board>)
			       .Then(ValidatorNonEmptyStringIsCalled)
			       .And(RepositoryDownloadIsCalled<Board>, 1)
			       .And(ResponseIs<Board>)
			       .And(ExceptionIsNotThrown)

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
			       .Given(AService)
			       .And(MatchingEntitiesExist)
			       .When(SearchIsCalled)
			       .Then(ValidatorNonEmptyStringIsCalled)
			       .And(RepositoryDownloadIsCalled<SearchResults>, 1)
			       .And(ResponseIsNotNull)
			       .And(ResponseIs<SearchResults>)
			       .And(ExceptionIsNotThrown)

			       .Execute();
		}
		[TestMethod]
		public void SearchMembers()
		{
			var story = new Story("SearchMembers");

			var feature = story.InOrderTo("search members on Trello.com")
			                   .AsA("TrelloService consumer")
			                   .IWant("all members which contain data matching my query");

			feature.WithScenario("Search a string")
			       .Given(AService)
			       .When(SearchMembersIsCalled)
			       .Then(ValidatorNonEmptyStringIsCalled)
			       .And(RepositoryRefreshCollectionIsCalled<Member>, 1)
			       .And(ResponseIsNotNull)
			       .And(ResponseIs<IEnumerable<Member>>)
			       .And(ExceptionIsNotThrown)

			       .Execute();
		}

		#endregion

		#region Given

		private void AService()
		{
			_systemUnderTest = new ServiceUnderTest();
		}

		private void ABoardExists()
		{
			_systemUnderTest.Dependencies.EntityRepository.Setup(r => r.Download<Board>(It.IsAny<EntityRequestType>(), It.IsAny<IDictionary<string, object>>()))
			                .Returns(new Board
				                {
					                Validator = _systemUnderTest.Dependencies.Validator.Object,
					                EntityRepository = _systemUnderTest.Dependencies.EntityRepository.Object
				                });
		}

		private void MatchingEntitiesExist()
		{
			_systemUnderTest.Dependencies.EntityRepository.Setup(r => r.Download<SearchResults>(It.IsAny<EntityRequestType>(), It.IsAny<IDictionary<string, object>>()))
			                .Returns(new SearchResults
				                {
					                Validator = _systemUnderTest.Dependencies.Validator.Object,
					                EntityRepository = _systemUnderTest.Dependencies.EntityRepository.Object
				                });
		}

		#endregion

		#region When

		[GenericMethodFormat("Retrieve<{0}>() is called")]
		private void RetrieveIsCalled<T>()
			where T : ExpiringObject, new()
		{
			Execute(() => _systemUnderTest.Sut.Retrieve<T>(TrelloIds.Test));
		}
		private void SearchIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.Search(TrelloIds.Test));
		}
		private void SearchMembersIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.SearchMembers(TrelloIds.Test).ToList());
		}

		#endregion

		#region Then

		private void ValidatorNonEmptyStringIsCalled()
		{
			_systemUnderTest.Dependencies.Validator.Verify(v => v.NonEmptyString(It.IsAny<string>()));
		}
		[GenericMethodFormat("Repository.Download<{0}>() is called {1} time(s)")]
		private void RepositoryDownloadIsCalled<T>(int times)
			where T : ExpiringObject
		{
			_systemUnderTest.Dependencies.EntityRepository.Verify(r => r.Download<T>(It.IsAny<EntityRequestType>(),
			                                                                         It.IsAny<Dictionary<string, object>>()), Times.Exactly(times));
		}
		[GenericMethodFormat("Repository.RefreshCollection<{0}>() is called {1} time(s)")]
		private void RepositoryRefreshCollectionIsCalled<T>(int times)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
		{
			_systemUnderTest.Dependencies.EntityRepository.Verify(r => r.RefreshCollection<T>(It.IsAny<ExpiringCollection<T>>(),
			                                                                                  It.IsAny<EntityRequestType>()), Times.Exactly(times));
		}

		#endregion
	}
}

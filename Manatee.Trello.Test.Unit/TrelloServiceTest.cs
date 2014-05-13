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
		private const string MockEntityId = "mock entity ID";

		#region Dependencies

		private new class DependencyCollection : UnitTestBase.DependencyCollection
		{
			public Mock<ITrelloServiceConfiguration> Config { get; private set; }

			public DependencyCollection()
			{
				Config = new Mock<ITrelloServiceConfiguration>();

				Config.SetupGet(c => c.Log).Returns(Log.Object);
			}
		}

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
				.When(RetrieveIsCalled<Board>, MockEntityId)
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
				.When(SearchIsCalled, MockEntityId)
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
				.When(SearchMembersIsCalled, MockEntityId)
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

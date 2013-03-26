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
	public class TrelloServiceUnitTest
	{
		public const string MockAuthKey = "some non-empty string";
		public const string MockAuthToken = "some non-empty string";
		public const string MockEntityId = "some non-empty string";

		#region Service Construction

		private class DependencyCollection
		{
			public Mock<IRestClient> RestClient { get; private set; }
			public Mock<IRestClientProvider> RestClientProvider { get; private set; }

			public DependencyCollection()
			{
				RestClient = new Mock<IRestClient>();
				RestClientProvider = new Mock<IRestClientProvider>();

				RestClientProvider.Setup(p => p.CreateRestClient(It.IsAny<string>()))
					.Returns(RestClient.Object);
			}
		}
		private class ServiceGroup
		{
			public DependencyCollection Dependencies { get; private set; }
			public TrelloService Sut { get; private set; }

			public ServiceGroup()
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

		// There should be no reason to modify or append the data.
		private ServiceGroup _serviceGroup;
		private Exception _exception;
		private object _actualResult, _request;

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
				.Given(AnEntityExists<Action>)
				.When(RetrieveIsCalled<Action>)
				.Then(MockExecuteIsCalled<Action>, Times.Once())
				.And(RetrieveReturns<Action>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Retrieve a Notification")
				.Given(AnEntityExists<Notification>)
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
			_serviceGroup = new ServiceGroup();
			_serviceGroup.Dependencies.RestClient.Setup(c => c.Execute(It.IsAny<IRestRequest<T>>()))
				.Returns(new RestSharpResponse<T>(new RestResponse<T>()) {Data = entity});
		}

		#endregion

		#region When

		private void RetrieveIsCalled<T>()
			where T : ExpiringObject, new()
		{
			_request = MockEntityId;
			CallService<string, T>(_serviceGroup.Sut.Retrieve<T>);
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
			_serviceGroup.Dependencies.RestClient.Verify(c => c.Execute(It.IsAny<IRestRequest<T>>()));
		}
		private void RetrieveReturns<T>()
			where T : ExpiringObject, new()
		{
			var actual = _actualResult as T;
			Assert.IsNotNull(actual);
			Assert.AreEqual(MockEntityId, actual.Id);
		}

		#endregion

		#region Support Methods

		private void CallService<TRequest, TResponse>(Func<TRequest, TResponse> func)
		{
			_exception = null;
			var request = (TRequest) _request;

			try
			{
				_actualResult = func(request);
			}
			catch (Exception e)
			{
				_exception = e;
			}
		}
		private static bool CollectionsMatch<T>(IEnumerable<T> a, IEnumerable<T> b)
		{
			return (a.Count() == b.Count()) && a.All(b.Contains);
		}

		#endregion
	}
}

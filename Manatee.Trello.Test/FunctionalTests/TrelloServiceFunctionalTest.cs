using System;
using Manatee.Trello.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test.FunctionalTests
{
	[TestClass]
	public class TrelloServiceFunctionalTest : TrelloTestBase<TrelloService>
	{
		private class DependencyCollection
		{
			public string AuthKey { get; set; }
			public string AuthToken { get; set; }
		}
		private class ServiceUnderTest : SystemUnderTest<DependencyCollection>
		{
			public ServiceUnderTest(string authKey, string authToken)
			{
				Dependencies.AuthKey = authKey;
				Dependencies.AuthToken = authToken;
				Sut = new TrelloService(Dependencies.AuthKey, Dependencies.AuthToken);
			}
		}

		private string _request;
		private ServiceUnderTest _serviceUnderTest;

		[TestMethod]
		public void Retrieve()
		{
			var story = new Story("TrelloService.Retrieve Succeeds");

			var feature = story.InOrderTo("retrieve data from Trello.com")
				.AsA("developer")
				.IWant("TrelloService to provide the requested data.");

			feature.WithScenario("Retrieve a Board")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, TrelloIds.BoardId)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<Board>)

				.WithScenario("Retrieve a Card")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, TrelloIds.CardId)
				.When(RetrieveIsCalled<Card>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<Card>)

				.WithScenario("Retrieve an Action")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, TrelloIds.ActionId)
				.When(RetrieveIsCalled<Action>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<Action>)

				.WithScenario("Retrieve a List")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, TrelloIds.ListId)
				.When(RetrieveIsCalled<List>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<List>)

				.WithScenario("Retrieve a Member by Username")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, TrelloIds.UserName)
				.When(RetrieveIsCalled<Member>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedMemberIsReturned)

				.WithScenario("Retrieve a Member by Id")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, TrelloIds.MemberId)
				.When(RetrieveIsCalled<Member>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<Member>)

				.WithScenario("Retrieve a Notification")
				.Given(ATrelloService, TrelloIds.Key, TrelloIds.Token)
				.And(AnId, TrelloIds.NotificationId)
				.When(RetrieveIsCalled<Notification>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<Notification>)

				.WithScenario("Retrieve a Organization")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, TrelloIds.OrganizationId)
				.When(RetrieveIsCalled<Organization>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<Organization>)

				.WithScenario("Retrieve a CheckList")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, TrelloIds.CheckListId)
				.When(RetrieveIsCalled<CheckList>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<CheckList>)

				.Execute();
		}
		[TestMethod]
		public void Retrieve_Fail()
		{
			var story = new Story("TrelloService.Retrieve Fails");

			var feature = story.InOrderTo("prevent invalid operations on Trello")
				.AsA("developer")
				.IWant("TrelloService to disallow invalid input.");

			feature.WithScenario("Retrieve an entity with invalid ID")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, TrelloIds.Invalid)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.WithScenario("Retrieve an entity with ID for different type")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, TrelloIds.CardId)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.WithScenario("Retrieve an entity with null ID")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, (string) null)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.WithScenario("Retrieve an entity with an empty ID")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, string.Empty)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.WithScenario("Retrieve an entity with a whitespace ID")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, "    ")
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.WithScenario("Retrieve an entity, supplying an invalid key")
				.Given(ATrelloService, TrelloIds.Invalid, (string) null)
				.And(AnId, TrelloIds.BoardId)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)

				.WithScenario("Retrieve an entity, supplying an invalid token")
				.Given(ATrelloService, TrelloIds.Key, TrelloIds.Invalid)
				.And(AnId, TrelloIds.BoardId)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.WithScenario("Retrieve a Notification without supplying token")
				.Given(ATrelloService, TrelloIds.Key, (string) null)
				.And(AnId, TrelloIds.NotificationId)
				.When(RetrieveIsCalled<Notification>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.Execute();
		}
		[TestMethod]
		public void Constructor()
		{
			var story = new Story("Constructor");

			var feature = story.InOrderTo("prevent invalid operations on Trello")
				.AsA("developer")
				.IWant("TrelloService constructor to disallow invalid Auth Keys.");

			feature.WithScenario("Create a service, supplying a null key")
				.Given(NoPreconditions)
				.When(ServiceIsCreated, (string)null, (string)null)
				.Then(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Create a service, supplying an empty key")
				.Given(NoPreconditions)
				.When(ServiceIsCreated, string.Empty, (string)null)
				.Then(ExceptionIsThrown<ArgumentException>)

				.WithScenario("Create a service, supplying a whitespace key")
				.Given(NoPreconditions)
				.When(ServiceIsCreated, "    ", (string)null)
				.Then(ExceptionIsThrown<ArgumentException>)

				.Execute();
		}

		#region Given

		private void NoPreconditions() {}
		private void ATrelloService(string authKey, string authToken)
		{
			_serviceUnderTest = new ServiceUnderTest(authKey, authToken);
		}
		private void AnId(string id)
		{
			_request = id;
		}

		#endregion

		#region When

		private void RetrieveIsCalled<T>()
			where T : ExpiringObject, new()
		{
			Execute(() => _serviceUnderTest.Sut.Retrieve<T>(_request));
		}
		private void ServiceIsCreated(string authKey, string authToken)
		{
			Execute(() => _serviceUnderTest = new ServiceUnderTest(authKey, authToken));
		}

		#endregion

		#region Then

		private void RequestedObjectIsReturned<T>()
			where T : ExpiringObject
		{
			Assert.IsNotNull(_actualResult);
			Assert.IsInstanceOfType(_actualResult, typeof(T));
			Assert.AreEqual(_request, ((T)_actualResult).Id);
		}
		private void RequestedMemberIsReturned()
		{
			Assert.IsNotNull(_actualResult);
			Assert.IsInstanceOfType(_actualResult, typeof(Member));
			Assert.AreEqual(_request, ((Member)_actualResult).Username);
		}

		#endregion
	}
}

using System;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test.Functional
{
	[TestClass]
	public class TrelloServiceFunctionalTest : TrelloTestBase<TrelloService>
	{
		private class DependencyCollection
		{
			public ITrelloServiceConfiguration Config { get; private set; }
			public string AppKey { get; set; }
			public string UserToken { get; set; }

			public DependencyCollection()
			{
				Config = new TrelloServiceConfiguration();
				var serializer = new ManateeSerializer();
				Config.Serializer = serializer;
				Config.Deserializer = serializer;
				Config.RestClientProvider = new RestSharpClientProvider(Config);
			}
		}
		private class ServiceUnderTest : SystemUnderTest<DependencyCollection>
		{
			public ServiceUnderTest(string appKey, string userToken)
			{
				Dependencies.AppKey = appKey;
				Dependencies.UserToken = userToken;
				Sut = new TrelloService(Dependencies.Config, new TrelloAuthorization(Dependencies.AppKey, Dependencies.UserToken));
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
				.Given(ATrelloService, TrelloIds.AppKey, (string) null)
				.And(AnId, TrelloIds.BoardId)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<Board>)

				.WithScenario("Retrieve a Card")
				.Given(ATrelloService, TrelloIds.AppKey, (string) null)
				.And(AnId, TrelloIds.CardId)
				.When(RetrieveIsCalled<Card>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<Card>)

				.WithScenario("Retrieve an Action")
				.Given(ATrelloService, TrelloIds.AppKey, (string) null)
				.And(AnId, TrelloIds.ActionId)
				.When(RetrieveIsCalled<Action>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<Action>)

				.WithScenario("Retrieve a List")
				.Given(ATrelloService, TrelloIds.AppKey, (string) null)
				.And(AnId, TrelloIds.ListId)
				.When(RetrieveIsCalled<List>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<List>)

				.WithScenario("Retrieve a Member by Username")
				.Given(ATrelloService, TrelloIds.AppKey, (string) null)
				.And(AnId, TrelloIds.UserName)
				.When(RetrieveIsCalled<Member>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedMemberIsReturned)

				.WithScenario("Retrieve a Member by Id")
				.Given(ATrelloService, TrelloIds.AppKey, (string) null)
				.And(AnId, TrelloIds.MemberId)
				.When(RetrieveIsCalled<Member>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<Member>)

				// This test is not valid since Notifications eventually expire and are removed from Trello.
				//.WithScenario("Retrieve a Notification")
				//.Given(ATrelloService, TrelloIds.AppKey, TrelloIds.UserToken)
				//.And(AnId, TrelloIds.NotificationId)
				//.When(RetrieveIsCalled<Notification>)
				//.Then(ExceptionIsNotThrown)
				//.And(ResponseIsNotNull)
				//.And(RequestedObjectIsReturned<Notification>)

				.WithScenario("Retrieve a Organization")
				.Given(ATrelloService, TrelloIds.AppKey, (string) null)
				.And(AnId, TrelloIds.OrganizationId)
				.When(RetrieveIsCalled<Organization>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<Organization>)

				.WithScenario("Retrieve a CheckList")
				.Given(ATrelloService, TrelloIds.AppKey, (string) null)
				.And(AnId, TrelloIds.CheckListId)
				.When(RetrieveIsCalled<CheckList>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedObjectIsReturned<CheckList>)

				.WithScenario("Retrieve a Token")
				.Given(ATrelloService, TrelloIds.AppKey, TrelloIds.UserToken)
				.And(AnId, TrelloIds.UserToken)
				.When(RetrieveIsCalled<Token>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNotNull)
				.And(RequestedTokenIsReturned)

				.Execute();
		}
		[TestMethod]
		public void Constructor()
		{
			var story = new Story("Constructor");

			var feature = story.InOrderTo("prevent invalid operations on Trello")
				.AsA("developer")
				.IWant("TrelloService constructor to disallow invalid App Keys.");

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
		[TestMethod]
		public void Search()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);
			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);

			var results = service.Search("manatee");

			Assert.IsNotNull(results);
			Assert.IsNotNull(results.Boards);
			Assert.AreNotEqual(0, results.Boards.Count());

			foreach (var board in results.Boards)
			{
				Console.WriteLine(board);
			}
		}
		[TestMethod]
		public void SearchMembers()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);
			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);

			var results = service.SearchMembers(TrelloIds.UserName);

			Assert.IsNotNull(results);
			Assert.AreNotEqual(0, results.Count());

			foreach (var member in results)
			{
				Console.WriteLine(member);
			}
		}
		[TestMethod]
		public void Me()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);
			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);

			var me = service.Me;

			Assert.IsNotNull(me);
			Assert.AreEqual(TrelloIds.UserName, me.Username);

			Console.WriteLine(me.FullName);
		}


		#region Given

		private void NoPreconditions() {}
		private void ATrelloService(string appKey, string userToken)
		{
			_serviceUnderTest = new ServiceUnderTest(appKey, userToken);
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
		private void ServiceIsCreated(string appKey, string userToken)
		{
			Execute(() => _serviceUnderTest = new ServiceUnderTest(appKey, userToken));
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
		private void RequestedTokenIsReturned()
		{
			Assert.IsNotNull(_actualResult);
			Assert.IsInstanceOfType(_actualResult, typeof(Token));
			Assert.AreEqual(_request, ((Token)_actualResult).Value);
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

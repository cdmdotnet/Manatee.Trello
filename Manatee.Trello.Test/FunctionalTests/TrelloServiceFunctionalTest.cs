using System;
using Manatee.Trello.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test.FunctionalTests
{
	[TestClass]
	public class TrelloServiceFunctionalTest
	{
		private Exception _exception;
		private object _actualResult;
		private string _authKey, _authToken, _request;

		[TestMethod]
		public void Retrieve_Success()
		{
			var story = new Story("TrelloService.Retrieve Succeeds");

			var feature = story.InOrderTo("retrieve data from Trello.com")
				.AsA("developer")
				.IWant("TrelloService to provide the requested data.");

			feature.WithScenario("Retrieve a Board")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.BoardId)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(RequestedObjectIsReturned<Board>)

				.WithScenario("Retrieve a Card")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.CardId)
				.When(RetrieveIsCalled<Card>)
				.Then(ExceptionIsNotThrown)
				.And(RequestedObjectIsReturned<Card>)

				.WithScenario("Retrieve an Action")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.ActionId)
				.When(RetrieveIsCalled<Action>)
				.Then(ExceptionIsNotThrown)
				.And(RequestedObjectIsReturned<Action>)

				.WithScenario("Retrieve a List")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.ListId)
				.When(RetrieveIsCalled<List>)
				.Then(ExceptionIsNotThrown)
				.And(RequestedObjectIsReturned<List>)

				.WithScenario("Retrieve a Member by Username")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.UserName)
				.When(RetrieveIsCalled<Member>)
				.Then(ExceptionIsNotThrown)
				.And(RequestedMemberIsReturned)

				.WithScenario("Retrieve a Member by Id")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.MemberId)
				.When(RetrieveIsCalled<Member>)
				.Then(ExceptionIsNotThrown)
				.And(RequestedObjectIsReturned<Member>)

				.WithScenario("Retrieve a Notification")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, TrelloIds.Token)
				.And(AnId, TrelloIds.NotificationId)
				.When(RetrieveIsCalled<Notification>)
				.Then(ExceptionIsNotThrown)
				.And(RequestedObjectIsReturned<Notification>)

				.WithScenario("Retrieve a Organization")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.OrganizationId)
				.When(RetrieveIsCalled<Organization>)
				.Then(ExceptionIsNotThrown)
				.And(RequestedObjectIsReturned<Organization>)

				.WithScenario("Retrieve a CheckList")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.CheckListId)
				.When(RetrieveIsCalled<CheckList>)
				.Then(ExceptionIsNotThrown)
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
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.Invalid)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.WithScenario("Retrieve an entity with ID for different type")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.CardId)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)
				
				.WithScenario("Retrieve an entity with null ID")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, (string)null)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.WithScenario("Retrieve an entity with an empty ID")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, string.Empty)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.WithScenario("Retrieve an entity with a whitespace ID")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, "    ")
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.WithScenario("Retrieve an entity, supplying a null key")
				.Given(AnAuthKey, (string) null)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.BoardId)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Retrieve an entity, supplying an empty key")
				.Given(AnAuthKey, string.Empty)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.BoardId)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsThrown<ArgumentException>)

				.WithScenario("Retrieve an entity, supplying a whitespace key")
				.Given(AnAuthKey, "    ")
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.BoardId)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsThrown<ArgumentException>)

				.WithScenario("Retrieve an entity, supplying an invalid key")
				.Given(AnAuthKey, TrelloIds.Invalid)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.BoardId)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)

				.WithScenario("Retrieve an entity, supplying an invalid token")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, TrelloIds.Invalid)
				.And(AnId, TrelloIds.BoardId)
				.When(RetrieveIsCalled<Board>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.WithScenario("Retrieve a Notification without supplying token")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string) null)
				.And(AnId, TrelloIds.NotificationId)
				.When(RetrieveIsCalled<Notification>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.WithScenario("Retrieve a sub-type by submitting its owner's ID")
				.Given(AnAuthKey, TrelloIds.Key)
				.And(AnAuthToken, (string)null)
				.And(AnId, TrelloIds.BoardId)
				.When(RetrieveIsCalled<BoardPreferences>)
				.Then(ExceptionIsNotThrown)
				.And(ResponseIsNull)

				.Execute();
		}

		#region Given

		private void AnAuthKey(string key)
		{
			_authKey = key;
		}
		private void AnAuthToken(string token)
		{
			_authToken = token;
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
			_exception = null;
			_actualResult = null;

			try
			{
				var service = new TrelloService(_authKey, _authToken);
				_actualResult = service.Retrieve<T>(_request);
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
		[GenericMethodFormat("{0} is thrown")]
		private void ExceptionIsThrown<T>() where T : Exception
		{
			Assert.IsNotNull(_exception);
			Assert.IsInstanceOfType(_exception, typeof(T));
		}
		private void ResponseIsNull()
		{
			Assert.IsNull(_actualResult);
		}
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

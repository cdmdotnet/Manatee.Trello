using System;
using System.Collections.Generic;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;
using Manatee.Trello.Test.FunctionalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class ListTest : EntityTestBase<List>
	{
		[TestMethod]
		public void Actions()
		{
			var story = new Story("Actions");

			var feature = story.InOrderTo("get all actions for a list")
				.AsA("developer")
				.IWant("to get Actions");

			feature.WithScenario("Access Actions property")
				.Given(AList)
				.And(EntityIsExpired)
				.When(ActionsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonAction>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Action>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Board()
		{
			var story = new Story("Board");

			var feature = story.InOrderTo("get the board which contains a list")
				.AsA("developer")
				.IWant("to get the Board");

			feature.WithScenario("Access Board property")
				.Given(AList)
				.And(EntityIsRefreshed)
				.When(BoardIsAccessed)
				.Then(MockApiGetIsCalled<IJsonList>, 1)
				.And(MockSvcRetrieveIsCalled<Board>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Board property when expired")
				.Given(AList)
				.And(EntityIsExpired)
				.When(BoardIsAccessed)
				.Then(MockApiGetIsCalled<IJsonList>, 1)
				.And(MockSvcRetrieveIsCalled<Board>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Cards()
		{
			var story = new Story("Cards");

			var feature = story.InOrderTo("get the cards contained within a list")
				.AsA("developer")
				.IWant("to get Cards");

			feature.WithScenario("Access Cards property")
				.Given(AList)
				.And(EntityIsExpired)
				.When(CardsIsAccessed)
				.Then(MockApiGetIsCalled<List<IJsonCard>>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<Card>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void IsClosed()
		{
			var story = new Story("IsClosed");

			var feature = story.InOrderTo("control a list is closed")
				.AsA("developer")
				.IWant("to get and set IsClosed");

			feature.WithScenario("Access IsClosed property")
				.Given(AList)
				.And(EntityIsRefreshed)
				.When(IsClosedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsClosed property when expired")
				.Given(AList)
				.And(EntityIsExpired)
				.When(IsClosedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property")
				.Given(AList)
				.And(EntityIsRefreshed)
				.When(IsClosedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property to null")
				.Given(AList)
				.And(EntityIsRefreshed)
				.And(IsClosedIs, (bool?) true)
				.When(IsClosedIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set IsClosed property to same")
				.Given(AList)
				.And(EntityIsRefreshed)
				.And(IsClosedIs, (bool?) true)
				.When(IsClosedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property without UserToken")
				.Given(AList)
				.And(TokenNotSupplied)
				.When(IsClosedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void IsSubscribed()
		{
			var story = new Story("IsSubscribed");

			var feature = story.InOrderTo("control whether the current member is subscribed to a list")
				.AsA("developer")
				.IWant("to get and set IsSubscribed");

			feature.WithScenario("Access IsSubscribed property")
				.Given(AList)
				.And(EntityIsRefreshed)
				.When(IsSubscribedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsSubscribed property when expired")
				.Given(AList)
				.And(EntityIsExpired)
				.When(IsSubscribedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property")
				.Given(AList)
				.And(EntityIsRefreshed)
				.When(IsSubscribedIsSet, (bool?)true)
				.Then(MockApiPutIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property to null")
				.Given(AList)
				.And(EntityIsRefreshed)
				.And(IsSubscribedIs, (bool?)true)
				.When(IsSubscribedIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set IsSubscribed property to same")
				.Given(AList)
				.And(EntityIsRefreshed)
				.And(IsSubscribedIs, (bool?)true)
				.When(IsSubscribedIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property without UserToken")
				.Given(AList)
				.And(TokenNotSupplied)
				.When(IsSubscribedIsSet, (bool?)true)
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Name()
		{
			var story = new Story("Name");

			var feature = story.InOrderTo("control a list's name")
				.AsA("developer")
				.IWant("to get and set the Name");

			feature.WithScenario("Access Name property")
				.Given(AList)
				.And(EntityIsRefreshed)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Name property when expired")
				.Given(AList)
				.And(EntityIsExpired)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property")
				.Given(AList)
				.And(EntityIsRefreshed)
				.When(NameIsSet, "name")
				.Then(MockApiPutIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to null")
				.Given(AList)
				.And(EntityIsRefreshed)
				.And(NameIs, "name")
				.When(NameIsSet, (string)null)
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Name property to empty")
				.Given(AList)
				.And(EntityIsRefreshed)
				.And(NameIs, "not description")
				.When(NameIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Name property to same")
				.Given(AList)
				.And(EntityIsRefreshed)
				.And(NameIs, "description")
				.When(NameIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property without UserToken")
				.Given(AList)
				.And(TokenNotSupplied)
				.When(NameIsSet, "name")
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Position()
		{
			var story = new Story("Position");

			var feature = story.InOrderTo("control the postition of a list within its board")
				.AsA("developer")
				.IWant("to get and set Position");

			feature.WithScenario("Access Position property")
				.Given(AList)
				.And(EntityIsRefreshed)
				.When(PositionIsAccessed)
				.Then(MockApiGetIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Position property when expired")
				.Given(AList)
				.And(EntityIsExpired)
				.When(PositionIsAccessed)
				.Then(MockApiGetIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property")
				.Given(AList)
				.And(EntityIsRefreshed)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(MockApiPutIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property to null")
				.Given(AList)
				.And(EntityIsRefreshed)
				.And(PositionIs, Trello.Position.Bottom)
				.When(PositionIsSet, (Position) null)
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Position property to same")
				.Given(AList)
				.And(EntityIsRefreshed)
				.And(PositionIs, Trello.Position.Bottom)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property without UserToken")
				.Given(AList)
				.And(TokenNotSupplied)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void AddCard()
		{
			var story = new Story("AddCard");

			var feature = story.InOrderTo("add a card to a list")
				.AsA("developer")
				.IWant("to call AddCard");

			feature.WithScenario("AddCard is called")
				.Given(AList)
				.When(AddCardIsCalled, "checklist")
				.Then(MockApiPostIsCalled<IJsonCard>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("AddCard is called with null name")
				.Given(AList)
				.When(AddCardIsCalled, (string) null)
				.Then(MockApiPostIsCalled<IJsonCard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddCard is called with empty name")
				.Given(AList)
				.When(AddCardIsCalled, string.Empty)
				.Then(MockApiPostIsCalled<IJsonCard>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddCard is called without UserToken")
				.Given(AList)
				.And(TokenNotSupplied)
				.When(AddCardIsCalled, "checklist")
				.Then(MockApiPutIsCalled<IJsonCard>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		[Ignore]
		public void Delete()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Move()
		{
			var story = new Story("Move");

			var feature = story.InOrderTo("move a card to a different board or list")
				.AsA("developer")
				.IWant("to call Move");

			feature.WithScenario("Move is called")
				.Given(AList)
				.When(MoveIsCalled, new Board {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<IJsonList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Move is called with null board")
				.Given(AList)
				.When(MoveIsCalled, (Board) null)
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Move is called with local board")
				.Given(AList)
				.When(MoveIsCalled, new Board())
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Board>>)

				.WithScenario("Move is called without UserToken")
				.Given(AList)
				.And(TokenNotSupplied)
				.When(MoveIsCalled, new Board {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<IJsonList>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		#region Given

		private void AList()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Svc.Object;
			SetupMockGet<IJsonList>();
			SetupMockPost<IJsonCard>();
		}
		private void IsClosedIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.IsClosed = value);
		}
		private void IsSubscribedIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.IsSubscribed = value);
		}
		private void NameIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Name = value);
		}
		private void PositionIs(Position value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Position = value);
		}

		#endregion

		#region When

		private void ActionsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Actions);
		}
		private void BoardIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Board);
		}
		private void CardsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Cards);
		}
		private void IsClosedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.IsClosed);
		}
		private void IsClosedIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.IsClosed = value);
		}
		private void IsSubscribedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.IsSubscribed);
		}
		private void IsSubscribedIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.IsSubscribed = value);
		}
		private void NameIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Name);
		}
		private void NameIsSet(string value)
		{
			Execute(() => _systemUnderTest.Sut.Name = value);
		}
		private void PositionIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Position);
		}
		private void PositionIsSet(Position value)
		{
			Execute(() => _systemUnderTest.Sut.Position = value);
		}
		private void AddCardIsCalled(string name)
		{
			Execute(() => _systemUnderTest.Sut.AddCard(name));
		}
		private void DeleteIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.Delete());
		}
		private void MoveIsCalled(Board board)
		{
			Execute(() => _systemUnderTest.Sut.Move(board));
		}

		#endregion
	}
}
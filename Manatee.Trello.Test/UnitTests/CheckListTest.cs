using System;
using System.Collections.Generic;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Test.FunctionalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class CheckListTest : EntityTestBase<CheckList>
	{
		[TestMethod]
		public void Board()
		{
			var story = new Story("Board");

			var feature = story.InOrderTo("get the board which contains a check list")
				.AsA("developer")
				.IWant("to get the Board");

			feature.WithScenario("Access Board property")
				.Given(ACheckList)
				.And(EntityIsNotExpired)
				.When(BoardIsAccessed)
				.Then(MockApiGetIsCalled<CheckList>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Board property when expired")
				.Given(ACheckList)
				.And(EntityIsExpired)
				.When(BoardIsAccessed)
				.Then(MockApiGetIsCalled<CheckList>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Card()
		{
			var story = new Story("Card");

			var feature = story.InOrderTo("control the card to which a check list belongs")
				.AsA("developer")
				.IWant("to get and set the Card");

			feature.WithScenario("Access Card property")
				.Given(ACheckList)
				.And(EntityIsNotExpired)
				.When(CardIsAccessed)
				.Then(MockApiGetIsCalled<CheckList>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Card property when expired")
				.Given(ACheckList)
				.And(EntityIsExpired)
				.When(CardIsAccessed)
				.Then(MockApiGetIsCalled<CheckList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Card property")
				.Given(ACheckList)
				.When(CardIsSet, new Card {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<CheckList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Card property to null")
				.Given(ACheckList)
				.And(CardIs, new Card {Id = TrelloIds.Invalid})
				.When(CardIsSet, (Card) null)
				.Then(MockApiPutIsCalled<CheckList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Card property to local card")
				.Given(ACheckList)
				.When(CardIsSet, new Card())
				.Then(MockApiPutIsCalled<CheckList>, 0)
				.And(ExceptionIsThrown<EntityNotOnTrelloException<Card>>)

				.WithScenario("Set Card property to same")
				.Given(ACheckList)
				.And(CardIs, new Card {Id = TrelloIds.Invalid})
				.When(CardIsSet, new Card {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<CheckList>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Card property without AuthToken")
				.Given(ACheckList)
				.And(TokenNotSupplied)
				.When(CardIsSet, new Card {Id = TrelloIds.Invalid})
				.Then(MockApiPutIsCalled<CheckList>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void CheckItems()
		{
			var story = new Story("CheckItems");

			var feature = story.InOrderTo("get all lists for a board")
				.AsA("developer")
				.IWant("to get Lists");

			feature.WithScenario("Access Lists property")
				.Given(ACheckList)
				.And(EntityIsExpired)
				.When(CheckItemsIsAccessed)
				.Then(MockApiGetIsCalled<List>, 0)
				.And(MockApiGetCollectionIsCalled<List>, 0)
				.And(NonNullValueOfTypeIsReturned<IEnumerable<CheckItem>>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Name()
		{
			var story = new Story("Name");

			var feature = story.InOrderTo("control a check list's name")
				.AsA("developer")
				.IWant("to get and set the Name");

			feature.WithScenario("Access Name property")
				.Given(ACheckList)
				.And(EntityIsNotExpired)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<CheckList>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Name property when expired")
				.Given(ACheckList)
				.And(EntityIsExpired)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<CheckList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property")
				.Given(ACheckList)
				.When(NameIsSet, "name")
				.Then(MockApiPutIsCalled<CheckList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to null")
				.Given(ACheckList)
				.And(NameIs, "name")
				.When(NameIsSet, (string) null)
				.Then(MockApiPutIsCalled<CheckList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Name property to empty")
				.Given(ACheckList)
				.And(NameIs, "not description")
				.When(NameIsSet, string.Empty)
				.Then(MockApiPutIsCalled<CheckList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Name property to same")
				.Given(ACheckList)
				.And(NameIs, "description")
				.When(NameIsSet, "description")
				.Then(MockApiPutIsCalled<CheckList>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property without AuthToken")
				.Given(ACheckList)
				.And(TokenNotSupplied)
				.When(NameIsSet, "name")
				.Then(MockApiPutIsCalled<CheckList>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Position()
		{
			var story = new Story("Position");

			var feature = story.InOrderTo("control the postition of a check list within its card")
				.AsA("developer")
				.IWant("to get and set Position");

			feature.WithScenario("Access Position property")
				.Given(ACheckList)
				.And(EntityIsNotExpired)
				.When(PositionIsAccessed)
				.Then(MockApiGetIsCalled<CheckList>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Position property when expired")
				.Given(ACheckList)
				.And(EntityIsExpired)
				.When(PositionIsAccessed)
				.Then(MockApiGetIsCalled<CheckList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property")
				.Given(ACheckList)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(MockApiPutIsCalled<CheckList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property to null")
				.Given(ACheckList)
				.And(PositionIs, Trello.Position.Bottom)
				.When(PositionIsSet, (Position) null)
				.Then(MockApiPutIsCalled<CheckList>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Position property to same")
				.Given(ACheckList)
				.And(PositionIs, Trello.Position.Bottom)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(MockApiPutIsCalled<CheckList>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property without AuthToken")
				.Given(ACheckList)
				.And(TokenNotSupplied)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(MockApiPutIsCalled<CheckList>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void AddCheckItem()
		{
			var story = new Story("AddCheckItem");

			var feature = story.InOrderTo("add a check item to a check list")
				.AsA("developer")
				.IWant("to add a check item");

			feature.WithScenario("AddCheckItem is called")
				.Given(ACheckList)
				.When(AddCheckItemIsCalled, "list")
				.Then(MockApiPostIsCalled<CheckItem>, 1)
				.And(NonNullValueOfTypeIsReturned<CheckItem>)
				.And(ExceptionIsNotThrown)

				.WithScenario("AddList is called with null")
				.Given(ACheckList)
				.When(AddCheckItemIsCalled, (string) null)
				.Then(MockApiPostIsCalled<CheckItem>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddList is called with empty")
				.Given(ACheckList)
				.When(AddCheckItemIsCalled, string.Empty)
				.Then(MockApiPostIsCalled<CheckItem>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("AddList is called without AuthToken")
				.Given(ACheckList)
				.And(TokenNotSupplied)
				.When(AddCheckItemIsCalled, "list")
				.Then(MockApiPutIsCalled<CheckItem>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var story = new Story("Delete");

			var feature = story.InOrderTo("delete a check list")
				.AsA("developer")
				.IWant("to call Delete");

			feature.WithScenario("Delete is called")
				.Given(ACheckList)
				.When(DeleteIsCalled)
				.Then(MockApiDeleteIsCalled<CheckList>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called without AuthToken")
				.Given(ACheckList)
				.And(TokenNotSupplied)
				.When(DeleteIsCalled)
				.Then(MockApiPutIsCalled<CheckList>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		#region Given

		private void ACheckList()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
			SetupMockPost<CheckItem>();
		}
		private void CardIs(Card value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Card = value);
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

		private void BoardIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Board);
		}
		private void CardIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Card);
		}
		private void CardIsSet(Card value)
		{
			Execute(() => _systemUnderTest.Sut.Card = value);
		}
		private void CheckItemsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.CheckItems);
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
		private void AddCheckItemIsCalled(string name)
		{
			Execute(() => _systemUnderTest.Sut.AddCheckItem(name));
		}
		private void DeleteIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.Delete());
		}

		#endregion
	}
}
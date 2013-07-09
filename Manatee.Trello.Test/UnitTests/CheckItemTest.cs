using System;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;
using Manatee.Trello.Test.FunctionalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class CheckItemTest : EntityTestBase<CheckItem>
	{
		[TestMethod]
		public void Name()
		{
			var story = new Story("Name");

			var feature = story.InOrderTo("control a card's name")
				.AsA("developer")
				.IWant("to get and set the Name");

			feature.WithScenario("Access Name property")
				.Given(ACheckItem)
				.And(EntityIsRefreshed)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonCheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Name property when expired")
				.Given(ACheckItem)
				.And(EntityIsExpired)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<IJsonCheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property")
				.Given(ACheckItem)
				.And(EntityIsRefreshed)
				.When(NameIsSet, "name")
				.Then(MockApiPutIsCalled<IJsonCheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to null")
				.Given(ACheckItem)
				.And(EntityIsRefreshed)
				.And(NameIs, "name")
				.When(NameIsSet, (string) null)
				.Then(MockApiPutIsCalled<IJsonCheckItem>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Name property to empty")
				.Given(ACheckItem)
				.And(EntityIsRefreshed)
				.And(NameIs, "not description")
				.When(NameIsSet, string.Empty)
				.Then(MockApiPutIsCalled<IJsonCheckItem>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Name property to same")
				.Given(ACheckItem)
				.And(EntityIsRefreshed)
				.And(NameIs, "description")
				.When(NameIsSet, "description")
				.Then(MockApiPutIsCalled<IJsonCheckItem>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property without UserToken")
				.Given(ACheckItem)
				.And(TokenNotSupplied)
				.When(NameIsSet, "name")
				.Then(MockApiPutIsCalled<IJsonCheckItem>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Position()
		{
			var story = new Story("Position");

			var feature = story.InOrderTo("control the postition of a check item within its check list")
				.AsA("developer")
				.IWant("to get and set Position");

			feature.WithScenario("Access Position property")
				.Given(ACheckItem)
				.And(EntityIsRefreshed)
				.When(PositionIsAccessed)
				.Then(MockApiGetIsCalled<IJsonCheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Position property when expired")
				.Given(ACheckItem)
				.And(EntityIsExpired)
				.When(PositionIsAccessed)
				.Then(MockApiGetIsCalled<IJsonCheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property")
				.Given(ACheckItem)
				.And(EntityIsRefreshed)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(MockApiPutIsCalled<IJsonCheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property to null")
				.Given(ACheckItem)
				.And(EntityIsRefreshed)
				.And(PositionIs, Trello.Position.Bottom)
				.When(PositionIsSet, (Position) null)
				.Then(MockApiPutIsCalled<IJsonCheckItem>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Position property to same")
				.Given(ACheckItem)
				.And(EntityIsRefreshed)
				.And(PositionIs, Trello.Position.Bottom)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(MockApiPutIsCalled<IJsonCheckItem>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property without UserToken")
				.Given(ACheckItem)
				.And(TokenNotSupplied)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(MockApiPutIsCalled<IJsonCheckItem>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void State()
		{
			var story = new Story("State");

			var feature = story.InOrderTo("control a check list item's state")
				.AsA("developer")
				.IWant("to get and set the State");

			feature.WithScenario("Access State property")
				.Given(ACheckItem)
				.And(EntityIsRefreshed)
				.When(StateIsAccessed)
				.Then(MockApiGetIsCalled<IJsonCheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access State property when expired")
				.Given(ACheckItem)
				.And(EntityIsExpired)
				.When(StateIsAccessed)
				.Then(MockApiGetIsCalled<IJsonCheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set State property")
				.Given(ACheckItem)
				.And(EntityIsRefreshed)
				.When(StateIsSet, CheckItemStateType.Complete)
				.Then(MockApiPutIsCalled<IJsonCheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set State property to same")
				.Given(ACheckItem)
				.And(EntityIsRefreshed)
				.And(StateIs, CheckItemStateType.Complete)
				.When(StateIsSet, CheckItemStateType.Complete)
				.Then(MockApiPutIsCalled<IJsonCheckItem>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set State property without UserToken")
				.Given(ACheckItem)
				.And(TokenNotSupplied)
				.When(StateIsSet, CheckItemStateType.Complete)
				.Then(MockApiPutIsCalled<IJsonCheckItem>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var story = new Story("Delete");

			var feature = story.InOrderTo("delete a check list item")
				.AsA("developer")
				.IWant("to call Delete");

			feature.WithScenario("Delete is called")
				.Given(ACheckItem)
				.When(DeleteIsCalled)
				.Then(MockApiDeleteIsCalled<IJsonCheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called without UserToken")
				.Given(ACheckItem)
				.And(TokenNotSupplied)
				.When(DeleteIsCalled)
				.Then(MockApiPutIsCalled<IJsonCheckItem>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		#region Given

		private void ACheckItem()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Svc.Object;
			var card = new Mock<Card>();
			card.SetupGet(c => c.Id).Returns(TrelloIds.Invalid);
			_systemUnderTest.Sut.Owner = new CheckList {Id = TrelloIds.Invalid, Card = card.Object};
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Svc.Object;
			SetupMockGet<IJsonCheckItem>();
		}
		private void NameIs(string value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Name = value);
		}
		private void PositionIs(Position value)
		{
			SetupProperty(() => _systemUnderTest.Sut.Position = value);
		}
		private void StateIs(CheckItemStateType value)
		{
			SetupProperty(() => _systemUnderTest.Sut.State = value);
		}

		#endregion

		#region When

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
		private void StateIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.State);
		}
		private void StateIsSet(CheckItemStateType value)
		{
			Execute(() => _systemUnderTest.Sut.State = value);
		}
		private void DeleteIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.Delete());
		}

		#endregion
	}
}
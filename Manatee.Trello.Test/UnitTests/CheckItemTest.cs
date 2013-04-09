using System;
using Manatee.Trello.Test.FunctionalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
				.And(EntityIsNotExpired)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<CheckItem>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Name property when expired")
				.Given(ACheckItem)
				.And(EntityIsExpired)
				.When(NameIsAccessed)
				.Then(MockApiGetIsCalled<CheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property")
				.Given(ACheckItem)
				.When(NameIsSet, "name")
				.Then(MockApiPutIsCalled<CheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to null")
				.Given(ACheckItem)
				.And(NameIs, "name")
				.When(NameIsSet, (string) null)
				.Then(MockApiPutIsCalled<CheckItem>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Name property to empty")
				.Given(ACheckItem)
				.And(NameIs, "not description")
				.When(NameIsSet, string.Empty)
				.Then(MockApiPutIsCalled<CheckItem>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Name property to same")
				.Given(ACheckItem)
				.And(NameIs, "description")
				.When(NameIsSet, "description")
				.Then(MockApiPutIsCalled<CheckItem>, 0)
				.And(ExceptionIsNotThrown)

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
				.And(EntityIsNotExpired)
				.When(PositionIsAccessed)
				.Then(MockApiGetIsCalled<CheckItem>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Position property when expired")
				.Given(ACheckItem)
				.And(EntityIsExpired)
				.When(PositionIsAccessed)
				.Then(MockApiGetIsCalled<CheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property")
				.Given(ACheckItem)
				.When(PositionIsSet, (Position) PositionValue.Bottom)
				.Then(MockApiPutIsCalled<CheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property to null")
				.Given(ACheckItem)
				.And(PositionIs, (Position) PositionValue.Bottom)
				.When(PositionIsSet, (Position) null)
				.Then(MockApiPutIsCalled<CheckItem>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set Position property to same")
				.Given(ACheckItem)
				.And(PositionIs, (Position) PositionValue.Bottom)
				.When(PositionIsSet, (Position) PositionValue.Bottom)
				.Then(MockApiPutIsCalled<CheckItem>, 0)
				.And(ExceptionIsNotThrown)

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
				.And(EntityIsNotExpired)
				.When(StateIsAccessed)
				.Then(MockApiGetIsCalled<CheckItem>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access State property when expired")
				.Given(ACheckItem)
				.And(EntityIsExpired)
				.When(StateIsAccessed)
				.Then(MockApiGetIsCalled<CheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set State property")
				.Given(ACheckItem)
				.When(StateIsSet, CheckItemStateType.Complete)
				.Then(MockApiPutIsCalled<CheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set State property to same")
				.Given(ACheckItem)
				.And(StateIs, CheckItemStateType.Complete)
				.When(StateIsSet, CheckItemStateType.Complete)
				.Then(MockApiPutIsCalled<CheckItem>, 0)
				.And(ExceptionIsNotThrown)

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
				.Then(MockApiDeleteIsCalled<CheckItem>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ACheckItem()
		{
			_systemUnderTest = new SystemUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
			_systemUnderTest.Sut.Owner = new CheckList {Id = TrelloIds.Invalid};
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
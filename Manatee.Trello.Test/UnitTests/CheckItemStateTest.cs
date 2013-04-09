using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class CheckItemStateTest : EntityTestBase<CheckItemState>
	{
		[TestMethod]
		public void State()
		{
			var story = new Story("State");

			var feature = story.InOrderTo("get a state of a check list item")
				.AsA("developer")
				.IWant("to get or set the State");

			feature.WithScenario("Access State property")
				.Given(ACheckItemState)
				.And(EntityIsNotExpired)
				.When(StateIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access State property when expired")
				.Given(ACheckItemState)
				.And(EntityIsExpired)
				.When(StateIsAccessed)
				.Then(MockApiGetIsCalled<Card>, 0)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ACheckItemState()
		{
			_systemUnderTest = new SystemUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
		}

		#endregion

		#region When

		private void StateIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.State);
		}

		#endregion
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using Manatee.Trello.Moq;
using Moq;
using NUnit.Framework;

namespace Manatee.Trello.UnitTests
{
	[TestFixture]
	public class Class1
	{
		[OneTimeSetUp]
		public void Setup()
		{
			TrelloMock.Initialize();
		}

		[Test]
		public void Test()
		{
			var action = new ActionMock();
			var list = new List<Action>
				{
					action.Object
				};
			var card = new CardMock();

			card.Actions.Setup(c => c.GetEnumerator()).Returns(list.GetEnumerator());

			foreach (var test in card.Object.Actions)
			{
				Console.Write(test.Type);
			}
		}
	}
}

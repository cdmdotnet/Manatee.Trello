using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Manatee.Trello.Moq.Tests
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

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test.FunctionalTests
{
	[TestClass]
	public class TrelloServiceFunctionalTest
	{
		[TestMethod]
		public void Retrieve()
		{
			var story = new Story("TrelloService.Retrieve");

			var feature = story.InOrderTo("retrieve data from Trello.com")
				.AsA("developer")
				.IWant("TrelloService to provide the requested data.");

			//feature.WithScenario("Retrieve a Board")
			//    .Given(AValidBoardId)
			//    .When(RetrieveIsCalled<Board>)
			//    .Then()
		}
	}
}

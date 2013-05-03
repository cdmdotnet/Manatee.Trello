using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Json.Newtonsoft;
using Manatee.Trello.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			//Options.UseNewtonsoftJson();
			var service = new TrelloService(TrelloIds.Key, TrelloIds.Token);

			var board = service.Retrieve<Board>(TrelloIds.BoardId);
			var members = board.Members();

			foreach (var member in members)
			{
				Console.WriteLine(member);
			}
		}
	}
}

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
			var service = new TrelloService(TrelloIds.AppKey);

			var token = service.Retrieve<Token>(TrelloIds.UserToken);

			Console.WriteLine(token.Member);
		}
	}
}

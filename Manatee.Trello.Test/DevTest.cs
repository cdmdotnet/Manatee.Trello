using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
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
			Options.UseNewtonsoftJson();
			var service = new TrelloService(TrelloIds.AppKey, TrelloIds.UserToken);
			var item = service.Retrieve<Card>("bPRrY7Rf");
			var attachment = item.Attachments.First();
			Console.WriteLine(attachment);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
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
		[Ignore]
		public void TestMethod1()
		{
			var service = new TrelloService(TrelloIds.AppKey, TrelloIds.UserToken);
			var member = service.Me;

			Console.WriteLine(member.FullName);
		}
	}
}

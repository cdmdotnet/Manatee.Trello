using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Json.Extensions;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;
using Manatee.Trello.Test.FunctionalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			var service = new TrelloService(TrelloIds.Key, TrelloIds.Token);

			var member = service.Retrieve<Member>(TrelloIds.UserName);
			var notification = member.Notifications.LastOrDefault();

			Console.WriteLine(notification.ToJson().GetIndentedString());
		}
	}
}

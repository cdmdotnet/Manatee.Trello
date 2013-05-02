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

			var member = service.Retrieve<Member>(TrelloIds.MemberId);
			var notifications = member.Notifications;

			foreach (var notification in notifications)
			{
				Console.WriteLine(notification);
			}
		}
	}
}

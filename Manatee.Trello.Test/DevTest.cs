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
			//Options.UseNewtonsoftJson();
			var service = new TrelloService(TrelloIds.AppKey, TrelloIds.UserToken);
			var context = new List<ExpiringObject>
			              	{
			              		service.Retrieve<Board>(TrelloIds.BoardId),
			              		service.Retrieve<Organization>(TrelloIds.OrganizationId),
			              		service.Retrieve<Member>(TrelloIds.MemberId),
			              		service.Retrieve<Board>("5144051cbd0da6681200201e")
			              	};
			var item = service.Search("card", context);
			foreach (var child in item.Cards)
			{
				Console.WriteLine(child);
			}
			Console.WriteLine();
			item = service.Search("card");
			foreach (var child in item.Cards)
			{
				Console.WriteLine(child);
			}
			Console.WriteLine();
			Console.WriteLine(item);
		}
	}
}

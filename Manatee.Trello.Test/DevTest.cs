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
			//Options.UseNewtonsoftJson();
			var service = new TrelloService(TrelloIds.AppKey, TrelloIds.UserToken);

			var list = service.Retrieve<List>(TrelloIds.ListId);
			var card = list.Cards.Last();

			var attachment = card.AddAttachment("Iron Manatee", "http://i.imgur.com/H7ybFd0.png");

			Console.WriteLine(attachment);

			attachment.Delete();

			Console.WriteLine(card.Attachments.Count());
		}
	}
}

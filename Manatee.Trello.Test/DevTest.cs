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
			Options.UseNewtonsoftJson();
			var service = new TrelloService(TrelloIds.Key, TrelloIds.Token);

			var results = service.Search("move");

			Console.WriteLine("Actions");
			foreach (var action in results.Actions)
			{
				Console.WriteLine(action);
			}
			Console.WriteLine();
			Console.WriteLine("Boards");
			foreach (var board in results.Boards)
			{
				Console.WriteLine(board);
			}
			Console.WriteLine();
			Console.WriteLine("Cards");
			foreach (var card in results.Cards)
			{
				Console.WriteLine(card);
			}
			Console.WriteLine();
			Console.WriteLine("Members");
			foreach (var member in results.Members)
			{
				Console.WriteLine(member);
			}
			Console.WriteLine();
			Console.WriteLine("Organizations");
			foreach (var organization in results.Organizations)
			{
				Console.WriteLine(organization);
			}
		}
	}
}

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
			var service = new TrelloService(TrelloIds.AppKey);
			var board = service.Retrieve<Board>("5144051cbd0da6681200201e");
			var start = DateTime.Now;
			Console.WriteLine(board);
			Console.WriteLine();
			foreach (var list in board.Lists)
			{
				Console.WriteLine("    {0}", list);
				foreach (var card in list.Cards)
				{
					Console.WriteLine("        {0}", card);
				}
				Console.WriteLine();
			}
			var end = DateTime.Now;
			Console.WriteLine("Total Time: {0}", end - start);
			Console.WriteLine();

			start = DateTime.Now;
			Console.WriteLine(board);
			Console.WriteLine();
			foreach (var list in board.Lists)
			{
				Console.WriteLine("    {0}", list);
				foreach (var card in list.Cards)
				{
					Console.WriteLine("        {0}", card);
				}
				Console.WriteLine();
			}
			end = DateTime.Now;
			Console.WriteLine("Total Time: {0}", end - start);
		}
	}
}

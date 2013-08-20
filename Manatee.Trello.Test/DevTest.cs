using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Manatee.Trello.Contracts;
using Manatee.Trello.Json;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.ManateeJson.Entities;
using Manatee.Trello.Rest;
using Manatee.Trello.RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		[TestMethod]
		//[Ignore]
		public void TestMethod1()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);

			var service = new TrelloService(options, TrelloIds.AppKey, TrelloIds.UserToken);
			var me = service.Me;

			Console.WriteLine(me);
			foreach (var board in me.Boards)
			{
				Console.WriteLine("  {0}", board);
				foreach (var list in board.Lists)
				{
					Console.WriteLine("    {0}", list);
					foreach (var card in list.Cards)
					{
						Console.WriteLine("      {0}", card);
					}
				}
			}
			Console.WriteLine();
			foreach (var action in me.Actions)
			{
				Console.WriteLine(action);
			}
		}
	}
}

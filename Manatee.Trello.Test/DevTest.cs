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
			var service = new TrelloService(TrelloIds.Key, TrelloIds.Token);

			var restClientProvider = (RestSharpClientProvider)service.RestClientProvider;
			var serializer = new NewtonsoftSerializer();
			restClientProvider.Serializer = serializer;
			restClientProvider.Deserializer = serializer;

			var card = service.Retrieve<Card>(TrelloIds.CardId);
			var actions = card.Actions.ToList();
			var board = card.Board;
			var list = board.Lists.First();

			foreach (var action in actions)
			{
				Console.WriteLine(action);
			}
			Console.WriteLine(board);
			Console.WriteLine(list);
		}
	}
}

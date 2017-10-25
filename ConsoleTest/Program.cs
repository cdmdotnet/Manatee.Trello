using System;
using System.Collections.Generic;
using Manatee.Trello;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;
using Manatee.Trello.Test;
using Action = System.Action;

namespace ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
		    Run(() =>
		        {
		            var board = new Board(TrelloIds.BoardId);
		            var type = board.PowerUps[0].GetType();
		            Console.WriteLine(board.PowerUps[0].Name);
		            Console.WriteLine(board.PowerUps[0].Id);
		            Console.WriteLine(type);

		            TrelloProcessor.Flush();
		        });
		}

		private static void Run(Action action)
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new RestSharpClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			TrelloConfiguration.ThrowOnTrelloError = true;

			action();
		}

		private static void OutputCollection<T>(string section, IEnumerable<T> collection)
		{
			Console.WriteLine(section);
			foreach (var item in collection)
			{
				Console.WriteLine("    {0}", item);
			}
		}
	}
}

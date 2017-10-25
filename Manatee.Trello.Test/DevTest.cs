using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.CustomFields;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			Run(() =>
				{
					TrelloConfiguration.ExpiryTime = TimeSpan.Zero;

					var board = new Board(TrelloIds.BoardId);
					var type = board.PowerUps[0].GetType();
					Console.WriteLine(board.PowerUps[0].Name);
					Console.WriteLine(board.PowerUps[0].Id);
					Console.WriteLine(type);

					CustomFieldsPowerUp.Register();
					type = board.PowerUps[0].GetType();
					Console.WriteLine(type);

					TrelloProcessor.Flush();
				});
		}

		private static void Run(System.Action action)
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new WebApiClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			action();

			TrelloProcessor.Flush();
		}

		private static void OutputCollection<T>(string section, IEnumerable<T> collection)
		{
			Console.WriteLine();
			Console.WriteLine(section);
			foreach (var item in collection)
			{
				Console.WriteLine($"    {item}");
			}
		}
	}
}

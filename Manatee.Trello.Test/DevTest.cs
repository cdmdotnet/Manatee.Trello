using System;
using System.Collections.Generic;
using System.Linq;
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
					var search = new Search(SearchFor.TextInName("greg"), 1, SearchModelType.Boards);
					var board = search.Boards.FirstOrDefault();
					var background = board.Preferences.Background;

					OutputCollection("recent cards", board.Cards.Filter(new DateTime(2017, 1, 1)));
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

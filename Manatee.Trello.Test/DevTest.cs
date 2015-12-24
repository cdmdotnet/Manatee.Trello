using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.Rest;
using Manatee.Trello.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		private class RestResponse<T> : IRestResponse<T>
		{
			public string Content { get; set; }
			public Exception Exception { get; set; }
			public T Data { get; set; }
		}

		[TestMethod]
		public void TestMethod1()
		{
			Run(() =>
				{
					var card = new Card(TrelloIds.CardId);
					OutputCollection(string.Format("CommentCount: {0}", card.Badges.Comments), card.Comments.Select(c => new Tuple<Action, DateTime?>(c, c.Data.LastEdited)));
					var copy = card.List.Cards.Add(card);
					OutputCollection(string.Format("CommentCount: {0}", copy.Badges.Comments), copy.Comments.Select(c => new Tuple<Action, DateTime?>(c, c.Data.LastEdited)));
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

			TrelloProcessor.Shutdown();
		}

		private static void OutputCollection<T>(string section, IEnumerable<T> collection)
		{
			Console.WriteLine();
			Console.WriteLine(section);
			foreach (var item in collection)
			{
				Console.WriteLine("    {0}", item);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.Rest;
using Manatee.Trello.RestSharp;
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
					var name = "ÅÄÖ";
					var list = new List(TrelloIds.ListId);
					var card = list.Cards.FirstOrDefault(c => c.Name == name) ?? list.Cards.Add(name);
					var hook = new Webhook<Card>(card, "http://requestb.in/o841yno8");
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

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
					TrelloConfiguration.RegisterPowerUp("56d5e249a98895a9797bebb9", (j, a) => new CustomFieldsPowerUp(j, a));

					var card = new Card(TrelloIds.CardId);
					OutputCollection("PowerUpData", card.PowerUpData.ToList());
					foreach (var field in card.CustomFields())
					{
						Console.WriteLine($"{field.Key} - {field.Value}");
					}
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

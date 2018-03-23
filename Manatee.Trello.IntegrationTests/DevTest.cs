using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.CustomFields;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.Tests.Common;
using Manatee.Trello.WebApi;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class DevTest
	{
		[Test]
		//[Ignore("This test fixture for development purposes only.")]
		public void TestMethod1()
		{
			Run(() =>
				{
					var card = new Card("3rm0AZg5");
					Console.WriteLine(card.Id);

					var fields = card.CustomFields();

					foreach (var field in fields)
					{
						Console.WriteLine($"{field.Name} = {field.Value}");
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

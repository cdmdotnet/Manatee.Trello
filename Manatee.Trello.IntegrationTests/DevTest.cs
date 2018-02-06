using System;
using System.Collections.Generic;
using System.Linq;
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
		[Ignore("This test fixture for development purposes only.")]
		public void TestMethod1()
		{
			Run(() =>
				{
					var list = new List(TrelloIds.ListId);
					var card = list.Cards[0];

					var attachment = card.Attachments.Add("https://somethingmassive.com/wp-content/themes/somethingmassive16/assets/images/logo-full.svg", "test");

					Console.WriteLine(attachment.Id);

					//OutputCollection("attachments", card.Attachments);
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

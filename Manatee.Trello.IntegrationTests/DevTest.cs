using System;
using System.Collections.Generic;
using System.Linq;
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
					var board = new Board(TrelloIds.BoardId);
					var definitions = board.CustomFields.ToList();
					var card = new Card(TrelloIds.CardId);
					
					Console.WriteLine(card.Id);
					Console.WriteLine(card);

					OutputCollection("custom fields", card.CustomFields);
				});
		}

		private static void Run(System.Action action)
		{
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

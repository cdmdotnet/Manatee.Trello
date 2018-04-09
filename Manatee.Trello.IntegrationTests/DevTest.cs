using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manatee.Trello.Tests.Common;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	//[Ignore("This test fixture for development purposes only.")]
	public class DevTest
	{
		private readonly TrelloFactory Factory = new TrelloFactory();

		[Test]
		public async Task TestMethod1()
		{
			await Run(async () =>
				{
					var board = Factory.Board(TrelloIds.BoardId);
					var definitions = board.CustomFields.ToList();
					var card = Factory.Card(TrelloIds.CardId);

					Console.WriteLine(card);

					OutputCollection("custom fields", card.CustomFields);
				});
		}

		[Test]
		public async Task TestMethod1Async()
		{
			await Run(async () =>
				{
					TrelloConfiguration.AutoUpdate = false;

					var board = Factory.Board(TrelloIds.BoardId);

					await board.Refresh();

					var definitions = board.CustomFields.Refresh();
					var card = Factory.Card(TrelloIds.CardId);

					Console.WriteLine(card);

					await card.Refresh();

					OutputCollection("custom fields", card.CustomFields);
				});
		}

		private static async Task Run(Func<Task> action)
		{
			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			await action();

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

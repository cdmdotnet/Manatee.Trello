using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Tests.Common;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	//[Ignore("This test fixture for development purposes only.")]
	public class DevTest
	{
		private readonly TrelloFactory _factory = new TrelloFactory();

		[Test]
		public async Task TestMethod1()
		{
			await Run(async () =>
				{
					var board = _factory.Board(TrelloIds.BoardId);

					await board.Refresh();

					Console.WriteLine(board);

					OutputCollection("actions", board.Actions);
					OutputCollection("cards", board.Cards);
					OutputCollection("custom fields", board.CustomFields);
					OutputCollection("labels", board.Labels);
					OutputCollection("lists", board.Lists);
					OutputCollection("members", board.Members);
					OutputCollection("memberships", board.Memberships);
					OutputCollection("powerups", board.PowerUps);
					OutputCollection("powerup data", board.PowerUpData);
				});
		}

		private static async Task Run(Func<Task> action)
		{
			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			//TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

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

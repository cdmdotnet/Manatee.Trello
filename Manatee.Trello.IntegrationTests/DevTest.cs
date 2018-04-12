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
					var entity = _factory.Card(TrelloIds.CardId);

					await entity.Refresh();

					Console.WriteLine(entity);

					OutputCollection("actions", entity.Actions);
					OutputCollection("checkLists", entity.CheckLists);
					OutputCollection("members", entity.Members);
					//OutputCollection("memberships", entity.Memberships);
					OutputCollection("powerup data", entity.PowerUpData);
					OutputCollection("stickers", entity.Stickers);
				});
		}

		private static async Task Run(Func<Task> action)
		{
			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			//TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			await action();

			await TrelloProcessor.Flush();
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

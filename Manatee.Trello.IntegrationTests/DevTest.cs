using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manatee.Trello.Tests.Common;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class DevTest
	{
		private readonly TrelloFactory Factory = new TrelloFactory();

		[Test]
		//[Ignore("This test fixture for development purposes only.")]
		public async Task TestMethod1()
		{
			Run(() =>
				{
					var board = Factory.Board(TrelloIds.BoardId);
					var definitions = board.CustomFields.ToList();
					var card = Factory.Card(TrelloIds.CardId);
					
					Console.WriteLine(card.Id);
					Console.WriteLine(card);

					OutputCollection("custom fields", card.CustomFields);
				});
		}

		private static void Run(System.Action action)
		{
			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			TrelloConfiguration.AutoUpdate = false;

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

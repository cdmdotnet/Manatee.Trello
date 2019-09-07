using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Logging;
using Manatee.Trello.Rest;
using Manatee.Trello.Tests.Common;
using Moq;
using NUnit.Framework;

namespace Manatee.Trello.UnitTests
{
	[TestFixture]
	[Ignore("This test fixture for development purposes only.")]
	public class DevTest
	{
		private readonly TrelloFactory _factory = new TrelloFactory();

		[Test]
		public async Task TestMethod1()
		{
			TrelloConfiguration.Log = new DebugLog();

			await Run(async ct =>
				{
					for (int i = 0; i < 20; i++)
					{
						var watch = new Stopwatch();
						watch.Start();
						await DoIt(ct);
						watch.Stop();
						Console.WriteLine(watch.ElapsedMilliseconds);
						Console.WriteLine();
						//TrelloConfiguration.Cache.Clear();
					}
				});
		}

		private async Task DoIt(CancellationToken ct)
		{
			var board = _factory.Board(TrelloIds.BoardId);
			await board.Refresh(true, ct);

			Console.WriteLine(board);

			var list1 = board.Lists[0];
			var list2 = board.Lists[1];
			var list3 = board.Lists[2];

			await Task.WhenAll(list1.Refresh(true, ct),
			                   list2.Refresh(true, ct),
			                   list3.Refresh(true, ct));

			Console.WriteLine(list1);
			Console.WriteLine(list2);
			Console.WriteLine(list3);

			var allCards = list1.Cards.Concat(list2.Cards).Concat(list3.Cards).ToList();

			Console.WriteLine(allCards.Count);
		}

		private static async Task Run(Func<CancellationToken, Task> action)
		{
			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = Environment.GetEnvironmentVariable("TRELLO_USER_TOKEN");
			License.RegisterLicense(Environment.GetEnvironmentVariable("TRELLO_LICENSE"));

			await action(CancellationToken.None);

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

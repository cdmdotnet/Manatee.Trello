using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Manatee.Json;
using Manatee.Trello.Internal;
using Manatee.Trello.Rest;
using Manatee.Trello.Tests.Common;
using Moq;
using NUnit.Framework;

namespace Manatee.Trello.UnitTests
{
	[TestFixture]
	//[Ignore("This test fixture for development purposes only.")]
	public class DevTest
	{
		private readonly TrelloFactory _factory = new TrelloFactory();

		[Test]
		public async Task TestMethod1()
		{
			await Run(async ct =>
				{
					var board = new Board(TrelloIds.BoardId);
					await board.Refresh(ct: ct);

					var f = new TrelloFactory();
					var card = f.Card(TrelloIds.CardId);

					await card.Comments.Refresh(ct: ct);
					var comment = card.Comments.OrderBy(c => c.CreationDate).LastOrDefault();

					await comment.Creator.Refresh(ct: ct);
					var creator = comment.Creator;

					Console.WriteLine(creator);
				});
		}

		private static async Task Run(Func<CancellationToken, Task> action)
		{
			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

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

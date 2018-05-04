using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;
using Manatee.Trello.Tests.Common;
using Manatee.Trello.UnitTests;
using Moq;
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
			await Run(async ct =>
				{
					var json = File.ReadAllText("C:\\Users\\gregs\\Downloads\\Board_JSON.json");

					var response = new Mock<IRestResponse<IJsonBoard>>();
					response.SetupGet(r => r.StatusCode)
					        .Returns(HttpStatusCode.OK);
					response.SetupGet(r => r.Content)
					        .Returns(json);

					var jsonBoard = TrelloConfiguration.Deserializer.Deserialize(response.Object);

					response.SetupGet(r => r.Data)
					        .Returns(jsonBoard);

					MockHost.MockRest<IJsonBoard>();
					MockHost.Client.Setup(c => c.Execute<IJsonBoard>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
					        .ReturnsAsync(() => response.Object);

					var board = new Board(TrelloIds.BoardId);

					await board.Refresh(ct);

					Console.WriteLine(board);
				});
		}

		private static async Task Run(Func<CancellationToken, Task> action)
		{
			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			//TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

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

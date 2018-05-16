using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Json;
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
			//await Run(async ct =>
				{
					Console.WriteLine($"before: {Environment.GetEnvironmentVariable("TEST_VAR", EnvironmentVariableTarget.User)}");
					Environment.SetEnvironmentVariable("TEST_VAR", Guid.NewGuid().ToString(), EnvironmentVariableTarget.User);
					Console.WriteLine($"after: {Environment.GetEnvironmentVariable("TEST_VAR", EnvironmentVariableTarget.User)}");
				}//);
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

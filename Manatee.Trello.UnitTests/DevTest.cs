using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.CardRepeater;
using Manatee.Trello.Tests.Common;
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
					CardRepeaterPowerUp.Register();

					var card = _factory.Card("R1Kc5KHd");

					var repitition = card.Repitition();
				});
		}

		private static async Task Run(Func<CancellationToken, Task> action)
		{
			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = Environment.GetEnvironmentVariable("TRELLO_USER_TOKEN") ?? TrelloIds.UserToken;

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

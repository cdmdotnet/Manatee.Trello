using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
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
		public void Issue241_RedundantMeRefresh()
		{
			Run(async ct =>
				{
					var text = File.ReadAllText(@"c:\users\gregs\desktop\response.json");
					var json = JsonValue.Parse(text);

					var foundTypes = json.Object["notifications"].Array.Select(jv => jv.Object["type"].String);
					var allTypes = Enum.GetValues(typeof(NotificationType)).Cast<NotificationType>().Select(t => t.GetDescription());

					var newTypes = foundTypes.Except(allTypes);

					OutputCollection("new types", newTypes);

				}).Wait();
		}

		[Test]
		public async Task TestMethod1()
		{
			await Run(async ct =>
				{
					var me = await _factory.Me(ct);
					await me.BoardBackgrounds.Refresh(true, ct);
					OutputCollection("backgrounds", me.BoardBackgrounds);

					var data = File.ReadAllBytes("C:\\Users\\gregs\\OneDrive\\Public\\Manatee Open-Source (shadow).png");
					var newBackground = await me.BoardBackgrounds.Add(data, ct);

					Console.WriteLine(newBackground);

					await newBackground.Delete(ct);
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

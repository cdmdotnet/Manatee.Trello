using System;
using System.Collections.Generic;
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
					var licenseText = "";

					License.RegisterLicense(licenseText);
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

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Manatee.Trello.Rest;
using Manatee.Trello.Tests.Common;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[SetUpFixture]
	public class TestEnvironment
	{
		public static TestEnvironment Current { get; private set; }

		public ITrelloFactory Factory { get; private set; }
		public IMe Me { get; private set; }
		public IOrganization Organization { get; private set; }
		public IBoard Board { get; private set; }

		public IRestRequest LastRequest { get; private set; }
		public IRestResponse LastResponse { get; private set; }

		[OneTimeSetUp]
		public async Task BuildEnvironment()
		{
			if (Current != null) throw new InvalidOperationException("Test setup occurring twice...");

			if (File.Exists("Manatee.Trello.run"))
				File.Delete("Manatee.Trello.run");

			Current = this;

			EnsureLicense();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = _GetUserToken();

			TrelloConfiguration.RestClientProvider =
				new CapturingClientProvider(TrelloConfiguration.RestClientProvider,
				                            r => LastRequest = r,
				                            r => LastResponse = r);
			TrelloConfiguration.Log = new ConsoleLog();

			var testTimeStamp = $"{DateTime.Now:yyMMddHHmmss}";

			Factory = new TrelloFactory();
			Me = await Factory.Me();

			Organization = await Me.Organizations.Add($"TestOrg_{testTimeStamp}");
			Board = await Organization.Boards.Add($"TestBoard_{testTimeStamp}");

			await Organization.Refresh();
			await Board.Refresh();
		}

		private static void EnsureLicense()
		{
			var license = Environment.GetEnvironmentVariable("TRELLO_LICENSE");

			if (license == null) return;

			License.RegisterLicense(license);
		}

		private static string _GetUserToken()
		{
			var envVar = Environment.GetEnvironmentVariable("TRELLO_USER_TOKEN");

			return envVar ?? TrelloIds.UserToken;
		}

		[OneTimeTearDown]
		public async Task DestroyEnvironment()
		{
			try
			{
				if (Board != null)
					await Board.Delete();
			}
			catch
			{
			}

			try
			{
				if (Organization != null)
					await Organization.Delete();
			}
			catch
			{
			}

			try
			{
				var licenseCounts = File.ReadAllText("Manatee.Trello.run");
				Console.WriteLine(licenseCounts);
			}
			catch
			{
			}
		}

		public async Task<IList> BuildList([CallerMemberName] string name = null)
		{
			name = $"{name}_List_{DateTime.Now:yyMMddHHmmss}";

			var list = await Board.Lists.Add(name);

			return list;
		}

		public async Task<ICard> BuildCard([CallerMemberName] string name = null)
		{
			name = $"{name}_Card_{DateTime.Now:yyMMddHHmmss}";

			var list = await Board.Lists.Add(name);

			var card = await list.Cards.Add(name);

			return card;
		}

		public async Task<ICheckList> BuildCheckList([CallerMemberName] string name = null)
		{
			name = $"{name}_CheckList_{DateTime.Now:yyMMddHHmmss}";

			var list = await Board.Lists.Add(name);

			var card = await list.Cards.Add(name);
			var checkList = await card.CheckLists.Add(name);

			return checkList;
		}

		public async Task<ICheckItem> BuildCheckItem([CallerMemberName] string name = null)
		{
			name = $"{name}_CheckItem_{DateTime.Now:yyMMddHHmmss}";

			var list = await Board.Lists.Add(name);

			var card = await list.Cards.Add(name);
			var checkList = await card.CheckLists.Add(name);
			var checkItem = await checkList.CheckItems.Add(name);

			return checkItem;
		}
	}
}

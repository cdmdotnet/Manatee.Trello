using System;
using Manatee.Trello.Tests.Common;
using Manatee.Trello.WebApi;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	[Ignore("This is not ready")]
	public class ScriptedTest
	{
		[OneTimeSetUp]
		public void Setup()
		{
			TrelloConfiguration.RestClientProvider = new WebApiClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = "0f3f5a4039c992dabcf82fd1daa4ed12590eeb6407635c0f8da7518af1721498";
		}

		[Test]
		public void Run()
		{
			IBoard board = null;
			try
			{
				board = Member.Me.Boards.Add($"TestBoard{Guid.NewGuid()}");
			}
			finally
			{
				board?.Delete();

				TrelloProcessor.Flush();
			}
		}
	}
}

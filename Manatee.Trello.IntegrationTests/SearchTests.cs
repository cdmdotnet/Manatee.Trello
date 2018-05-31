using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Manatee.Trello.Tests.Common;
using NUnit.Framework;

namespace Manatee.Trello.IntegrationTests
{
	[TestFixture]
	public class SearchTests
	{
		[Test]
		public async Task SearchReturnsCards()
		{
			// Due to indexing and/or data distribution latency on the Trello servers, this test
			// must be performed on pre-existing data.  Let's hope that the sandbox remains in a
			// good state...
			var board = TestEnvironment.Current.Factory.Board(TrelloIds.BoardId);
			var search = TestEnvironment.Current.Factory.Search("backup", modelTypes: SearchModelType.Cards, context: new[] {board});

			await search.Refresh();

			search.Cards.Should().Contain(c => c.Name.ToLower() == "account backup");
			search.Cards.Should().Contain(c => c.Name.ToLower() == "board backup");
		}

		[Test]
		public async Task SearchForCardsWithLabel()
		{
			// Due to indexing and/or data distribution latency on the Trello servers, this test
			// must be performed on pre-existing data.  Let's hope that the sandbox remains in a
			// good state...
			var board = TestEnvironment.Current.Factory.Board(TrelloIds.BoardId);

			await board.Refresh();

			var label = board.Labels.First(l => l.Name == "test target");
			Console.WriteLine(label.Id);
			var search = TestEnvironment.Current.Factory.Search(label.Id, limit: 100, modelTypes: SearchModelType.Cards, context: new[] { board });

			await search.Refresh();

			search.Cards.Should().Contain(c => c.Name.ToLower() == "account backup");
			search.Cards.Should().Contain(c => c.Name.ToLower() == "board backup");
		}
	}
}

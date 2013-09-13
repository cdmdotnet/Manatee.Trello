using System;
using System.Linq;
using System.Text;
using System.Threading;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		[TestMethod]
		//[Ignore]
		public void TestMethod1()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);

			var start = DateTime.Now;
			var board = service.Retrieve<Board>(TrelloIds.BoardId);

			foreach (var boardMembership in board.Memberships)
			{
				Console.WriteLine(boardMembership);
			}

			var end = DateTime.Now;
			Console.WriteLine(end - start);
		}
	}
}

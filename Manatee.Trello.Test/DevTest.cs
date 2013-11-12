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

			//var board = service.Retrieve<Board>(TrelloIds.BoardId);
			//var webhook = board.CreateWebhook("http://requestb.in/1c0gbor1");

			var member = service.Retrieve<Member>("gregsdennis");
			foreach (var action in member.Actions)
			{
				Console.WriteLine(action);
			}
		}
		[TestMethod]
		public void AttachmentTest()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);

			var list = service.Retrieve<List>(TrelloIds.ListId);
			var card = list.AddCard("Attachment Test");
			card.AddAttachment("new attachment", TrelloIds.AttachmentUrl);
		}
	}
}

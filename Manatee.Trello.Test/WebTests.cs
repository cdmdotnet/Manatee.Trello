using System;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class WebTests
	{
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
		[TestMethod]
		public void GetAllActions()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);

			var member = service.Retrieve<Member>("gregsdennis");
			foreach (var action in member.Actions)
			{
				Console.WriteLine(action);
			}
		}
		[TestMethod]
		public void CardCreationDateTest()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);

			var list = service.Retrieve<List>(TrelloIds.ListId);
			foreach (var action in list.Actions)
			{
				Console.WriteLine(action);
			}

			var me = service.Me;
			foreach (var action in me.Actions)
			{
				Console.WriteLine(action);
			}
		}
		[TestMethod]
		public void TestBoardCardsExtensionMethod()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);

			var me = service.Me;
			foreach (var card in me.AllCards())
			{
				Console.WriteLine(card);
			}
		}
		[TestMethod]
		public void WebhookForBoardGeneratingWebException()
		{
			var options = new TrelloServiceConfiguration();
			var serializer = new ManateeSerializer();
			options.Serializer = serializer;
			options.Deserializer = serializer;
			options.RestClientProvider = new RestSharpClientProvider(options);

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(options, auth);

			var board = service.Retrieve<Board>(TrelloIds.BoardId);
			var hook = board.CreateWebhook("http://12.25.107.2/");

			Console.WriteLine(hook == null ? "null" : hook.IsActive.ToString());
		}
	}
}

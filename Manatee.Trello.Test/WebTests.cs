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
			TrelloServiceConfiguration.ThrowOnTrelloError = false;
			var serializer = new ManateeSerializer();
			TrelloServiceConfiguration.Serializer = serializer;
			TrelloServiceConfiguration.Deserializer = serializer;
			TrelloServiceConfiguration.RestClientProvider = new RestSharpClientProvider();

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(auth);

			var list = service.Retrieve<List>(TrelloIds.ListId);
			var card = list.AddCard("Attachment Test");
			card.AddAttachment("new attachment", TrelloIds.AttachmentUrl);
		}
		[TestMethod]
		public void GetAllActions()
		{
			TrelloServiceConfiguration.ThrowOnTrelloError = false;
			var serializer = new ManateeSerializer();
			TrelloServiceConfiguration.Serializer = serializer;
			TrelloServiceConfiguration.Deserializer = serializer;
			TrelloServiceConfiguration.RestClientProvider = new RestSharpClientProvider();

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(auth);

			var member = service.Retrieve<Member>("gregsdennis");
			foreach (var action in member.Actions)
			{
				Console.WriteLine(action);
			}
		}
		[TestMethod]
		public void CardCreationDateTest()
		{
			TrelloServiceConfiguration.ThrowOnTrelloError = false;
			var serializer = new ManateeSerializer();
			TrelloServiceConfiguration.Serializer = serializer;
			TrelloServiceConfiguration.Deserializer = serializer;
			TrelloServiceConfiguration.RestClientProvider = new RestSharpClientProvider();

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(auth);

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
			TrelloServiceConfiguration.ThrowOnTrelloError = false;
			var serializer = new ManateeSerializer();
			TrelloServiceConfiguration.Serializer = serializer;
			TrelloServiceConfiguration.Deserializer = serializer;
			TrelloServiceConfiguration.RestClientProvider = new RestSharpClientProvider();

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(auth);

			var me = service.Me;
			foreach (var card in me.AllCards())
			{
				Console.WriteLine(card);
			}
		}
		[TestMethod]
		public void WebhookForBoardGeneratingWebException()
		{
			TrelloServiceConfiguration.ThrowOnTrelloError = false;
			var serializer = new ManateeSerializer();
			TrelloServiceConfiguration.Serializer = serializer;
			TrelloServiceConfiguration.Deserializer = serializer;
			TrelloServiceConfiguration.RestClientProvider = new RestSharpClientProvider();

			var auth = new TrelloAuthorization(TrelloIds.AppKey, TrelloIds.UserToken);
			var service = new TrelloService(auth);

			var board = service.Retrieve<Board>(TrelloIds.BoardId);
			var hook = board.CreateWebhook("http://requestb.in/1k36jm21");

			Console.WriteLine(hook == null ? "null" : hook.IsActive.ToString());
		}
	}
}

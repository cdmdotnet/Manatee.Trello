using System;
using System.Linq;
using System.Text;
using System.Threading;
using Manatee.Json;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;
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

			var me = service.Me;

			foreach (var org in me.Organizations)
			{
				Console.WriteLine(org);
			}
		}
		[TestMethod]
		[Ignore]
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
		//[Ignore]
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
		[Ignore]
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
	}
}

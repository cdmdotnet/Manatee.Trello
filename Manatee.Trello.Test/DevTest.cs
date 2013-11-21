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

			var member = service.Retrieve<Member>("gregsdennis");
			var board = member.Boards.FirstOrDefault(b => b.Name.Contains("Sandbox"));
			foreach (var list in board.Lists)
			{
				Console.WriteLine("{0}: {1}", list, list.Cards.Count());
			}

			var body = "{\"action\":{\"id\":\"528196b42c1da06b3d00109c\",\"idMemberCreator\":\"50b693ad6f122b4310000a3c\"," +
					   "\"data\":{\"board\":{\"shortLink\":\"VHHdzCU0\",\"name\":\"Sandbox\",\"id\":\"51478f6469fd3d9341001dae\"}," +
					   "\"list\":{\"name\":\"List\",\"id\":\"51478f6469fd3d9341001daf\"},\"card\":{\"idShort\":170," +
					   "\"id\":\"5281900d11f20d237a00069b\"}},\"type\":\"deleteCard\",\"date\":\"2013-11-12T02:47:16.310Z\"," +
					   "\"memberCreator\":{\"id\":\"50b693ad6f122b4310000a3c\",\"avatarHash\":\"e97c40e0d0b85ab66661dbff5082d627\"," +
					   "\"fullName\":\"Greg Dennis\",\"initials\":\"GSD\",\"username\":\"gregsdennis\"}},\"model\":" +
					   "{\"id\":\"51478f6469fd3d9341001dae\",\"name\":\"Sandbox\",\"desc\":\"a new description set by the test\"," +
					   "\"descData\":{\"emoji\":{}},\"closed\":false,\"idOrganization\":\"50d4eb07a1b0902152003329\",\"pinned\":true," +
					   "\"url\":\"https://trello.com/b/VHHdzCU0/sandbox\",\"shortUrl\":\"https://trello.com/b/VHHdzCU0\"," +
					   "\"prefs\":{\"permissionLevel\":\"public\",\"voting\":\"disabled\",\"comments\":\"public\",\"invitations\":\"members\"," +
					   "\"selfJoin\":false,\"cardCovers\":true,\"background\":\"grey\",\"backgroundColor\":\"#A8A8A3\"," +
					   "\"backgroundImage\":null,\"backgroundImageScaled\":null,\"backgroundTile\":false,\"backgroundBrightness\":" +
					   "\"unknown\",\"canBePublic\":true,\"canBeOrg\":true,\"canBePrivate\":true,\"canInvite\":true}," +
					   "\"labelNames\":{\"yellow\":\"yellow\",\"red\":\"red\",\"purple\":\"purple\",\"orange\":\"orange\"," +
					   "\"green\":\"green\",\"blue\":\"blue\"}}}";
			service.ProcessWebhookNotification(body);
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
	}
}

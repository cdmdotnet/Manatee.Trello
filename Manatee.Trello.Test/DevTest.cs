using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Json;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.ManateeJson.Entities;
using Manatee.Trello.RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IQueryable = Manatee.Trello.Contracts.IQueryable;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			var prefs = "{\"permissionLevel\":\"public\",\"voting\":\"disabled\",\"comments\":\"public\",\"invitations\":\"admins\",\"selfJoin\":false,\"cardCovers\":true,\"calendarFeedEnabled\":false,\"background\":\"52f1250988a333e91877a347\",\"backgroundColor\":null,\"backgroundImage\":\"https://trello-backgrounds.s3.amazonaws.com/50b693ad6f122b4310000a3c/6859edf28d45e798376c4893c2e99b85/Manatee-Json-Board-Background.png\",\"backgroundImageScaled\":[{\"width\":140,\"height\":100,\"url\":\"https://trello-backgrounds.s3.amazonaws.com/50b693ad6f122b4310000a3c/03b8ce1cd24883e4e0c1520c3664fb40/Manatee-Json-Board-Background.png_140x100.png\"},{\"width\":480,\"height\":480,\"url\":\"https://trello-backgrounds.s3.amazonaws.com/50b693ad6f122b4310000a3c/35bb5ff120f1fe712bc2d661b3f5b368/Manatee-Json-Board-Background.png_480x480.png\"},{\"width\":960,\"height\":960,\"url\":\"https://trello-backgrounds.s3.amazonaws.com/50b693ad6f122b4310000a3c/ef72b888097125b746c00d32af2e5a8f/Manatee-Json-Board-Background.png_960x960.png\"},{\"width\":1024,\"height\":1024,\"url\":\"https://trello-backgrounds.s3.amazonaws.com/50b693ad6f122b4310000a3c/4867acacbcd5c6625efcec1cba03b93e/Manatee-Json-Board-Background.png_1024x1024.png\"},{\"width\":1280,\"height\":1280,\"url\":\"https://trello-backgrounds.s3.amazonaws.com/50b693ad6f122b4310000a3c/f79228e884cde190984461bddb7fa0a1/Manatee-Json-Board-Background.png_1280x1280.png\"}],\"backgroundTile\":false,\"backgroundBrightness\":\"unknown\",\"canBePublic\":true,\"canBeOrg\":true,\"canBePrivate\":true,\"canInvite\":true}";
			var json = JsonValue.Parse(prefs);

			var serializer = new ManateeSerializer().Serializer;
			var result = serializer.Deserialize<ManateeBoardPreferences>(json);
		}

		private static void Run(System.Action action)
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new RestSharpClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			action();

			TrelloProcessor.Shutdown();
		}

		private static void OutputCollection<T>(string section, IEnumerable<T> collection)
		{
			Console.WriteLine(section);
			foreach (var item in collection)
			{
				Console.WriteLine("    {0}", item);
			}
		}
	}
}

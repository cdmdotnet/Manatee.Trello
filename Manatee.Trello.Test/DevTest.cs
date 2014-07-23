using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.RequestProcessing;
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
		public void TestMethod1()
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new RestSharpClientProvider();

			TrelloAuthorization.Default.AppKey = TrelloIds.AppKey;
			TrelloAuthorization.Default.UserToken = TrelloIds.UserToken;

			var org = new Organization("trelloapps");

			Console.WriteLine("External members disabled: {0}", org.Preferences.ExternalMembersDisabled);
			Console.WriteLine("Associated domain: {0}", org.Preferences.AssociatedDomain);

			var board = new Board("iXQE21lJ");

			Console.WriteLine("Allow self-join: {0}", board.Preferences.AllowSelfJoin);
			Console.WriteLine("Commenting: {0}", board.Preferences.Commenting);

			SpinWait.SpinUntil(() => !RestRequestProcessor.HasRequests);
		}
	}
}

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
using Manatee.Trello.Extensions;
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
			Run(() =>
				{
					var board = new Board(TrelloIds.BoardId);
					foreach (var list in board.Lists.Filter(ListFilter.All))
					{
						Console.WriteLine(list);
						foreach (var card in list.Cards)
						{
							Console.WriteLine("- {0}", card);
							foreach (var label in card.Labels)
							{
								Console.WriteLine("  - {0}", label.Color);
							}
						}
					}
					Console.WriteLine(board);
				});
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

			TrelloConfiguration.ThrowOnTrelloError = true;

			action();

			Thread.Sleep(500);

			SpinWait.SpinUntil(() => !RestRequestProcessor.HasRequests);
		}
	}
}

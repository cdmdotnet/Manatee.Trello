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
using Manatee.Trello.Rest;
using Manatee.Trello.RestSharp;
using Manatee.Trello.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IQueryable = Manatee.Trello.Contracts.IQueryable;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		private class RestResponse<T> : IRestResponse<T>
		{
			public string Content { get; set; }
			public Exception Exception { get; set; }
			public T Data { get; set; }
		}

		[TestMethod]
		public void TestMethod1()
		{
			Run(() =>
				{
					var board = new Board("BVlClkAR");
					Console.WriteLine(board.CreationDate);
				});
		}

		private static void Run(System.Action action)
		{
			var serializer = new ManateeSerializer();
			TrelloConfiguration.Serializer = serializer;
			TrelloConfiguration.Deserializer = serializer;
			TrelloConfiguration.JsonFactory = new ManateeFactory();
			TrelloConfiguration.RestClientProvider = new WebApiClientProvider();

			TrelloAuthorization.Default.AppKey = "062109670e7f56b88783721892f8f66f";
			TrelloAuthorization.Default.UserToken = "45e7f6458f667684c8e8059dc069e4d35737f43df91a9dd4edd9cc2968af6ef1";

			action();

			TrelloProcessor.Shutdown();
		}

		private static void OutputCollection<T>(string section, IEnumerable<T> collection)
		{
			Console.WriteLine();
			Console.WriteLine(section);
			foreach (var item in collection)
			{
				Console.WriteLine("    {0}", item);
			}
		}
	}
}

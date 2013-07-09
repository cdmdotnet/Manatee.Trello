using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Json;
using Manatee.Trello.Json.Manatee.Entities;
using Manatee.Trello.Json.Newtonsoft;
using Manatee.Trello.Rest;
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
			//TrelloServiceConfiguration.Default.UseNewtonsoftJson();
			var service = new TrelloService(TrelloIds.AppKey, TrelloIds.UserToken);
			service.HoldRequests();
			var board = service.Retrieve<Board>("5144051cbd0da6681200201e");
			//var board = service.Retrieve<Board>(TrelloIds.Invalid);

			Console.WriteLine(board);

			Console.WriteLine("Queued requests:");

			foreach (var request in service.GetUnsentRequests())
			{
				Console.WriteLine("{0} ({1}) {2}", request.Request.Method, request.RequestedType, request.Request.Resource);
				foreach (var parameter in request.Request.Parameters)
				{
					Console.WriteLine("    {0}: {1}", parameter.Key, parameter.Value);
				}
				Console.WriteLine();
			}

			service.ResumeRequests();
		}
	}
}

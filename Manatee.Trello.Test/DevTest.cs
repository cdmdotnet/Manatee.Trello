using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Manatee.Trello.Json.Newtonsoft;
using Manatee.Trello.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test
{
	[TestClass]
	public class DevTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			//Options.UseNewtonsoftJson();
			var service = new TrelloService(TrelloIds.AppKey);

			var periods = Enum.GetValues(typeof (AuthorizationPeriod)).Cast<AuthorizationPeriod>();
			foreach (var authorizationPeriod in periods)
			{
				Console.WriteLine(authorizationPeriod.ToDescription());
			}
		}
	}
}

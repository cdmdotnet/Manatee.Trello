using System.Collections.Generic;
using Manatee.Trello.Rest;

namespace Manatee.Trello.UnitTests.Framework
{
	internal class MockedRestRequest : IRestRequest
	{
		public RestMethod Method { get; set; }
		public string Resource { get; set; }
		public IRestResponse Response { get; set; }
		public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();
		public object Body { get; private set; }

		public void AddParameter(string name, object value)
		{
			Parameters[name] = value;
		}

		public void AddBody(object body)
		{
			Body = body;
		}
	}
}
using System.Collections.Generic;
using Manatee.Trello.Rest;

namespace Manatee.Trello.UnitTests.Framework
{
	internal class MockedRestRequestProvider : IRestRequestProvider
	{
		public IRestRequest Create(string endpoint, IDictionary<string, object> parameters = null)
		{
			return new MockedRestRequest();
		}
	}
}
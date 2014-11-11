using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Rest;
using Manatee.Trello.Test.Unit.Factories;
using Moq;

namespace Manatee.Trello.Test.Unit.Mocks
{
	public class MockRestClient : IRestClient
	{
		private readonly List<Type> _calls;

		public MockRestClient()
		{
			_calls = new List<Type>();
		}

		public IRestResponse Execute(IRestRequest request)
		{
			var mock = new Mock<IRestResponse>();
			_calls.Add(null);
			return mock.Object;
		}
		public IRestResponse<TRequest> Execute<TRequest>(IRestRequest request)
			where TRequest : class
		{
			var mockResponse = new Mock<IRestResponse<TRequest>>();
			var mockData = JsonObjectFactory.Get<TRequest>();
			mockResponse.SetupGet(r => r.Data)
						.Returns(mockData.Object);
			_calls.Add(typeof (TRequest));
			return mockResponse.Object;
		}
		public void Verify(Type type, int count = -1)
		{
			if (count == -1)
				if (_calls.Any(c => c == type)) return;
				else
					throw new Exception("Expected at least one call, but no calls were made.");
			var found = _calls.Count(c => c == type);
			if (found != count)
				throw new Exception(string.Format("Expected {0} calls, but {1} calls were made", count, found));
		}
	}
}
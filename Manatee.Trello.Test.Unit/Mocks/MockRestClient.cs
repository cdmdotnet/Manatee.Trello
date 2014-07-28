using Manatee.Trello.Rest;
using Manatee.Trello.Test.Unit.Factories;
using Moq;

namespace Manatee.Trello.Test.Unit.Mocks
{
	public class MockRestClient : IRestClient
	{
		public IRestResponse Execute(IRestRequest request)
		{
			var mock = new Mock<IRestResponse>();
			return mock.Object;
		}
		public IRestResponse<TRequest> Execute<TRequest>(IRestRequest request)
			where TRequest : class
		{
			var mockResponse = new Mock<IRestResponse<TRequest>>();
			var mockData = JsonObjectFactory.Get<TRequest>();
			mockResponse.SetupGet(r => r.Data)
				.Returns(mockData.Object);
			return mockResponse.Object;
		}
	}
}
using Manatee.Trello.Rest;
using Moq;

namespace Manatee.Trello.Test.Unit.Mocks
{
	public class MockRestClient : IRestClient
	{
		public IRestResponse<TRequest> Execute<TRequest>(IRestRequest request)
			where TRequest : class
		{
			var mock = new Mock<IRestResponse<TRequest>>();
			mock.SetupGet(r => r.Data)
				.Returns(new Mock<TRequest>().Object);
			return mock.Object;
		}
	}
}
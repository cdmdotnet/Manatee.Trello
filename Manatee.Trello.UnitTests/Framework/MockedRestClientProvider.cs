using Manatee.Trello.Rest;
using Moq;

namespace Manatee.Trello.UnitTests.Framework
{
	internal class MockedRestClientProvider : IRestClientProvider
	{
		public IRestRequestProvider RequestProvider { get; } = new MockedRestRequestProvider();

		public Mock<IRestClient> ClientMock { get; } = new Mock<IRestClient>();

		public IRestClient CreateRestClient(string apiBaseUrl)
		{
			return ClientMock.Object;
		}
	}
}
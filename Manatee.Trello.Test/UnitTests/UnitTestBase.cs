using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Trello.Contracts;
using Manatee.Trello.Rest;
using Moq;

namespace Manatee.Trello.Test.UnitTests
{
	public abstract class UnitTestBase<T> : TrelloTestBase<T>
	{
		protected class DependencyCollection
		{
			public class MockRequestProvider : IRestRequestProvider
			{
				public IRestRequest Create(string endpoint)
				{
					var mock = new Mock<IRestRequest>();
					return mock.Object;
				}
			}
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

			public Mock<IRestClientProvider> RestClientProvider { get; private set; }
			public MockRequestProvider RequestProvider { get; private set; }
			public MockRestClient Client { get; private set; }

			public DependencyCollection()
			{
				RestClientProvider = new Mock<IRestClientProvider>();
				RequestProvider = new MockRequestProvider();
				Client = new MockRestClient();

				RestClientProvider.SetupGet(p => p.RequestProvider)
					.Returns(RequestProvider);
				RestClientProvider.Setup(p => p.CreateRestClient(It.IsAny<string>()))
					.Returns(Client);
			}
		}
	}
}

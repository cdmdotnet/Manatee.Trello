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
		private static Mock<ILog> _logMock;

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
			public Mock<ITrelloServiceConfiguration> Config { get; private set; }
			public Mock<ILog> Log { get { return _logMock; } }
			public Mock<ICache> Cache { get; private set; }

			public DependencyCollection()
			{
				RestClientProvider = new Mock<IRestClientProvider>();
				RequestProvider = new MockRequestProvider();
				Client = new MockRestClient();
				Config = new Mock<ITrelloServiceConfiguration>();
				Cache = new	Mock<ICache>();

				Config.SetupGet(c => c.RestClientProvider)
					  .Returns(RestClientProvider.Object);
				Config.SetupGet(c => c.Log)
					  .Returns(Log.Object);
				RestClientProvider.SetupGet(p => p.RequestProvider)
				                  .Returns(RequestProvider);
				RestClientProvider.Setup(p => p.CreateRestClient(It.IsAny<string>()))
				                  .Returns(Client);
			}
		}

		static UnitTestBase()
		{
			_logMock = new Mock<ILog>();
			_logMock.Setup(l => l.Error(It.IsAny<Exception>(), It.Is<bool>(b => b)))
			        .Callback((Exception e, bool b) => { throw e; });
		}

	}
}

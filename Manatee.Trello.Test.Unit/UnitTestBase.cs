using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Rest;
using Manatee.Trello.Test.Unit.Mocks;
using Moq;

namespace Manatee.Trello.Test.Unit
{
	public abstract class UnitTestBase<T> : TrelloTestBase<T>
	{
		protected class DependencyCollection
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

			public Mock<IRestClientProvider> RestClientProvider { get; private set; }
			public Mock<IRestRequestProvider> RequestProvider { get; private set; }
			public MockRestClient RestClient { get; private set; }
			public Mock<ILog> Log { get; private set; }
			public Mock<ICache> Cache { get; private set; }
			public Mock<ITrelloServiceConfiguration> Config { get; private set; }
			public MockValidator Validator { get; private set; }
			public Mock<IEntityRepository> EntityRepository { get; private set; }

			public DependencyCollection()
			{
				Cache = new	Mock<ICache>();
				Log = new Mock<ILog>();
				RestClientProvider = new Mock<IRestClientProvider>();
				RequestProvider = new Mock<IRestRequestProvider>();
				RestClient = new MockRestClient();

				Config.SetupGet(c => c.RestClientProvider)
					  .Returns(RestClientProvider.Object);
				Config.SetupGet(c => c.Log)
					  .Returns(Log.Object);
				Config.SetupGet(c => c.Cache)
				      .Returns(Cache.Object);
				Config.SetupGet(c => c.ItemDuration)
				      .Returns(TimeSpan.FromSeconds(60));
				Log.Setup(l => l.Error(It.IsAny<Exception>(), It.Is<bool>(b => b)))
						.Callback((Exception e, bool b) => { throw e; });
				RestClientProvider.SetupGet(p => p.RequestProvider)
				                  .Returns(RequestProvider.Object);
				RestClientProvider.Setup(p => p.CreateRestClient(It.IsAny<string>()))
				                  .Returns(RestClient);
				RequestProvider.Setup(p => p.Create(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
				               .Returns(new Mock<IRestRequest>().Object);
				RequestProvider.Setup(p => p.Create(It.IsAny<IRestRequest>()))
							   .Returns((IRestRequest r) => r);
			}

		}
	}
}

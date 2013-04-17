using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;
using Manatee.Trello.Test.FunctionalTests;
using Moq;

namespace Manatee.Trello.Test.UnitTests
{
	public abstract class UnitTestBase<T> : TrelloTestBase<T>
	{
		protected class DependencyCollection
		{
			public class MockRequestProvider : IRestRequestProvider
			{
				public IRestRequest<TRequest> Create<TRequest>()
					where TRequest : ExpiringObject, new()
				{
					return new Mock<IRestRequest<TRequest>>().Object;
				}
				public IRestRequest<TRequest> Create<TRequest>(string id)
					where TRequest : ExpiringObject, new()
				{
					var mock = new Mock<IRestRequest<TRequest>>();
					mock.SetupGet(r => r.Template).Returns(new TRequest { Id = id });
					return mock.Object;
				}
				public IRestRequest<TRequest> Create<TRequest>(ExpiringObject obj)
					where TRequest : ExpiringObject, new()
				{
					var mock = new Mock<IRestRequest<TRequest>>();
					mock.SetupGet(r => r.Template).Returns(new TRequest { Id = obj.Id });
					return mock.Object;
				}
				public IRestRequest<TRequest> Create<TRequest>(IEnumerable<ExpiringObject> tokens, ExpiringObject entity, string urlExtension)
					where TRequest : ExpiringObject, new()
				{
					return new Mock<IRestRequest<TRequest>>().Object;
				}
				public IRestCollectionRequest<TRequest> CreateCollectionRequest<TRequest>(IEnumerable<ExpiringObject> tokens, ExpiringObject entity)
					where TRequest : ExpiringObject, new()
				{
					return new Mock<IRestCollectionRequest<TRequest>>().Object;
				}
			}

			public Mock<IRestClientProvider> RestClientProvider { get; private set; }
			public MockRequestProvider RequestProvider { get; private set; }

			public DependencyCollection()
			{
				RestClientProvider = new Mock<IRestClientProvider>();
				RequestProvider = new MockRequestProvider();

				RestClientProvider.SetupGet(p => p.RequestProvider)
					.Returns(RequestProvider);
			}
		}
	}
}

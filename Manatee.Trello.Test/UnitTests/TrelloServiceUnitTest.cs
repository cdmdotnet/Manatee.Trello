using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class TrelloServiceUnitTest
	{
		private class DependencyCollection
		{
			public Mock<IRestClientProvider> RestClientProvider { get; private set; }
			public Mock<IRestClient> RestClient { get; private set; }
			public string AuthKey { get; private set; }
			public string AuthToken { get; private set; }

			public DependencyCollection()
			{
				AuthKey = TrelloIds.Key;
				RestClient = new Mock<IRestClient>();
				RestClientProvider = new Mock<IRestClientProvider>();

				RestClientProvider.Setup(p => p.CreateRestClient(It.IsAny<string>()))
					.Returns(RestClient.Object);
			}
			public DependencyCollection(string token)
				: this()
			{
				AuthToken = token;
			}
		}
		
		private class SystemUnderTest
		{
			public DependencyCollection Dependencies { get; private set; }
			public TrelloService Service { get; private set; }

			public SystemUnderTest()
				: this(null) {}
			public SystemUnderTest(string token)
			{
				Dependencies = new DependencyCollection(token);
				Service = new TrelloService(Dependencies.AuthKey, Dependencies.AuthToken)
				          	{
				          		RestClientProvider = Dependencies.RestClientProvider.Object
				          	};
			}
		}

		[TestMethod]
		public void TestMethod1()
		{
			var sut = new SystemUnderTest();
			sut.Dependencies.RestClient.Setup(c => c.Execute(It.IsAny<IRestRequest<Card>>()))
				.Returns(new Mock<IRestResponse<Card>>().Object);

			var card = sut.Service.Retrieve<Card>(TrelloIds.CardId);

			sut.Dependencies.RestClient.Verify(c => c.Execute(It.IsAny<IRestRequest<Card>>()));
		}
	}
}

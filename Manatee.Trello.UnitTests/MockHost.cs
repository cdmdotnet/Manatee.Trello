using System.Collections.Generic;
using System.Net;
using System.Threading;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;
using Moq;

namespace Manatee.Trello.UnitTests
{
	public static class MockHost
	{
		public static Mock<IRestClient> Client { get; private set; } 
		public static Mock<IRestResponse<IJsonCard>> CardResponse { get; private set; }

		public static void MockRest()
		{
			CardResponse = new Mock<IRestResponse<IJsonCard>>();
			CardResponse.SetupGet(r => r.StatusCode)
			            .Returns(HttpStatusCode.OK);
			CardResponse.SetupGet(r => r.Content)
			            .Returns("{}");

			Client = new Mock<IRestClient>();
			Client.Setup(c => c.Execute<IJsonCard>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
			      .ReturnsAsync(CardResponse.Object);

			var requestProvider = new Mock<IRestRequestProvider>();
			requestProvider.Setup(p => p.Create(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
			               .Returns(Mock.Of<IRestRequest>);

			var clientProvider = new Mock<IRestClientProvider>();
			clientProvider.Setup(p => p.CreateRestClient(It.IsAny<string>()))
			              .Returns(Client.Object);
			clientProvider.SetupGet(p => p.RequestProvider)
			              .Returns(requestProvider.Object);

			TrelloConfiguration.RestClientProvider = clientProvider.Object;
		}

		public static void ResetRest()
		{
			TrelloConfiguration.RestClientProvider = null;
		}

		public static Mock<IJsonFactory> JsonFactory { get; private set; }

		public static void MockJson()
		{
			JsonFactory = new Mock<IJsonFactory>();

			TrelloConfiguration.JsonFactory = JsonFactory.Object;
		}
	}
}
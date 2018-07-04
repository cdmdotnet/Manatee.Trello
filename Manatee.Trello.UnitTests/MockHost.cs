using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;
using Moq;

namespace Manatee.Trello.UnitTests
{
	public static class MockHost
	{
		private static Mock<IRestClient> _client;
		private static string _currentCaller;

		public static Mock<IRestClient> Client => _client ?? (_client = new Mock<IRestClient>());
		public static Mock Response { get; private set; }

		public static void MockRest<T>(string content = null, [CallerMemberName] string caller = null)
			where T : class
		{
			if (_currentCaller != null && _currentCaller != caller)
				throw new InvalidOperationException("You forgot to reset the mocks, you idiot.");

			_currentCaller = caller;

			var response = new Mock<IRestResponse<T>>();
			Response = response;
			response.SetupGet(r => r.StatusCode)
			            .Returns(HttpStatusCode.OK);
			response.SetupGet(r => r.Content)
			            .Returns(content ?? "{}");
			if (content != null)
				response.SetupGet(r => r.Data)
				        .Returns(() => TrelloConfiguration.Deserializer.Deserialize<T>(content));

			Client.Setup(c => c.Execute(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
			      .ReturnsAsync(response.Object);
			Client.Setup(c => c.Execute<T>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
			      .ReturnsAsync(response.Object);

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
			_client = null;
			_currentCaller = null;
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
using System;
using Manatee.Trello.Rest;

namespace Manatee.Trello.IntegrationTests
{
	public class CapturingClientProvider : IRestClientProvider
	{
		private readonly IRestClientProvider _restClientProviderImplementation;

		public Action<IRestRequest> CaptureRequest { get; }
		public Action<IRestResponse> CaptureResponse { get; }

		public CapturingClientProvider(IRestClientProvider restClientProviderImplementation, 
		                               Action<IRestRequest> captureRequest,
		                               Action<IRestResponse> captureResponse)
		{
			_restClientProviderImplementation = restClientProviderImplementation;
			CaptureRequest = captureRequest;
			CaptureResponse = captureResponse;
		}

		public IRestRequestProvider RequestProvider => _restClientProviderImplementation.RequestProvider;

		public IRestClient CreateRestClient(string apiBaseUrl)
		{
			var client = _restClientProviderImplementation.CreateRestClient(apiBaseUrl);

			return new CapturingClient(client, CaptureRequest, CaptureResponse);
		}
	}
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Rest;

namespace Manatee.Trello.IntegrationTests
{
	public class CapturingClient : IRestClient
	{
		private readonly IRestClient _client;

		public Action<IRestRequest> CaptureRequest { get; }
		public Action<IRestResponse> CaptureResponse { get; }

		public CapturingClient(IRestClient client,
		                       Action<IRestRequest> captureRequest,
		                       Action<IRestResponse> captureResponse)
		{
			_client = client;
			CaptureRequest = captureRequest;
			CaptureResponse = captureResponse;
		}

		public async Task<IRestResponse> Execute(IRestRequest request, CancellationToken ct)
		{
			CaptureRequest(request);
			var response = await _client.Execute(request, ct);
			CaptureResponse(response);

			return response;
		}

		public async Task<IRestResponse<T>> Execute<T>(IRestRequest request, CancellationToken ct) where T : class
		{
			CaptureRequest(request);
			var response = await _client.Execute<T>(request, ct);
			CaptureResponse(response);

			return response;
		}
	}
}
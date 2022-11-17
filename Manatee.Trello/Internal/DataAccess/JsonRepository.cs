using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.DataAccess
{
	internal static class JsonRepository
	{
		public static async Task Execute(TrelloAuthorization auth, Endpoint endpoint, CancellationToken ct, IDictionary<string, object> parameters = null)
		{
			var request = BuildRequest(auth, endpoint, parameters);
			var response = await RestRequestProcessor.AddRequest(request, ct);
			ValidateResponse(response);
		}
		public static async Task<T> Execute<T>(TrelloAuthorization auth, Endpoint endpoint, CancellationToken ct, IDictionary<string, object> parameters = null)
			where T : class
		{
			var request = BuildRequest(auth, endpoint, parameters);
			var response = await ProcessRequest<T>(request, ct);
			return response?.Data;
		}

	    public static async Task<T> Execute<T>(TrelloAuthorization auth, Endpoint endpoint, T body, CancellationToken ct)
			where T : class
		{
			var request = BuildRequest(auth, endpoint);
			request.AddBody(body);
			var response = await ProcessRequest<T>(request, ct);
			return response?.Data;
		}

	    public static async Task<TResponse> Execute<TRequest, TResponse>(TrelloAuthorization auth, Endpoint endpoint, TRequest body, CancellationToken ct)
			where TResponse : class
		{
			var request = BuildRequest(auth, endpoint);
			request.AddBody(body);
			var response = await ProcessRequest<TResponse>(request, ct);
			return response?.Data;
		}

		private static IRestRequest BuildRequest(TrelloAuthorization auth, Endpoint endpoint, IDictionary<string, object> parameters = null)
		{
			var request = TrelloConfiguration.RestClientProvider.RequestProvider.Create(endpoint.ToString(), parameters);
			request.Method = endpoint.Method;
			PrepRequest(request, auth);
			return request;
		}
		private static void PrepRequest(IRestRequest request, TrelloAuthorization auth)
		{
			request.AddParameter("key", auth.AppKey);
			if (auth.UserToken != null)
				request.AddParameter("token", auth.UserToken);
		}
		private static void ValidateResponse(IRestResponse response)
		{
			if (response.Exception != null)
			{
				TrelloConfiguration.Log.Error(response.Exception);
				if (TrelloConfiguration.ThrowOnTrelloError)
				{
					if (response.StatusCode != 0)
					{
						throw new HttpRequestException($"HTTP Error {response.StatusCode}", response.Exception);
					}

					throw response.Exception;
				}
			}
		}
	    private static async Task<IRestResponse<T>> ProcessRequest<T>(IRestRequest request, CancellationToken ct)
		    where T : class
	    {
			var response = await RestRequestProcessor.AddRequest<T>(request, ct);
	        ValidateResponse(response);
		    return response as IRestResponse<T>;
	    }
	}
}

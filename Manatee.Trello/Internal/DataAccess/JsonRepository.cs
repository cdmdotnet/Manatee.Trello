using System.Collections.Generic;
using System.Threading.Tasks;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.DataAccess
{
	internal static class JsonRepository
	{
		public static async Task Execute(TrelloAuthorization auth, Endpoint endpoint, IDictionary<string, object> parameters = null)
		{
			var request = BuildRequest(auth, endpoint, parameters);
			await RestRequestProcessor.AddRequest(request);
			ValidateResponse(request);
		}
		public static async Task<T> Execute<T>(TrelloAuthorization auth, Endpoint endpoint, IDictionary<string, object> parameters = null)
			where T : class
		{
			var request = BuildRequest(auth, endpoint, parameters);
			AddDefaultParameters<T>(request);
			await ProcessRequest<T>(request);
		    var response = request.Response as IRestResponse<T>;
			return response?.Data;
		}

	    public static async Task<T> Execute<T>(TrelloAuthorization auth, Endpoint endpoint, T body)
			where T : class
		{
			var request = BuildRequest(auth, endpoint);
			request.AddBody(body);
			AddDefaultParameters<T>(request);
		    await ProcessRequest<T>(request);
			var response = request.Response as IRestResponse<T>;
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
		private static void ValidateResponse(IRestRequest request)
		{
			if (request.Response.Exception != null)
				TrelloConfiguration.Log.Error(request.Response.Exception, TrelloConfiguration.ThrowOnTrelloError);
		}
		private static void AddDefaultParameters<T>(IRestRequest request)
		{
			if (request.Method != RestMethod.Get) return;
			var defaultParameters = RestParameterRepository.GetParameters<T>();
			foreach (var parameter in defaultParameters)
			{
				request.AddParameter(parameter.Key, parameter.Value);
			}
		}
	    private static async Task ProcessRequest<T>(IRestRequest request) where T : class
	    {
	        await RestRequestProcessor.AddRequest<T>(request);
	        ValidateResponse(request);
	    }
	}
}

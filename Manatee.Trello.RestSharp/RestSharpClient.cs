using System;
using System.Net;
using Manatee.Trello.Contracts;
using RestSharp;
using IRestClient = Manatee.Trello.Rest.IRestClient;
using IRestRequest = Manatee.Trello.Rest.IRestRequest;
using IRestResponse = Manatee.Trello.Rest.IRestResponse;

namespace Manatee.Trello.RestSharp
{
	internal class RestSharpClient : RestClient, IRestClient
	{
		private readonly ILog _log;
		private readonly global::RestSharp.Deserializers.IDeserializer _deserializer;

		public RestSharpClient(ILog log, global::RestSharp.Deserializers.IDeserializer deserializer, string apiBaseUrl)
			: base(apiBaseUrl)
		{
			_log = log;
			_deserializer = deserializer;
			ClearHandlers();
			AddHandler("application/json", _deserializer);
		}

		public IRestResponse Execute(IRestRequest request)
		{
			var restSharpResponse = ExecuteWithRetry(request);
			return new RestSharpResponse(restSharpResponse);
		}

		public Rest.IRestResponse<T> Execute<T>(IRestRequest request)
			where T : class
		{
			var restSharpResponse = ExecuteWithRetry(request);
			var data = _deserializer.Deserialize<T>(restSharpResponse);
			return new RestSharpResponse<T>(restSharpResponse, data);
		}

		private global::RestSharp.IRestResponse ExecuteWithRetry(IRestRequest request)
		{
			var restSharpRequest = (RestSharpRequest)request;
			var count = 0;
			bool failOut;
			global::RestSharp.IRestResponse restSharpResponse;
			do
			{
				count++;
				restSharpResponse = base.Execute(restSharpRequest);
				failOut = !TrelloConfiguration.RetryStatusCodes.Contains(restSharpResponse.StatusCode);
			} while (failOut && count <= TrelloConfiguration.MaxRetryCount);
			ValidateResponse(restSharpResponse);
			return restSharpResponse;
		}

		private void ValidateResponse(global::RestSharp.IRestResponse response)
		{
			if (response == null)
				_log.Error(new WebException("Received null response from Trello."));
			if (response.StatusCode != HttpStatusCode.OK)
				_log.Error(new WebException($"Trello returned with an error.\nError Message: {response.ErrorMessage}\nContent: {response.Content}",
				                            response.ErrorException));
		}
	}
}
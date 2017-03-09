using System;
using System.ComponentModel.Design.Serialization;
using System.Net;
using System.Security.Permissions;
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
			var response = ExecuteWithRetry(request);
			return response;
		}

		public Rest.IRestResponse<T> Execute<T>(IRestRequest request)
			where T : class
		{
			var response = ExecuteWithRetry<T>(request);
			return response;
		}

		private IRestResponse ExecuteWithRetry(IRestRequest request)
		{
			var restSharpRequest = (RestSharpRequest)request;
			var count = 0;
			IRestResponse restResponse;
			global::RestSharp.IRestResponse restSharpResponse;
			do
			{
				count++;
				restSharpResponse = base.Execute(restSharpRequest);
				restResponse = MapResponse(restSharpResponse);
			} while (TrelloConfiguration.RetryPredicate(restResponse, count));
			ValidateResponse(restSharpResponse);
			return restResponse;
		}
		private static IRestResponse MapResponse(global::RestSharp.IRestResponse response)
		{
			TrelloConfiguration.Log.Debug($"Status Code: {response.StatusCode} ({(int)response.StatusCode})\n" +
										  $"Content: {response.Content}");
			var restResponse = new RestSharpResponse
				{
					Content = response.Content,
					StatusCode = response.StatusCode
				};
			return restResponse;
		}
		private Rest.IRestResponse<T> ExecuteWithRetry<T>(IRestRequest request)
			where T : class
		{
			var restSharpRequest = (RestSharpRequest)request;
			var count = 0;
			Rest.IRestResponse<T> restResponse;
			global::RestSharp.IRestResponse restSharpResponse;
			do
			{
				count++;
				restSharpResponse = base.Execute(restSharpRequest);
				restResponse = MapResponse<T>(restSharpResponse);
			} while (TrelloConfiguration.RetryPredicate(restResponse, count));
			ValidateResponse(restSharpResponse);
			return restResponse;
		}
		private Rest.IRestResponse<T> MapResponse<T>(global::RestSharp.IRestResponse response)
			where T : class
		{
			TrelloConfiguration.Log.Debug($"Status Code: {response.StatusCode} ({(int) response.StatusCode})\n" +
			                              $"Content: {response.Content}");
			var restResponse = new RestSharpResponse<T>
				{
					Content = response.Content,
					StatusCode = response.StatusCode
				};
			try
			{
				restResponse.Data = _deserializer.Deserialize<T>(response);
			}
			catch (Exception e)
			{
				restResponse.Exception = e;
			}
			return restResponse;
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
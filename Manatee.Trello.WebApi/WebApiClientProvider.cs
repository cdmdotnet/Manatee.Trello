using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	public class WebApiClientProvider : IRestClientProvider
	{
		public IRestRequestProvider RequestProvider { get; private set; }

		public IRestClient CreateRestClient(string apiBaseUrl)
		{
			throw new NotImplementedException();
		}
	}

	public class WebApiClient : IRestClient
	{
		public IRestResponse Execute(IRestRequest request)
		{
			var client = new HttpClient();
			HttpResponseMessage response;
			switch (request.Method)
			{
				case RestMethod.Get:
					response = Task.Run(() => client.GetAsync(request.Resource)).Result;
					break;
				case RestMethod.Put:
					response = Task.Run(() => client.PutAsync(request.Resource, new ObjectContent(typeof(), request.))).Result;
					break;
				case RestMethod.Post:
					response = Task.Run(() => client.PostAsync(request.Resource)).Result;
					break;
				case RestMethod.Delete:
					response = Task.Run(() => client.DeleteAsync(request.Resource)).Result;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var restResponse = new WebApiRestResponse
				{
					Content = Task.Run(() => response.Content.ReadAsStringAsync()).Result
				};
			return restResponse;
		}
		public IRestResponse<T> Execute<T>(IRestRequest request) where T : class
		{
			throw new NotImplementedException();
		}
	}

	public class WebApiRequestProvider : IRestRequestProvider
	{
		public IRestRequest Create(string endpoint, IDictionary<string, object> parameters = null)
		{
			var request = new WebApiRestRequest {Resource = endpoint};
			if (parameters != null)
			{
				foreach (var parameter in parameters)
				{
					request.AddParameter(parameter.Key, parameter.Value);
				}
			}

			return request;
		}
	}

	public class WebApiRestRequest : IRestRequest
	{
		private readonly Dictionary<string, object> _parameters; 

		public RestMethod Method { get; set; }
		public string Resource { get; set; }
		public IRestResponse Response { get; set; }

		public WebApiRestRequest()
		{
			_parameters = new Dictionary<string, object>();
		}

		public void AddParameter(string name, object value)
		{
			_parameters[name] = value;
		}
		public void AddBody(object body)
		{
			throw new NotImplementedException();
		}
	}

	public class WebApiRestResponse : IRestResponse
	{
		public string Content { get; set; }
		public Exception Exception { get; set; }
	}

	public class WebApiRestResponse<T> : WebApiRestResponse, IRestResponse<T>
	{
		public T Data { get; set; }
	}
}
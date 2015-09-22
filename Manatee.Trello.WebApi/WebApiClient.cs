using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	public class WebApiClient : IRestClient
	{
		private const string TrelloApiBaseUrl = @"https://api.trello.com/1";
		private readonly WebApiFormatter _formatter;

		internal WebApiClient()
		{
			_formatter = new WebApiFormatter();
		}

		public IRestResponse Execute(IRestRequest request)
		{
			return Task.Run(() => ExecuteAsync(request)).Result;
		}
		public IRestResponse<T> Execute<T>(IRestRequest request) where T : class
		{
			return Task.Run(() => ExecuteAsync<T>(request)).Result;
		}

		private async Task<IRestResponse> ExecuteAsync(IRestRequest request)
		{
			HttpResponseMessage response;
			using (var client = new HttpClient())
			{
				var webRequest = (WebApiRestRequest)request;
				switch (request.Method)
				{
					case RestMethod.Get:
						response = await client.GetAsync(GetFullResource(webRequest));
						break;
					case RestMethod.Put:
						response = await client.PutAsync(GetFullResource(webRequest), GetContent(webRequest));
						break;
					case RestMethod.Post:
						response = await client.PostAsync(GetFullResource(webRequest), GetContent(webRequest));
						break;
					case RestMethod.Delete:
						response = await client.DeleteAsync(GetFullResource(webRequest));
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			var restResponse = new WebApiRestResponse();
			if (response.Content != null)
				restResponse.Content = await response.Content.ReadAsStringAsync();
			return restResponse;
		}
		private async Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request) where T : class
		{
			HttpResponseMessage response;
			using (var client = new HttpClient())
			{
				var webRequest = (WebApiRestRequest) request;
				switch (request.Method)
				{
					case RestMethod.Get:
						response = await client.GetAsync(GetFullResource(webRequest));
						break;
					case RestMethod.Put:
						response = await client.PutAsync(GetFullResource(webRequest), GetContent(webRequest));
						break;
					case RestMethod.Post:
						response = await client.PostAsync(GetFullResource(webRequest), GetContent(webRequest));
						break;
					case RestMethod.Delete:
						response = await client.DeleteAsync(GetFullResource(webRequest));
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			var restResponse = new WebApiRestResponse<T>();
			if (response.Content != null)
			{
				restResponse.Content = await response.Content.ReadAsStringAsync();
				restResponse.Data = await response.Content.ReadAsAsync<T>(new[] {_formatter});
			}
			return restResponse;
		}
		private string GetFullResource(WebApiRestRequest request)
		{
			if (request.File != null)
				return string.Format("{0}/{1}", TrelloApiBaseUrl, request.Resource);
			return string.Format("{0}/{1}?{2}", TrelloApiBaseUrl, request.Resource, string.Join("&", request.Parameters.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value))));
		}
		private HttpContent GetContent(WebApiRestRequest request)
		{
			if (request.File != null)
			{
				var formData = new MultipartFormDataContent();
				foreach (var parameter in request.Parameters)
				{
					var content = new StringContent(parameter.Value.ToString());
					formData.Add(content, string.Format("\"{0}\"", parameter.Key));
				}
				var byteContent = new ByteArrayContent(request.File);
				formData.Add(byteContent, "\"file\"", string.Format("\"{0}\"", request.FileName));
				return formData;
			}
			return new ObjectContent(GetRequestType(request), request.Body, _formatter);
		}
		private static Type GetRequestType(WebApiRestRequest request)
		{
			return request.Body == null ? typeof(object) : request.Body.GetType();
		}
	}
}
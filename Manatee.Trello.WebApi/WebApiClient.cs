using System;
using System.Net.Http;
using System.Threading.Tasks;
using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	public class WebApiClient : IRestClient
	{
		private readonly WebApiFormatter _formatter;

		internal WebApiClient()
		{
			_formatter = new WebApiFormatter();
		}

		public IRestResponse Execute(IRestRequest request)
		{
			var client = new HttpClient();
			HttpResponseMessage response;
			var webRequest = (WebApiRestRequest) request;
			switch (request.Method)
			{
				case RestMethod.Get:
					response = Task.Run(() => client.GetAsync(webRequest.GetFullResource())).Result;
					break;
				case RestMethod.Put:
					response = Task.Run(() => client.PutAsync(webRequest.GetFullResource(), GetContent(webRequest))).Result;
					break;
				case RestMethod.Post:
					response = Task.Run(() => client.PostAsync(webRequest.GetFullResource(), GetContent(webRequest))).Result;
					break;
				case RestMethod.Delete:
					response = Task.Run(() => client.DeleteAsync(webRequest.GetFullResource())).Result;
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
			var client = new HttpClient();
			HttpResponseMessage response;
			var webRequest = (WebApiRestRequest)request;
			switch (request.Method)
			{
				case RestMethod.Get:
					response = Task.Run(() => client.GetAsync(webRequest.GetFullResource())).Result;
					break;
				case RestMethod.Put:
					response = Task.Run(() => client.PutAsync(webRequest.GetFullResource(), GetContent(webRequest))).Result;
					break;
				case RestMethod.Post:
					response = Task.Run(() => client.PostAsync(webRequest.GetFullResource(), GetContent(webRequest))).Result;
					break;
				case RestMethod.Delete:
					response = Task.Run(() => client.DeleteAsync(webRequest.GetFullResource())).Result;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var restResponse = new WebApiRestResponse<T>();
			if (response.Content != null)
				restResponse.Data = Task.Run(() => response.Content.ReadAsAsync<T>(new[] {_formatter})).Result;
			return restResponse;
		}

		private ObjectContent GetContent(WebApiRestRequest request)
		{
			if (request.File != null)
			{
				using (var formData = new MultipartFormDataContent())
				{
					var byteContent = new ByteArrayContent(request.File);
					formData.Add(byteContent, request.FileName);
				}
			}
			return new ObjectContent(GetRequestType(request), request.Body, _formatter);
		}
		private static Type GetRequestType(WebApiRestRequest request)
		{
			return request.Body == null ? typeof(object) : request.Body.GetType();
		}
	}
}
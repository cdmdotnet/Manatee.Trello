using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	internal class WebApiClient : IRestClient
	{
		private const string TrelloApiBaseUrl = @"https://api.trello.com/1";
		private readonly WebApiJsonFormatter _formatter;
		private readonly WebApiTextFormatter _errorLogger;

		internal WebApiClient()
		{
			_formatter = new WebApiJsonFormatter();
			_errorLogger = new WebApiTextFormatter();
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
			IRestResponse response;
			var webRequest = (WebApiRestRequest)request;
			switch (request.Method)
			{
				case RestMethod.Get:
					response = await ExecuteWithRetry(c => c.GetAsync(GetFullResource(webRequest)));
					break;
				case RestMethod.Put:
					response = await ExecuteWithRetry(c => c.PutAsync(GetFullResource(webRequest), GetContent(webRequest)));
					break;
				case RestMethod.Post:
					response = await ExecuteWithRetry(c => c.PostAsync(GetFullResource(webRequest), GetContent(webRequest)));
					break;
				case RestMethod.Delete:
					response = await ExecuteWithRetry(c => c.DeleteAsync(GetFullResource(webRequest)));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return response;
		}
		private async Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request) where T : class
		{
			IRestResponse<T> response;
			var webRequest = (WebApiRestRequest) request;
			switch (request.Method)
			{
				case RestMethod.Get:
					response = await ExecuteWithRetry<T>(c => c.GetAsync(GetFullResource(webRequest)));
					break;
				case RestMethod.Put:
					response = await ExecuteWithRetry<T>(c => c.PutAsync(GetFullResource(webRequest), GetContent(webRequest)));
					break;
				case RestMethod.Post:
					response = await ExecuteWithRetry<T>(c => c.PostAsync(GetFullResource(webRequest), GetContent(webRequest)));
					break;
				case RestMethod.Delete:
					response = await ExecuteWithRetry<T>(c => c.DeleteAsync(GetFullResource(webRequest)));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return response;
		}
		private static async Task<IRestResponse> ExecuteWithRetry(Func<HttpClient, Task<HttpResponseMessage>> call)
		{
			IRestResponse restResponse;
			HttpResponseMessage response;
			using (var client = new HttpClient())
			{
				var count = 0;
				do
				{
					count++;
					response = await call(client);
					restResponse = await MapResponse(response);
				} while (TrelloConfiguration.RetryPredicate(restResponse, count));
			}
			if (!response.IsSuccessStatusCode)
				throw new HttpException("Received a failure from Trello.\n" +
										$"Status Code: {response.StatusCode} ({(int)response.StatusCode})\n" +
										$"Content: {response.Content.ReadAsStringAsync().Result}");
			return restResponse;
		}
		private static async Task<IRestResponse> MapResponse(HttpResponseMessage response)
		{
			var restResponse = new WebApiRestResponse
				{
					Content = await response.Content.ReadAsStringAsync(),
					StatusCode = response.StatusCode
				};
			TrelloConfiguration.Log.Debug($"Status Code: {response.StatusCode} ({(int) response.StatusCode})\n" +
				                              $"Content: {restResponse.Content}");
			return restResponse;
		}
		private async Task<IRestResponse<T>> ExecuteWithRetry<T>(Func<HttpClient, Task<HttpResponseMessage>> call) where T : class
		{
			IRestResponse<T> restResponse;
			using (var client = new HttpClient())
			{
				var count = 0;
				do
				{
					count++;
					var response = await call(client);
					restResponse = await MapResponse<T>(response);
				} while (TrelloConfiguration.RetryPredicate(restResponse, count));
			}
			return restResponse;
		}
		private async Task<IRestResponse<T>> MapResponse<T>(HttpResponseMessage response) where T : class
		{
			var restResponse = new WebApiRestResponse<T>
				{
					Content = await response.Content.ReadAsStringAsync(),
					StatusCode = response.StatusCode
				};
			TrelloConfiguration.Log.Debug($"Status Code: {response.StatusCode} ({(int) response.StatusCode})\n" +
				                            $"Content: {restResponse.Content}");
			try
			{
				restResponse.Data = await response.Content.ReadAsAsync<T>(new MediaTypeFormatter[] {_formatter, _errorLogger});
			}
			catch (Exception e)
			{
				restResponse.Exception = e;
			}
			return restResponse;
		}
		private HttpContent GetContent(WebApiRestRequest request)
		{
			if (request.File != null)
			{
				var formData = new MultipartFormDataContent();
				foreach (var parameter in request.Parameters)
				{
					var content = new StringContent(parameter.Value.ToString());
					formData.Add(content, $"\"{parameter.Key}\"");
				}
				var byteContent = new ByteArrayContent(request.File);
				formData.Add(byteContent, "\"file\"", $"\"{request.FileName}\"");
				return formData;
			}
			return new ObjectContent(GetRequestType(request), request.Body, _formatter);
		}
		private static string GetFullResource(WebApiRestRequest request)
		{
			if (request.File != null)
				return $"{TrelloApiBaseUrl}/{request.Resource}";
			return $"{TrelloApiBaseUrl}/{request.Resource}?{string.Join("&", request.Parameters.Select(kvp => $"{kvp.Key}={kvp.Value}").ToList())}";
		}
		private static Type GetRequestType(WebApiRestRequest request)
		{
			return request.Body?.GetType() ?? typeof(object);
		}
	}
}
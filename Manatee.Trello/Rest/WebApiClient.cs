using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;

namespace Manatee.Trello.Rest
{
	internal class WebApiClient : IRestClient, IDisposable
	{
		private readonly string _baseUri;
		private readonly HttpClient _client;

		public WebApiClient(string baseUri)
		{
			_client = TrelloConfiguration.HttpClientFactory();
			_baseUri = baseUri;
		}

		~WebApiClient()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public Task<IRestResponse> Execute(IRestRequest request, CancellationToken ct)
		{
			return ExecuteAsync(request, ct);
		}

		public Task<IRestResponse<T>> Execute<T>(IRestRequest request, CancellationToken ct)
			where T : class
		{
			return ExecuteAsync<T>(request, ct);
		}

		private async Task<IRestResponse> ExecuteAsync(IRestRequest request, CancellationToken ct)
		{
			IRestResponse response;
			var webRequest = (WebApiRestRequest) request;
			switch (request.Method)
			{
				case RestMethod.Get:
					response = await ExecuteWithRetry(() => _client.GetAsync(GetFullResource(webRequest), ct));
					break;
				case RestMethod.Put:
					response = await ExecuteWithRetry(() => _client.PutAsync(GetFullResource(webRequest), GetContent(webRequest), ct));
					break;
				case RestMethod.Post:
					response = await ExecuteWithRetry(() => _client.PostAsync(GetFullResource(webRequest), GetContent(webRequest), ct));
					break;
				case RestMethod.Delete:
					response = await ExecuteWithRetry(() => _client.DeleteAsync(GetFullResource(webRequest), ct));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return response;
		}

		private async Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request, CancellationToken ct)
			where T : class
		{
			IRestResponse<T> response;
			var webRequest = (WebApiRestRequest) request;
			switch (request.Method)
			{
				case RestMethod.Get:
					response = await ExecuteWithRetry<T>(() => _client.GetAsync(GetFullResource(webRequest), ct));
					break;
				case RestMethod.Put:
					response = await ExecuteWithRetry<T>(() => _client.PutAsync(GetFullResource(webRequest), GetContent(webRequest), ct));
					break;
				case RestMethod.Post:
					response = await ExecuteWithRetry<T>(() => _client.PostAsync(GetFullResource(webRequest), GetContent(webRequest), ct));
					break;
				case RestMethod.Delete:
					response = await ExecuteWithRetry<T>(() => _client.DeleteAsync(GetFullResource(webRequest), ct));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return response;
		}

		private static async Task<IRestResponse> ExecuteWithRetry(Func<Task<HttpResponseMessage>> call)
		{
			IRestResponse restResponse;
			HttpResponseMessage response;
			var count = 0;
			do
			{
				count++;
				using(response = await call()) {
					restResponse = await MapResponse(response);
				}
			} while (TrelloConfiguration.RetryPredicate(restResponse, count));

			if (!response.IsSuccessStatusCode)
				throw new HttpRequestException("Received a failure from Trello.\n" +
				                               $"Status Code: {response.StatusCode} ({(int) response.StatusCode})\n" +
				                               $"Content: {restResponse.Content}");
			return restResponse;
		}

		private static async Task<IRestResponse> MapResponse(HttpResponseMessage response)
		{
			var restResponse = new WebApiRestResponse
				{
					Content = await response.Content.ReadAsStringAsync(),
					StatusCode = response.StatusCode
				};
			TrelloConfiguration.Log.Info($"Status Code: {response.StatusCode} ({(int) response.StatusCode})");
			TrelloConfiguration.Log.Debug($"\tContent: {restResponse.Content}");
			return restResponse;
		}

		private static async Task<IRestResponse<T>> ExecuteWithRetry<T>(Func<Task<HttpResponseMessage>> call)
			where T : class
		{
			IRestResponse<T> restResponse;
			var count = 0;
			do
			{
				count++;
				using(var response = await call()) {
					restResponse = await MapResponse<T>(response);
				}
			} while (TrelloConfiguration.RetryPredicate(restResponse, count));

			return restResponse;
		}

		private static async Task<IRestResponse<T>> MapResponse<T>(HttpResponseMessage response)
			where T : class
		{
			var restResponse = new WebApiRestResponse<T>
				{
					Content = await response.Content.ReadAsStringAsync(),
					StatusCode = response.StatusCode
				};
			TrelloConfiguration.Log.Info($"Status Code: {response.StatusCode} ({(int) response.StatusCode})");
			TrelloConfiguration.Log.Debug($"\tContent: {restResponse.Content}");
			try
			{
				var body = restResponse.Content;
				if (!response.IsSuccessStatusCode ||
				    response.Content.Headers.ContentType.MediaType == "text/plain")
					restResponse.Exception = new TrelloInteractionException(body);
				else
					restResponse.Data = TrelloConfiguration.Deserializer.Deserialize<T>(body);
			}
			catch (Exception e)
			{
				restResponse.Exception = e;
			}

			return restResponse;
		}

		private static HttpContent GetContent(WebApiRestRequest request)
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
				TrelloConfiguration.Log.Debug($"\tContent: {formData}");

				return formData;
			}

			if (request.Body == null) return null;

			var body = TrelloConfiguration.Serializer.Serialize(request.Body);
			var jsonContent = new StringContent(body);
			jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			TrelloConfiguration.Log.Debug($"\tContent: {body}");

			return jsonContent;
		}

		private string GetFullResource(WebApiRestRequest request)
		{
			TrelloConfiguration.Log.Info($"Sending: {request.Method} {request.Resource}");
			if (request.File != null)
				return $"{_baseUri}/{request.Resource}";
			return $"{_baseUri}/{request.Resource}?{string.Join("&", request.Parameters.Select(kvp => $"{kvp.Key}={UrlEncode(kvp.Value)}"))}";
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				_client?.Dispose();
			}
		}

		private static string UrlEncode(object value)
		{
			return WebUtility.UrlEncode(value.ToString());
		}
	}
}
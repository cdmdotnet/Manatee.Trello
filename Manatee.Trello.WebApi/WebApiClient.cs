/***************************************************************************************

	Copyright 2016 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		WebApiClient.cs
	Namespace:		Manatee.Trello.WebApi
	Class Name:		WebApiClient
	Purpose:		Implements IRestClient for WebApi.

***************************************************************************************/

using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
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
			{
				restResponse.Content = await response.Content.ReadAsStringAsync();
				//TrelloConfiguration.Log.Debug(restResponse.Content);
			}
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
				TrelloConfiguration.Log.Debug(restResponse.Content);
				restResponse.Data = await response.Content.ReadAsAsync<T>(new MediaTypeFormatter[] {_formatter, _errorLogger});
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
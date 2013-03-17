// Source: https://bitbucket.org/mrshrinkray/chello/wiki/Home
// Modified per the following
//		- Class was originally created as a base class for other services.  Modified to public to be used independently.
//		- Updated to personal coding standards.

using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace Manatee.Trello.Rest
{
	public class TrelloApi
	{
		private const string ApiBaseUrl = "https://api.trello.com/1";

		private readonly Serializer _serializer;

		public string AuthKey { get; private set; }
		public string AuthToken { get; private set; }

		public TrelloApi(string authKey, string authToken)
		{
			if (string.IsNullOrEmpty(authKey))
				throw new ArgumentException("Authentication key required. Auth keys can be generated from https://trello.com/1/appKey/generate", "authKey");
			AuthKey = authKey;
			AuthToken = authToken;
			_serializer = new Serializer();
		}

		public T GetRequest<T>(string path, params string[] args) where T : new()
		{
			var client = GenerateRestClient();
			var request = new RestRequest(BuildUrl(path, args)) {DateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ"};
			return client.Execute<T>(request).Data;
		}
		public TOutput PutRequest<TOutput, TInput>(TInput obj, string path, params string[] args) where TOutput : new()
		{
			return Request<TOutput, TInput>(Method.PUT, obj, path, args);
		}
		public TOutput PostRequest<TOutput, TInput>(TInput obj, string path, params string[] args) where TOutput : new()
		{
			return Request<TOutput, TInput>(Method.POST, obj, path, args);
		}
		public void DeleteRequest(string path, params string[] args)
		{
			Request<List<object>, object>(Method.DELETE, null, path, args);
		}

		private TOutput Request<TOutput, TInput>(Method method, TInput obj, string path, params string[] args) where TOutput : new()
		{
			var client = GenerateRestClient();
			var request = new RestRequest(BuildUrl(path, args), method)
			              	{
			              		RequestFormat = DataFormat.Json,
			              		JsonSerializer = _serializer
			              	};
			request.AddBody(obj);
			return client.Execute<TOutput>(request).Data;
		}
		private string BuildUrl(string path, params string[] args)
		{
			// Assume for now that no querystring is added
			path = string.Format(path, args);
			path = string.Format("{0}{1}key={2}", path, path.Contains("?") ? "&" : "?", AuthKey);
			if (!string.IsNullOrEmpty(AuthToken))
				path = string.Format("{0}&token={1}", path, AuthToken);
			return path;
		}
		private RestClient GenerateRestClient()
		{
			var client = new RestClient(ApiBaseUrl);
			client.AddHandler("application/json", _serializer);
			client.AddHandler("text/json", _serializer);
			return client;
		}
	}
}

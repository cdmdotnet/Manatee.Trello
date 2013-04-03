/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		TrelloRest.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		TrelloRest
	Purpose:		Provides a Trello-specific wrapper for RestSharp.

***************************************************************************************/
using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;

namespace Manatee.Trello.Rest
{
	internal class TrelloRest : ITrelloRest
	{
		private const string ApiBaseUrl = "https://api.trello.com/1";
		private readonly string _authKey;
		private readonly string _authToken;

		private IRestClientProvider _restProvider;
		
		public IRestClientProvider RestClientProvider
		{
			get { return _restProvider ?? (_restProvider = new RestSharpClientProvider()); }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				if (_restProvider != null)
					throw new InvalidOperationException("REST provider already set.");
				_restProvider = value;
			}
		}
		public IRestRequestProvider RequestProvider
		{
			get { return RestClientProvider.RequestProvider; }
		}

		public TrelloRest(string authKey, string authToken)
		{
			if (authKey == null)
				throw new ArgumentNullException("authKey");
			if (string.IsNullOrWhiteSpace(authKey))
				throw new ArgumentException("Authentication key required. Auth keys can be generated from https://trello.com/1/appKey/generate", "authKey");
			_authKey = authKey;
			_authToken = authToken;
		}

		public T Get<T>(IRestRequest<T> request)
			where T : ExpiringObject, new()
		{
			return Execute(request, Method.Get);
		}
		public IEnumerable<T> Get<T>(IRestCollectionRequest<T> request)
			where T : ExpiringObject, new()
		{
			return Execute(request, Method.Get);
		}
		public T Put<T>(IRestRequest<T> request)
			where T : ExpiringObject, new()
		{
			return Execute(request, Method.Put);
		}
		public T Post<T>(IRestRequest<T> request)
			where T : ExpiringObject, new()
		{
			return Execute(request, Method.Post);
		}
		public T Delete<T>(IRestRequest<T> request)
			where T : ExpiringObject, new()
		{
			return Execute(request, Method.Delete);
		}

		private void PrepRequest<T>(IRestRequest<T> request, Method method)
			where T : ExpiringObject, new()
		{
			request.Method = method;
			request.AddParameter("key", _authKey);
			if (_authToken != null)
				request.AddParameter("token", _authToken);
			request.AddParameters();
			if (request.ParameterSource != null)
				request.AddBody(request.ParameterSource);
		}
		private T Execute<T>(IRestRequest<T> request, Method method)
			where T : ExpiringObject, new()
		{
			var client = GenerateRestClient();
			PrepRequest(request, method);
			var response = client.Execute(request);
			return response.Data;
		}
		private IEnumerable<T> Execute<T>(IRestCollectionRequest<T> request, Method method)
			where T : ExpiringObject, new()
		{
			var client = GenerateRestClient();
			PrepRequest(request, method);
			var response = client.Execute(request);
			return response.Data;
		}
		private IRestClient GenerateRestClient()
		{
			return RestClientProvider.CreateRestClient(ApiBaseUrl);
		}
	}
}

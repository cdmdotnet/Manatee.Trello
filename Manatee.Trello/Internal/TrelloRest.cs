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
	Namespace:		Manatee.Trello.Internal
	Class Name:		TrelloRest
	Purpose:		Provides a Trello-specific wrapper for RestSharp.

***************************************************************************************/
using System;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal
{
	internal class TrelloRest : ITrelloRest
	{
		private const string ApiBaseUrl = "https://api.trello.com/1";
		private readonly string _appKey;
		private string _userToken;

		private IRestClientProvider _restProvider;
		
		public IRestClientProvider RestClientProvider
		{
			get { return _restProvider ?? (_restProvider = Options.RestClientProvider); }
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
			get
			{
				return RestClientProvider.RequestProvider;
			}
		}
		public string AppKey { get { return _appKey; } }
		public string UserToken { get { return _userToken; } set { _userToken = value; } }

		public TrelloRest(string appKey, string userToken)
		{
			if (appKey == null)
				throw new ArgumentNullException("appKey");
			if (string.IsNullOrWhiteSpace(appKey))
				throw new ArgumentException("Application key required. App keys can be generated from https://trello.com/1/appKey/generate", "appKey");
			_appKey = appKey;
			_userToken = userToken;
		}

		public T Get<T>(IRestRequest request)
			where T : class
		{
			return Execute<T>(request, RestMethod.Get);
		}
		public T Put<T>(IRestRequest request)
			where T : class
		{
			return Execute<T>(request, RestMethod.Put);
		}
		public T Post<T>(IRestRequest request)
			where T : class
		{
			return Execute<T>(request, RestMethod.Post);
		}
		public T Delete<T>(IRestRequest request)
			where T : class
		{
			return Execute<T>(request, RestMethod.Delete);
		}

		private void PrepRequest(IRestRequest request, RestMethod method)
		{
			request.Method = method;
			request.AddParameter("key", _appKey);
			if (_userToken != null)
				request.AddParameter("token", _userToken);
		}
		private T Execute<T>(IRestRequest request, RestMethod method)
			where T : class
		{
			var client = GenerateRestClient();
			PrepRequest(request, method);
			var response = client.Execute<T>(request);
			return response.Data;
		}
		private IRestClient GenerateRestClient()
		{
			return RestClientProvider.CreateRestClient(ApiBaseUrl);
		}
	}
}

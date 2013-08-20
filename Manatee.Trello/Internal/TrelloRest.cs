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
using System.Linq;
using System.Text;
using System.Threading;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal
{
	internal class TrelloRest : ITrelloRest
	{
		private readonly ILog _log;
		private readonly IRestRequestProcessor _requestProcessor;
		private readonly string _appKey;

		public string AppKey { get { return _appKey; } }
		public string UserToken { get; set; }

		public TrelloRest(ILog log, IRestRequestProcessor requestProcessor, string appKey, string userToken)
		{
			if (string.IsNullOrWhiteSpace(appKey))
				_log.Error(new ArgumentException("Application key required. App keys can be generated from https://trello.com/1/appKey/generate", "appKey"));
			_log = log;
			_requestProcessor = requestProcessor;
			_appKey = appKey;
			UserToken = userToken;
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

		private T Execute<T>(IRestRequest request, RestMethod method)
			where T : class
		{
			_requestProcessor.AddRequest<T>(request, method);
			SpinWait.SpinUntil(() => request.Response != null);
			var response = (IRestResponse<T>) request.Response;
			return response.Data;
		}
	}
}

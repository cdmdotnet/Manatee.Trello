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
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal
{
	internal class TrelloRest : ITrelloRest
	{
		private readonly ILog _log;
		private readonly IRequestQueue _requestQueue;
		private readonly string _appKey;
		private string _userToken;

		public string AppKey { get { return _appKey; } }
		public string UserToken { get { return _userToken; } set { _userToken = value; } }

		public TrelloRest(ILog log, IRequestQueue requestQueue, string appKey, string userToken)
		{
			if (string.IsNullOrWhiteSpace(appKey))
				_log.Error(new ArgumentException("Application key required. App keys can be generated from https://trello.com/1/appKey/generate", "appKey"));
			_log = log;
			_requestQueue = requestQueue;
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
			PrepRequest(request, method);
			LogRequest(request);
			var queuedRequest = new QueuedRestRequest {Request = request, RequestedType = typeof (T)};
			_requestQueue.Enqueue(queuedRequest);
			SpinWait.SpinUntil(() => queuedRequest.CanContinue);
			var response = (IRestResponse<T>) queuedRequest.Response;
			return response == null ? null : response.Data;
		}
		private void LogRequest(IRestRequest request)
		{
			var sb = new StringBuilder();
			sb.AppendLine("Queuing: {0} {1}", request.Method, request.Resource);
			if ((request.Parameters != null) && request.Parameters.Any())
			{
				sb.AppendLine("    Parameters:");
				foreach (var parameter in request.Parameters)
				{
					sb.AppendLine("       {0}: {1}", parameter.Key, parameter.Value);
				}
			}
			sb.AppendLine();
			_log.Info(sb.ToString());
		}
	}
}

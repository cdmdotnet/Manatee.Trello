/***************************************************************************************

	Copyright 2012 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		RequestHandler.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		RequestHandler
	Purpose:		Implements IRequestHandler to handle REST requests as
					they appear in a queue.

***************************************************************************************/

using System;
using System.Linq;
using System.Text;
using Manatee.Trello.Contracts;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal
{
	internal class RequestQueueHandler : IRequestQueueHandler
	{
		private const string ApiBaseUrl = "https://api.trello.com/1";
		private readonly ILog _log;
		private readonly IRequestQueue _queue;
		private readonly IRestClientProvider _restProvider;
		private readonly IRestExecuteHandler _handler;
		private bool _isActive, _isRunning;

		public bool IsActive
		{
			get { return NetworkHandler.HasActiveConnection() && _isActive; }
			set
			{
				_isActive = value;
				if (NetworkHandler.HasActiveConnection() && _isActive)
				{
					EnableHandler(null, null);
				}
			}
		}

		public RequestQueueHandler(ILog log, IRequestQueue queue, IRestClientProvider restProvider, IRestExecuteHandler handler)
		{
			_log = log;
			_queue = queue;
			_queue.ItemQueued += EnableHandler;
			_restProvider = restProvider;
			_handler = handler;
			_isActive = true;
		}
		public T Handle<T>(IRestRequest request)
			where T : class
		{
			var client = _restProvider.CreateRestClient(ApiBaseUrl);
			var retVal = client.Execute<T>(request);
			return retVal.Data;
		}

		private void EnableHandler(object sender, EventArgs e)
		{
			if (_isRunning) return;
			_isRunning = true;
			var client = _restProvider.CreateRestClient(ApiBaseUrl);
			IQueuedRestRequest request;
			while (IsActive && (request = _queue.Dequeue()) != null)
			{
				LogRequest(request.Request);
				var response = _handler.Execute(client, request.RequestedType, request.Request);
				request.Response = response;
				LogResponse(request.Request, response);
				request.CanContinue = true;
			}
			if (!IsActive)
			{
				foreach (var queuedRequest in _queue)
				{
					LogNonResponse(queuedRequest.Request);
					queuedRequest.CanContinue = true;
				}
			}
			_isRunning = false;
		}
		private void LogRequest(IRestRequest request)
		{
			var sb = new StringBuilder();
			sb.AppendLine("Sending: {0} {1}", request.Method, request.Resource);
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
		private void LogResponse(IRestRequest request, IRestResponse response)
		{
			var sb = new StringBuilder();
			sb.AppendLine("    Requested: {0}", request.Resource);
			sb.AppendLine("    Status Code: {0}", response.StatusCode);
			sb.AppendLine("    Content: {0}", response.Content);
			_log.Info("Response:\n{0}", sb);
		}
		private void LogNonResponse(IRestRequest request)
		{
			var sb = new StringBuilder();
			sb.AppendLine("    Requested: {0}", request.Resource);
			sb.AppendLine("    Either the service is configured to not send requests or Trello.com is not accessible.");
			_log.Info("No Response:\n{0}", sb);
		}
	}
}
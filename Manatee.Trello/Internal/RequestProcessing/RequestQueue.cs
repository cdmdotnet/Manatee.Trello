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
 
	File Name:		RequestQueue.cs
	Namespace:		Manatee.Trello.Internal.RequestProcessing
	Class Name:		RequestQueue
	Purpose:		Implements IRequestQueue.

***************************************************************************************/
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal class RequestQueue : IRequestQueue
	{
		private readonly ILog _log;
		private readonly INetworkMonitor _networkMonitor;
		private readonly Queue<QueuableRestRequest> _queue;

		public int Count { get { return _queue.Count; } }

		public RequestQueue(ILog log, INetworkMonitor networkMonitor)
		{
			_log = log;
			_networkMonitor = networkMonitor;
			_queue = new Queue<QueuableRestRequest>();
		}

		public void Enqueue<T>(IRestRequest request)
			where T : class
		{
			LogRequest(request, "Queuing");
			_queue.Enqueue(new QueuableRestRequest<T>(request));
		}
		public void DequeueAndExecute(IRestClient client)
		{
			if (_queue.Count == 0) return;
			var request = _queue.Dequeue();
			Execute(client, request);
		}

		private void Execute(IRestClient client, QueuableRestRequest queuableRequest)
		{
			if (_networkMonitor.IsConnected)
			{
				LogRequest(queuableRequest.Request, "Sending");
				queuableRequest.Execute(client);
				LogResponse(queuableRequest.Request.Response);
			}
			else
			{
				LogRequest(queuableRequest.Request, "Stubbing");
				queuableRequest.CreateNullResponse();
			}
		}
		private void LogRequest(IRestRequest request, string action)
		{
			_log.Info("{2}: {0} {1}", request.Method, request.Resource, action);
		}
		private void LogResponse(IRestResponse response)
		{
			_log.Info("Received: {0}", response.Content);
		}
	}
}
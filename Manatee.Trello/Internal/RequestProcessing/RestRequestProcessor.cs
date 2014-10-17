/***************************************************************************************

	Copyright 2013 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		RestRequestProcessor.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		RestRequestProcessor
	Purpose:		Processes REST requests as they appear on the queue.

***************************************************************************************/

using System;
using System.Threading;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal static class RestRequestProcessor
	{
		private const string BaseUrl = "https://api.trello.com/1";

		private static readonly RequestQueue _queue;
		private static readonly object _lock;
		private static readonly Thread _workerThread;
		private static bool _shutdown;
		private static bool _isActive;
		private static bool _isProcessing;

		public static bool IsActive
		{
			get { return _isActive; }
			set
			{
				_isActive = value;
				Pulse();
			}
		}
		public static bool HasRequests { get { return _queue.Count != 0 || _isProcessing; } }

		static RestRequestProcessor()
		{
			_queue = new RequestQueue();
			_lock = new object();
			_shutdown = false;
			_isActive = true;
			_workerThread = new Thread(Process) { IsBackground = true };
			_workerThread.Start();
			NetworkMonitor.ConnectionStatusChanged += Pulse;
		}

		public static void AddRequest(IRestRequest request, object signal)
		{
			LogRequest(request, "Queuing");
			_queue.Enqueue(request, signal);
			Pulse();
		}
		public static void AddRequest<T>(IRestRequest request, object signal)
			where T : class
		{
			LogRequest(request, "Queuing");
			_queue.Enqueue<T>(request, signal);
			Pulse();
		}
		public static void ShutDown()
		{
			_shutdown = true;
			Pulse();
			_workerThread.Join();
		}

		private static void Process()
		{
			lock (_lock)
			{
				var client = TrelloConfiguration.RestClientProvider.CreateRestClient(BaseUrl);
				while (true)
				{
					Monitor.Wait(_lock);
					_isProcessing = true;
					while (!_shutdown && IsActive && (_queue.Count != 0))
					{
						var request = _queue.Dequeue();
						if (request == null) break;
						Execute(client, request);
					}
					_isProcessing = false;
					if (_shutdown) return;
				}
			}
		}
		private static void Pulse()
		{
			if (_isProcessing) return;
			lock (_lock)
				Monitor.Pulse(_lock);
		}
		private static void Execute(IRestClient client, QueuableRestRequest queuableRequest)
		{
			if (NetworkMonitor.IsConnected)
			{
				LogRequest(queuableRequest.Request, "Sending");
				try
				{
					queuableRequest.Execute(client);
					LogResponse(queuableRequest.Request.Response, "Received");
				}
				catch (Exception e)
				{
					var tie = new TrelloInteractionException(e);
					queuableRequest.CreateNullResponse(tie);
					TrelloConfiguration.Log.Error(tie, false);
				}
			}
			else
			{
				LogRequest(queuableRequest.Request, "Stubbing");
				queuableRequest.CreateNullResponse();
			}
		}
		private static void LogRequest(IRestRequest request, string action)
		{
			TrelloConfiguration.Log.Info("{2}: {0} {1}", request.Method, request.Resource, action);
		}
		private static void LogResponse(IRestResponse response, string action)
		{
			TrelloConfiguration.Log.Info("{0}: {1}", action, response.Content);
		}
	}
}
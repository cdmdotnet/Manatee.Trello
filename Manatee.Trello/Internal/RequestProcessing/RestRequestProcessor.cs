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
 
	File Name:		RestRequestProcessor.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		RestRequestProcessor
	Purpose:		Processes REST requests as they appear on the queue.

***************************************************************************************/

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Manatee.Trello.Contracts;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal class RestRequestProcessor : IRestRequestProcessor
	{
		private const string BaseUrl = "https://api.trello.com/1";

		private readonly ILog _log;
		private readonly Queue<QueuableRestRequest> _queue;
		private readonly IRestClientProvider _clientProvider;
		private readonly string _appKey;
		private readonly string _userToken;
		private readonly object _lock;
		private readonly Thread _workerThread;
		private bool _shutdown;
		private bool _isActive;

		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				_isActive = value;
				Pulse();
			}
		}

		public RestRequestProcessor(ILog log, Queue<QueuableRestRequest> queue, IRestClientProvider clientProvider, string appKey, string userToken)
		{
			_log = log;
			_queue = queue;
			_clientProvider = clientProvider;
			_appKey = appKey;
			_userToken = userToken;
			_lock = new object();
			_shutdown = false;
			_isActive = true;
			_workerThread = new Thread(Process);
			_workerThread.Start();
		}

		public void AddRequest<T>(IRestRequest request, RestMethod method)
			where T : class
		{
			PrepRequest(request, method);
			LogRequest(request, "Queuing");
			var queueRequest = new QueuableRestRequest
				{
					Request = request,
					ExecuteMethod = c => { request.Response = c.Execute<T>(request); }
				};
			_queue.Enqueue(queueRequest);
			Pulse();
		}
		public void ShutDown()
		{
			_shutdown = true;
			Pulse();
			_workerThread.Join();
		}

		private void Process()
		{
			lock (_lock)
			{
				while (true)
				{
					Monitor.Wait(_lock);
					var client = _clientProvider.CreateRestClient(BaseUrl);
					while (!_shutdown && IsActive && (_queue.Count != 0))
					{
						var request = _queue.Dequeue();
						LogRequest(request.Request, "Sending");
						request.ExecuteMethod(client);
					}
					if (_shutdown) return;
				}
			}
		}
		private void Pulse()
		{
			lock (_lock)
				Monitor.Pulse(_lock);
		}
		private void PrepRequest(IRestRequest request, RestMethod method)
		{
			request.Method = method;
			request.AddParameter("key", _appKey);
			if (_userToken != null)
				request.AddParameter("token", _userToken);
		}
		private void LogRequest(IRestRequest request, string action)
		{
			var sb = new StringBuilder();
			sb.AppendLine("{2}: {0} {1}", request.Method, request.Resource, action);
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
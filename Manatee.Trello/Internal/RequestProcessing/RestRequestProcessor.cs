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

using System;
using System.Threading;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal class RestRequestProcessor : IRestRequestProcessor
	{
		private const string BaseUrl = "https://api.trello.com/1";

		private readonly IRequestQueue _queue;
		private readonly IRestClientProvider _clientProvider;
		private readonly string _appKey;
		private readonly object _lock;
		private readonly Thread _workerThread;
		private bool _shutdown;
		private bool _isActive;
		private bool _isProcessing;

		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				_isActive = value;
				Pulse();
			}
		}
		public string AppKey { get { return _appKey; } }
		public string UserToken { get; set; }

		public RestRequestProcessor(IRequestQueue queue, IRestClientProvider clientProvider, TrelloAuthorization auth)
		{
			_queue = queue;
			_clientProvider = clientProvider;
			_appKey = auth.AppKey;
			UserToken = auth.UserToken;
			_lock = new object();
			_shutdown = false;
			_isActive = true;
			_workerThread = new Thread(Process) {IsBackground = true};
			_workerThread.Start();
		}

		public void AddRequest<T>(IRestRequest request)
			where T : class
		{
			PrepRequest(request);
			_queue.Enqueue<T>(request);
			Pulse();
		}
		public void ShutDown()
		{
			_shutdown = true;
			Pulse();
			_workerThread.Join();
		}
		public void NetworkStatusChanged(object sender, EventArgs e)
		{
			Pulse();
		}

		private void Process()
		{
			lock (_lock)
			{
				while (true)
				{
					Monitor.Wait(_lock);
					_isProcessing = true;
					var client = _clientProvider.CreateRestClient(BaseUrl);
					while (!_shutdown && IsActive && (_queue.Count != 0))
					{
						_queue.DequeueAndExecute(client);
					}
					_isProcessing = false;
					if (_shutdown) return;
				}
			}
		}
		private void Pulse()
		{
			if (_isProcessing) return;
			lock (_lock)
				Monitor.Pulse(_lock);
		}
		private void PrepRequest(IRestRequest request)
		{
			request.AddParameter("key", _appKey);
			if (UserToken != null)
				request.AddParameter("token", UserToken);
		}
	}
}
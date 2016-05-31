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
using System.Diagnostics;
using System.Threading;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal static class RestRequestProcessor
	{
		private const string BaseUrl = "https://api.trello.com/1";

		private static readonly Semaphore _semaphore;
		private static int _pendingRequestCount;
		private static bool _cancelPendingRequests;

#if IOS
		private static System.Action _lastCall;

		public static event System.Action LastCall
		{
			add { _lastCall += value; }
			remove { _lastCall -= value; }
		}
#else
		public static event System.Action LastCall;
#endif

		static RestRequestProcessor()
		{
			_semaphore = new Semaphore(0, TrelloProcessor.ConcurrentCallCount);
			if (NetworkMonitor.IsConnected)
				_semaphore.Release(TrelloProcessor.ConcurrentCallCount);
			else
				NetworkMonitor.ConnectionStatusChanged += () => _semaphore.Release(TrelloProcessor.ConcurrentCallCount);
		}

		public static void AddRequest(IRestRequest request, object hold)
		{
			new Thread(() => Process(c => request.Response = c.Execute(request), request, hold)).Start();
		}
		public static void AddRequest<T>(IRestRequest request, object hold)
			where T : class
		{
			new Thread(() => Process(c => request.Response = c.Execute<T>(request), request, hold)).Start();
		}
		public static void Flush()
		{
#if IOS
			var handler = _lastCall;
#else
			var handler = LastCall;
#endif
			handler?.Invoke();
		}
		public static void CancelPendingRequests()
		{
			_cancelPendingRequests = true;
			Flush();
		}

		private static void Process(Action<IRestClient> ask, IRestRequest request, object hold)
		{
			try
			{
				_pendingRequestCount++;
				_semaphore.WaitOne();
				Execute(ask, request);
			}
			catch (Exception e)
			{
				TrelloConfiguration.Log.Error(e);
			}
			finally
			{
				lock (hold)
					Monitor.Pulse(hold);
				_pendingRequestCount--;
				if (_pendingRequestCount == 0)
					_cancelPendingRequests = false;
				_semaphore.Release();
			}
		}
		private static void Execute(Action<IRestClient> ask, IRestRequest request)
		{
			if (NetworkMonitor.IsConnected && !_cancelPendingRequests)
			{
				var client = TrelloConfiguration.RestClientProvider.CreateRestClient(BaseUrl);
				LogRequest(request, "Sending");
				try
				{
					ask(client);
					LogResponse(request.Response, "Received");
				}
				catch (Exception e)
				{
					var tie = new TrelloInteractionException(e);
					request.Response = new NullRestResponse { Exception = e };
					TrelloConfiguration.Log.Error(tie, false);
				}
			}
			else
			{
				LogRequest(request, "Stubbing");
				request.Response = new NullRestResponse();
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
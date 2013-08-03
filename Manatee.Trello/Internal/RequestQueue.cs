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
	Namespace:		Manatee.Trello.Internal
	Class Name:		RequestQueue
	Purpose:		Queues REST requests so that they may be saved in the event
					that an internet connection is unavailable.

***************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal
{
	internal class RequestQueue : IRequestQueue
	{
		private readonly Queue<IQueuedRestRequest> _queue;

		public event EventHandler ItemQueued;

		public RequestQueue()
		{
			_queue = new Queue<IQueuedRestRequest>();
		}

		public IQueuedRestRequest Dequeue()
		{
			if (_queue.Count == 0) return null;
			var retVal = _queue.Dequeue();
			return retVal;
		}
		public void Enqueue(IQueuedRestRequest request)
		{
			_queue.Enqueue(request);
			if (ItemQueued != null)
				ItemQueued(this, new EventArgs());
		}
		public void BulkEnqueue(IEnumerable<IQueuedRestRequest> requests)
		{
			foreach (var request in requests)
			{
				_queue.Enqueue(request);
			}
		}
		public IEnumerator<IQueuedRestRequest> GetEnumerator()
		{
			return _queue.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
﻿/***************************************************************************************

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
 
	File Name:		RequestQueue.cs
	Namespace:		Manatee.Trello.Internal.RequestProcessing
	Class Name:		RequestQueue
	Purpose:		Implements IRequestQueue.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal class RequestQueue
	{
		private readonly Queue<QueuableRestRequest> _queue;

		public int Count { get { return _queue.Count; } }

		public RequestQueue()
		{
			_queue = new Queue<QueuableRestRequest>();
		}

		public void Enqueue(IRestRequest request)
		{
			_queue.Enqueue(new QueuableRestRequest(request));
		}
		public void Enqueue<T>(IRestRequest request)
			where T : class
		{
			_queue.Enqueue(new QueuableRestRequest<T>(request));
		}
		public QueuableRestRequest Dequeue()
		{
			if (_queue.Count == 0) return null;
			var request = _queue.Dequeue();
			return request;
		}
	}
}
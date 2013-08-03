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
 
	File Name:		IRequestQueue.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		IRequestQueue
	Purpose:		Defines methods required to queue REST requests.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Internal;

namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// Defines methods required to queue REST requests.
	/// </summary>
	public interface IRequestQueue : IEnumerable<IQueuedRestRequest>
	{
		/// <summary>
		/// Raised when an item is added to the queue.
		/// </summary>
		event EventHandler ItemQueued;

		/// <summary>
		/// Removes the first item from the queue.
		/// </summary>
		/// <returns>The first item in the queue, or null if the queue is empty.</returns>
		IQueuedRestRequest Dequeue();
		/// <summary>
		/// Adds an item to the queue, then raises ItemQueued.
		/// </summary>
		/// <param name="request">A request.</param>
		void Enqueue(IQueuedRestRequest request);
		/// <summary>
		/// Adds a collection of requests to the queue at once, then raises ItemQueued once.
		/// </summary>
		/// <param name="requests">A collection of requests.</param>
		void BulkEnqueue(IEnumerable<IQueuedRestRequest> requests);
	}
}
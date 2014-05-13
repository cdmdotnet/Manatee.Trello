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
 
	File Name:		OfflineChangeQueue.cs
	Namespace:		Manatee.Trello.Internal.RequestProcessing
	Class Name:		OfflineChangeQueue
	Purpose:		Implements IOfflineChangeQueue.

***************************************************************************************/

using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal class OfflineChangeQueue
	{
		private readonly Queue<OfflineChange> _queue;

		public OfflineChangeQueue()
		{
			_queue = new Queue<OfflineChange>();
		}

		public void Enqueue(ExpiringObject entity, Endpoint endpoint, IDictionary<string, object> parameters)
		{
			var change = new OfflineChange(entity, endpoint, parameters.ToDictionary(p => p.Key, p => p.Value));
			_queue.Enqueue(change);
		}
		public void Requeue(IEnumerable<OfflineChange> changes)
		{
			foreach (var change in changes)
			{
				_queue.Enqueue(change);
			}
		}
		public OfflineChange Dequeue()
		{
			return _queue.Count == 0 ? null : _queue.Dequeue();
		}
		public void ResolveId(string replace, string replaceWith)
		{
			foreach (var offlineChange in _queue)
			{
				offlineChange.Endpoint.Resolve(replace, replaceWith);
				var keys = offlineChange.Parameters.Keys.ToList();
				foreach (var key in keys)
				{
					if (offlineChange.Parameters[key].ToString() == replace)
						offlineChange.Parameters[key] = replaceWith;
				}
			}
		}
	}
}
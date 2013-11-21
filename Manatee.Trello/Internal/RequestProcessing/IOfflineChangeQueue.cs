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
 
	File Name:		IOfflineChangeQueue.cs
	Namespace:		Manatee.Trello.Internal.RequestProcessing
	Class Name:		IOfflineChangeQueue
	Purpose:		Defines methods required to track and process offline changes.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal.RequestProcessing
{
	/// <summary>
	/// Defines methods required to track and process offline changes.
	/// </summary>
	/// <remarks>
	/// This interface is only exposed for unit testing purposes.
	/// </remarks>
	public interface IOfflineChangeQueue
	{
		/// <summary />
		void Enqueue(ExpiringObject entity, Endpoint endpoint, IDictionary<string, object> parameters);
		/// <summary />
		void Requeue(IEnumerable<OfflineChange> changes);
		/// <summary />
		OfflineChange Dequeue();
		/// <summary />
		void ResolveId(string replace, string replaceWith);
	}
}
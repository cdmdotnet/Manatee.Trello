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
 
	File Name:		IRequestQueue.cs
	Namespace:		Manatee.Trello.Internal.RequestProcessing
	Class Name:		IRequestQueue
	Purpose:		Defines methods required to manage REST requests in a queue.

***************************************************************************************/

using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	/// <summary>
	/// Defines methods required to manage REST requests in a queue.
	/// </summary>
	/// <remarks>
	/// This interface is only exposed for unit testing purposes.
	/// </remarks>
	public interface IRequestQueue
	{
		/// <summary />
		int Count { get; }

		/// <summary />
		void Enqueue<T>(IRestRequest request) where T : class;
		/// <summary />
		void DequeueAndExecute(IRestClient client);
	}
}
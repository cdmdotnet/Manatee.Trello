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
 
	File Name:		IQueuedRestRequest.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		IQueuedRestRequest
	Purpose:		Defines properties required for a REST request to be queued.

***************************************************************************************/

using System;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal
{
	/// <summary>
	/// Defines properties required for a REST request to be queued.
	/// </summary>
	public interface IQueuedRestRequest
	{
		/// <summary>
		/// The request to send.
		/// </summary>
		IRestRequest Request { get; set; }
		/// <summary>
		/// The type of data expected to receive.
		/// </summary>
		Type RequestedType { get; set; }
		/// <summary>
		/// The response from sending the request.
		/// </summary>
		IRestResponse Response { get; set; }
		/// <summary>
		/// A flag used by the service to indicate that execution may continue.
		/// Does not necessarily indicate that the request was sent.
		/// </summary>
		bool CanContinue { get; set; }
	}
}
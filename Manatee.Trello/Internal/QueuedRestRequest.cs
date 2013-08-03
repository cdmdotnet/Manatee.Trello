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
 
	File Name:		QueuedRestRequest.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		QueuedRestRequest
	Purpose:		Implements IQueuedRestRequest to wrap REST requests so that
					they may be queued.

***************************************************************************************/

using System;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal
{
	internal class QueuedRestRequest : IQueuedRestRequest
	{
		public IRestRequest Request { get; set; }
		public Type RequestedType { get; set; }
		public IRestResponse Response { get; set; }
		public bool CanContinue { get; set; }
	}
}
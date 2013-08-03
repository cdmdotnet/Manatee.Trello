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
 
	File Name:		IRequestQueueHandler.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		IRequestQueueHandler
	Purpose:		Defines methods required to handle REST requests as they\
					appear in a queue.

***************************************************************************************/

using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal
{
	internal interface IRequestQueueHandler
	{
		bool IsActive { get; set; }
		bool IsConnected { get; }
		T Handle<T>(IRestRequest request) where T : class;
	}
}
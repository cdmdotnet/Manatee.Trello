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
 
	File Name:		NullRestResponse.cs
	Namespace:		Manatee.Trello.Internal.RequestProcessing
	Class Name:		NullRestResponse
	Purpose:		Implements IRestResponse as a placeholder for when the network
					is unavailable.

***************************************************************************************/

using System.Net;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal class NullRestResponse<T> : IRestResponse<T>
	{
		public string Content { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		public T Data { get; set; }
	}
}
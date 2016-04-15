/***************************************************************************************

	Copyright 2016 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		WebApiRestResponse.cs
	Namespace:		Manatee.Trello.WebApi
	Class Name:		WebApiRestResponse
	Purpose:		Implements IRestResponse for WebApi.

***************************************************************************************/

using System;
using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	internal class WebApiRestResponse : IRestResponse
	{
		public string Content { get; set; }
		public Exception Exception { get; set; }
	}

	internal class WebApiRestResponse<T> : WebApiRestResponse, IRestResponse<T>
	{
		public T Data { get; set; }
	}
}
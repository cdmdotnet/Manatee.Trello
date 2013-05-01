/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		RestResponse.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		RestResponse<T>
	Purpose:		Wraps RestSharp.IRestResponse<T> in a class which implements
					IRestResponse<T>.

***************************************************************************************/
using System;
using System.Net;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Rest
{
	internal class RestSharpResponse<T> : IRestResponse<T>
	{
		private readonly RestSharp.IRestResponse _response;

		public RestSharpResponse(RestSharp.IRestResponse response, T data)
		{
			_response = response;
			Data = data;
		}

		public string Content { get { return _response.Content; } }
		public T Data { get; private set; }
		public HttpStatusCode StatusCode { get { return _response.StatusCode; } }
	}
}
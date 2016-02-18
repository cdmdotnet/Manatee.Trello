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
 
	File Name:		RestSharpResponse.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		RestSharpResponse<T>
	Purpose:		Wraps RestSharp.IRestResponse<T> in a class which implements
					IRestResponse<T>.

***************************************************************************************/

using System;
using IRestResponse = RestSharp.IRestResponse;

namespace Manatee.Trello.RestSharp
{
	internal class RestSharpResponse : Rest.IRestResponse
	{
		private readonly IRestResponse _response;

		public string Content => _response.Content;
		public Exception Exception { get; set; }

		public RestSharpResponse(IRestResponse response)
		{
			_response = response;
		}
	}

	internal class RestSharpResponse<T> : RestSharpResponse, Rest.IRestResponse<T>
	{
		public T Data { get; private set; }

		public RestSharpResponse(IRestResponse response, T data)
			: base(response)
		{
			Data = data;
		}
	}
}
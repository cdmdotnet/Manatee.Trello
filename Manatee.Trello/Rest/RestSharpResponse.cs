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
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Wraps RestSharp.IRestResponse&lt;T&gt; in a class which implements IRestResponse&lt;T&gt;.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class RestSharpResponse<T> : IRestResponse<T>
		where T : new()
	{
		private readonly RestSharp.IRestResponse<T> _response;

		/// <summary>
		/// Creates a new instance of the RestResponse&lt;T&gt; class.
		/// </summary>
		/// <param name="response"></param>
		public RestSharpResponse(RestSharp.IRestResponse<T> response)
		{
			_response = response;
		}

		/// <summary>
		/// The JSON content returned by the call.
		/// </summary>
		public string Content
		{
			get { return _response.Content; }
		}
		/// <summary>
		/// The deserialized data.
		/// </summary>
		public T Data
		{
			get { return _response.Data; }
		}
	}
}
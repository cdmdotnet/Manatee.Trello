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
 
	File Name:		RestException.cs
	Namespace:		Manatee.Trello.Exceptions
	Class Name:		RestException<T>
	Purpose:		Thrown when a RESTful call has some manner of failure.

***************************************************************************************/
using System;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Exceptions
{
	/// <summary>
	/// Thrown when a RESTful call has some manner of failure.
	/// </summary>
	/// <typeparam name="T">The type of object which was requested.</typeparam>
	public class RestException<T> : Exception
		where T : new()
	{
		/// <summary>
		/// The request object which generated the error.
		/// </summary>
		public IRestRequest<T> Request { get; private set; }
		/// <summary>
		/// The response object associated with the error.
		/// </summary>
		public IRestResponse Response { get; private set; }

		/// <summary>
		/// Creates a new instance of the RestException&lt;T&gt; class.
		/// </summary>
		/// <param name="request">The request object which generated the error.</param>
		/// <param name="response">The response object associated with the error.</param>
		public RestException(IRestRequest<T> request, IRestResponse response)
			: this("An error occurred during the request.", request, response) {}
		/// <summary>
		/// Creates a new instance of the RestException&lt;T&gt; class.
		/// </summary>
		/// <param name="message">A message for the exception.</param>
		/// <param name="request">The request object which generated the error.</param>
		/// <param name="response">The response object associated with the error.</param>
		public RestException(string message, IRestRequest<T> request, IRestResponse response)
			: base(message)
		{
			Request = request;
			Response = response;
		}
	}
}

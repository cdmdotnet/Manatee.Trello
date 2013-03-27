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
	public class RestException<T> : Exception
		where T : new()
	{
		public IRestRequest<T> Request { get; private set; }
		public IRestResponse Response { get; private set; }

		public RestException(IRestRequest<T> request, IRestResponse response)
			: this("An error occurred during the request.", request, response) {}
		public RestException(string message, IRestRequest<T> request, IRestResponse response)
			: base(message)
		{
			Request = request;
			Response = response;
		}
	}
}

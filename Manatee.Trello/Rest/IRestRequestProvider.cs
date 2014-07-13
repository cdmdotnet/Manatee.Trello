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
 
	File Name:		IRestRequestProvider.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		IRestRequestProvider
	Purpose:		Defines methods required to generate IRequest objects
					used to make RESTful calls.

***************************************************************************************/

using System.Collections.Generic;

namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Defines methods to generate IRequest objects used to make RESTful calls.
	/// </summary>
	public interface IRestRequestProvider
	{
		/// <summary>
		/// Creates a general request using a collection of objects and an additional parameter to
		/// generate the resource string and an object to supply additional parameters.
		/// </summary>
		/// <param name="endpoint">The method endpoint the request calls.</param>
		/// <param name="parameters">A list of paramaters to include in the request.</param>
		/// <returns>An IRestRequest instance which can be sent to an IRestClient.</returns>
		IRestRequest Create(string endpoint, IDictionary<string, object> parameters = null);
	}
}
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
 
	File Name:		IDeserializer.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IDeserializer
	Purpose:		Defines methods required by the IRestClient to deserialize a
					response from JSON to an object.

***************************************************************************************/
using Manatee.Trello.Rest;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines methods required by the IRestClient to deserialize a response
	/// from JSON to an object.
	/// </summary>
	public interface IDeserializer
	{
		/// <summary>
		/// Attempts to deserialize a RESTful response to the indicated type.
		/// </summary>
		/// <typeparam name="T">The type of object expected.</typeparam>
		/// <param name="response">The response object which contains the JSON to deserialize.</param>
		/// <returns>The requested object, if JSON is valid; null otherwise.</returns>
		T Deserialize<T>(IRestResponse<T> response);
		/// <summary>
		/// Attempts to deserialize a RESTful response to the indicated type.
		/// </summary>
		/// <typeparam name="T">The type of object expected.</typeparam>
		/// <param name="content">A string which contains the JSON to deserialize.</param>
		/// <returns>The requested object, if JSON is valid; null otherwise.</returns>
		T Deserialize<T>(string content);
	}
}
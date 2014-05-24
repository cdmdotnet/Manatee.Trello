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
 
	File Name:		IRestClient.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		IRestClient
	Purpose:		Defines methods required to make RESTful calls.

***************************************************************************************/

namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Defines methods required to make RESTful calls.
	/// </summary>
	public interface IRestClient
	{
		/// <summary>
		/// Makes a RESTful call and ignores any return data.
		/// </summary>
		/// <param name="request">The request.</param>
		IRestResponse Execute(IRestRequest request);

		/// <summary>
		/// Makes a RESTful call and expects a single object to be returned.
		/// </summary>
		/// <typeparam name="T">The expected type of object to receive in response.</typeparam>
		/// <param name="request">The request.</param>
		/// <returns>The response.</returns>
		IRestResponse<T> Execute<T>(IRestRequest request)
			where T : class;
	}
}
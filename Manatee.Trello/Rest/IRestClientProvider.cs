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
 
	File Name:		IRestClientProvider.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		IRestClientProvider
	Purpose:		Defines methods required to create an instance of IRestClient.

***************************************************************************************/
namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Defines methods required to create an instance of IRestClient.
	/// </summary>
	public interface IRestClientProvider
	{
		/// <summary>
		/// Creates requests for the client.
		/// </summary>
		IRestRequestProvider RequestProvider { get; }

		/// <summary>
		/// Creates an instance of IRestClient.
		/// </summary>
		/// <param name="apiBaseUrl">The base URL to be used by the client</param>
		/// <returns>An instance of IRestClient.</returns>
		IRestClient CreateRestClient(string apiBaseUrl);
	}
}
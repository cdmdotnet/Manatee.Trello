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
 
	File Name:		ITrelloService.cs
	Namespace:		Manatee.Trello.Contracts
	Class Name:		ITrelloService
	Purpose:		Defines methods required to retrieve entities from Trello.

***************************************************************************************/
using System.Collections.Generic;
using Manatee.Trello.Internal;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// Defines methods required to retrieve entities from Trello.
	/// </summary>
	public interface ITrelloService
	{
		/// <summary>
		/// Gets and sets the AuthToken for the service.
		/// </summary>
		string AuthToken { get; set; }
		/// <summary>
		/// Provides caching for retrieved entities.
		/// </summary>
		ICache Cache { get; set; }
		/// <summary>
		/// Gets the Member object associated with the provided AuthToken.
		/// </summary>
		Member Me { get; }
		/// <summary>
		/// Gets and sets the IRestClientProvider to be used by the service.
		/// </summary>
		IRestClientProvider RestClientProvider { get; set; }
		/// <summary>
		/// Facilitates calling the Trello API.
		/// </summary>
		/// <remarks>
		/// Provided for testing.  It is not recommended that this is used.
		/// </remarks>
		ITrelloRest Api { get; }
		/// <summary>
		/// Retrieves an entity from Trello.
		/// </summary>
		/// <typeparam name="T">The type of entity to retrieve.</typeparam>
		/// <param name="id">The ID of the entity.</param>
		/// <returns>The entity which corresponds to the ID, or null if not found.</returns>
		T Retrieve<T>(string id) where T : ExpiringObject, new();
		/// <summary>
		/// Searches actions, boards, cards, members and organizations for a provided
		/// query string.
		/// </summary>
		/// <param name="query">The query string.</param>
		/// <returns>An object which contains the results of the query.</returns>
		SearchResults Search(string query);
		/// <summary>
		/// Searches for members whose names or usernames match a provided query string.
		/// </summary>
		/// <param name="query">The query string.</param>
		/// <param name="limit">The maximum number of results to return.</param>
		/// <returns>A collection of members.</returns>
		IEnumerable<Member> SearchMembers(string query, int limit = 0);
	}
}
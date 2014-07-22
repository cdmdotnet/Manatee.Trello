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
 
	File Name:		IJsonSearch.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonSearch
	Purpose:		Defines the JSON structure for the Search object.

***************************************************************************************/
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Search object.
	/// </summary>
	public interface IJsonSearch
	{
		/// <summary>
		/// Lists the IDs of actions which match the query.
		/// </summary>
		List<IJsonAction> Actions { get; set; }
		/// <summary>
		/// Lists the IDs of boards which match the query.
		/// </summary>
		List<IJsonBoard> Boards { get; set; }
		/// <summary>
		/// Lists the IDs of cards which match the query.
		/// </summary>
		List<IJsonCard> Cards { get; set; }
		/// <summary>
		/// Lists the IDs of members which match the query.
		/// </summary>
		List<IJsonMember> Members { get; set; }
		/// <summary>
		/// Lists the IDs of organizations which match the query.
		/// </summary>
		List<IJsonOrganization> Organizations { get; set; }
		string Query { get; set; }
		List<IJsonCacheable> Context { get; set; }
		SearchModelType Types { get; set; }
	}
}
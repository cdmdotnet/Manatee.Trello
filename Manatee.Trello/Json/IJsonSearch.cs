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
		[JsonDeserialize]
		List<IJsonAction> Actions { get; set; }
		/// <summary>
		/// Lists the IDs of boards which match the query.
		/// </summary>
		[JsonDeserialize]
		List<IJsonBoard> Boards { get; set; }
		/// <summary>
		/// Lists the IDs of cards which match the query.
		/// </summary>
		[JsonDeserialize]
		List<IJsonCard> Cards { get; set; }
		/// <summary>
		/// Lists the IDs of members which match the query.
		/// </summary>
		[JsonDeserialize]
		List<IJsonMember> Members { get; set; }
		/// <summary>
		/// Lists the IDs of organizations which match the query.
		/// </summary>
		[JsonDeserialize]
		List<IJsonOrganization> Organizations { get; set; }
		/// <summary>
		/// Gets or sets the search query.
		/// </summary>
		[JsonSerialize(IsRequired = true)]
		string Query { get; set; }
		/// <summary>
		/// Gets or sets a collection of boards, cards, and organizations within
		/// which the search should run.
		/// </summary>
		[JsonSerialize]
		List<IJsonCacheable> Context { get; set; }
		/// <summary>
		/// Gets or sets which types of objects should be returned.
		/// </summary>
		[JsonSerialize]
		SearchModelType? Types { get; set; }
		/// <summary>
		/// Gets or sets how many results to return;
		/// </summary>
		[JsonSerialize]
		int? Limit { get; set; }
		/// <summary>
		/// Gets or sets whether the search should match on partial words.
		/// </summary>
		[JsonSerialize]
		bool Partial { get; set; }
	}
}
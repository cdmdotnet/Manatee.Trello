/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		IJsonMemberSearch.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonMemberSearch
	Purpose:		Defines the JSON structure for a member search.

***************************************************************************************/
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for a member search.
	/// </summary>
	public interface IJsonMemberSearch
	{
		/// <summary>
		/// Gets or sets a list of members.
		/// </summary>
		[JsonDeserialize]
		List<IJsonMember> Members { get; set; }
		/// <summary>
		/// Gets or sets a board within which the search should run.
		/// </summary>
		[JsonDeserialize]
		IJsonBoard Board { get; set; }
		/// <summary>
		/// Gets or sets the number of results to return.
		/// </summary>
		[JsonSerialize]
		int? Limit { get; set; }
		/// <summary>
		/// Gets or sets whether only organization members should be returned.
		/// </summary>
		[JsonSerialize]
		bool? OnlyOrgMembers { get; set; }
		/// <summary>
		/// Gets or sets an organization within which the search should run.
		/// </summary>
		[JsonSerialize]
		IJsonOrganization Organization { get; set; }
		/// <summary>
		/// Gets or sets the search query.
		/// </summary>
		[JsonSerialize(IsRequired = true)]
		string Query { get; set; }
	}
}
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
 
	File Name:		IJsonOrganizationPreferences.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonOrganizationPreferences
	Purpose:		Defines the JSON structure for the OrganizationPreferences object.

***************************************************************************************/
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the OrganizationPreferences object.
	/// </summary>
	public interface IJsonOrganizationPreferences
	{
		/// <summary>
		/// Gets or sets the permission level.
		/// </summary>
		[JsonDeserialize]
		OrganizationPermissionLevel? PermissionLevel { get; set; }
		/// <summary>
		/// Gets or sets organization invitation restrictions.
		/// </summary>
		[JsonDeserialize]
		List<object> OrgInviteRestrict { get; set; }
		/// <summary>
		/// Gets or sets whether external members are disabled.
		/// </summary>
		[JsonDeserialize]
		bool? ExternalMembersDisabled { get; set; }
		/// <summary>
		/// Gets or sets the Google Apps domain.
		/// </summary>
		[JsonDeserialize]
		string AssociatedDomain { get; set; }
		/// <summary>
		/// Gets or sets the visibility of boards owned by the organization.
		/// </summary>
		[JsonDeserialize]
		IJsonBoardVisibilityRestrict BoardVisibilityRestrict { get; set; }
	}
}
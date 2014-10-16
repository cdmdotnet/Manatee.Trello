﻿/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		MembershipFilter.cs
	Namespace:		Manatee.Trello
	Class Name:		MembershipFilter
	Purpose:		Enumerates the filter options for membership collections.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the filter options for membership collections.
	/// </summary>
	public enum MembershipFilter
	{
		/// <summary>
		/// Filters to only the owner of the user token.
		/// </summary>
		/// <remarks>
		/// Get the board/organization membership information in addition to the member's profile.
		/// </remarks>
		[Description("me")]
		Me,
		/// <summary>
		/// Filters to only normal members.
		/// </summary>
		[Description("normal")]
		Normal,
		/// <summary>
		/// Filters to only admins.
		/// </summary>
		[Description("admin")]
		Admin,
		/// <summary>
		/// Filters to only active members.
		/// </summary>
		[Description("active")]
		Active,
		/// <summary>
		/// Filters to only deactivated members.
		/// </summary>
		[Description("deactivated")]
		Deactivated
	}
}

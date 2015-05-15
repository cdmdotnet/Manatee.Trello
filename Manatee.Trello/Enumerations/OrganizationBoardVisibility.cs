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
 
	File Name:		OrganizationBoardVisibility.cs
	Namespace:		Manatee.Trello
	Class Name:		OrganizationBoardVisibility
	Purpose:		Enumerates known visibility levels for board in orgainzations.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates known visibility levels for board in orgainzations.
	/// </summary>
	public enum OrganizationBoardVisibility
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that a board within an organization will not be visible.
		/// </summary>
		[Description("none")]
		None,
		/// <summary>
		/// Indicates that a board within an organization will be visible to the organization admins.
		/// </summary>
		[Description("admin")]
		Admin,
		/// <summary>
		/// Indicates that a board within an organization will be visible to the organization members.
		/// </summary>
		[Description("org")]
		Org
	}
}
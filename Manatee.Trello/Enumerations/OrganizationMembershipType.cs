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
 
	File Name:		OrganizationMembershipType.cs
	Namespace:		Manatee.Trello
	Class Name:		OrganizationMembershipType
	Purpose:		Enumerates known organization membership types on Trello.com.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	///<summary>
	/// Enumerates known board membership types.
	///</summary>
	public enum OrganizationMembershipType
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates the member is an admin of the organization.
		/// </summary>
		[Description("admin")]
		Admin,
		/// <summary>
		/// Indicates the member is a normal member of the organization.
		/// </summary>
		[Description("normal")]
		Normal,
		/// <summary>
		/// Indicates the member is may only view the organization.
		/// </summary>
		[Description("observer")]
		Observer,
		/// <summary>
		/// Indicates the member has been invited, but has not yet joined Trello.
		/// </summary>
		[Description("ghost")]
		Ghost
	}
}

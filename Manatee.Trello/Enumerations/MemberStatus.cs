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
 
	File Name:		MemberStatus.cs
	Namespace:		Manatee.Trello
	Class Name:		MemberStatus
	Purpose:		Enumerates known values for member status on Trello.com.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates known values for a member's activity status.
	/// </summary>
	public enum MemberStatus
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates the member is not connected to the website.
		/// </summary>
		[Description("disconnected")]
		Disconnected,
		/// <summary>
		/// Indicates the member is connected to the website but inactive.
		/// </summary>
		[Description("idle")]
		Idle,
		/// <summary>
		/// Indicates the member is actively using the website.
		/// </summary>
		[Description("active")]
		Active
	}
}
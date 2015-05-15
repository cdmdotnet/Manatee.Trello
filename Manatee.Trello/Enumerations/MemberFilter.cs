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
 
	File Name:		MemberFilter.cs
	Namespace:		Manatee.Trello
	Class Name:		MemberFilter
	Purpose:		Enumerates the filter options for member collections.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the filter options for member collections.
	/// </summary>
	public enum MemberFilter
	{
		/// <summary>
		/// Filters to only normal members.
		/// </summary>
		[Description("normal")]
		Normal,
		/// <summary>
		/// Filters to only admins.
		/// </summary>
		[Description("admins")]
		Admins,
		/// <summary>
		/// Filters to only owners.
		/// </summary>
		/// <remarks>
		/// Per @doug at Trello regarding <see cref="Admins"/> == <see cref="Owners"/>: "Turns out owners was once used by the iOS app and we only have it there for backwards compatibility. They are the same."
		/// </remarks>
		[Description("owners")]
		Owners,
	}
}

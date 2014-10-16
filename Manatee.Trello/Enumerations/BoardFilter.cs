/***************************************************************************************

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
 
	File Name:		BoardFilter.cs
	Namespace:		Manatee.Trello
	Class Name:		BoardFilter
	Purpose:		Enumerates the filter options for board collections.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the filter options for board collections.
	/// </summary>
	public enum BoardFilter
	{
		/// <summary>
		/// Filters to boards that only members can access.
		/// </summary>
		[Description("members")]
		Members,
		/// <summary>
		/// Filters to boards that only organization members can access.
		/// </summary>
		[Description("organization")]
		Organization,
		/// <summary>
		/// Filters to boards that are publicly accessible.
		/// </summary>
		[Description("public")]
		Public,
		/// <summary>
		/// Filters to open boards.
		/// </summary>
		[Description("open")]
		Open,
		/// <summary>
		/// Filters to closed boards.
		/// </summary>
		[Description("closed")]
		Closed,
		/// <summary>
		/// Filters to pinned boards.
		/// </summary>
		[Description("pinned")]
		Pinned,
		/// <summary>
		/// Filters to unpinned boards.
		/// </summary>
		[Description("unpinned")]
		Unpinned,
		/// <summary>
		/// Filters to starred boards.
		/// </summary>
		[Description("starred")]
		Starred
	}
}

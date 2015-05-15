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
 
	File Name:		ListFilter.cs
	Namespace:		Manatee.Trello
	Class Name:		ListFilter
	Purpose:		Enumerates the filter options for list collections.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the filter options for list collections.
	/// </summary>
	public enum ListFilter
	{
		/// <summary>
		/// Filters to only unarchived lists.
		/// </summary>
		[Description("open")]
		Open,
		/// <summary>
		/// Filters to only archived lists.
		/// </summary>
		[Description("closed")]
		Closed,
		/// <summary>
		/// Indicates that all lists should be returned.
		/// </summary>
		[Description("all")]
		All,
	}
}

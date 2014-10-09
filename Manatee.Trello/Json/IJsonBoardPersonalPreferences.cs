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
 
	File Name:		IJsonBoardPersonalPreferences.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonBoardPersonalPreferences
	Purpose:		Defines the JSON structure for the BoardPersonalPreferences object.

***************************************************************************************/
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the BoardPersonalPreferences object.
	/// </summary>
	public interface IJsonBoardPersonalPreferences
	{
		///<summary>
		/// Gets or sets whether the side bar (right side of the screen) is shown
		///</summary>
		[JsonDeserialize]
		bool? ShowSidebar { get; set; }
		///<summary>
		/// Gets or sets whether the members section of the list of the side bar is shown.
		///</summary>
		[JsonDeserialize]
		bool? ShowSidebarMembers { get; set; }
		/// <summary>
		/// Gets or sets whether the board actions (Add List/Add Member/Filter Cards) section of the side bar is shown.
		/// </summary>
		[JsonDeserialize]
		bool? ShowSidebarBoardActions { get; set; }
		/// <summary>
		/// Gets or sets whether the activity section of the side bar is shown.
		/// </summary>
		[JsonDeserialize]
		bool? ShowSidebarActivity { get; set; }
		///<summary>
		/// Gets or sets whether the list guide (left side of the screen) is expanded.
		///</summary>
		[JsonDeserialize]
		bool? ShowListGuide { get; set; }
		///<summary>
		/// Gets or sets the position of new cards when they are added via email.
		///</summary>
		[JsonDeserialize]
		IJsonPosition EmailPosition { get; set; }
		///<summary>
		/// Gets or sets the list for new cards when they are added via email.
		///</summary>
		[JsonDeserialize]
		IJsonList EmailList { get; set; }
	}
}
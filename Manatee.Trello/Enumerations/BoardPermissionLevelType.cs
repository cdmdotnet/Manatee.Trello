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
 
	File Name:		BoardPermissionLevelType.cs
	Namespace:		Manatee.Trello
	Class Name:		BoardPermissionLevelType
	Purpose:		Enumerates known values for board permission levels on Trello.com.

***************************************************************************************/
namespace Manatee.Trello
{
	///<summary>
	/// Enumerates known values for board permission levels
	///</summary>
	public enum BoardPermissionLevelType
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that the board can only be viewed by its members.
		/// </summary>
		Private,
		/// <summary>
		/// Indicates that the board may be viewed by any member of the organization to which the board belongs.
		/// </summary>
		Org,
		/// <summary>
		/// Indicates that anyone (even non-Trello users) may view the board.
		/// </summary>
		Public
	}
}
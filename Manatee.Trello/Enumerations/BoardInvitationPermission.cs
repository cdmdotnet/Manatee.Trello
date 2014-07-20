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
 
	File Name:		BoardInvitationPermission.cs
	Namespace:		Manatee.Trello
	Class Name:		BoardInvitationPermission
	Purpose:		Enumerates known board invitation permission levels on Trello.com.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	///<summary>
	/// Enumerates known board invitation permission levels.
	///</summary>
	public enum BoardInvitationPermission
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that any member of the board may extend an invitation to join the board.
		/// </summary>
		[Description("members")]
		Members,
		/// <summary>
		/// Indicates that only admins of the board may extend an invitation to joni the board.
		/// </summary>
		[Description("admins")]
		Admins
	}
}
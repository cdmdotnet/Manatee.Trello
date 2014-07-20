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
 
	File Name:		BoardVotingPermission.cs
	Namespace:		Manatee.Trello
	Class Name:		BoardVotingPermission
	Purpose:		Enumerates known voting permission levels for a board on
					Trello.com.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates known voting permission levels for a board
	/// </summary>
	public enum BoardVotingPermission
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that only members of the board may vote on cards.
		/// </summary>
		[Description("members")]
		Members,
		/// <summary>
		/// Indicates that only members of the organization to which the board belongs may vote on cards.
		/// </summary>
		[Description("org")]
		Org,
		/// <summary>
		/// Indicates that any Trello member may vote on cards.
		/// </summary>
		[Description("public")]
		Public,
		/// <summary>
		/// Indicates that no members may vote on cards.
		/// </summary>
		[Description("disabled")]
		Disabled
	}
}
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
 
	File Name:		CheckItemState.cs
	Namespace:		Manatee.Trello
	Class Name:		CheckItemState
	Purpose:		Enumerates known values for an item in a checklist on Trello.com.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates known values for an item in a checklist.
	/// </summary>
	public enum CheckItemState
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that the checlist item is not checked.
		/// </summary>
		[Description("incomplete")]
		Incomplete,
		/// <summary>
		/// Indicates that the checlist item is checked.
		/// </summary>
		[Description("complete")]
		Complete
	}
}
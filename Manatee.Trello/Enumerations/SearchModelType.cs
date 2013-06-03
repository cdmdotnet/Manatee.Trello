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
 
	File Name:		SearchModelType.cs
	Namespace:		Manatee.Trello
	Class Name:		SearchModelType
	Purpose:		Enumerates the model types for which one can search.

***************************************************************************************/
using System;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the model types for which one can search.
	/// </summary>
	[Flags]
	public enum SearchModelType
	{
		/// <summary>
		/// Indicates the search should return actions.
		/// </summary>
		Actions = 1,
		/// <summary>
		/// Indicates the search should return boards.
		/// </summary>
		Boards = 2,
		/// <summary>
		/// Indicates the search should return cards.
		/// </summary>
		Cards = 4,
		/// <summary>
		/// Indicates the search should return members.
		/// </summary>
		Members = 8,
		/// <summary>
		/// Indicates the search should return organizations.
		/// </summary>
		Organizations = 16,
		/// <summary>
		/// Indicates the search should return all model types.
		/// </summary>
		All = 31
	}
}
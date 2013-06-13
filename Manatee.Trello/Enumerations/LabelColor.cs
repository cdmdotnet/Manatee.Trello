﻿/***************************************************************************************

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
 
	File Name:		LabelColor.cs
	Namespace:		Manatee.Trello
	Class Name:		LabelColor
	Purpose:		Enumerates current label colors on Trello.com.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates label colors for a board.
	/// </summary>
	public enum LabelColor
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown = -1,
		/// <summary>
		/// Indicates a green label.
		/// </summary>
		Green,
		/// <summary>
		/// Indicates a yellow label.
		/// </summary>
		Yellow,
		/// <summary>
		/// Indicates an orange label.
		/// </summary>
		Orange,
		/// <summary>
		/// Indicates a red label.
		/// </summary>
		Red,
		/// <summary>
		/// Indicates a purple label.
		/// </summary>
		Purple,
		/// <summary>
		/// Indicates a blue label.
		/// </summary>
		Blue
	}
}
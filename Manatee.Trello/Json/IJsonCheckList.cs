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
 
	File Name:		IJsonCheckList.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonCheckList
	Purpose:		Defines the JSON structure for the CheckList object.

***************************************************************************************/

using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the CheckList object.
	/// </summary>
	public interface IJsonCheckList : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the name of this checklist.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the ID of the board which contains this checklist.
		/// </summary>
		[JsonDeserialize]
		IJsonBoard Board { get; set; }
		/// <summary>
		/// Gets or sets the ID of the card which contains this checklist.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonCard Card { get; set; }
		/// <summary>
		/// Gets or sets the collection of items in this checklist.
		/// </summary>
		[JsonDeserialize]
		List<IJsonCheckItem> CheckItems { get; set; }
		/// <summary>
		/// Gets or sets the position of this checklist.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonPosition Pos { get; set; }
		/// <summary>
		/// Gets or sets a checklist to copy during creation.
		/// </summary>
		[JsonSerialize]
		IJsonCheckList CheckListSource { get; set; }
	}
}
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
 
	File Name:		IJsonActionOldData.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonActionOldData
	Purpose:		Defines the JSON structure for the ActionOldData object.

***************************************************************************************/
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the ActionOldData object.
	/// </summary>
	public interface IJsonActionOldData
	{
		/// <summary>
		/// Gets or sets an old description.
		/// </summary>
		[JsonDeserialize]
		string Desc { get; set; }
		/// <summary>
		/// Gets or sets an old list.
		/// </summary>
		[JsonDeserialize]
		IJsonList List { get; set; }
		/// <summary>
		/// Gets or sets an old position.
		/// </summary>
		[JsonDeserialize]
		double? Pos { get; set; }
		/// <summary>
		/// Gets or sets old text.
		/// </summary>
		[JsonDeserialize]
		string Text { get; set; }
		/// <summary>
		/// Gets or sets whether an item was closed.
		/// </summary>
		[JsonDeserialize]
		bool? Closed { get; set; }
	}
}
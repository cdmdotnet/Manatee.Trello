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
 
	File Name:		IJsonBoardBackground.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonBoardBackground
	Purpose:		Defines the JSON structure for the BoardBackground object.

***************************************************************************************/

using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the BoardBackground object.
	/// </summary>
	public interface IJsonBoardBackground : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Color { get; set; }
		/// <summary>
		/// Gets or sets the url for the image.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Image { get; set; }
		/// <summary>
		/// Gets or sets a collection of scaled images.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		List<IJsonImagePreview> ImageScaled { get; set; }
		/// <summary>
		/// Gets or sets whether the image should be tiled when displayed.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		bool? Tile { get; set; }
		//string Brightness // What are the possible values?  Have only seen "unknown" and "dark".
	}
}

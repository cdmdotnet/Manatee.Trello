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
 
	File Name:		IJsonSticker.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonSticker
	Purpose:		Defines the JSON structure for the Sticker object.

***************************************************************************************/

using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Sticker object.
	/// </summary>
	public interface IJsonSticker : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the position of the left edge of the sticker.
		/// </summary>
		[JsonSerialize]
		[JsonDeserialize]
		double? Left { get; set; }
		/// <summary>
		/// Gets or sets the name of the sticker.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Name { get; set; }
		///<summary>
		/// Gets or sets a collection of previews for the attachment.
		///</summary>
		[JsonDeserialize]
		List<IJsonImagePreview> Previews { get; set; }
		/// <summary>
		/// Gets or sets the rotation angle of the sticker in degrees.
		/// </summary>
		[JsonSerialize]
		[JsonDeserialize]
		int? Rotation { get; set; }
		/// <summary>
		/// Gets or sets the position of the top edge of the sticker.
		/// </summary>
		[JsonSerialize]
		[JsonDeserialize]
		double? Top { get; set; }
		/// <summary>
		/// Gets or sets the image's URL.
		/// </summary>
		[JsonDeserialize]
		string Url { get; set; }
		/// <summary>
		/// Gets or sets the sticker's z-index.
		/// </summary>
		[JsonSerialize]
		[JsonDeserialize]
		int? ZIndex { get; set; }
	}
}
﻿/***************************************************************************************

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
 
	File Name:		ImagePreview.cs
	Namespace:		Manatee.Trello
	Class Name:		ImagePreview
	Purpose:		Represents a preview for an attachment on a card.

***************************************************************************************/

using Manatee.Trello.Contracts;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a preview for an attachment on a card.
	/// </summary>
	public class ImagePreview : ICacheable
	{
		/// <summary>
		/// Gets the preview's height in pixels.
		/// </summary>
		public int? Height { get; private set; }
		/// <summary>
		/// Gets the preview's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets whether the attachment was scaled to generate the preview.
		/// </summary>
		public bool? IsScaled { get; set; }
		/// <summary>
		/// Gets the URI where the preview data is stored.
		/// </summary>
		public string Url { get; private set; }
		/// <summary>
		/// Gets the preview's width in pixels.
		/// </summary>
		public int? Width { get; private set; }

		internal ImagePreview(IJsonImagePreview json)
		{
			Id = json.Id;
			Height = json.Height;
			IsScaled = json.Scaled;
			Url = json.Url;
			Width = json.Width;
		}
	}
}
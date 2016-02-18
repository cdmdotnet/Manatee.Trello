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
 
	File Name:		ImagePreview.cs
	Namespace:		Manatee.Trello
	Class Name:		ImagePreview
	Purpose:		Represents a preview for an attachment on a card.

***************************************************************************************/

using System;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a preview for an attachment on a card.
	/// </summary>
	public class ImagePreview : ICacheable
	{
		private DateTime? _creation;

		/// <summary>
		/// Gets the creation date of the image preview.
		/// </summary>
		public DateTime CreationDate
		{
			get
			{
				if (_creation == null)
					_creation = Id.ExtractCreationDate();
				return _creation.Value;
			}
		}
		/// <summary>
		/// Gets the preview's height in pixels.
		/// </summary>
		public int? Height { get; }
		/// <summary>
		/// Gets the preview's ID.
		/// </summary>
		public string Id { get; }
		/// <summary>
		/// Gets whether the attachment was scaled to generate the preview.
		/// </summary>
		public bool? IsScaled { get; set; }
		/// <summary>
		/// Gets the URI where the preview data is stored.
		/// </summary>
		public string Url { get; }
		/// <summary>
		/// Gets the preview's width in pixels.
		/// </summary>
		public int? Width { get; }

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
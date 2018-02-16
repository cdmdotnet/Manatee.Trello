using System;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a preview for an image.
	/// </summary>
	/// <remarks>
	/// Used for card <see cref="Attachment"/>s and <see cref="BoardBackground"/>s.
	/// </remarks>
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
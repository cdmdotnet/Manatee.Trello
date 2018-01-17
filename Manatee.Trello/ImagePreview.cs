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
		public virtual DateTime CreationDate
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
		public virtual int? Height { get; }
		/// <summary>
		/// Gets the preview's ID.
		/// </summary>
		public virtual string Id { get; }
		/// <summary>
		/// Gets whether the attachment was scaled to generate the preview.
		/// </summary>
		public virtual bool? IsScaled { get; set; }
		/// <summary>
		/// Gets the URI where the preview data is stored.
		/// </summary>
		public virtual string Url { get; }
		/// <summary>
		/// Gets the preview's width in pixels.
		/// </summary>
		public virtual int? Width { get; }

		[Obsolete("This constructor is only for mocking purposes.")]
		public ImagePreview(ImagePreview doNotUse)
		{
		}
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
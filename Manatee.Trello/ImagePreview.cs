using System;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public interface IImagePreview : ICacheable
	{
		/// <summary>
		/// Gets the creation date of the image preview.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets the preview's height in pixels.
		/// </summary>
		int? Height { get; }

		/// <summary>
		/// Gets whether the attachment was scaled to generate the preview.
		/// </summary>
		bool? IsScaled { get; set; }

		/// <summary>
		/// Gets the URI where the preview data is stored.
		/// </summary>
		string Url { get; }

		/// <summary>
		/// Gets the preview's width in pixels.
		/// </summary>
		int? Width { get; }
	}

	/// <summary>
	/// Represents a preview for an attachment on a card.
	/// </summary>
	public class ImagePreview : IImagePreview
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
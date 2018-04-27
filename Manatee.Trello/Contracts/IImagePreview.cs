using System;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a preview for an image.
	/// </summary>
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
}
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the BoardBackground object.
	/// </summary>
	public interface IJsonBoardBackground : IJsonCacheable, IAcceptId
	{
		/// <summary>
		/// The bottom color of a gradient background.
		/// </summary>
		string BottomColor { get; set; }
		/// <summary>
		/// Gets the overall brightness of the background.
		/// </summary>
		BoardBackgroundBrightness? Brightness { get; set; }
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
		/// <summary>
		/// The top color of a gradient background.
		/// </summary>
		string TopColor { get; set; }
	}
}

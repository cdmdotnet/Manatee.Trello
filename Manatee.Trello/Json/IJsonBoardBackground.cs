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

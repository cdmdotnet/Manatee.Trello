using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Sticker object.
	/// </summary>
	public interface IJsonSticker : IJsonCacheable, IAcceptId
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
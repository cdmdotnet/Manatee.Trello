using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a sticker on a card.
	/// </summary>
	public interface ISticker : ICacheable
	{
		/// <summary>
		/// Gets or sets the position of the left edge.
		/// </summary>
		double? Left { get; set; }

		/// <summary>
		/// Gets the name of the sticker.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the collection of previews.
		/// </summary>
		IReadOnlyCollection<IImagePreview> Previews { get; }

		/// <summary>
		/// Gets or sets the rotation.
		/// </summary>
		/// <remarks>
		/// Rotation is clockwise and in degrees.
		/// </remarks>
		int? Rotation { get; set; }

		/// <summary>
		/// Gets or sets the position of the top edge.
		/// </summary>
		double? Top { get; set; }

		/// <summary>
		/// Gets the URL for the sticker's image.
		/// </summary>
		string ImageUrl { get; }

		/// <summary>
		/// Gets or sets the z-index.
		/// </summary>
		int? ZIndex { get; set; }

		/// <summary>
		/// Raised when data on the attachment is updated.
		/// </summary>
		event Action<ISticker, IEnumerable<string>> Updated;

		/// <summary>
		/// Deletes the card.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the card from Trello's server, however, this object will
		/// remain in memory and all properties will remain accessible.
		/// </remarks>
		Task Delete();

		/// <summary>
		/// Marks the card to be refreshed the next time data is accessed.
		/// </summary>
		Task Refresh();
	}
}
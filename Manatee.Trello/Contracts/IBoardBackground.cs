using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a background image for a board.
	/// </summary>
	public interface IBoardBackground : ICacheable
	{
		/// <summary>
		/// Gets the color of a stock solid-color background.
		/// </summary>
		WebColor Color { get; }

		/// <summary>
		/// Gets the image of a background.
		/// </summary>
		string Image { get; }

		/// <summary>
		/// Gets whether the image is tiled when displayed.
		/// </summary>
		bool? IsTiled { get; }

		/// <summary>
		/// Gets a collections of scaled background images.
		/// </summary>
		IReadOnlyCollection<IImagePreview> ScaledImages { get; }

		/// <summary>
		/// Gets the bottom color of a gradient background.
		/// </summary>
		WebColor BottomColor { get; }

		/// <summary>
		/// Gets the brightness of the background.
		/// </summary>
		BoardBackgroundBrightness? Brightness { get; }

		/// <summary>
		/// Gets the top color of a gradient background.
		/// </summary>
		WebColor TopColor { get; }

		/// <summary>
		/// Gets the type of background.
		/// </summary>
		BoardBackgroundType? Type { get; }

		/// <summary>
		/// Deletes a custom board background;
		/// </summary>
		/// <param name="ct"></param>
		/// <returns></returns>
		Task Delete(CancellationToken ct = default);
	}
}
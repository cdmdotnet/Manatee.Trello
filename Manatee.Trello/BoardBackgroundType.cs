using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the board background types.
	/// </summary>
	public enum BoardBackgroundType
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that the background is one of the Trello-provided defaults.
		/// </summary>
		[Display(Description = "default")]
		Default,
		/// <summary>
		/// Indicates that the background is one of the Trello-provided premium backgrounds.
		/// </summary>
		[Display(Description = "premium")]
		Premium,
		/// <summary>
		/// Indicates that the background is user-provided.
		/// </summary>
		[Display(Description = "custom")]
		Custom,
	}
}

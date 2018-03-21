using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	/// <summary>
	/// Specifies the brightness level of the board background.
	/// </summary>
	public enum BoardBackgroundBrightness
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that the board background is light.
		/// </summary>
		[Display(Description = "light")]
		Light,
		/// <summary>
		/// Indicates that the board background is dark.
		/// </summary>
		[Display(Description = "dark")]
		Dark
	}
}

using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates label colors for a board.
	/// </summary>
	public enum LabelColor
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,

		[Display(Description = "none")]
		None,

		/// <summary>
		/// Indicates a green label.
		/// </summary>
		[Display(Description = "green")]
		Green,

		/// <summary>
		/// Indicates a yellow label.
		/// </summary>
		[Display(Description = "yellow")]
		Yellow,

		/// <summary>
		/// Indicates an orange label.
		/// </summary>
		[Display(Description = "orange")]
		Orange,

		/// <summary>
		/// Indicates a red label.
		/// </summary>
		[Display(Description = "red")]
		Red,

		/// <summary>
		/// Indicates a purple label.
		/// </summary>
		[Display(Description = "purple")]
		Purple,

		/// <summary>
		/// Indicates a blue label.
		/// </summary>
		[Display(Description = "blue")]
		Blue,

		/// <summary>
		/// Indicates a blue label.
		/// </summary>
		[Display(Description = "pink")]
		Pink,

		/// <summary>
		/// Indicates a blue label.
		/// </summary>
		[Display(Description = "sky")]
		Sky,

		/// <summary>
		/// Indicates a blue label.
		/// </summary>
		[Display(Description = "lime")]
		Lime,

		/// <summary>
		/// Indicates a blue label.
		/// </summary>
		[Display(Description = "black")]
		Black,
	}
}

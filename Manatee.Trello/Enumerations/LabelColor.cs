using System.ComponentModel;

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
		/// <summary>
		/// Indicates a green label.
		/// </summary>
		[Description("green")]
		Green,
		/// <summary>
		/// Indicates a yellow label.
		/// </summary>
		[Description("yellow")]
		Yellow,
		/// <summary>
		/// Indicates an orange label.
		/// </summary>
		[Description("orange")]
		Orange,
		/// <summary>
		/// Indicates a red label.
		/// </summary>
		[Description("red")]
		Red,
		/// <summary>
		/// Indicates a purple label.
		/// </summary>
		[Description("purple")]
		Purple,
		/// <summary>
		/// Indicates a blue label.
		/// </summary>
		[Description("blue")]
		Blue,
		/// <summary>
		/// Indicates a blue label.
		/// </summary>
		[Description("pink")]
		Pink,
		/// <summary>
		/// Indicates a blue label.
		/// </summary>
		[Description("sky")]
		Sky,
		/// <summary>
		/// Indicates a blue label.
		/// </summary>
		[Description("lime")]
		Lime,
		/// <summary>
		/// Indicates a blue label.
		/// </summary>
		[Description("black")]
		Black,
	}
}

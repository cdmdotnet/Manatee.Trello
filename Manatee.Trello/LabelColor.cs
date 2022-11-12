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

		/// <summary>
		/// Indicates a colorless label.
		/// </summary>
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
        /// Indicates a pink label.
        /// </summary>
        [Display(Description = "pink")]
        Pink,

        /// <summary>
        /// Indicates a sky label.
        /// </summary>
        [Display(Description = "sky")]
        Sky,

        /// <summary>
        /// Indicates a lime label.
        /// </summary>
        [Display(Description = "lime")]
        Lime,

        /// <summary>
        /// Indicates a black label.
        /// </summary>
        [Display(Description = "black")]
        Black,

        /// <summary>
		/// Indicates a green dark label.
		/// </summary>
		[Display(Description = "green_dark")]
        GreenDark,

        /// <summary>
        /// Indicates a yellow dark label.
        /// </summary>
        [Display(Description = "yellow_dark")]
        YellowDark,

        /// <summary>
        /// Indicates a orange dark label.
        /// </summary>
        [Display(Description = "orange_dark")]
        OrangeDark,

        /// <summary>
        /// Indicates a red dark label.
        /// </summary>
        [Display(Description = "red_dark")]
        RedDark,

        /// <summary>
        /// Indicates a purple dark label.
        /// </summary>
        [Display(Description = "purple_dark")]
        PurpleDark,

        /// <summary>
        /// Indicates a blue dark label.
        /// </summary>
        [Display(Description = "blue_dark")]
        BlueDark,

        /// <summary>
        /// Indicates a pink dark label.
        /// </summary>
        [Display(Description = "pink_dark")]
        PinkDark,

        /// <summary>
        /// Indicates a sky dark label.
        /// </summary>
        [Display(Description = "sky_dark")]
        SkyDark,

        /// <summary>
        /// Indicates a lime dark label.
        /// </summary>
        [Display(Description = "lime_dark")]
        LimeDark,

        /// <summary>
        /// Indicates a black dark label.
        /// </summary>
        [Display(Description = "black_dark")]
        BlackDark,

        /// <summary>
        /// Indicates a green light label.
        /// </summary>
        [Display(Description = "green_light")]
        GreenLight,

        /// <summary>
        /// Indicates a yellow light label.
        /// </summary>
        [Display(Description = "yellow_light")]
        YellowLight,

        /// <summary>
        /// Indicates a orange light label.
        /// </summary>
        [Display(Description = "orange_light")]
        OrangeLight,

        /// <summary>
        /// Indicates a red light label.
        /// </summary>
        [Display(Description = "red_light")]
        RedLight,

        /// <summary>
        /// Indicates a purple light label.
        /// </summary>
        [Display(Description = "purple_light")]
        PurpleLight,

        /// <summary>
        /// Indicates a blue light label.
        /// </summary>
        [Display(Description = "blue_light")]
        BlueLight,

        /// <summary>
        /// Indicates a pink light label.
        /// </summary>
        [Display(Description = "pink_light")]
        PinkLight,

        /// <summary>
        /// Indicates a sky light label.
        /// </summary>
        [Display(Description = "sky_light")]
        SkyLight,

        /// <summary>
        /// Indicates a lime light label.
        /// </summary>
        [Display(Description = "lime_light")]
        LimeLight,

        /// <summary>
        /// Indicates a black light label.
        /// </summary>
        [Display(Description = "black_light")]
        BlackLight
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the options for additional data to keep when copying cards.
	/// </summary>
	[Flags]
	public enum CardCopyKeepFromSourceOptions
	{
		/// <summary>
		/// Indicates that no additional data should be copied.
		/// </summary>
		[Display(Description = "none")]
		None = 0,
		/// <summary>
		/// Indicates that attachments should be copied.
		/// </summary>
		[Display(Description = "attachments")]
		Attachments = 1 << 0,
		/// <summary>
		/// Indicates that checklists should be copied.
		/// </summary>
		[Display(Description = "checklists")]
		Checklists = 1 << 1,
		/// <summary>
		/// Indicates that comments should be copied.
		/// </summary>
		[Display(Description = "comments")]
		Comments = 1 << 2,
		/// <summary>
		/// Indicates that due should be copied.
		/// </summary>
		[Display(Description = "due")]
		Due = 1 << 3,
		/// <summary>
		/// Indicates that labels should be copied.
		/// </summary>
		[Display(Description = "labels")]
		Labels = 1 << 4,
		/// <summary>
		/// Indicates that members should be copied.
		/// </summary>
		[Display(Description = "members")]
		Members = 1 << 5,
		/// <summary>
		/// Indicates that stickers should be copied.
		/// </summary>
		[Display(Description = "stickers")]
		Stickers = 1 << 6,
		/// <summary>
		/// Indicates that all additional data should be copied.
		/// </summary>
		[Display(Description = "all")]
		All = Attachments | Checklists | Comments | Due | Labels | Members | Stickers
	}
}
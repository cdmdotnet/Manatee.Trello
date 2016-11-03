using System;
using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the model types for which one can search.
	/// </summary>
	[Flags]
	public enum SearchModelType
	{
		/// <summary>
		/// Indicates the search should return actions.
		/// </summary>
		[Description("actions")]
		Actions = 0x01,
		/// <summary>
		/// Indicates the search should return boards.
		/// </summary>
		[Description("boards")]
		Boards = 0x02,
		/// <summary>
		/// Indicates the search should return cards.
		/// </summary>
		[Description("cards")]
		Cards = 0x04,
		/// <summary>
		/// Indicates the search should return members.
		/// </summary>
		[Description("members")]
		Members = 0x08,
		/// <summary>
		/// Indicates the search should return organizations.
		/// </summary>
		[Description("orgainzations")]
		Organizations = 0x10,
		/// <summary>
		/// Indicates the search should return all model types.
		/// </summary>
		[Description("all")]
		All = 0x1F
	}
}
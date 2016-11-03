using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the filter options for card collections.
	/// </summary>
	public enum CardFilter
	{
		/// <summary>
		/// Filters to only cards that are visible (not archived or in lists which are archived).
		/// </summary>
		[Description("visible")]
		Visible,
		/// <summary>
		/// Filters to only unarchived cards.
		/// </summary>
		[Description("open")]
		Open,
		/// <summary>
		/// Filters to only archived cards.
		/// </summary>
		[Description("closed")]
		Closed,
		/// <summary>
		/// Indicates that all cards should be returned.
		/// </summary>
		[Description("all")]
		All
	}
}

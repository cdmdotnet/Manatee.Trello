using System.ComponentModel.DataAnnotations;

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
		[Display(Description="visible")]
		Visible,
		/// <summary>
		/// Filters to only unarchived cards.
		/// </summary>
		[Display(Description="open")]
		Open,
		/// <summary>
		/// Filters to only archived cards.
		/// </summary>
		[Display(Description="closed")]
		Closed,
		/// <summary>
		/// Indicates that all cards should be returned.
		/// </summary>
		[Display(Description="all")]
		All
	}
}

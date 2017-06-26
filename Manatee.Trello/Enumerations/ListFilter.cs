using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the filter options for list collections.
	/// </summary>
	public enum ListFilter
	{
		/// <summary>
		/// Filters to only unarchived lists.
		/// </summary>
		[Display(Description="open")]
		Open,
		/// <summary>
		/// Filters to only archived lists.
		/// </summary>
		[Display(Description="closed")]
		Closed,
		/// <summary>
		/// Indicates that all lists should be returned.
		/// </summary>
		[Display(Description="all")]
		All,
	}
}

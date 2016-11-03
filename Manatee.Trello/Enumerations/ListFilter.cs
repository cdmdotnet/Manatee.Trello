using System.ComponentModel;

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
		[Description("open")]
		Open,
		/// <summary>
		/// Filters to only archived lists.
		/// </summary>
		[Description("closed")]
		Closed,
		/// <summary>
		/// Indicates that all lists should be returned.
		/// </summary>
		[Description("all")]
		All,
	}
}

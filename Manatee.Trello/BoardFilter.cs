using System;
using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the filter options for board collections.  Can be combined with the bitwise-OR operator.
	/// </summary>
	[Flags]
	public enum BoardFilter
	{
		/// <summary>
		/// Filters to boards that only members can access.
		/// </summary>
		[Display(Description="members")]
		Members = 1 << 0,
		/// <summary>
		/// Filters to boards that only organization members can access.
		/// </summary>
		[Display(Description="organization")]
		Organization = 1 << 1,
		/// <summary>
		/// Filters to boards that are publicly accessible.
		/// </summary>
		[Display(Description="public")]
		Public = 1 << 2,
		/// <summary>
		/// Filters to open boards.
		/// </summary>
		[Display(Description="open")]
		Open = 1 << 3,
		/// <summary>
		/// Filters to closed boards.
		/// </summary>
		[Display(Description="closed")]
		Closed = 1 << 4,
		/// <summary>
		/// Filters to pinned boards.
		/// </summary>
		[Display(Description="pinned")]
		Pinned = 1 << 5,
		/// <summary>
		/// Filters to unpinned boards.
		/// </summary>
		[Display(Description="unpinned")]
		Unpinned = 1 << 6,
		/// <summary>
		/// Filters to starred boards.
		/// </summary>
		[Display(Description="starred")]
		Starred = 1 << 7,
		/// <summary>
		/// Indicates that all boards should be returned.
		/// </summary>
		[Display(Description="all")]
		All = 1 << 8 - 1,
	}
}

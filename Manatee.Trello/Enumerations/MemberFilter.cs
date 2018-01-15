using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the filter options for member collections.
	/// </summary>
	public enum MemberFilter
	{
		/// <summary>
		/// Filters to only normal members.
		/// </summary>
		[Display(Description="normal")]
		Normal,
		/// <summary>
		/// Filters to only admins.
		/// </summary>
		[Display(Description="admins")]
		Admins,
		/// <summary>
		/// Filters to only owners.
		/// </summary>
		/// <remarks>
		/// Per @doug at Trello regarding <see cref="Admins"/> == <see cref="Owners"/>: "Turns out owners was once used by the iOS app and we only have it there for backwards compatibility. They are the same."
		/// </remarks>
		[Display(Description="owners")]
		Owners,
	}
}

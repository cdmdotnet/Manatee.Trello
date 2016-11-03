using System.ComponentModel;

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
		[Description("normal")]
		Normal,
		/// <summary>
		/// Filters to only admins.
		/// </summary>
		[Description("admins")]
		Admins,
		/// <summary>
		/// Filters to only owners.
		/// </summary>
		/// <remarks>
		/// Per @doug at Trello regarding <see cref="Admins"/> == <see cref="Owners"/>: "Turns out owners was once used by the iOS app and we only have it there for backwards compatibility. They are the same."
		/// </remarks>
		[Description("owners")]
		Owners,
	}
}

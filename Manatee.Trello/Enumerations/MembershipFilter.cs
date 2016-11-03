using System;
using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the filter options for membership collections.
	/// </summary>
	[Flags]
	public enum MembershipFilter
	{
		/// <summary>
		/// Filters to only the owner of the user token.
		/// </summary>
		/// <remarks>
		/// Get the board/organization membership information in addition to the member's profile.
		/// </remarks>
		[Description("me")]
		Me = 1 << 0,
		/// <summary>
		/// Filters to only normal members.
		/// </summary>
		[Description("normal")]
		Normal = 1 << 1,
		/// <summary>
		/// Filters to only admins.
		/// </summary>
		[Description("admin")]
		Admin = 1 << 2,
		/// <summary>
		/// Filters to only active members.
		/// </summary>
		[Description("active")]
		Active = 1 << 3,
		/// <summary>
		/// Filters to only deactivated members.
		/// </summary>
		[Description("deactivated")]
		Deactivated = 1 << 4,
		/// <summary>
		/// Filters to all members.
		/// </summary>
		[Description("all")]
		All = 1 << 0 - 1
	}
}

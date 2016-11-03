using System.ComponentModel;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates known values for a member's activity status.
	/// </summary>
	public enum MemberStatus
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates the member is not connected to the website.
		/// </summary>
		[Description("disconnected")]
		Disconnected,
		/// <summary>
		/// Indicates the member is connected to the website but inactive.
		/// </summary>
		[Description("idle")]
		Idle,
		/// <summary>
		/// Indicates the member is actively using the website.
		/// </summary>
		[Description("active")]
		Active
	}
}
using System.ComponentModel.DataAnnotations;

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
		[Display(Description="disconnected")]
		Disconnected,
		/// <summary>
		/// Indicates the member is connected to the website but inactive.
		/// </summary>
		[Display(Description="idle")]
		Idle,
		/// <summary>
		/// Indicates the member is actively using the website.
		/// </summary>
		[Display(Description="active")]
		Active
	}
}
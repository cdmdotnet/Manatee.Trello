using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	///<summary>
	/// Enumerates known board invitation permission levels.
	///</summary>
	public enum BoardInvitationPermission
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that any member of the board may extend an invitation to join the board.
		/// </summary>
		[Display(Description="members")]
		Members,
		/// <summary>
		/// Indicates that only admins of the board may extend an invitation to joni the board.
		/// </summary>
		[Display(Description="admins")]
		Admins
	}
}
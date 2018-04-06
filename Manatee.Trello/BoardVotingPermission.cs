using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates known voting permission levels for a board
	/// </summary>
	public enum BoardVotingPermission
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that only members of the board may vote on cards.
		/// </summary>
		[Display(Description="members")]
		Members,
		/// <summary>
		/// Indicates that only members of the organization to which the board belongs may vote on cards.
		/// </summary>
		[Display(Description="org")]
		Org,
		/// <summary>
		/// Indicates that any Trello member may vote on cards.
		/// </summary>
		[Display(Description="public")]
		Public,
		/// <summary>
		/// Indicates that no members may vote on cards.
		/// </summary>
		[Display(Description="disabled")]
		Disabled
	}
}
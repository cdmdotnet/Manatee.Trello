using System.ComponentModel;

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
		[Description("members")]
		Members,
		/// <summary>
		/// Indicates that only members of the organization to which the board belongs may vote on cards.
		/// </summary>
		[Description("org")]
		Org,
		/// <summary>
		/// Indicates that any Trello member may vote on cards.
		/// </summary>
		[Description("public")]
		Public,
		/// <summary>
		/// Indicates that no members may vote on cards.
		/// </summary>
		[Description("disabled")]
		Disabled
	}
}
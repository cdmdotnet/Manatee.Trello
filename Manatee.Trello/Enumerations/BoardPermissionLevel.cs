using System.ComponentModel;

namespace Manatee.Trello
{
	///<summary>
	/// Enumerates known values for board permission levels
	///</summary>
	public enum BoardPermissionLevel
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that the board can only be viewed by its members.
		/// </summary>
		[Description("private")]
		Private,
		/// <summary>
		/// Indicates that the board may be viewed by any member of the organization to which the board belongs.
		/// </summary>
		[Description("org")]
		Org,
		/// <summary>
		/// Indicates that anyone (even non-Trello users) may view the board.
		/// </summary>
		[Description("public")]
		Public
	}
}
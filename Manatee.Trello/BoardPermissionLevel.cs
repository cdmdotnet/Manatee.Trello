using System.ComponentModel.DataAnnotations;

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
		[Display(Description="private")]
		Private,
		/// <summary>
		/// Indicates that the board may be viewed by any member of the organization to which the board belongs.
		/// </summary>
		[Display(Description="org")]
		Org,
		/// <summary>
		/// Indicates that anyone (even non-Trello users) may view the board.
		/// </summary>
		[Display(Description="public")]
		Public
	}
}
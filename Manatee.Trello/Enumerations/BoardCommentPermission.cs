﻿using System.ComponentModel;

namespace Manatee.Trello
{
	///<summary>
	/// Enumerates known board commenting permission levels.
	///</summary>
	public enum BoardCommentPermission
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that only members of the board may comment on cards.
		/// </summary>
		[Description("members")]
		Members,
		/// <summary>
		/// Indicates that observers may make comments on cards.
		/// </summary>
		[Description("observers")]
		Observers,
		/// <summary>
		/// Indicates that only members of the organization to which the board belongs may comment on cards.
		/// </summary>
		[Description("org")]
		Org,
		/// <summary>
		/// Indicates that any Trello member may comment on cards.
		/// </summary>
		[Description("public")]
		Public,
		/// <summary>
		/// Indicates that no members may comment on cards.
		/// </summary>
		[Description("disabled")]
		Disabled
	}
}
namespace Manatee.Trello
{
	/// <summary>
	/// Represents the preferences for a board.
	/// </summary>
	public interface IBoardPreferences
	{
		/// <summary>
		/// Gets or sets the general visibility of the board.
		/// </summary>
		BoardPermissionLevel? PermissionLevel { get; set; }

		/// <summary>
		/// Gets or sets whether voting is enabled and which members are allowed to vote. 
		/// </summary>
		BoardVotingPermission? Voting { get; set; }

		/// <summary>
		/// Gets or sets whether commenting is enabled and which members are allowed to add comments.
		/// </summary>
		BoardCommentPermission? Commenting { get; set; }

		/// <summary>
		/// Gets or sets which members may invite others to the board.
		/// </summary>
		BoardInvitationPermission? Invitations { get; set; }

		/// <summary>
		/// Gets or sets whether any Trello member may join the board themselves or if an invitation must be sent.
		/// </summary>
		bool? AllowSelfJoin { get; set; }

		/// <summary>
		/// Gets or sets whether card covers are shown.
		/// </summary>
		bool? ShowCardCovers { get; set; }

		/// <summary>
		/// Gets or sets whether the calendar feed is enabled.
		/// </summary>
		bool? IsCalendarFeedEnabled { get; set; }

		/// <summary>
		/// Gets or sets the card aging style for the Card Aging power up.
		/// </summary>
		CardAgingStyle? CardAgingStyle { get; set; }

		/// <summary>
		/// Gets or sets the background of the board.
		/// </summary>
		IBoardBackground Background { get; set; }
	}
}
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the BoardPreferences object.
	/// </summary>
	public interface IJsonBoardPreferences
	{
		/// <summary>
		/// Gets or sets who may view the board.
		/// </summary>
		[JsonDeserialize]
		BoardPermissionLevel? PermissionLevel { get; set; }
		/// <summary>
		/// Gets or sets who may vote on cards.
		/// </summary>
		[JsonDeserialize]
		BoardVotingPermission? Voting { get; set; }
		/// <summary>
		/// Gets or sets who may comment on cards.
		/// </summary>
		[JsonDeserialize]
		BoardCommentPermission? Comments { get; set; }
		/// <summary>
		/// Gets or sets who may extend invitations to join the board.
		/// </summary>
		[JsonDeserialize]
		BoardInvitationPermission? Invitations { get; set; }
		/// <summary>
		/// Gets or sets whether a Trello member may join the board without an invitation.
		/// </summary>
		[JsonDeserialize]
		bool? SelfJoin { get; set; }
		/// <summary>
		/// Gets or sets whether card covers are shown on the board.
		/// </summary>
		[JsonDeserialize]
		bool? CardCovers { get; set; }
		/// <summary>
		/// Gets or sets whether the calendar feed is enabled.
		/// </summary>
		[JsonDeserialize]
		bool? CalendarFeed { get; set; }
		/// <summary>
		/// Gets or sets the style of card aging is used, if the power up is enabled.
		/// </summary>
		[JsonDeserialize]
		CardAgingStyle? CardAging { get; set; }
		/// <summary>
		/// Gets or sets the background image of the board.
		/// </summary>
		[JsonDeserialize]
		IJsonBoardBackground Background { get; set; }
	}
}
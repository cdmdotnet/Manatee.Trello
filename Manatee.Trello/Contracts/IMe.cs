namespace Manatee.Trello
{
	/// <summary>
	/// Represents the current member.
	/// </summary>
	public interface IMe : IMember
	{
		/// <summary>
		/// Gets or sets the source type for the member's avatar.
		/// </summary>
		new AvatarSource? AvatarSource { get; set; }

		/// <summary>
		/// Gets or sets the member's bio.
		/// </summary>
		new string Bio { get; set; }

		/// <summary>
		/// Gets the collection of boards owned by the member.
		/// </summary>
		new IBoardCollection Boards { get; }

		/// <summary>
		/// Gets or sets the member's email.
		/// </summary>
		string Email { get; set; }

		/// <summary>
		/// Gets or sets the member's full name.
		/// </summary>
		new string FullName { get; set; }

		/// <summary>
		/// Gets or sets the member's initials.
		/// </summary>
		new string Initials { get; set; }

		/// <summary>
		/// Gets the collection of notificaitons for the member.
		/// </summary>
		IReadOnlyNotificationCollection Notifications { get; }

		/// <summary>
		/// Gets the collection of organizations to which the member belongs.
		/// </summary>
		new IOrganizationCollection Organizations { get; }

		/// <summary>
		/// Gets the set of preferences for the member.
		/// </summary>
		IMemberPreferences Preferences { get; }

		/// <summary>
		/// Gets the collection of the member's board stars.
		/// </summary>
		new IStarredBoardCollection StarredBoards { get; }

		/// <summary>
		/// Gets or sets the member's username.
		/// </summary>
		new string UserName { get; set; }
	}
}
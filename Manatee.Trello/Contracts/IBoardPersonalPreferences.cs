namespace Manatee.Trello
{
	/// <summary>
	/// Represents the user-specific preferences for a board.
	/// </summary>
	public interface IBoardPersonalPreferences
	{
		/// <summary>
		/// Gets or sets the <see cref="List"/> which will be used to post new cards submitted by email.
		/// </summary>
		IList EmailList { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="Position"/> within a <see cref="List"/> which will be used to post new cards submitted by email.
		/// </summary>
		Position EmailPosition { get; set; }

		/// <summary>
		/// Gets or sets whether to show the list guide.
		/// </summary>
		/// <remarks>
		/// It appears that this may be deprecated by Trello.
		/// </remarks>
		bool? ShowListGuide { get; set; }

		/// <summary>
		/// Gets or sets whether to show the side bar.
		/// </summary>
		bool? ShowSidebar { get; set; }

		/// <summary>
		/// Gets or sets whether to show the activity list in the side bar.
		/// </summary>
		/// <remarks>
		/// It appears that this may be deprecated by Trello.
		/// </remarks>
		bool? ShowSidebarActivity { get; set; }

		/// <summary>
		/// Gets or sets whether to show the board action list in the side bar.
		/// </summary>
		/// <remarks>
		/// It appears that this may be deprecated by Trello.
		/// </remarks>
		bool? ShowSidebarBoardActions { get; set; }

		/// <summary>
		/// Gets or sets whether to show the board members in the side bar.
		/// </summary>
		/// <remarks>
		/// It appears that this may be deprecated by Trello.
		/// </remarks>
		bool? ShowSidebarMembers { get; set; }
	}
}
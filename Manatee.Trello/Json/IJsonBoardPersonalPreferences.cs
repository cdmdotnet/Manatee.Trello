namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the BoardPersonalPreferences object.
	/// </summary>
	public interface IJsonBoardPersonalPreferences
	{
		///<summary>
		/// Gets or sets whether the side bar (right side of the screen) is shown
		///</summary>
		[JsonDeserialize]
		bool? ShowSidebar { get; set; }
		///<summary>
		/// Gets or sets whether the members section of the list of the side bar is shown.
		///</summary>
		[JsonDeserialize]
		bool? ShowSidebarMembers { get; set; }
		/// <summary>
		/// Gets or sets whether the board actions (Add List/Add Member/Filter Cards) section of the side bar is shown.
		/// </summary>
		[JsonDeserialize]
		bool? ShowSidebarBoardActions { get; set; }
		/// <summary>
		/// Gets or sets whether the activity section of the side bar is shown.
		/// </summary>
		[JsonDeserialize]
		bool? ShowSidebarActivity { get; set; }
		///<summary>
		/// Gets or sets whether the list guide (left side of the screen) is expanded.
		///</summary>
		[JsonDeserialize]
		bool? ShowListGuide { get; set; }
		///<summary>
		/// Gets or sets the position of new cards when they are added via email.
		///</summary>
		[JsonDeserialize]
		IJsonPosition EmailPosition { get; set; }
		///<summary>
		/// Gets or sets the list for new cards when they are added via email.
		///</summary>
		[JsonDeserialize]
		IJsonList EmailList { get; set; }
	}
}
using System;
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Board object.
	/// </summary>
	public interface IJsonBoard : IJsonCacheable, IAcceptId
	{
		///<summary>
		/// Gets or sets the board's name.
		///</summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Name { get; set; }
		///<summary>
		/// Gets or sets the board's description.
		///</summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Desc { get; set; }
		///<summary>
		/// Gets or sets whether this board is closed.
		///</summary>
		[JsonDeserialize]
		[JsonSerialize]
		bool? Closed { get; set; }
		/// <summary>
		/// Gets or sets the ID of the organization, if any, to which this board belongs.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonOrganization Organization { get; set; }
		/// <summary>
		/// Gets or sets whether the board is pinned.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		bool? Pinned { get; set; }
		/// <summary>
		/// Gets or sets a set of preferences for the board.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		[JsonSpecialSerialization]
		IJsonBoardPreferences Prefs { get; set; }
		///<summary>
		/// Gets or sets the URL for this board.
		///</summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Url { get; set; }
		///<summary>
		/// Gets or sets whether the user is subscribed to this board.
		///</summary>
		[JsonDeserialize]
		[JsonSerialize]
		bool? Subscribed { get; set; }
		/// <summary>
		/// Gets or sets a board to be used as a template.
		/// </summary>
		[JsonSerialize]
		IJsonBoard BoardSource { get; set; }
		/// <summary>
		/// Gets or sets whether the board is starred on the member's dashboard.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		bool? Starred { get; set; }
		/// <summary>
		/// Gets or sets the date the board last had activity.
		/// </summary>
		[JsonDeserialize]
		DateTime? DateLastActivity { get; set; }
		/// <summary>
		/// Gets or sets the date the board was last viewed.
		/// </summary>
		[JsonDeserialize]
		DateTime? DateLastView { get; set; }
		/// <summary>
		/// Gets or sets the short link (ID).
		/// </summary>
		[JsonDeserialize]
		string ShortLink { get; set; }
		/// <summary>
		/// Gets or sets the short URL.
		/// </summary>
		[JsonDeserialize]
		string ShortUrl { get; set; }
		/// <summary>
		/// Gets or sets a list of actions.
		/// </summary>
		[JsonDeserialize]
		List<IJsonAction> Actions { get; set; }
		/// <summary>
		/// Gets or sets a list of cards.
		/// </summary>
		[JsonDeserialize]
		List<IJsonCard> Cards { get; set; }
		/// <summary>
		/// Gets or sets a list of custom field definitions.
		/// </summary>
		[JsonDeserialize]
		List<IJsonCustomFieldDefinition> CustomFields { get; set; }
		/// <summary>
		/// Gets or sets a list of labels.
		/// </summary>
		[JsonDeserialize]
		List<IJsonLabel> Labels { get; set; }
		/// <summary>
		/// Gets or sets a list of lists.
		/// </summary>
		[JsonDeserialize]
		List<IJsonList> Lists { get; set; }
		/// <summary>
		/// Gets or sets a list of members.
		/// </summary>
		[JsonDeserialize]
		List<IJsonMember> Members { get; set; }
		/// <summary>
		/// Gets or sets a list of memberships.
		/// </summary>
		[JsonDeserialize]
		List<IJsonBoardMembership> Memberships { get; set; }
		/// <summary>
		/// Gets or sets a list of power-ups.
		/// </summary>
		[JsonDeserialize]
		List<IJsonPowerUp> PowerUps { get; set; }
		/// <summary>
		/// Gets or sets a list of power-up data.
		/// </summary>
		[JsonDeserialize]
		List<IJsonPowerUpData> PowerUpData { get; set; }
	}
}
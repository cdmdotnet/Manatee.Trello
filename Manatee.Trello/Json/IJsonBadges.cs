using System;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Badges object.
	/// </summary>
	public interface IJsonBadges
	{
		/// <summary>
		/// Gets or sets the number of votes.
		/// </summary>
		[JsonDeserialize]
		int? Votes { get; set; }
		/// <summary>
		/// Gets or sets whether the member has voted for this card.
		/// </summary>
		[JsonDeserialize]
		bool? ViewingMemberVoted { get; set; }
		/// <summary>
		/// Gets or sets whether the member is subscribed to the card.
		/// </summary>
		[JsonDeserialize]
		bool? Subscribed { get; set; }
		/// <summary>
		/// Gets or sets the FogBugz ID.
		/// </summary>
		[JsonDeserialize]
		string Fogbugz { get; set; }
		/// <summary>
		/// Gets or sets the due date, if one exists.
		/// </summary>
		[JsonDeserialize]
		DateTime? Due { get; set; }
		/// <summary>
		/// Gets or sets whether the card has a description.
		/// </summary>
		[JsonDeserialize]
		bool? Description { get; set; }
		/// <summary>
		/// Gets or sets the number of comments.
		/// </summary>
		[JsonDeserialize]
		int? Comments { get; set; }
		/// <summary>
		/// Gets or sets the number of check items which have been checked.
		/// </summary>
		[JsonDeserialize]
		int? CheckItemsChecked { get; set; }
		/// <summary>
		/// Gets or sets the number of check items.
		/// </summary>
		[JsonDeserialize]
		int? CheckItems { get; set; }
		///<summary>
		/// Gets or sets the number of attachments.
		///</summary>
		[JsonDeserialize]
		int? Attachments { get; set; }
	}
}
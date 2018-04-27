using System;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a collection of badges which summarize the contents of a card.
	/// </summary>
	public interface IBadges
	{
		/// <summary>
		/// Gets the number of attachments on this card.
		/// </summary>
		int? Attachments { get; }

		/// <summary>
		/// Gets the number of check items on this card.
		/// </summary>
		int? CheckItems { get; }

		/// <summary>
		/// Gets the number of check items on this card which are checked.
		/// </summary>
		int? CheckItemsChecked { get; }

		/// <summary>
		/// Gets the number of comments on this card.
		/// </summary>
		int? Comments { get; }

		/// <summary>
		/// Gets the due date for this card.
		/// </summary>
		DateTime? DueDate { get; }

		/// <summary>
		/// Gets some FogBugz information.
		/// </summary>
		string FogBugz { get; }

		/// <summary>
		/// Gets whether this card has a description.
		/// </summary>
		bool? HasDescription { get; }

		/// <summary>
		/// Gets whether the current member has voted for this card.
		/// </summary>
		bool? HasVoted { get; }

		/// <summary>
		/// Gets wheterh this card has been marked complete.
		/// </summary>
		bool? IsComplete { get; }

		/// <summary>
		/// Gets whether the current member is subscribed to this card.
		/// </summary>
		bool? IsSubscribed { get; }

		/// <summary>
		/// Gets the number of votes for this card.
		/// </summary>
		int? Votes { get; }
	}
}
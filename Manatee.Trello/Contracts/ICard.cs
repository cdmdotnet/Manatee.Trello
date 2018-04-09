using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a card.
	/// </summary>
	public interface ICard : ICanWebhook, IQueryable
	{
		/// <summary>
		/// Gets the collection of actions performed on this card.
		/// </summary>
		/// <remarks>By default imposed by Trello, this contains actions of types <see cref="ActionType.CommentCard"/> and <see cref="ActionType.UpdateCardIdList"/>.</remarks>
		IReadOnlyCollection<IAction> Actions { get; }

		/// <summary>
		/// Gets the collection of attachments contained in the card.
		/// </summary>
		IAttachmentCollection Attachments { get; }

		/// <summary>
		/// Gets the badges summarizing the content of the card.
		/// </summary>
		IBadges Badges { get; }

		/// <summary>
		/// Gets the board to which the card belongs.
		/// </summary>
		IBoard Board { get; }

		/// <summary>
		/// Gets the collection of checklists contained in the card.
		/// </summary>
		ICheckListCollection CheckLists { get; }

		/// <summary>
		/// Gets the collection of comments made on the card.
		/// </summary>
		ICommentCollection Comments { get; }

		/// <summary>
		/// Gets the creation date of the card.
		/// </summary>
		DateTime CreationDate { get; }

		IEnumerable<CustomField> CustomFields { get; }

		/// <summary>
		/// Gets or sets the card's description.
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Gets or sets the card's due date.
		/// </summary>
		DateTime? DueDate { get; set; }

		/// <summary>
		/// Gets or sets whether the card is archived.
		/// </summary>
		bool? IsArchived { get; set; }

		/// <summary>
		/// Gets or sets whether the card is complete.  Associated with <see cref="DueDate"/>.
		/// </summary>
		bool? IsComplete { get; set; }

		/// <summary>
		/// Gets or sets whether the current member is subscribed to the card.
		/// </summary>
		bool? IsSubscribed { get; set; }

		/// <summary>
		/// Gets the collection of labels on the card.
		/// </summary>
		ICardLabelCollection Labels { get; }

		/// <summary>
		/// Gets the most recent date of activity on the card.
		/// </summary>
		DateTime? LastActivity { get; }

		/// <summary>
		/// Gets or sets the list to the card belongs.
		/// </summary>
		IList List { get; set; }

		/// <summary>
		/// Gets the collection of members who are assigned to the card.
		/// </summary>
		IMemberCollection Members { get; }

		/// <summary>
		/// Gets or sets the card's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the card's position.
		/// </summary>
		Position Position { get; set; }

		/// <summary>
		/// Gets specific data regarding power-ups.
		/// </summary>
		IReadOnlyCollection<IPowerUpData> PowerUpData { get; }

		/// <summary>
		/// Gets the card's short ID.
		/// </summary>
		int? ShortId { get; }

		/// <summary>
		/// Gets the card's short URL.
		/// </summary>
		/// <remarks>
		/// Because this value does not change, it can be used as a permalink.
		/// </remarks>
		string ShortUrl { get; }

		/// <summary>
		/// Gets the collection of stickers which appear on the card.
		/// </summary>
		ICardStickerCollection Stickers { get; }

		/// <summary>
		/// Gets the card's full URL.
		/// </summary>
		/// <remarks>
		/// Trello will likely change this value as the name changes.  You can use <see cref="ShortUrl"/> for permalinks.
		/// </remarks>
		string Url { get; }

		/// <summary>
		/// Gets all members who have voted for this card.
		/// </summary>
		IReadOnlyCollection<IMember> VotingMembers { get; }

		/// <summary>
		/// Retrieves a check list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching check list, or null if none found.</returns>
		/// <remarks>
		/// Matches on CheckList.Id and CheckList.Name.  Comparison is case-sensitive.
		/// </remarks>
		ICheckList this[string key] { get; }

		/// <summary>
		/// Retrieves the check list at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The check list.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		ICheckList this[int index] { get; }

		/// <summary>
		/// Raised when data on the card is updated.
		/// </summary>
		event Action<ICard, IEnumerable<string>> Updated;

		/// <summary>
		/// Deletes the card.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the card from Trello's server, however, this object will
		/// remain in memory and all properties will remain accessible.
		/// </remarks>
		Task Delete(CancellationToken ct = default(CancellationToken));

		/// <summary>
		/// Marks the card to be refreshed the next time data is accessed.
		/// </summary>
		Task Refresh(CancellationToken ct = default(CancellationToken));
	}
}
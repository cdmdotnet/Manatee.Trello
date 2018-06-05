using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a board.
	/// </summary>
	public interface IBoard : ICanWebhook, IQueryable
	{
		/// <summary>
		/// Gets the collection of actions performed on and within this board.
		/// </summary>
		IReadOnlyActionCollection Actions { get; }

		/// <summary>
		/// Gets the collection of cards contained within this board.
		/// </summary>
		/// <remarks>
		/// This property only exposes unarchived cards.
		/// </remarks>
		IReadOnlyCardCollection Cards { get; }

		/// <summary>
		/// Gets the creation date of the board.
		/// </summary>
		DateTime CreationDate { get; }
		/// <summary>
		/// Gets the collection of custom fields defined on the board.
		/// </summary>
		ICustomFieldDefinitionCollection CustomFields { get; }

		/// <summary>
		/// Gets or sets the board's description.
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Gets or sets whether this board is closed.
		/// </summary>
		bool? IsClosed { get; set; }

		/// <summary>
		/// Gets or sets whether the current member is subscribed to this board.
		/// </summary>
		bool? IsSubscribed { get; set; }

		/// <summary>
		/// Gets the collection of labels for this board.
		/// </summary>
		IBoardLabelCollection Labels { get; }

		/// <summary>
		/// Gets the collection of lists on this board.
		/// </summary>
		/// <remarks>
		/// This property only exposes unarchived lists.
		/// </remarks>
		IListCollection Lists { get; }

		/// <summary>
		/// Gets the collection of members on this board.
		/// </summary>
		IReadOnlyMemberCollection Members { get; }

		/// <summary>
		/// Gets the collection of members and their privileges on this board.
		/// </summary>
		IBoardMembershipCollection Memberships { get; }

		/// <summary>
		/// Gets or sets the board's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the organization to which this board belongs.
		/// </summary>
		/// <remarks>
		/// Setting null makes the board's first admin the owner.
		/// </remarks>
		IOrganization Organization { get; set; }

		/// <summary>
		/// Gets metadata about any active power-ups.
		/// </summary>
		IPowerUpCollection PowerUps { get; }

		/// <summary>
		/// Gets specific data regarding power-ups.
		/// </summary>
		IReadOnlyCollection<IPowerUpData> PowerUpData { get; }

		/// <summary>
		/// Gets the set of preferences for the board.
		/// </summary>
		IBoardPreferences Preferences { get; }

		/// <summary>
		/// Gets the set of preferences for the board.
		/// </summary>
		IBoardPersonalPreferences PersonalPreferences { get; }

		/// <summary>
		/// Gets the board's URI.
		/// </summary>
		string Url { get; }

		/// <summary>
		/// Gets or sets wheterh this board is pinned.
		/// </summary>
		bool? IsPinned { get; }

		/// <summary>
		/// Gets or sets wheterh this board is pinned.
		/// </summary>
		bool? IsStarred { get; }

		/// <summary>
		/// Gets the date of the board's most recent activity.
		/// </summary>
		DateTime? LastActivity { get; }

		/// <summary>
		/// Gets the date when the board was most recently viewed.
		/// </summary>
		DateTime? LastViewed { get; }

		/// <summary>
		/// Gets the board's short URI.
		/// </summary>
		string ShortLink { get; }

		/// <summary>
		/// Gets the board's short link (ID).
		/// </summary>
		string ShortUrl { get; }

		/// <summary>
		/// Retrieves a list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching list, or null if none found.</returns>
		/// <remarks>
		/// Matches on List.Id and List.Name.  Comparison is case-sensitive.
		/// </remarks>
		IList this[string key] { get; }

		/// <summary>
		/// Retrieves the list at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The list.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		IList this[int index] { get; }

		/// <summary>
		/// Raised when data on the board is updated.
		/// </summary>
		event Action<IBoard, IEnumerable<string>> Updated;

		/// <summary>
		/// Deletes the board.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the board from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		Task Delete(CancellationToken ct = default(CancellationToken));

		/// <summary>
		/// Refreshes the board data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken));
	}
}
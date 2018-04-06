using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a list.
	/// </summary>
	public interface IList : ICanWebhook
	{
		/// <summary>
		/// Gets the collection of actions performed on the list.
		/// </summary>
		IReadOnlyCollection<IAction> Actions { get; }

		/// <summary>
		/// Gets or sets the board on which the list belongs.
		/// </summary>
		IBoard Board { get; set; }

		/// <summary>
		/// Gets the collection of cards contained in the list.
		/// </summary>
		ICardCollection Cards { get; }

		/// <summary>
		/// Gets the creation date of the list.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets or sets whether the list is archived.
		/// </summary>
		bool? IsArchived { get; set; }

		/// <summary>
		/// Gets or sets whether the current member is subscribed to the list.
		/// </summary>
		bool? IsSubscribed { get; set; }

		/// <summary>
		/// Gets the list's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets the list's position.
		/// </summary>
		Position Position { get; set; }

		/// <summary>
		/// Retrieves a card which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching card, or null if none found.</returns>
		/// <remarks>
		/// Matches on Card.Id and Card.Name.  Comparison is case-sensitive.
		/// </remarks>
		ICard this[string key] { get; }

		/// <summary>
		/// Retrieves the card at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The card.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		ICard this[int index] { get; }

		/// <summary>
		/// Raised when data on the list is updated.
		/// </summary>
		event Action<IList, IEnumerable<string>> Updated;

		/// <summary>
		/// Marks the list to be refreshed the next time data is accessed.
		/// </summary>
		void Refresh();
	}
}
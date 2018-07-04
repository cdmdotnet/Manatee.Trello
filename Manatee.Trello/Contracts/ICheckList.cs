using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a checklist.
	/// </summary>
	public interface ICheckList : ICacheable, IRefreshable
	{
		/// <summary>
		/// Gets the board on which the checklist belongs.
		/// </summary>
		IBoard Board { get; }

		/// <summary>
		/// Gets the card on which the checklist belongs.
		/// </summary>
		ICard Card { get; }

		/// <summary>
		/// Gets the collection of items in the checklist.
		/// </summary>
		ICheckItemCollection CheckItems { get; }

		/// <summary>
		/// Gets the creation date of the checklist.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets the checklist's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets the checklist's position.
		/// </summary>
		Position Position { get; set; }

		/// <summary>
		/// Retrieves a check list item which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching check list item, or null if none found.</returns>
		/// <remarks>
		/// Matches on CheckItem.Id and CheckItem.Name.  Comparison is case-sensitive.
		/// </remarks>
		ICheckItem this[string key] { get; }

		/// <summary>
		/// Retrieves the check list item at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The check list item.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		ICheckItem this[int index] { get; }

		/// <summary>
		/// Raised when data on the check list is updated.
		/// </summary>
		event Action<ICheckList, IEnumerable<string>> Updated;

		/// <summary>
		/// Deletes the checklist.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the checklist from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		Task Delete(CancellationToken ct = default(CancellationToken));
	}
}
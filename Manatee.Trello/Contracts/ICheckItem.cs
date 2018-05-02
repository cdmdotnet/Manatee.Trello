using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a checklist item.
	/// </summary>
	public interface ICheckItem : ICacheable
	{
		/// <summary>
		/// Gets or sets the checklist to which the item belongs.
		/// </summary>
		/// <remarks>
		/// Trello only supports moving a check item between lists on the same card.
		/// </remarks>
		ICheckList CheckList { get; set; }

		/// <summary>
		/// Gets the creation date of the checklist item.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets or sets the checklist item's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the checklist item's position.
		/// </summary>
		Position Position { get; set; }

		/// <summary>
		/// Gets or sets the checklist item's state.
		/// </summary>
		CheckItemState? State { get; set; }

		/// <summary>
		/// Raised when data on the checklist item is updated.
		/// </summary>
		event Action<ICheckItem, IEnumerable<string>> Updated;

		/// <summary>
		/// Deletes the checklist item.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the checklist item from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		Task Delete(CancellationToken ct = default(CancellationToken));

		/// <summary>
		/// Refreshes the checklist item data.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Refresh(CancellationToken ct = default(CancellationToken));
	}
}
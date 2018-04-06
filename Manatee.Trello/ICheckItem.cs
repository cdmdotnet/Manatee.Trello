using System;
using System.Collections.Generic;

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
		/// <remarks>
		/// This permanently deletes the checklist item from Trello's server, however, this
		/// object will remain in memory and all properties will remain accessible.
		/// </remarks>
		void Delete();

		/// <summary>
		/// Marks the checklist item to be refreshed the next time data is accessed.
		/// </summary>
		void Refresh();
	}
}
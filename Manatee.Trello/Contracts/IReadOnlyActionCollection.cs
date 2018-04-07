using System;
using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of actions.
	/// </summary>
	public interface IReadOnlyActionCollection : IReadOnlyCollection<IAction>
	{
		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="actionType">The action type.</param>
		void Filter(ActionType actionType);
		/// <summary>
		/// Adds a number of filters to the collection.
		/// </summary>
		/// <param name="actionTypes">A collection of action types.</param>
		void Filter(IEnumerable<ActionType> actionTypes);
		/// <summary>
		/// Adds a date-based filter to the collection.
		/// </summary>
		/// <param name="start">The start date.</param>
		/// <param name="end">The end date.</param>
		void Filter(DateTime? start, DateTime? end);
	}
}
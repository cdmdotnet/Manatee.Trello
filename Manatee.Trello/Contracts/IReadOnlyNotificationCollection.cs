using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of notifications.
	/// </summary>
	public interface IReadOnlyNotificationCollection : IReadOnlyCollection<INotification>
	{
		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter type.</param>
		void Filter(NotificationType filter);
		/// <summary>
		/// Adds a set of filters to the collection.
		/// </summary>
		/// <param name="filters">A collection of filters.</param>
		void Filter(IEnumerable<NotificationType> filters);
	}
}
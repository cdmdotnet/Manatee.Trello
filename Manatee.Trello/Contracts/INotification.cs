using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a notification.
	/// </summary>
	public interface INotification : ICacheable, IRefreshable
	{
		/// <summary>
		/// Gets the creation date of the notification.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets the member who performed the action which created the notification.
		/// </summary>
		IMember Creator { get; }

		/// <summary>
		/// Gets any data associated with the notification.
		/// </summary>
		INotificationData Data { get; }

		/// <summary>
		/// Gets the date and teim at which the notification was issued.
		/// </summary>
		DateTime? Date { get; }

		/// <summary>
		/// Gets or sets whether the notification has been read.
		/// </summary>
		bool? IsUnread { get; set; }

		/// <summary>
		/// Gets the type of notification.
		/// </summary>
		NotificationType? Type { get; }

		/// <summary>
		/// Raised when data on the notification is updated.
		/// </summary>
		event Action<INotification, IEnumerable<string>> Updated;
	}
}
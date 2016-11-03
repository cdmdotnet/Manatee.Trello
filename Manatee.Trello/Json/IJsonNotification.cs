using System;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Notification object.
	/// </summary>
	public interface IJsonNotification : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets whether the notification has been read.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		bool? Unread { get; set; }
		/// <summary>
		/// Gets or sets the notification's type.
		/// </summary>
		[JsonDeserialize]
		NotificationType? Type { get; set; }
		///<summary>
		/// Gets or sets the date on which the notification was created.
		///</summary>
		[JsonDeserialize]
		DateTime? Date { get; set; }
		/// <summary>
		/// Gets or sets the data associated with the notification.  Contents depend upon the notification's type.
		/// </summary>
		[JsonDeserialize]
		IJsonNotificationData Data { get; set; }
		/// <summary>
		/// Gets or sets the ID of the member whose action spawned the notification.
		/// </summary>
		[JsonDeserialize]
		IJsonMember MemberCreator { get; set; }
	}
}
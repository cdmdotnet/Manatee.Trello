/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Notification.cs
	Namespace:		Manatee.Trello
	Class Name:		Notification
	Purpose:		Represents a notification.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a notification.
	/// </summary>
	public class Notification : ICacheable
	{
		private static readonly Dictionary<NotificationType, Func<Notification, string>> _stringDefinitions;

		private readonly Field<Member> _creator;
		private readonly Field<DateTime?> _date;
		private readonly Field<bool?> _isUnread;
		private readonly Field<NotificationType?> _type;
		private readonly NotificationContext _context;
		private DateTime? _creation;

		/// <summary>
		/// Gets the creation date of the notification.
		/// </summary>
		public DateTime CreationDate
		{
			get
			{
				if (_creation == null)
					_creation = Id.ExtractCreationDate();
				return _creation.Value;
			}
		}
		/// <summary>
		/// Gets the member who performed the action which created the notification.
		/// </summary>
		public Member Creator => _creator.Value;
		/// <summary>
		/// Gets any data associated with the notification.
		/// </summary>
		public NotificationData Data { get; }
		/// <summary>
		/// Gets the date and teim at which the notification was issued.
		/// </summary>
		public DateTime? Date => _date.Value;
		/// <summary>
		/// Gets the notification's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets or sets whether the notification has been read.
		/// </summary>
		public bool? IsUnread
		{
			get { return _isUnread.Value; }
			set { _isUnread.Value = value; }
		}
		/// <summary>
		/// Gets the type of notification.
		/// </summary>
		public NotificationType? Type => _type.Value;

		internal IJsonNotification Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

#if IOS
		private Action<Notification, IEnumerable<string>> _updatedInvoker;

		/// <summary>
		/// Raised when data on the notification is updated.
		/// </summary>
		public event Action<Notification, IEnumerable<string>> Updated
		{
			add { _updatedInvoker += value; }
			remove { _updatedInvoker -= value; }
		}
#else
		/// <summary>
		/// Raised when data on the notification is updated.
		/// </summary>
		public event Action<Notification, IEnumerable<string>> Updated;
#endif

		static Notification()
		{
			_stringDefinitions = new Dictionary<NotificationType, Func<Notification, string>>
				{
					{NotificationType.AddedAttachmentToCard, n => $"{n.Creator} attached {n.Data.Attachment} to card {n.Data.Card}."},
					{NotificationType.AddedToBoard, n => $"{n.Creator} added you to board {n.Data.Board}."},
					{NotificationType.AddedToCard, n => $"{n.Creator} assigned you to card {n.Data.Card}."},
					{NotificationType.AddedToOrganization, n => $"{n.Creator} added member {n.Data.Member} to organization {n.Data.Organization}."},
					{NotificationType.AddedMemberToCard, n => $"{n.Creator} assigned member {n.Data.Member} to card {n.Data.Card}."},
					{NotificationType.AddAdminToBoard, n => $"{n.Creator} added member {n.Data.Member} to board {n.Data.Board} as an admin."},
					{NotificationType.AddAdminToOrganization, n => $"{n.Creator} added member {n.Data.Member} to organization {n.Data.Organization} as an admin."},
					{NotificationType.ChangeCard, n => $"{n.Creator} changed card {n.Data.Card}."},
					{NotificationType.CloseBoard, n => $"{n.Creator} closed board {n.Data.Board}."},
					{NotificationType.CommentCard, n => $"{n.Creator} commented on card #{n.Data.Card}: '{n.Data.Text}'."},
					{NotificationType.CreatedCard, n => $"{n.Creator} created card {n.Data.Card} on board {n.Data.Board}."},
					{NotificationType.RemovedFromBoard, n => $"{n.Creator} removed member {n.Data.Member} from board {n.Data.Board}."},
					{NotificationType.RemovedFromCard, n => $"{n.Creator} removed you from card {n.Data.Card}."},
					{NotificationType.RemovedMemberFromCard, n => $"{n.Creator} removed member {n.Data.Member} from card {n.Data.Card}."},
					{NotificationType.RemovedFromOrganization, n => $"{n.Creator} removed member {n.Data.Member} from organization {n.Data.Organization}."},
					{NotificationType.MentionedOnCard, n => $"{n.Creator} mentioned you on card {n.Data.Card}: '{n.Data.Text}'."},
					{NotificationType.UpdateCheckItemStateOnCard, n => $"{n.Creator} updated checkItem {n.Data.CheckItem} on card {n.Data.Card}."},
					{NotificationType.MakeAdminOfBoard, n => $"{n.Creator} made member {n.Data.Member} an admin of board {n.Data.Board}."},
					{NotificationType.MakeAdminOfOrganization, n => $"{n.Creator} made member {n.Data.Member} an admin of organization {n.Data.Organization}."},
					{NotificationType.CardDueSoon, n => $"Card {n.Data.Card} is due soon."},
				};
		}
		/// <summary>
		/// Creates a new <see cref="Notification"/> object.
		/// </summary>
		/// <param name="id">The notification's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public Notification(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new NotificationContext(id, auth);
			_context.Synchronized += Synchronized;

			_creator = new Field<Member>(_context, nameof(Creator));
			_date = new Field<DateTime?>(_context, nameof(Date));
			Data = new NotificationData(_context.NotificationDataContext);
			_isUnread = new Field<bool?>(_context, nameof(IsUnread));
			_type = new Field<NotificationType?>(_context, nameof(Type));

			TrelloConfiguration.Cache.Add(this);
		}
		internal Notification(IJsonNotification json, TrelloAuthorization auth)
			: this(json.Id, auth)
		{
			_context.Merge(json);
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return Type.HasValue ? _stringDefinitions[Type.Value](this) : "Notification type could not be determined.";
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
#if IOS
			var handler = _updatedInvoker;
#else
			var handler = Updated;
#endif
			handler?.Invoke(this, properties);
		}
	}
}
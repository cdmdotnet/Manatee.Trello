/***************************************************************************************

	Copyright 2014 Greg Dennis

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
		private readonly Field<NotificationType> _type;
		private readonly NotificationContext _context;

		/// <summary>
		/// Gets the member who performed the action which created the notification.
		/// </summary>
		public Member Creator { get { return _creator.Value; } }
		/// <summary>
		/// Gets any data associated with the notification.
		/// </summary>
		public NotificationData Data { get; private set; }
		/// <summary>
		/// Gets the date and teim at which the notification was issued.
		/// </summary>
		public DateTime? Date { get { return _date.Value; } }
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
		public NotificationType Type { get { return _type.Value; } }

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
					{NotificationType.AddedAttachmentToCard, n => string.Format("{0} attached {1} to card {2}.", n.Creator, n.Data.Attachment, n.Data.Card)},
					{NotificationType.AddedToBoard, n => string.Format("{0} added you to board {1}.", n.Creator, n.Data.Board)},
					{NotificationType.AddedToCard, n => string.Format("{0} assigned you to card {1}.", n.Creator, n.Data.Card)},
					{NotificationType.AddedToOrganization, n => string.Format("{0} added member {1} to organization {2}.", n.Creator, n.Data.Member, n.Data.Organization)},
					{NotificationType.AddedMemberToCard, n => string.Format("{0} assigned member {1} to card {2}.", n.Creator, n.Data.Member, n.Data.Card)},
					{NotificationType.AddAdminToBoard, n => string.Format("{0} added member {1} to board {2} as an admin.", n.Creator, n.Data.Member, n.Data.Board)},
					{NotificationType.AddAdminToOrganization, n => string.Format("{0} added member {1} to organization {2} as an admin.", n.Creator, n.Data.Member, n.Data.Organization)},
					{NotificationType.ChangeCard, n => string.Format("{0} changed card {1}.", n.Creator, n.Data.Card)},
					{NotificationType.CloseBoard, n => string.Format("{0} closed board {1}.", n.Creator, n.Data.Board)},
					{NotificationType.CommentCard, n => string.Format("{0} commented on card #{1}: '{2}'.", n.Creator, n.Data.Card, n.Data.Text)},
					{NotificationType.CreatedCard, n => string.Format("{0} created card {1} on board {2}.", n.Creator, n.Data.Card, n.Data.Board)},
					{NotificationType.RemovedFromBoard, n => string.Format("{0} removed member {1} from board {2}.", n.Creator, n.Data.Member, n.Data.Board)},
					{NotificationType.RemovedFromCard, n => string.Format("{0} removed you from card {1}.", n.Creator, n.Data.Card)},
					{NotificationType.RemovedMemberFromCard, n => string.Format("{0} removed member {1} from card {2}.", n.Creator, n.Data.Member, n.Data.Card)},
					{NotificationType.RemovedFromOrganization, n => string.Format("{0} removed member {1} from organization {2}.", n.Creator, n.Data.Member, n.Data.Organization)},
					{NotificationType.MentionedOnCard, n => string.Format("{0} mentioned you on card {1}: '{2}'.", n.Creator, n.Data.Card, n.Data.Text)},
					{NotificationType.UpdateCheckItemStateOnCard, n => string.Format("{0} updated checkItem {1} on card {2}.", n.Creator, n.Data.CheckItem, n.Data.Card)},
					{NotificationType.MakeAdminOfBoard, n => string.Format("{0} made member {1} an admin of board {2}.", n.Creator, n.Data.Member, n.Data.Board)},
					{NotificationType.MakeAdminOfOrganization, n => string.Format("{0} made member {1} an admin of organization {2}.", n.Creator, n.Data.Member, n.Data.Organization)},
					{NotificationType.CardDueSoon, n => string.Format("Card {0} is due soon.", n.Data.Card)},
				};
		}
		/// <summary>
		/// Creates a new <see cref="Notification"/> object.
		/// </summary>
		/// <param name="id">The notification's ID.</param>
		public Notification(string id)
		{
			Id = id;
			_context = new NotificationContext(id);
			_context.Synchronized += Synchronized;

			_creator = new Field<Member>(_context, () => Creator);
			_date = new Field<DateTime?>(_context, () => Date);
			Data = new NotificationData(_context.NotificationDataContext);
			_isUnread = new Field<bool?>(_context, () => IsUnread);
			_type = new Field<NotificationType>(_context, () => Type);

			TrelloConfiguration.Cache.Add(this);
		}
		internal Notification(IJsonNotification json)
			: this(json.Id)
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
			return _stringDefinitions[Type](this);
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
#if IOS
			var handler = _updatedInvoker;
#else
			var handler = Updated;
#endif
			if (handler != null)
				handler(this, properties);
		}
	}
}
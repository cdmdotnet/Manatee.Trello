/***************************************************************************************

	Copyright 2013 Little Crab Solutions

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
	Purpose:		Represents a member notification on Trello.com.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a member notification.
	/// </summary>
	public class Notification : ExpiringObject, IEquatable<Notification>, IComparable<Notification>
	{
		private static readonly OneToOneMap<NotificationType, string> _typeMap;
		private static readonly Dictionary<NotificationType, Func<Notification, string>> _stringDefinitions;

		private readonly Dictionary<string, ExpiringObject> _entities;
		private IJsonNotification _jsonNotification;
		private Member _memberCreator;
		private NotificationType _type;

		public Attachment Attachment
		{
			get { return TryGetEntity<Attachment>("attachment", "attachment.id", EntityRequestType.Attachment_Read_Refresh); }
		}
		public Board Board
		{
			get { return TryGetEntity<Board>("board", "board.id", EntityRequestType.Board_Read_Refresh); }
		}
		public Card Card
		{
			get { return TryGetEntity<Card>("card", "card.id", EntityRequestType.Card_Read_Refresh); }
		}
		public int? CardShortId
		{
			get { return (int?)Data.TryGetNumber("card", "idShort"); }
		}
		public CheckList CheckList
		{
			get { return TryGetEntity<CheckList>("checklist", "checklist.id", EntityRequestType.CheckList_Read_Refresh); }
		}
		public CheckItem CheckItem
		{
			get { return TryGetEntity<CheckItem>("checkItem", "checkItem.id", EntityRequestType.CheckItem_Read_Refresh); }
		}
		/// <summary>
		/// Data associated with the notification.  Contents depend upon the notification's type.
		/// </summary>
		private IJsonNotificationData Data
		{
			get { return _jsonNotification.Data; }
		}
		///<summary>
		/// The date on which the notification was created.
		///</summary>
		public DateTime? Date { get { return _jsonNotification.Date; } }
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public sealed override string Id
		{
			get { return _jsonNotification.Id; }
			internal set { _jsonNotification.Id = value; }
		}
		/// <summary>
		/// Gets or sets whether the notification has been read.
		/// </summary>
		public bool? IsUnread
		{
			get
			{
				VerifyNotExpired();
				return _jsonNotification.Unread;
			}
			set
			{

				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonNotification.Unread == value) return;
				_jsonNotification.Unread = value;
				Parameters.Add("unread", _jsonNotification.Unread.ToLowerString());
				Upload(EntityRequestType.Notification_Write_IsUnread);
			}
		}
		public List List
		{
			get { return TryGetEntity<List>("list", "list.id", EntityRequestType.List_Read_Refresh); }
		}
		public List ListAfter
		{
			get { return TryGetEntity<List>("listAfter", "listAfter.id", EntityRequestType.List_Read_Refresh); }
		}
		public List ListBefore
		{
			get { return TryGetEntity<List>("listBefore", "listBefore.id", EntityRequestType.List_Read_Refresh); }
		}
		public Member Member
		{
			get { return TryGetEntity<Member>("member", "member.id", EntityRequestType.Member_Read_Refresh); }
		}
		/// <summary>
		/// Gets the member whose action spawned the notification.
		/// </summary>
		public Member MemberCreator
		{
			get { return UpdateById(ref _memberCreator, EntityRequestType.Member_Read_Refresh, _jsonNotification.IdMemberCreator); }
		}
		public Organization Organization
		{
			get { return TryGetEntity<Organization>("organization", "organization", EntityRequestType.Organization_Read_Refresh); }
		}
		public Board SourceBoard
		{
			get { return TryGetEntity<Board>("boardSource", "boardSource.id", EntityRequestType.Board_Read_Refresh); }
		}
		public Card SourceCard
		{
			get { return TryGetEntity<Card>("cardSource", "cardSource.id", EntityRequestType.Card_Read_Refresh); }
		}
		public string Text
		{
			get { return Data.TryGetString("text"); }
		}
		/// <summary>
		/// Gets the notification's type.
		/// </summary>
		public NotificationType Type
		{
			get { return _type; }
		}
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return _jsonNotification is InnerJsonNotification; } }

		static Notification()
		{
			_typeMap = new OneToOneMap<NotificationType, string>
			           	{
			           		{NotificationType.AddedAttachmentToCard, "addedAttachmentToCard"},
			           		{NotificationType.AddedToBoard, "addedToBoard"},
			           		{NotificationType.AddedToCard, "addedToCard"},
			           		{NotificationType.AddedToOrganization, "addedToOrganization"},
			           		{NotificationType.AddedMemberToCard, "addedMemberToCard"},
			           		{NotificationType.AddAdminToBoard, "addAdminToBoard"},
			           		{NotificationType.AddAdminToOrganization, "addAdminToOrganization"},
			           		{NotificationType.ChangeCard, "changeCard"},
			           		{NotificationType.CloseBoard, "closeBoard"},
			           		{NotificationType.CommentCard, "commentCard"},
			           		{NotificationType.CreatedCard, "createdCard"},
			           		{NotificationType.InvitedToBoard, "invitedToBoard"},
			           		{NotificationType.InvitedToOrganization, "invitedToOrganization"},
			           		{NotificationType.RemovedFromBoard, "removedFromBoard"},
			           		{NotificationType.RemovedFromCard, "removedFromCard"},
			           		{NotificationType.RemovedMemberFromCard, "removedMemberFromCard"},
			           		{NotificationType.RemovedFromOrganization, "removedFromOrganization"},
			           		{NotificationType.MentionedOnCard, "mentionedOnCard"},
			           		{NotificationType.UnconfirmedInvitedToBoard, "unconfirmedInvitedToBoard"},
			           		{NotificationType.UnconfirmedInvitedToOrganization, "unconfirmedInvitedToOrganization"},
			           		{NotificationType.UpdateCheckItemStateOnCard, "updateCheckItemStateOnCard"},
			           		{NotificationType.MakeAdminOfBoard, "makeAdminOfBoard"},
			           		{NotificationType.MakeAdminOfOrganization, "makeAdminOfOrganization"},
			           		{NotificationType.CardDueSoon, "cardDueSoon"},
			           	};
			_stringDefinitions = new Dictionary<NotificationType, Func<Notification, string>>
				{
					{NotificationType.AddedAttachmentToCard, n => n.ToString("{0} attached {1} to card {2}.", n.GetString("attachment.name"), n.GetString("card.name"))},
					{NotificationType.AddedToBoard, n => n.ToString("{0} added you to board {1}.", n.GetString("board.name"))},
					{NotificationType.AddedToCard, n => n.ToString("{0} assigned you to card {1}.", n.GetString("card.name"))},
					{NotificationType.AddedToOrganization, n => n.ToString("{0} added member {1} to organization {2}.", n.Member.FullName, n.GetString("organization.name"))},
					{NotificationType.AddedMemberToCard, n => n.ToString("{0} assigned member {1} to card {2}.", n.Member.FullName, n.GetString("card.name"))},
					{NotificationType.AddAdminToBoard, n => n.ToString("{0} added member {1} to board {2} as an admin.", n.Member.FullName, n.GetString("board.name"))},
					{NotificationType.AddAdminToOrganization, n => n.ToString("{0} added member {1} to organization {2} as an admin.", n.Member.FullName, n.GetString("organization.name"))},
					{NotificationType.ChangeCard, n => n.ToString("{0} changed card {1}.", n.GetString("card.name"))},
					{NotificationType.CloseBoard, n => n.ToString("{0} closed board {1}.", n.GetString("board.name"))},
					{NotificationType.CommentCard, n => n.ToString("{0} commented on card #{1}: '{2}'.", n.GetString("card.name"), n.GetString("text"))},
					{NotificationType.CreatedCard, n => n.ToString("{0} created card {1} on board {2}.", n.GetString("card.name"), n.GetString("board.name"))},
					{NotificationType.RemovedFromBoard, n => n.ToString("{0} removed member {1} from board {2}.", n.Member.FullName, n.GetString("board.name"))},
					{NotificationType.RemovedFromCard, n => n.ToString("{0} removed you from card {1}.", n.GetString("card.name"))},
					{NotificationType.RemovedMemberFromCard, n => n.ToString("{0} removed member {1} from organization {2}.", n.Member.FullName, n.GetString("organization.name"))},
					{NotificationType.RemovedFromOrganization, n => n.ToString("{0} removed member {1} from organization {2}.", n.Member.FullName, n.GetString("organization.name"))},
					{NotificationType.MentionedOnCard, n => n.ToString("{0} mentioned you on card {1}: '{2}'.", n.GetString("card.name"), n.GetString("text"))},
					{NotificationType.UpdateCheckItemStateOnCard, n => n.ToString("{0} updated checkItem {1} on card {2}.", n.GetString("checkItem.name"), n.GetString("card.name"))},
					{NotificationType.MakeAdminOfBoard, n => n.ToString("{0} made member {1} an admin of board {2}.", n.Member.FullName, n.GetString("board.name"))},
					{NotificationType.MakeAdminOfOrganization, n => n.ToString("{0} made member {1} an admin of organization {2}.", n.Member.FullName, n.GetString("organization.name"))},
					{NotificationType.CardDueSoon, n => n.ToString("Card {1} is due soon.", n.GetString("card.name"))},
				};
		}
		/// <summary>
		/// Creates a new instance of the Notification class.
		/// </summary>
		public Notification()
		{
			_jsonNotification = new InnerJsonNotification();
			_entities = new Dictionary<string, ExpiringObject>();
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Notification other)
		{
			return Id == other.Id;
		}
		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
		/// </returns>
		/// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (!(obj is Notification)) return false;
			return Equals((Notification) obj);
		}
		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public int CompareTo(Notification other)
		{
			var diff = Date - other.Date;
			return diff.HasValue ? (int) diff.Value.TotalMilliseconds : 0;
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
			if (_stringDefinitions.ContainsKey(Type))
				return _stringDefinitions[Type](this);
			Log.Info("I don't have notification type '{0}' configured yet.  If you can, please find the log entry where " +
					 "the JSON data is being received and email it to littlecrabsolutions@yahoo.com.  I'll try to add " +
					 "it in the next release.", _jsonNotification.Type);
			return string.Format("{0} did something, but it's classified.", MemberCreator.FullName);
		}
		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public override sealed bool Refresh()
		{
			Parameters["_id"] = Id;
			AddDefaultParameters();
			return EntityRepository.Refresh(this, EntityRequestType.Notification_Read_Refresh);
		}

		internal override void ApplyJson(object obj)
		{
			_jsonNotification = (IJsonNotification)obj;
			UpdateType();
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}

		private void Upload(EntityRequestType requestType)
		{
			Parameters["_id"] = Id;
			EntityRepository.Upload(requestType, Parameters);
		}

		private void UpdateType()
		{
			_type = _typeMap.Any(kvp => kvp.Value == _jsonNotification.Type) ? _typeMap[_jsonNotification.Type] : NotificationType.Unknown;
		}
		private T TryGetEntity<T>(string index, string path, EntityRequestType request)
			where T : ExpiringObject
		{
			if (_entities.ContainsKey(index))
				return (T)_entities[index];
			var id = _jsonNotification.Data.TryGetString(path.Split('.'));
			if (id == null) return null;
			T entity = null;
			try
			{
				UpdateById(ref entity, request, id);
				if (entity.IsStubbed)
					entity = null;
				else
					_entities[index] = entity;
			}
			catch {}
			return entity;
		}
		private string ToString(string format, params string[] parameters)
		{
			var allParameters = new List<object> {MemberCreator};
			allParameters.AddRange(parameters);
			return string.Format(format, allParameters.ToArray());
		}
		private string GetString(string path)
		{
			var split = path.Split('.');
			object value = _jsonNotification.Data.TryGetString(split);
			if (value != null) return value.ToString();
			value = _jsonNotification.Data.TryGetNumber(split);
			if (value != null) return value.ToString();
			value = _jsonNotification.Data.TryGetNumber(split);
			if (value != null) return value.ToString();
			return string.Empty;
		}
	}
}
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

		private IJsonNotification _jsonNotification;
		private Member _memberCreator;
		private NotificationType _type;

		/// <summary>
		/// Data associated with the notification.  Contents depend upon the notification's type.
		/// </summary>
		internal IJsonNotificationData Data
		{
			get { return (_jsonNotification == null) ? null : _jsonNotification.Data; }
			set
			{
				if (_jsonNotification == null) return;
				_jsonNotification.Data = value;
			}
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
			get { return _jsonNotification != null ? _jsonNotification.Id : base.Id; }
			internal set
			{
				if (_jsonNotification != null)
					_jsonNotification.Id = value;
				base.Id = value;
			}
		}
		/// <summary>
		/// Gets or sets whether the notification has been read.
		/// </summary>
		public bool? IsUnread
		{
			get
			{
				VerifyNotExpired();
				return (_jsonNotification == null) ? null : _jsonNotification.Unread;
			}
			set
			{

				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonNotification == null) return;
				if (_jsonNotification.Unread == value) return;
				_jsonNotification.Unread = value;
				Parameters.Add("unread", _jsonNotification.Unread.ToLowerString());
				Upload(EntityRequestType.Notification_Write_IsUnread);
			}
		}
		/// <summary>
		/// Gets the member whose action spawned the notification.
		/// </summary>
		public Member MemberCreator
		{
			get
			{
				if (_jsonNotification == null) return null;
				if (_jsonNotification.IdMemberCreator == null) return null;
				return UpdateById(ref _memberCreator, EntityRequestType.Member_Read_Refresh, _jsonNotification.IdMemberCreator);
			}
		}
		/// <summary>
		/// Gets the notification's type.
		/// </summary>
		public NotificationType Type
		{
			get { return _type; }
			internal set { _type = value; }
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
		}
		/// <summary>
		/// Creates a new instance of the Notification class.
		/// </summary>
		public Notification()
		{
			_jsonNotification = new InnerJsonNotification();
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
			return string.Format("{0} did something noteworthy.", MemberCreator == null ? "An unknown member" : MemberCreator.FullName);
		}
		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public override sealed bool Refresh()
		{
			if (_jsonNotification is InnerJsonNotification) return false;
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
	}
}
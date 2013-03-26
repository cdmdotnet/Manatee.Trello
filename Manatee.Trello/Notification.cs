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
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	//[
	//   {
	//      "id":"5144719b047913c06e00294e",
	//      "unread":true,
	//      "type":"addedToBoard",
	//      "date":"2013-03-16T13:20:27.311Z",
	//      "data":{
	//         "board":{
	//            "name":"Manatee.Json",
	//            "id":"50d227239c7b29575f000f99"
	//         }
	//      },
	//      "idMemberCreator":"50b693ad6f122b4310000a3c",
	//      "memberCreator":{
	//         "id":"50b693ad6f122b4310000a3c",
	//         "avatarHash":"e97c40e0d0b85ab66661dbff5082d627",
	//         "fullName":"Greg Dennis",
	//         "initials":"GD",
	//         "username":"gregsdennis"
	//      }
	//   },
	/// <summary>
	/// Represents a member notification.
	/// </summary>
	public class Notification : JsonCompatibleExpiringObject, IEquatable<Notification>
	{
		private static readonly OneToOneMap<NotificationType, string> _typeMap;

		private NotificationData _data;
		private DateTime? _date;
		private bool? _isUnread;
		private string _memberCreatorId;
		private Member _member;
		private string _apiType;
		private NotificationType _type;

		/// <summary>
		/// Data associated with the notification.  Contents depend upon the notification's type.
		/// </summary>
		public object Data { get { return _data; } }
		///<summary>
		/// The date on which the notification was created.
		///</summary>
		public DateTime? Date
		{
			get
			{
				VerifyNotExpired();
				return _date;
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
				return _isUnread;
			}
			set
			{
				_isUnread = value;
				Parameters.Add("unread", _isUnread.ToLowerString());
				Put();
			}
		}
		/// <summary>
		/// Gets the member whose action spawned the notification.
		/// </summary>
		private Member MemberCreator
		{
			get
			{
				VerifyNotExpired();
				return ((_member == null) || (_member.Id != _memberCreatorId)) && (Svc != null)
				       	? (_member = Svc.Retrieve<Member>(_memberCreatorId))
				       	: _member;
			}
		}
		/// <summary>
		/// Gets the notification's type.
		/// </summary>
		public NotificationType Type
		{
			get
			{
				VerifyNotExpired();
				return _type;
			}
		}

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
			_data = new NotificationData(null, this);
		}
		internal Notification(TrelloService svc, string id)
			: base(svc, id)
		{
			_data = new NotificationData(svc, this);
		}

		/// <summary>
		/// Builds an object from a JsonValue.
		/// </summary>
		/// <param name="json">The JsonValue representation of the object.</param>
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_data = obj.TryGetObject("data").FromJson<NotificationData>();
			var date = obj.TryGetString("due");
			_date = string.IsNullOrWhiteSpace(date) ? (DateTime?) null : DateTime.Parse(date);
			_isUnread = obj.TryGetBoolean("unread");
			_memberCreatorId = obj.TryGetString("idMemberCreator");
			_apiType = obj.TryGetString("type");
			UpdateType();
		}
		/// <summary>
		/// Converts an object to a JsonValue.
		/// </summary>
		/// <returns>
		/// The JsonValue representation of the object.
		/// </returns>
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"data", _data.ToJson()},
			           		{"due", _date.HasValue ? _date.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			           		{"unread", _isUnread.HasValue ? _isUnread.Value : JsonValue.Null},
			           		{"idMemberCreator", _memberCreatorId},
			           		{"type", _apiType}
			           	};
			return json;
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

		internal override bool Match(string id)
		{
			return Id == id;
		}
		internal override void Refresh(ExpiringObject entity)
		{
			var notification = entity as Notification;
			if (notification == null) return;
			_date = notification._date;
			_isUnread = notification._isUnread;
			_memberCreatorId = notification._memberCreatorId;
			_apiType = notification._apiType;
			UpdateType();
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override sealed void Get()
		{
			var entity = Svc.Api.Get(Svc.RequestProvider.Create<Notification>(Id));
			Refresh(entity);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateSerivce()
		{
			_data.Svc = Svc;
			if (_member != null) _member.Svc = Svc;
		}
		
		private void Put()
		{
			Svc.PutAndCache(Svc.RequestProvider.Create<Notification>(this));
		}

		private void UpdateType()
		{
			_type = _typeMap.Any(kvp => kvp.Value == _apiType) ? _typeMap[_apiType] : NotificationType.Unknown;
		}
		private void UpdateApiType()
		{
			if (_typeMap.Any(kvp => kvp.Key == _type))
				_apiType = _typeMap[_type];
		}
	}
}
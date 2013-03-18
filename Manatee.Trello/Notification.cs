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
using Manatee.Trello.Implementation;

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
	public class Notification : EntityBase, IEquatable<Notification>
	{
		private static readonly OneToOneMap<NotificationType, string> _typeMap;

		private NotificationData _data;
		private DateTime? _date;
		private bool? _isUnread;
		private string _memberCreatorId;
		private Member _member;
		private string _apiType;
		private NotificationType _type;

		private object Data { get { return _data; } }
		public DateTime? Date
		{
			get
			{
				VerifyNotExpired();
				return _date;
			}
			set { _date = value; }
		}
		public bool? IsUnread
		{
			get
			{
				VerifyNotExpired();
				return _isUnread;
			}
			set { _isUnread = value; }
		}
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
		public NotificationType Type
		{
			get { return _type; }
			set
			{
				_type = value;
				UpdateApiType();
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
		public Notification() {}
		internal Notification(TrelloService svc, string id)
			: base(svc, id) {}

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
		public bool Equals(Notification other)
		{
			return Id == other.Id;
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
		internal override bool Match(string id)
		{
			return Id == id;
		}

		protected override sealed void Refresh()
		{
			var entity = Svc.Api.GetEntity<Notification>(Id);
			Refresh(entity);
		}
		protected override void PropigateSerivce()
		{
			_data.Svc = Svc;
			if (_member != null) _member.Svc = Svc;
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
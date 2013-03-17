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
 
	File Name:		Action.cs
	Namespace:		Manatee.Trello
	Class Name:		Action
	Purpose:		Represents an action on Trello.com.

***************************************************************************************/
using System;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//{
	//  "id":"5144719b047913c06e00294d",
	//  "idMemberCreator":"50b693ad6f122b4310000a3c",
	//  "data":{
	//     "board":{
	//        "name":"Manatee.Json",
	//        "id":"50d227239c7b29575f000f99"
	//     },
	//     "idMember":"514464db3fa062da6e00254f"
	//  },
	//  "type":"makeNormalMemberOfBoard",
	//  "date":"2013-03-16T13:20:27.315Z",
	//  "member":{
	//     "id":"514464db3fa062da6e00254f",
	//     "avatarHash":null,
	//     "fullName":"Little Crab Solutions",
	//     "initials":"LS",
	//     "username":"s_littlecrabsolutions"
	//  },
	//  "memberCreator":{
	//     "id":"50b693ad6f122b4310000a3c",
	//     "avatarHash":"e97c40e0d0b85ab66661dbff5082d627",
	//     "fullName":"Greg Dennis",
	//     "initials":"GD",
	//     "username":"gregsdennis"
	//  }
	//}
	public class Action : EntityBase
	{
		private static readonly OneToOneMap<ActionType, string> _typeMap;

		private string _apiType;
		private string _memberCreatorId;
		private Member _member;
		private readonly ActionData _data;
		private ActionType _type;
		private DateTime? _date;

		public Member MemberCreator
		{
			get
			{
				VerifyNotExpired();
				return ((_member == null) || (_member.Id != _memberCreatorId)) && (Svc != null)
				       	? (_member = Svc.Retrieve<Member>(_memberCreatorId))
				       	: _member;
			}
		}
		public ActionData Data { get { return _data; } }
		public ActionType Type
		{
			get { return _type; }
			set
			{
				_type = value;
				UpdateApiType();
			}
		}
		private DateTime? Date
		{
			get
			{
				VerifyNotExpired();
				return _date;
			}
		}

		static Action()
		{
			_typeMap = new OneToOneMap<ActionType, string>
			           	{
			           		{ActionType.AddAttachmentToCard, "addAttachmentToCard"},
			           		{ActionType.AddChecklistToCard, "addChecklistToCard"},
			           		{ActionType.AddMemberToBoard, "addMemberToBoard"},
			           		{ActionType.AddMemberToCard, "addMemberToCard"},
			           		{ActionType.AddMemberToOrganization, "addMemberToOrganization"},
			           		{ActionType.AddToOrganizationBoard, "addToOrganizationBoard"},
			           		{ActionType.CommentCard, "commentCard"},
			           		{ActionType.CopyCommentCard, "copyCommentCard"},
			           		{ActionType.ConvertToCardFromCheckItem, "convertToCardFromCheckItem"},
			           		{ActionType.CopyBoard, "copyBoard"},
			           		{ActionType.CreateBoard, "createBoard"},
			           		{ActionType.CreateCard, "createCard"},
			           		{ActionType.CopyCard, "copyCard"},
			           		{ActionType.CreateList, "createList"},
			           		{ActionType.CreateOrganization, "createOrganization"},
			           		{ActionType.DeleteAttachmentFromCard, "deleteAttachmentFromCard"},
			           		{ActionType.DeleteBoardInvitation, "deleteBoardInvitation"},
			           		{ActionType.DeleteOrganizationInvitation, "deleteOrganizationInvitation"},
			           		{ActionType.MakeAdminOfBoard, "makeAdminOfBoard"},
			           		{ActionType.MakeNormalMemberOfBoard, "makeNormalMemberOfBoard"},
			           		{ActionType.MakeNormalMemberOfOrganization, "makeNormalMemberOfOrganization"},
			           		{ActionType.MakeObserverOfBoard, "makeObserverOfBoard"},
			           		{ActionType.MemberJoinedTrello, "memberJoinedTrello"},
			           		{ActionType.MoveCardFromBoard, "moveCardFromBoard"},
			           		{ActionType.MoveListFromBoard, "moveListFromBoard"},
			           		{ActionType.MoveCardToBoard, "moveCardToBoard"},
			           		{ActionType.MoveListToBoard, "moveListToBoard"},
			           		{ActionType.RemoveAdminFromBoard, "removeAdminFromBoard"},
			           		{ActionType.RemoveAdminFromOrganization, "removeAdminFromOrganization"},
			           		{ActionType.RemoveChecklistFromCard, "removeChecklistFromCard"},
			           		{ActionType.RemoveFromOrganizationBoard, "removeFromOrganizationBoard"},
			           		{ActionType.RemoveMemberFromCard, "removeMemberFromCard"},
			           		{ActionType.UnconfirmedBoardInvitation, "unconfirmedBoardInvitation"},
			           		{ActionType.UnconfirmedOrganizationInvitation, "unconfirmedOrganizationInvitation"},
			           		{ActionType.UpdateBoard, "updateBoard"},
			           		{ActionType.UpdateCard, "updateCard"},
			           		{ActionType.UpdateCheckItemStateOnCard, "updateCheckItemStateOnCard"},
			           		{ActionType.UpdateChecklist, "updateChecklist"},
			           		{ActionType.UpdateMember, "updateMember"},
			           		{ActionType.UpdateOrganization, "updateOrganization"},
			           		{ActionType.UpdateCardIdList, "updateCard:idList"},
			           		{ActionType.UpdateCardClosed, "updateCard:closed"},
			           		{ActionType.UpdateCardDesc, "updateCard:desc"},
			           		{ActionType.UpdateCardName, "updateCard:name"},
			           	};
		}
		public Action()
		{
			_data = new ActionData();
		}
		internal Action(TrelloService svc, string id)
			: base(svc, id)
		{
			_data = new ActionData(svc, this);
		}
		
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_apiType = obj.TryGetString("type");
			var date = obj.TryGetString("date");
			_date = string.IsNullOrWhiteSpace(date) ? (DateTime?) null : DateTime.Parse(date);
			_memberCreatorId = obj.TryGetString("idCreatorMember");
			UpdateType();
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"type", _apiType},
			           		{"data", _data == null ? JsonValue.Null : _data.ToJson()},
			           		{"date", _date.HasValue ? _date.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			           		{"idCreatorMember", _memberCreatorId},
			           	};
			return json;
		}
		public override bool Equals(EquatableExpiringObject other)
		{
			var action = other as Action;
			if (action == null) return false;
			return Id == action.Id;
		}

		internal override void Refresh(EquatableExpiringObject entity)
		{
			var action = entity as Action;
			if (action == null) return;
			_apiType = action._apiType;
			_date = action._date;
			_memberCreatorId = action._memberCreatorId;
			UpdateType();
		}
		internal override bool Match(string id)
		{
			return Id == id;
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.GetEntity<Action>(Id);
			Refresh(entity);
		}
		protected override void PropigateSerivce()
		{
			_data.Svc = Svc;
			if (_member != null) _member.Svc = Svc;
		}

		private void UpdateType()
		{
			_type = _typeMap.Any(kvp => kvp.Value == _apiType) ? _typeMap[_apiType] : ActionType.Unknown;
		}
		private void UpdateApiType()
		{
			if (_typeMap.Any(kvp => kvp.Key == _type))
				_apiType = _typeMap[_type];
		}
	}
}

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
 
	File Name:		Board.cs
	Namespace:		Manatee.Trello
	Class Name:		Board
	Purpose:		Represents a board on Trello.com.

***************************************************************************************/
using System;
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	//{
	//   "id":"5144051cbd0da6681200201e",
	//   "name":"Manatee.Trello",
	//   "desc":"Manatee.Trello is a TrelloAPI wrapper for C#.NET.  The goal of this library is to expose the RESTful TrelloAPI as a fully object-oriented design which implements a lazy design pattern.\n\nEvery object retrieved through the TrelloService possesses properties which one would expect to find by simply using the Trello web interface.  For example, a Board has a collection of Lists, each of which have a collection of cards.  The cards also reference back to the board in which they are contained.  The service caches all of the objects it returns so that memory is not wasted on multiple instances of a single object.\n\nFurthermore, each object is self-updating with a globally configurable expiration.  To minimize calls to the RESTful API, each object only updates if it has expired, and then only when it is accessed.  If a Member object is retrieved from the service, and its Boards collection is never access, no data for those boards are ever downloaded.\n\nThis project uses my own JSON library as well: [Manatee.Json](https://trello.com/board/manatee-json/50d227239c7b29575f000f99)",
	//   "closed":false,
	//   "idOrganization":"50d4eb07a1b0902152003329",
	//   "pinned":true,
	//   "url":"https://trello.com/board/manatee-trello/5144051cbd0da6681200201e",
	//   "prefs":{
	//      "permissionLevel":"public",
	//      "voting":"members",
	//      "comments":"members",
	//      "invitations":"members",
	//      "selfJoin":false,
	//      "cardCovers":true
	//   },
	//   "labelNames":{
	//      "red":"",
	//      "orange":"",
	//      "yellow":"",
	//      "green":"",
	//      "blue":"",
	//      "purple":""
	//   }
	//}
	public class Board : JsonCompatibleExpiringObject, IEquatable<Board>
	{
		private readonly ExpiringList<Board, Action> _actions;
		private string _description;
		private bool? _isClosed;
		private bool? _isPinned;
		private bool? _isSubscribed;
		private readonly LabelNames _labelNames;
		private readonly ExpiringList<Board, List> _lists;
		private readonly ExpiringList<Board, BoardMembership> _members;
		private string _name;
		private string _organizationId;
		private Organization _organization;
		private readonly BoardPreferences _preferences;
		private readonly BoardPersonalPreferences _personalPreferences;
		private string _url;

		public IEnumerable<Action> Actions { get { return _actions; } }
		public string Description
		{
			get
			{
				VerifyNotExpired();
				return _description;
			}
			set
			{
				_description = value;
				Parameters.Add("desc", _description);
				Put();
			}
		}
		public bool? IsClosed
		{
			get
			{
				VerifyNotExpired();
				return _isClosed;
			}
			set
			{
				_isClosed = value;
				Parameters.Add("closed", _isClosed.ToLowerString());
				Put();
			}
		}
		public bool? IsPinned
		{
			get
			{
				VerifyNotExpired();
				return _isPinned;
			}
			set
			{
				_isPinned = value;
				Parameters.Add("pinned", _isPinned.ToLowerString());
				Put();
			}
		}
		public bool? IsSubscribed
		{
			get
			{
				VerifyNotExpired();
				return _isSubscribed;
			}
			set
			{
				_isPinned = value;
				Parameters.Add("subscribed", _isSubscribed.ToLowerString());
				Put();
			}
		}
		public LabelNames LabelNames { get { return _labelNames; } }
		public IEnumerable<List> Lists { get { return _lists; } }
		public IEnumerable<BoardMembership> Members { get { return _members; } }
		public string Name
		{
			get
			{
				VerifyNotExpired();
				return _name;
			}
			set
			{
				_name = value;
				Parameters.Add("name", _name);
				Put();
			}
		}
		public Organization Organization
		{
			get
			{
				VerifyNotExpired();
				return ((_organization == null) || (_organization.Id != _organizationId)) && (Svc != null)
				       	? (_organization = Svc.Retrieve<Organization>(_organizationId))
				       	: _organization;
			}
			set
			{
				_organizationId = value.Id;
				Parameters.Add("idOrganization", _organizationId);
				Put();
			}
		}
		public BoardPersonalPreferences PersonalPreferences { get { return _personalPreferences; } }
		public BoardPreferences Preferences { get { return _preferences; } }
		public string Url
		{
			get
			{
				VerifyNotExpired();
				return _url;
			}
		}

		public Board()
		{
			_actions = new ExpiringList<Board, Action>(this);
			_labelNames = new LabelNames(null, this);
			_lists = new ExpiringList<Board, List>(this);
			_members = new ExpiringList<Board, BoardMembership>(this);
			_personalPreferences = new BoardPersonalPreferences(null, this);
			_preferences = new BoardPreferences(null, this);
		}
		internal Board(TrelloService svc, string id)
			: base(svc, id)
		{
			_actions = new ExpiringList<Board, Action>(svc, this);
			_labelNames = new LabelNames(svc, this);
			_lists = new ExpiringList<Board, List>(svc, this);
			_members = new ExpiringList<Board, BoardMembership>(svc, this);
			_personalPreferences = new BoardPersonalPreferences(svc, this);
			_preferences = new BoardPreferences(svc, this);
		}

		public List AddList(string title, Position position = null)
		{
			var request = new Request<List>(this);
			Parameters.Add("name", title);
			Parameters.Add("idBoard", Id);
			if ((position != null) && position.IsValid)
				Parameters.Add("pos", position);
			var list = Svc.PostAndCache(request);
			_lists.MarkForUpdate();
			_actions.MarkForUpdate();
			return list;
		}
		public void AddOrUpdateMember(Member member, BoardMembershipType type = BoardMembershipType.Normal)
		{
			var request = new Request<Member>(new ExpiringObject[]{this, member}, this);
			Parameters.Add("type", type.ToLowerString());
			Svc.PutAndCache(request);
			_members.MarkForUpdate();
			_actions.MarkForUpdate();
		}
		public void MarkAsViewed()
		{
			var request = new Request<Board>(new ExpiringObject[] {this}, urlExtension: "markAsViewed");
			Svc.PostAndCache(request);
			_actions.MarkForUpdate();
		}
		private void InviteMember(Member member, BoardMembershipType type = BoardMembershipType.Normal)
		{
			throw new NotSupportedException("Inviting members to boards is not yet supported by the Trello API.");
		}
		public void RemoveMember(Member member)
		{
			Svc.DeleteFromCache(new Request<Board>(new ExpiringObject[] {this, member}));
		}
		private void RescindInvitation(Member member)
		{
			throw new NotSupportedException("Inviting members to boards is not yet supported by the Trello API.");
		}
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			_description = obj.TryGetString("desc");
			_isClosed = obj.TryGetBoolean("closed");
			_isPinned = obj.TryGetBoolean("pinned");
			_isSubscribed = obj.TryGetBoolean("subscribed");
			_name = obj.TryGetString("name");
			_organizationId = obj.TryGetString("idOrganization");
			_url = obj.TryGetString("url");
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"id", Id},
			           		{"desc", _description},
			           		{"closed", _isClosed.HasValue ? _isClosed.Value : JsonValue.Null},
			           		{"pinned", _isPinned.HasValue ? _isPinned.Value : JsonValue.Null},
			           		{"subscribed", _isSubscribed.HasValue ? _isSubscribed.Value : JsonValue.Null},
			           		{"labelNames", _labelNames != null ? _labelNames.ToJson() : JsonValue.Null},
			           		{"name", _name},
			           		{"idOrganization", _organizationId},
			           		{"prefs", _preferences != null ? _preferences.ToJson() : JsonValue.Null},
			           		{"myprefs", _personalPreferences != null ? _personalPreferences.ToJson() : JsonValue.Null},
			           		{"url", _url}
			           	};
			return json;
		}
		public bool Equals(Board other)
		{
			return Id == other.Id;
		}

		internal override void Refresh(ExpiringObject entity)
		{
			var board = entity as Board;
			if (board == null) return;
			_description = board._description;
			_isClosed = board._isClosed;
			_isPinned = board._isPinned;
			_name = board._name;
			_organizationId = board._organizationId;
			_url = board._url;
		}
		internal override bool Match(string id)
		{
			return Id == id;
		}

		protected override void Get()
		{
			var entity = Svc.Api.Get(new Request<Board>(Id));
			Refresh(entity);
		}
		protected override void PropigateSerivce()
		{
			_actions.Svc = Svc;
			_labelNames.Svc = Svc;
			_lists.Svc = Svc;
			_members.Svc = Svc;
			_personalPreferences.Svc = Svc;
			_preferences.Svc = Svc;
			if (_organization != null) _organization.Svc = Svc;
		}

		private void Put()
		{
			Svc.PutAndCache(new Request<Board>(this));
			_actions.MarkForUpdate();
		}
	}
}

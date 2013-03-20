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
 
	File Name:		Card.cs
	Namespace:		Manatee.Trello
	Class Name:		Card
	Purpose:		Represents a card on Trello.com.

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
	//   "id":"5144071650af56251f001927",
	//   "badges":{
	//      "votes":0,
	//      "viewingMemberVoted":false,
	//      "subscribed":false,
	//      "fogbugz":"",
	//      "due":null,
	//      "description":true,
	//      "comments":0,
	//      "checkItemsChecked":1,
	//      "checkItems":4,
	//      "attachments":0
	//   },
	//   "checkItemStates":[
	//      {
	//         "idCheckItem":"514463bfd02ebee350000d1c",
	//         "state":"complete"
	//      }
	//   ],
	//   "closed":false,
	//   "desc":"Allow others to contribute to project once the basics are up and running.",
	//   "due":null,
	//   "idBoard":"5144051cbd0da6681200201e",
	//   "idChecklists":[
	//      "514463bce0807abe320028a2"
	//   ],
	//   "idList":"5144051cbd0da6681200201f",
	//   "idMembers":[

	//   ],
	//   "idShort":6,
	//   "idAttachmentCover":null,
	//   "manualCoverAttachment":false,
	//   "labels":[
	//      {
	//         "color":"green",
	//         "name":""
	//      },
	//      {
	//         "color":"yellow",
	//         "name":""
	//      }
	//   ],
	//   "name":"Publish Beta to SourceForge",
	//   "pos":393215,
	//   "url":"https://trello.com/card/publish-beta-to-sourceforge/5144051cbd0da6681200201e/6"
	//}
	public class Card : EntityBase, IEquatable<Card>
	{
		private readonly ExpiringList<Card, Action> _actions;
		private string _attachmentCoverId;
		private readonly ExpiringList<Card, Attachment> _attachments;
		private readonly Badges _badges;
		private string _boardId;
		private Board _board;
		private readonly ExpiringList<Card, CheckItemState> _checkItemStates;
		private readonly ExpiringList<Card, CheckList> _checkLists;
		private string _description;
		private DateTime? _dueDate;
		private bool? _isClosed;
		private readonly ExpiringList<Card, Label> _labels;
		private string _listId;
		private List _list;
		private bool? _manualCoverAttachment;
		private readonly ExpiringList<Card, Member> _members;
		private string _name;
		private Position _position;
		private int? _shortId;
		private string _url;
		private readonly ExpiringList<Card, VotingMember> _votingMembers;

		public IEnumerable<Action> Actions { get { return _actions; } }
		public string AttachmentCoverId
		{
			get
			{
				VerifyNotExpired();
				return _attachmentCoverId;
			}
		}
		public IEnumerable<Attachment> Attachments { get { return _attachments; } }
		public Badges Badges { get { return _badges; } }
		public Board Board
		{
			get
			{
				VerifyNotExpired();
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null) ? (_board = Svc.Retrieve<Board>(_boardId)) : _board;
			}
		}
		public IEnumerable<CheckItemState> CheckItemStates { get { return _checkItemStates; } }
		public IEnumerable<CheckList> CheckLists { get { return _checkLists; } }
		public string Description
		{
			get
			{
				VerifyNotExpired();
				return _description;
			}
			set { _description = value; }
		}
		public DateTime? DueDate
		{
			get
			{
				VerifyNotExpired();
				return _dueDate;
			}
			set { _dueDate = value; }
		}
		public bool? IsClosed
		{
			get
			{
				VerifyNotExpired();
				return _isClosed;
			}
			set { _isClosed = value; }
		}
		public IEnumerable<Label> Labels { get { return _labels; } }
		public List List
		{
			get
			{
				VerifyNotExpired();
				return ((_list == null) || (_list.Id != _listId)) && (Svc != null) ? (_list = Svc.Retrieve<List>(_listId)) : _list;
			}
		}
		public bool? ManualCoverAttachment
		{
			get
			{
				VerifyNotExpired();
				return _manualCoverAttachment;
			}
			set { _manualCoverAttachment = value; }
		}
		public IEnumerable<Member> Members { get { return _members; } }
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
			}
		}
		public Position Position
		{
			get
			{
				VerifyNotExpired();
				return _position;
			}
			set { _position = value; }
		}
		public int? ShortId
		{
			get
			{
				VerifyNotExpired();
				return _shortId;
			}
		}
		public string Url
		{
			get
			{
				VerifyNotExpired();
				return _url;
			}
		}
		private IEnumerable<Member> VotingMembers { get { return _votingMembers; } }

		public Card()
		{
			_actions = new ExpiringList<Card, Action>(this);
			_attachments = new ExpiringList<Card, Attachment>(this);
			_badges = new Badges(null, this);
			_checkItemStates = new ExpiringList<Card, CheckItemState>(this);
			_checkLists = new ExpiringList<Card, CheckList>(this);
			_labels = new ExpiringList<Card, Label>(this);
			_members = new ExpiringList<Card, Member>(this);
			_votingMembers = new ExpiringList<Card, VotingMember>(this);
			Parameters = new RestParameterCollection();
		}
		internal Card(TrelloService svc, string id)
			: base(svc, id)
		{
			_actions = new ExpiringList<Card, Action>(svc, this);
			_attachments = new ExpiringList<Card, Attachment>(this);
			_badges = new Badges(svc, this);
			_checkItemStates = new ExpiringList<Card, CheckItemState>(svc, this);
			_checkLists = new ExpiringList<Card, CheckList>(svc, this);
			_labels = new ExpiringList<Card, Label>(svc, this);
			_members = new ExpiringList<Card, Member>(svc, this);
			_votingMembers = new ExpiringList<Card, VotingMember>(this);
		}

		//public CheckList AddCheckList(string title)
		//{
		//    var request = new CreateCheckListInCardRequest(this, title);
		//    var checkList = Svc.PostAndCache<Card, CheckList, CreateCheckListInCardRequest>(request);
		//    checkList.Svc = Svc;
		//    _checkLists.MarkForUpdate();
		//    return checkList;
		//}
		//public void AddComment(string comment)
		//{
		//    var request = new AddCommentToCardRequest(this, comment);
		//    Svc.PostAndCache<Card, PostComment, AddCommentToCardRequest>(request);
		//    _actions.MarkForUpdate();
		//}
		//public void ApplyLabel(LabelColor color)
		//{
		//    var request = new ApplyLabelToCardRequest(this, color);
		//    Svc.PostAndCache<Card, Label, ApplyLabelToCardRequest>(request);
		//    _actions.MarkForUpdate();
		//}
		public void Archive()
		{

		}
		public void AssignDueDate(DateTime? date)
		{

		}
		//public void AssignMember(Member member)
		//{
		//    var request = new AddMemberToCardRequest(this, member.Id);
		//    Svc.PostAndCache<Card, Member, AddMemberToCardRequest>(request);
		//    _members.MarkForUpdate();
		//}
		public void AttachFile(string name, string uri)
		{

		}
		public void Delete()
		{

		}
		public void Move(Board board, List list, int? position = null)
		{

		}
		public void RemoveLabel(LabelColor color)
		{
			
		}
		public void RemoveMember(Member member)
		{
			
		}
		public void Rename(string name)
		{

		}
		public void SendToBoard()
		{

		}
		public void Subscribe()
		{

		}
		public void Unsubscribe()
		{

		}
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			_attachmentCoverId = obj.TryGetString("idAttachmentCover");
			_boardId = obj.TryGetString("idBoard");
			_description = obj.TryGetString("desc");
			var due = obj.TryGetString("due");
			_dueDate = string.IsNullOrWhiteSpace(due) ? (DateTime?)null : DateTime.Parse(due);
			_isClosed = obj.TryGetBoolean("closed");
			_listId = obj.TryGetString("idList");
			_manualCoverAttachment = obj.TryGetBoolean("manualCoverAttachment");
			_name = obj.TryGetString("name");
			_position = new Position(PositionValue.Unknown);
			if (obj.ContainsKey("pos"))
				_position.FromJson(obj["pos"]);
			_shortId = (int?) obj.TryGetNumber("idShort");
			_url = obj.TryGetString("url");
		}
		public override JsonValue ToJson()
		{
			VerifyNotExpired();
			var json = new JsonObject
			           	{
			           		{"id", Id},
			           		{"idAttachmentCover", _attachmentCoverId},
			           		{"badges", _badges != null ? _badges.ToJson() : JsonValue.Null},
			           		{"idBoard", _boardId},
			           		{"checkItemStates", _checkItemStates.ToJson()},
			           		{"desc", _description},
			           		{"due", _dueDate.HasValue ? _dueDate.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			           		{"closed", _isClosed.HasValue ? _isClosed.Value : JsonValue.Null},
			           		{"labels", _labels.ToJson()},
			           		{"idList", _listId},
			           		{"manualCoverAttachment", _manualCoverAttachment.HasValue ? _manualCoverAttachment.Value : JsonValue.Null},
			           		{"name", _name},
			           		{"pos", _position.ToJson()},
			           		{"idShort", _shortId.HasValue ? _shortId.Value : JsonValue.Null},
			           		{"url", _url}
			           	};
			return json;
		}
		public bool Equals(Card other)
		{
			return Id == other.Id;
		}

		internal override void Refresh(ExpiringObject entity)
		{
			var card = entity as Card;
			if (card == null) return;
			_attachmentCoverId = card._attachmentCoverId;
			_boardId = card._boardId;
			_description = card._description;
			_dueDate = card._dueDate;
			_isClosed = card._isClosed;
			_manualCoverAttachment = card._manualCoverAttachment;
			_name = card._name;
			_position = card._position;
			_shortId = card._shortId;
			_url = card._url;
		}
		internal override bool Match(string id)
		{
			return Id == id;
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.Get(new Request<Card>(Id));
			Refresh(entity);
		}
		protected override void PropigateSerivce()
		{
			_actions.Svc = Svc;
			_attachments.Svc = Svc;
			_badges.Svc = Svc;
			_checkItemStates.Svc = Svc;
			_checkLists.Svc = Svc;
			_labels.Svc = Svc;
			_members.Svc = Svc;
			if (_board != null) _board.Svc = Svc;
			if (_list != null) _list.Svc = Svc;
		}
	}
}

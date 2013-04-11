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
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;

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
	/// <summary>
	/// Represents a card.
	/// </summary>
	public class Card : JsonCompatibleExpiringObject, IEquatable<Card>
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
		private bool? _isSubscribed;
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

		///<summary>
		/// Enumerates all actions associated with this card.
		///</summary>
		public IEnumerable<Action> Actions { get { return _actions; } }
		/// <summary>
		/// Gets the ID of the attachment cover image.
		/// </summary>
		public string AttachmentCoverId
		{
			get
			{
				VerifyNotExpired();
				return _attachmentCoverId;
			}
		}
		/// <summary>
		/// Enumerates the cards attachments.
		/// </summary>
		public IEnumerable<Attachment> Attachments { get { return _attachments; } }
		/// <summary>
		/// Gets the badges summarizing the card's contents.
		/// </summary>
		public Badges Badges { get { return _badges; } }
		/// <summary>
		/// Gets the board which contains the card.
		/// </summary>
		public Board Board
		{
			get
			{
				VerifyNotExpired();
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null) ? (_board = Svc.Get(Svc.RequestProvider.Create<Board>(_boardId))) : _board;
			}
		}
		/// <summary>
		/// Enumerates the checklist items and their states.
		/// </summary>
		public IEnumerable<CheckItemState> CheckItemStates { get { return _checkItemStates; } }
		/// <summary>
		/// Enumerates the card's checklists.
		/// </summary>
		public IEnumerable<CheckList> CheckLists { get { return _checkLists; } }
		/// <summary>
		/// Enumerates the card's comments.
		/// </summary>
		public IEnumerable<CommentCardAction> Comments { get { return _actions.OfType<CommentCardAction>(); } }
		/// <summary>
		/// Gets and sets the card's description.
		/// </summary>
		public string Description
		{
			get
			{
				VerifyNotExpired();
				return _description;
			}
			set
			{
				if (_description == value) return;
				_description = value ?? string.Empty;
				Parameters.Add("desc", _description);
				Put();
			}
		}
		/// <summary>
		/// Gets and sets the card's due date.
		/// </summary>
		public DateTime? DueDate
		{
			get
			{
				VerifyNotExpired();
				return _dueDate;
			}
			set
			{
				if (_dueDate == value) return;
				Validate.Nullable(value);
				_dueDate = value;
				Parameters.Add("due", _dueDate);
				Put();
			}
		}
		/// <summary>
		/// Gets and sets whether a card has been archived.
		/// </summary>
		public bool? IsClosed
		{
			get
			{
				VerifyNotExpired();
				return _isClosed;
			}
			set
			{
				if (_isClosed == value) return;
				Validate.Nullable(value);
				_isClosed = value;
				Parameters.Add("closed", _isClosed.ToLowerString());
				Put();
			}
		}
		/// <summary>
		/// Gets and sets whether the current member is subscribed to this card.
		/// </summary>
		public bool? IsSubscribed
		{
			get
			{
				VerifyNotExpired();
				return _isSubscribed;
			}
			set
			{
				if (_isSubscribed == value) return;
				Validate.Nullable(value);
				_isSubscribed = value;
				Parameters.Add("subscribed", _isSubscribed.ToLowerString());
				Put();
			}
		}
		/// <summary>
		/// Enumerates the labels applied to this card.
		/// </summary>
		public IEnumerable<Label> Labels { get { return _labels; } }
		/// <summary>
		/// Gets the list which contains this card.
		/// </summary>
		public List List
		{
			get
			{
				VerifyNotExpired();
				return ((_list == null) || (_list.Id != _listId)) && (Svc != null) ? (_list = Svc.Get(Svc.RequestProvider.Create<List>(_listId))) : _list;
			}
		}
		/// <summary>
		/// Gets whether the cover attachment was manually selected ?
		/// </summary>
		public bool? ManualCoverAttachment
		{
			get
			{
				VerifyNotExpired();
				return _manualCoverAttachment;
			}
		}
		/// <summary>
		/// Enumerates the members assigned to this card.
		/// </summary>
		public IEnumerable<Member> Members { get { return _members; } }
		/// <summary>
		/// Gets and sets the card's name
		/// </summary>
		public string Name
		{
			get
			{
				VerifyNotExpired();
				return _name;
			}
			set
			{
				if (_name == value) return;
				Validate.NonEmptyString(value);
				_name = value;
				Parameters.Add("name", _name);
				Put();
			}
		}
		/// <summary>
		/// Gets and sets the card's position.
		/// </summary>
		public Position Position
		{
			get
			{
				VerifyNotExpired();
				return _position;
			}
			set
			{
				if (_position == value) return;
				Validate.Position(value);
				_position = value;
				Parameters.Add("pos", _position);
				Put();
			}
		}
		/// <summary>
		/// Gets the cards short ID.
		/// </summary>
		public int? ShortId { get { return _shortId; } }
		/// <summary>
		/// Gets the URL for this card.
		/// </summary>
		public string Url { get { return _url; } }
		/// <summary>
		/// Enumerates the members who have voted for this card.
		/// </summary>
		public IEnumerable<Member> VotingMembers { get { return _votingMembers; } }

		internal override string Key { get { return "cards"; } }

		/// <summary>
		/// Creates a new instance of the Card class.
		/// </summary>
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
		}
		internal Card(ITrelloRest svc, string id)
			: base(svc, id)
		{
			_actions = new ExpiringList<Card, Action>(svc, this);
			_attachments = new ExpiringList<Card, Attachment>(svc, this);
			_badges = new Badges(svc, this);
			_checkItemStates = new ExpiringList<Card, CheckItemState>(svc, this);
			_checkLists = new ExpiringList<Card, CheckList>(svc, this);
			_labels = new ExpiringList<Card, Label>(svc, this);
			_members = new ExpiringList<Card, Member>(svc, this);
			_votingMembers = new ExpiringList<Card, VotingMember>(svc, this);
		}

		/// <summary>
		/// Adds an attachment to the card.
		/// </summary>
		/// <returns>The attachment object.</returns>
		internal Attachment AddAttachment()
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Adds a checklist to the card.
		/// </summary>
		/// <param name="name">The name of the checklist</param>
		/// <param name="position">The desired position of the checklist.  Default is Bottom.  Invalid positions are ignored.</param>
		/// <returns>The checklist.</returns>
		public CheckList AddCheckList(string name, Position position = null)
		{
			if (Svc == null) return null;
			Validate.NonEmptyString(name);
			var request = Svc.RequestProvider.Create<CheckList>(new ExpiringObject[] {new CheckList()}, this);
			Parameters.Add("name", name);
			if ((position != null) && position.IsValid)
				Parameters.Add("position", position);
			Parameters.Add("idCard", Id);
			var checkList = Svc.Post(request);
			_checkLists.MarkForUpdate();
			return checkList;
		}
		/// <summary>
		/// Adds a comment to the card.
		/// </summary>
		/// <param name="comment"></param>
		public void AddComment(string comment)
		{
			if (Svc == null) return;
			Validate.NonEmptyString(comment);
			var request = Svc.RequestProvider.Create<Card>(new ExpiringObject[] { this, new Action() }, this, "comments");
			Parameters.Add("text", comment);
			Svc.Post(request);
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Applies a lable to the card.
		/// </summary>
		/// <param name="color">The color of the label.</param>
		public void ApplyLabel(LabelColor color)
		{
			if (Svc == null) return;
			var request = Svc.RequestProvider.Create<Card>(new ExpiringObject[] {this, new Label()}, this);
			Parameters.Add("value", color.ToLowerString());
			Svc.Post(request);
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Assigns a member to the card.
		/// </summary>
		/// <param name="member">The member to assign.</param>
		public void AssignMember(Member member)
		{
			if (Svc == null) return;
			Validate.Entity(member);
			var request = Svc.RequestProvider.Create<Card>(new ExpiringObject[] {this, new Member()}, this);
			Parameters.Add("value", member.Id);
			Svc.Post(request);
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Marks all notifications associated to this card as read.
		/// </summary>
		public void ClearNotifications()
		{
			if (Svc == null) return;
			var request = Svc.RequestProvider.Create<Card>(new ExpiringObject[] { this }, urlExtension: "markAssociatedNotificationsRead");
			Svc.Post(request);
		}
		/// <summary>
		/// Deletes the card.  This cannot be undone.
		/// </summary>
		public void Delete()
		{
			if (Svc == null) return;
			var request = Svc.RequestProvider.Create<Card>(Id);
			Svc.Delete(request);
		}
		/// <summary>
		/// Moves the card to another board/list/position.
		/// </summary>
		/// <param name="board">The destination board.</param>
		/// <param name="list">The destination list.</param>
		/// <param name="position">The destination position.  Default is Bottom.  Invalid positions are ignored.</param>
		public void Move(Board board, List list, Position position = null)
		{
			if (Svc == null) return;
			Validate.Entity(board);
			Validate.Entity(list);
			if (!board.Lists.Contains(list))
				throw new InvalidOperationException("The indicated list does not exist on the indicated board.");
			if (_boardId != board.Id)
				Parameters.Add("idBoard", board.Id);
			if (_boardId != list.Id)
				Parameters.Add("idList", list.Id);
			if (position != null)
				Parameters.Add("pos", position);
			Svc.Put(Svc.RequestProvider.Create<Card>(this));
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Removes a label from a card.
		/// </summary>
		/// <param name="color"></param>
		public void RemoveLabel(LabelColor color)
		{
			if (Svc == null) return;
			var request = Svc.RequestProvider.Create<Card>(new ExpiringObject[] {this, new Label()}, urlExtension: color.ToLowerString());
			Svc.Delete(request);
		}
		/// <summary>
		/// Removes (unassigns) a member from a card.
		/// </summary>
		/// <param name="member"></param>
		public void RemoveMember(Member member)
		{
			if (Svc == null) return;
			Validate.Entity(member);
			var request = Svc.RequestProvider.Create<Card>(new ExpiringObject[] { this, member });
			Svc.Delete(request);
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
			Id = obj.TryGetString("id");
			_attachmentCoverId = obj.TryGetString("idAttachmentCover");
			_boardId = obj.TryGetString("idBoard");
			_description = obj.TryGetString("desc");
			var due = obj.TryGetString("due");
			_dueDate = string.IsNullOrWhiteSpace(due) ? (DateTime?)null : DateTime.Parse(due);
			_isClosed = obj.TryGetBoolean("closed");
			_isSubscribed = obj.TryGetBoolean("subscribed");
			_listId = obj.TryGetString("idList");
			_manualCoverAttachment = obj.TryGetBoolean("manualCoverAttachment");
			_name = obj.TryGetString("name");
			_position = new Position(PositionValue.Unknown);
			if (obj.ContainsKey("pos"))
				_position.FromJson(obj["pos"]);
			_shortId = (int?) obj.TryGetNumber("idShort");
			_url = obj.TryGetString("url");
			_isInitialized = true;
		}
		/// <summary>
		/// Converts an object to a JsonValue.
		/// </summary>
		/// <returns>
		/// The JsonValue representation of the object.
		/// </returns>
		public override JsonValue ToJson()
		{
			if (!_isInitialized) VerifyNotExpired();
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
			           		{"subscribed", _isSubscribed.HasValue ? _isSubscribed.Value : JsonValue.Null},
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
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Card other)
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
			if (!(obj is Card)) return false;
			return Equals((Card) obj);
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
			return base.GetHashCode();
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
			_isSubscribed = card._isSubscribed;
			_manualCoverAttachment = card._manualCoverAttachment;
			_name = card._name;
			_position = card._position;
			_shortId = card._shortId;
			_url = card._url;
			_isInitialized = true;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			var entity = Svc.Get(Svc.RequestProvider.Create<Card>(Id));
			Refresh(entity);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateSerivce()
		{
			_actions.Svc = Svc;
			_attachments.Svc = Svc;
			_badges.Svc = Svc;
			_checkItemStates.Svc = Svc;
			_checkLists.Svc = Svc;
			_labels.Svc = Svc;
			_members.Svc = Svc;
			_votingMembers.Svc = Svc;
			if (_board != null) _board.Svc = Svc;
			if (_list != null) _list.Svc = Svc;
		}

		private void Put()
		{
			if (Svc == null)
			{
				Parameters.Clear();
				return;
			}
			var request = Svc.RequestProvider.Create<Card>(this);
			Svc.Put(request);
			_actions.MarkForUpdate();
		}
	}
}

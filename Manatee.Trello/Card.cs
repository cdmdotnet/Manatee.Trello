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
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a card.
	/// </summary>
	public class Card : ExpiringObject, IEquatable<Card>, IComparable<Card>
	{
		private IJsonCard _jsonCard;
		private readonly ExpiringList<Action, IJsonAction> _actions;
		private readonly ExpiringList<Attachment, IJsonAttachment> _attachments;
		private readonly Badges _badges;
		private Board _board;
		private readonly ExpiringList<CheckList, IJsonCheckList> _checkLists;
		private readonly ExpiringList<Label, IJsonLabel> _labels;
		private List _list;
		private readonly ExpiringList<Member, IJsonMember> _members;
		private Position _position;
		private readonly ExpiringList<VotingMember, IJsonMember> _votingMembers;
		private bool _isDeleted;

		///<summary>
		/// Enumerates all actions associated with this card.
		///</summary>
		public IEnumerable<Action> Actions { get { return _isDeleted ? null : _actions; } }
		/// <summary>
		/// Gets the ID of the attachment cover image.
		/// </summary>
		public string AttachmentCoverId
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return (_jsonCard == null) ? null : _jsonCard.IdAttachmentCover;
			}
		}
		/// <summary>
		/// Enumerates the cards attachments.
		/// </summary>
		public IEnumerable<Attachment> Attachments { get { return _isDeleted ? Enumerable.Empty<Attachment>() : _attachments; } }
		/// <summary>
		/// Gets the badges summarizing the card's contents.
		/// </summary>
		public Badges Badges { get { return _isDeleted ? null : _badges; } }
		/// <summary>
		/// Gets the board which contains the card.
		/// </summary>
		public Board Board
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				if (_jsonCard == null) return null;
				return ((_board == null) || (_board.Id != _jsonCard.IdBoard)) && (Svc != null)
				       	? (_board = Svc.Retrieve<Board>(_jsonCard.IdBoard))
				       	: _board;
			}
		}
		/// <summary>
		/// Enumerates the card's checklists.
		/// </summary>
		public IEnumerable<CheckList> CheckLists { get { return _isDeleted ? Enumerable.Empty<CheckList>() : _checkLists; } }
		internal ExpiringList<CheckList, IJsonCheckList> CheckListsList { get { return _checkLists; } }
		/// <summary>
		/// Enumerates the card's comments.
		/// </summary>
		public IEnumerable<CommentCardAction> Comments { get { return _isDeleted ? Enumerable.Empty<CommentCardAction>() : _actions.OfType<CommentCardAction>(); } }
		/// <summary>
		/// Gets or sets the card's description.
		/// </summary>
		public string Description
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return (_jsonCard == null) ? null : _jsonCard.Desc;
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				if (_jsonCard == null) return;
				if (_jsonCard.Desc == value) return;
				_jsonCard.Desc = value ?? string.Empty;
				Parameters.Add("desc", _jsonCard.Desc);
				Put();
			}
		}
		/// <summary>
		/// Gets or sets the card's due date.
		/// </summary>
		public DateTime? DueDate
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return (_jsonCard == null) ? null : _jsonCard.Due;
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonCard == null) return;
				if (_jsonCard.Due == value) return;
				_jsonCard.Due = value;
				Parameters.Add("due", _jsonCard.Due.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
				Put();
			}
		}
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonCard != null ? _jsonCard.Id : base.Id; }
			internal set
			{
				if (_jsonCard != null)
					_jsonCard.Id = value;
				base.Id = value;
			}
		}
		/// <summary>
		/// Gets or sets whether a card has been archived.
		/// </summary>
		public bool? IsClosed
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return (_jsonCard == null) ? null : _jsonCard.Closed;
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonCard == null) return;
				if (_jsonCard.Closed == value) return;
				_jsonCard.Closed = value;
				Parameters.Add("closed", _jsonCard.Closed.ToLowerString());
				Put();
			}
		}
		/// <summary>
		/// Gets or sets whether the current member is subscribed to this card.
		/// </summary>
		public bool? IsSubscribed
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return (_jsonCard == null) ? null : _jsonCard.Subscribed;
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonCard == null) return;
				if (_jsonCard.Subscribed == value) return;
				_jsonCard.Subscribed = value;
				Parameters.Add("subscribed", _jsonCard.Subscribed.ToLowerString());
				Put();
			}
		}
		/// <summary>
		/// Enumerates the labels applied to this card.
		/// </summary>
		public IEnumerable<Label> Labels { get { return _isDeleted ? Enumerable.Empty<Label>() : _labels; } }
		/// <summary>
		/// Gets the date of last activity for this card.
		/// </summary>
		public DateTime? LastActivityDate { get { return _jsonCard.DateLastActivity; } }
		/// <summary>
		/// Gets the list which contains this card.
		/// </summary>
		public List List
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				if (_jsonCard == null) return null;
				return ((_list == null) || (_list.Id != _jsonCard.IdList)) && (Svc != null)
				       	? (_list = Svc.Retrieve<List>(_jsonCard.IdList))
				       	: _list;
			}
		}
		/// <summary>
		/// Gets whether the cover attachment was manually selected ?
		/// </summary>
		public bool? ManualCoverAttachment
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return (_jsonCard == null) ? null : _jsonCard.ManualCoverAttachment;
			}
		}
		/// <summary>
		/// Enumerates the members assigned to this card.
		/// </summary>
		public IEnumerable<Member> Members { get { return _isDeleted ? Enumerable.Empty<Member>() : _members; } }
		/// <summary>
		/// Gets or sets the card's name
		/// </summary>
		public string Name
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return (_jsonCard == null) ? null : _jsonCard.Name;
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Validator.NonEmptyString(value);
				if (_jsonCard == null) return;
				if (_jsonCard.Name == value) return;
				_jsonCard.Name = value;
				Parameters.Add("name", _jsonCard.Name);
				Put();
			}
		}
		/// <summary>
		/// Gets or sets the card's position.
		/// </summary>
		public Position Position
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return _position;
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Validator.Position(value);
				if (_position == value) return;
				_position = value;
				Parameters.Add("pos", _position);
				Put();
				MarkForUpdate();
				if (_list != null)
					_list.CardsList.MarkForUpdate();
			}
		}
		/// <summary>
		/// Gets the card's short ID.
		/// </summary>
		public int? ShortId { get { return (_jsonCard == null) ? null : _jsonCard.IdShort; } }
		/// <summary>
		/// Gets the URL for this card.
		/// </summary>
		public string Url { get { return (_jsonCard == null) ? null : _jsonCard.Url; } }
		/// <summary>
		/// Enumerates the members who have voted for this card.
		/// </summary>
		public IEnumerable<Member> VotingMembers { get { return _isDeleted ? Enumerable.Empty<Member>() : _votingMembers; } }
		/// <summary>
		/// Sets whether the card should generate a notification email when the due date is near.
		/// </summary>
		public bool WarnWhenUpcoming
		{
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Parameters.Add("warnWhenUpcoming", value);
				Put();
			}
		}

		internal static string TypeKey { get { return "cards"; } }
		internal static string TypeKey2 { get { return "cards"; } }
		internal override string Key { get { return TypeKey; } }
		internal override string Key2 { get { return TypeKey2; } }

		/// <summary>
		/// Creates a new instance of the Card class.
		/// </summary>
		public Card()
		{
			_jsonCard = new InnerJsonCard();
			_actions = new ExpiringList<Action, IJsonAction>(this, Action.TypeKey) {Fields = "id"};
			_attachments = new ExpiringList<Attachment, IJsonAttachment>(this, Attachment.TypeKey) {Fields = "all"};
			_badges = new Badges(this);
			_checkLists = new ExpiringList<CheckList, IJsonCheckList>(this, CheckList.TypeKey) {Fields = "id"};
			_labels = new ExpiringList<Label, IJsonLabel>(this, Label.TypeKey);
			_members = new ExpiringList<Member, IJsonMember>(this, Member.TypeKey) {Fields = "id"};
			_votingMembers = new ExpiringList<VotingMember, IJsonMember>(this, VotingMember.TypeKey) {Fields = "id"};
		}

		/// <summary>
		/// Adds an attachment to the card.
		/// </summary>
		/// <returns>The attachment object.</returns>
		public Attachment AddAttachment(string name, string url)
		{
			if (Svc == null) return null;
			Validator.Writable();
			Validator.Url(url);
			if (string.IsNullOrWhiteSpace(name))
			{
				name = url.Split('/').LastOrDefault();
				if (string.IsNullOrWhiteSpace(name))
					name = url;
			}
			var attachment = new Attachment();
			var endpoint = EndpointGenerator.Default.Generate(this, attachment);
			var request = RequestProvider.Create(endpoint.ToString());
			request.AddParameter("name", name);
			request.AddParameter("url", url);
			attachment.ApplyJson(Api.Post<IJsonAttachment>(request));
			attachment.Owner = this;
			_attachments.MarkForUpdate();
			_actions.MarkForUpdate();
			return attachment;
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
			if (_isDeleted) return null;
			Validator.Writable();
			Validator.NonEmptyString(name);
			var checkList = new CheckList();
			var endpoint = EndpointGenerator.Default.Generate(checkList);
			var request = RequestProvider.Create(endpoint.ToString());
			request.AddParameter("name", name);
			if ((position != null) && position.IsValid)
				request.AddParameter("pos", position);
			request.AddParameter("idCard", Id);
			checkList.ApplyJson(Api.Post<IJsonCheckList>(request));
			checkList.Svc = Svc;
			_checkLists.MarkForUpdate();
			_actions.MarkForUpdate();
			return checkList;
		}
		/// <summary>
		/// Adds a comment to the card.
		/// </summary>
		/// <param name="comment"></param>
		public void AddComment(string comment)
		{
			if (Svc == null) return;
			if (_isDeleted) return;
			Validator.Writable();
			Validator.NonEmptyString(comment);
			var endpoint = EndpointGenerator.Default.Generate(this, new Action());
			endpoint.Append("comments");
			var request = RequestProvider.Create(endpoint.ToString());
			request.AddParameter("text", comment);
			Api.Post<IJsonAction>(request);
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Applies a lable to the card.
		/// </summary>
		/// <param name="color">The color of the label.</param>
		public void ApplyLabel(LabelColor color)
		{
			if (Svc == null) return;
			if (_isDeleted) return;
			Validator.Writable();
			var endpoint = EndpointGenerator.Default.Generate(this, new Label());
			var request = RequestProvider.Create(endpoint.ToString());
			request.AddParameter("value", color.ToLowerString());
			Api.Post<IJsonCard>(request);
			_labels.MarkForUpdate();
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Assigns a member to the card.
		/// </summary>
		/// <param name="member">The member to assign.</param>
		public void AssignMember(Member member)
		{
			if (Svc == null) return;
			if (_isDeleted) return;
			Validator.Writable();
			Validator.Entity(member);
			var endpoint = EndpointGenerator.Default.Generate(this);
			endpoint.Append("idMembers");
			var request = RequestProvider.Create(endpoint.ToString());
			request.AddParameter("value", member.Id);
			Api.Post<List<IJsonMember>>(request);
			_members.MarkForUpdate();
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Marks all notifications associated to this card as read.
		/// </summary>
		public void ClearNotifications()
		{
			if (Svc == null) return;
			if (_isDeleted) return;
			Validator.Writable();
			var endpoint = EndpointGenerator.Default.Generate(this);
			endpoint.Append("markAssociatedNotificationsRead");
			var request = RequestProvider.Create(endpoint.ToString());
			Api.Post<IJsonCard>(request);
		}
		/// <summary>
		/// Deletes the card.  This cannot be undone.
		/// </summary>
		public void Delete()
		{
			if (Svc == null) return;
			if (_isDeleted) return;
			Validator.Writable();
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = RequestProvider.Create(endpoint.ToString());
			Api.Delete<IJsonCard>(request);
			if (_list != null)
				_list.CardsList.MarkForUpdate();
			if (Svc.Configuration.Cache != null)
				Svc.Configuration.Cache.Remove(this);
			_isDeleted = true;
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
			if (_isDeleted) return;
			Validator.Writable();
			Validator.Entity(board);
			Validator.Entity(list);
			if (!board.Lists.Contains(list))
				Log.Error(new InvalidOperationException(string.Format("Board '{0}' does not contain list with ID '{1}'.", board.Name, list.Id)));
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = RequestProvider.Create(endpoint.ToString());
			if (_jsonCard.IdBoard != board.Id)
				request.AddParameter("idBoard", board.Id);
			if (_jsonCard.IdList != list.Id)
				request.AddParameter("idList", list.Id);
			if (position != null)
				request.AddParameter("pos", position);
			Api.Put<IJsonCard>(request);
			_list.CardsList.MarkForUpdate();
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Removes a label from a card.
		/// </summary>
		/// <param name="color"></param>
		public void RemoveLabel(LabelColor color)
		{
			if (Svc == null) return;
			if (_isDeleted) return;
			Validator.Writable();
			var endpoint = EndpointGenerator.Default.Generate(this, new Label());
			endpoint.Append(color.ToLowerString());
			var request = RequestProvider.Create(endpoint.ToString());
			Api.Delete<IJsonCard>(request);
			_labels.MarkForUpdate();
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Removes (unassigns) a member from a card.
		/// </summary>
		/// <param name="member"></param>
		public void RemoveMember(Member member)
		{
			if (Svc == null) return;
			if (_isDeleted) return;
			Validator.Writable();
			Validator.Entity(member);
			var endpoint = EndpointGenerator.Default.Generate2(this, member);
			var request = RequestProvider.Create(endpoint.ToString());
			Api.Delete<IJsonCard>(request);
			_members.MarkForUpdate();
			_actions.MarkForUpdate();
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
		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public int CompareTo(Card other)
		{
			var order = Position.Value - other.Position.Value;
			return (int) order;
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
			return Name;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override bool Refresh()
		{
			if (_isDeleted) return false;
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = RequestProvider.Create(endpoint.ToString());
			request.AddParameter("fields", "closed,dateLastActivity,desc,due,idBoard,idList,idShort,idAttachmentCover,manualCoverAttachment,name,pos,url,subscribed");
			request.AddParameter("actions", "none");
			request.AddParameter("attachments", "false");
			request.AddParameter("badges", "false");
			request.AddParameter("members", "false");
			request.AddParameter("membersVoted", "false");
			request.AddParameter("checkItemStates", "false");
			request.AddParameter("checkLists", "false");
			request.AddParameter("board", "false");
			request.AddParameter("list", "false");
			var obj = Api.Get<IJsonCard>(request);
			if (obj == null) return false;
			ApplyJson(obj);
			return true;
		}

		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService()
		{
			_actions.Svc = Svc;
			_attachments.Svc = Svc;
			_badges.Svc = Svc;
			_checkLists.Svc = Svc;
			_labels.Svc = Svc;
			_members.Svc = Svc;
			_votingMembers.Svc = Svc;
			if (_board != null) _board.Svc = Svc;
			if (_list != null) _list.Svc = Svc;
		}

		internal override void ApplyJson(object obj)
		{
			if (obj is IRestResponse)
				_jsonCard = ((IRestResponse<IJsonCard>)obj).Data;
			else
				_jsonCard = (IJsonCard)obj;
			_position = _jsonCard.Pos.HasValue ? new Position(_jsonCard.Pos.Value) : Position.Unknown;
		}

		private void Put()
		{
			if (Svc == null)
			{
				Parameters.Clear();
				return;
			}
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = RequestProvider.Create(endpoint.ToString());
			foreach (var parameter in Parameters)
			{
				request.AddParameter(parameter.Key, parameter.Value);
			}
			Api.Put<IJsonCard>(request);
			Parameters.Clear();
			_actions.MarkForUpdate();
		}
	}
}

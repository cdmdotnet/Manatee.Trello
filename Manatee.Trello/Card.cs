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

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a card.
	/// </summary>
	public class Card : ExpiringObject, IEquatable<Card>, IComparable<Card>
	{
		private IJsonCard _jsonCard;
		private readonly Badges _badges;
		private Board _board;
		private List _list;
		private Position _position;
		private bool _isDeleted;

		///<summary>
		/// Enumerates all actions associated with this card.
		///</summary>
		public IEnumerable<Action> Actions
		{
			get
			{
				return _isDeleted
						   ? Enumerable.Empty<Action>()
						   : BuildList<Action>(EntityRequestType.Card_Read_Actions);
			}
		}
		/// <summary>
		/// Gets the ID of the attachment cover image.
		/// </summary>
		public string AttachmentCoverId
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return _jsonCard.IdAttachmentCover;
			}
		}
		/// <summary>
		/// Enumerates the cards attachments.
		/// </summary>
		public IEnumerable<Attachment> Attachments
		{
			get
			{
				return _isDeleted
						   ? Enumerable.Empty<Attachment>()
						   : BuildList<Attachment>(EntityRequestType.Card_Read_Attachments, fields: "all");
			}
		}
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
				return UpdateById(ref _board, EntityRequestType.Board_Read_Refresh, _jsonCard.IdBoard);
			}
		}
		/// <summary>
		/// Enumerates the card's checklists.
		/// </summary>
		public IEnumerable<CheckList> CheckLists
		{
			get
			{
				return _isDeleted
						   ? Enumerable.Empty<CheckList>()
						   : BuildList<CheckList>(EntityRequestType.Card_Read_CheckLists);
			}
		}
		/// <summary>
		/// Enumerates the card's comments.
		/// </summary>
		public IEnumerable<Action> Comments
		{
			get
			{
				return _isDeleted
						   ? Enumerable.Empty<Action>()
						   : BuildList<Action>(EntityRequestType.Card_Read_Actions, filter: "commentCard");
			}
		}
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
				if (_jsonCard.Desc == value) return;
				_jsonCard.Desc = value;
				Parameters.Add("desc", _jsonCard.Desc ?? string.Empty);
				Upload(EntityRequestType.Card_Write_Description);
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
				return _jsonCard.Due;
			}
			set
			{

				if (_isDeleted) return;
				Validator.Writable();
				if (_jsonCard.Due == value) return;
				_jsonCard.Due = value;
				var strValue = _jsonCard.Due == null
								   ? string.Empty
								   : _jsonCard.Due.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
				Parameters.Add("due", strValue);
				Upload(EntityRequestType.Card_Write_DueDate);
			}
		}
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonCard.Id; }
			internal set { _jsonCard.Id = value; }
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
				return _jsonCard.Closed;
			}
			set
			{

				if (_isDeleted) return;
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonCard.Closed == value) return;
				_jsonCard.Closed = value;
				Parameters.Add("closed", _jsonCard.Closed.ToLowerString());
				Upload(EntityRequestType.Card_Write_IsClosed);
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
				return _jsonCard.Subscribed;
			}
			set
			{

				if (_isDeleted) return;
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonCard.Subscribed == value) return;
				_jsonCard.Subscribed = value;
				Parameters.Add("subscribed", _jsonCard.Subscribed.ToLowerString());
				Upload(EntityRequestType.Card_Write_IsSubscribed);
			}
		}
		/// <summary>
		/// Enumerates the labels applied to this card.
		/// </summary>
		public IEnumerable<Label> Labels
		{
			get
			{
				return _isDeleted
						   ? Enumerable.Empty<Label>()
						   : BuildList<Label>(EntityRequestType.Card_Read_Labels);
			}
		}
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
				return UpdateById(ref _list, EntityRequestType.List_Read_Refresh, _jsonCard.IdList);
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
				return _jsonCard.ManualCoverAttachment;
			}
		}
		/// <summary>
		/// Enumerates the members assigned to this card.
		/// </summary>
		public IEnumerable<Member> Members
		{
			get
			{
				return _isDeleted
						   ? Enumerable.Empty<Member>()
						   : BuildList<Member>(EntityRequestType.Card_Read_Members);
			}
		}
		/// <summary>
		/// Gets or sets the card's name
		/// </summary>
		public string Name
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return _jsonCard.Name;
			}
			set
			{

				if (_isDeleted) return;
				Validator.Writable();
				Validator.NonEmptyString(value);
				if (_jsonCard.Name == value) return;
				_jsonCard.Name = value;
				Parameters.Add("name", _jsonCard.Name);
				Upload(EntityRequestType.Card_Write_Name);
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
				Upload(EntityRequestType.Card_Write_Position);
				MarkForUpdate();
			}
		}
		/// <summary>
		/// Gets the card's short ID.
		/// </summary>
		public int? ShortId { get { return _jsonCard.IdShort; } }
		/// <summary>
		/// Gets the URL for this card.
		/// </summary>
		public string Url { get { return _jsonCard.Url; } }
		/// <summary>
		/// Enumerates the members who have voted for this card.
		/// </summary>
		public IEnumerable<Member> VotingMembers
		{
			get
			{
				return _isDeleted
						   ? Enumerable.Empty<Member>()
						   : BuildList<Member>(EntityRequestType.Card_Read_VotingMembers);
			}
		}
		/// <summary>
		/// Sets whether the card should generate a notification email when the due date is near.
		/// </summary>
		public bool WarnWhenUpcoming
		{
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Parameters.Add("warnWhenUpcoming", value.ToLowerString());
				Upload(EntityRequestType.Card_Write_WarnWhenUpcoming);
			}
		}
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return _jsonCard is InnerJsonCard; } }

		/// <summary>
		/// Creates a new instance of the Card class.
		/// </summary>
		public Card()
		{
			_jsonCard = new InnerJsonCard();
			_badges = new Badges(this);
		}

		/// <summary>
		/// Adds an attachment to the card.
		/// </summary>
		/// <returns>The attachment object.</returns>
		public Attachment AddAttachment(string name, string url)
		{
			Validator.Writable();
			Validator.Url(url);
			if (string.IsNullOrWhiteSpace(name))
			{
				name = url.Split('/').LastOrDefault();
				if (string.IsNullOrWhiteSpace(name))
					name = url;
			}
			Parameters.Add("name", name);
			Parameters.Add("url", url);
			Parameters["_id"] = Id;
			var attachment = EntityRepository.Download<Attachment>(EntityRequestType.Card_Write_AddAttachment, Parameters);
			attachment.Owner = this;
			UpdateDependencies(attachment);
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
			if (_isDeleted) return null;
			Validator.Writable();
			Validator.NonEmptyString(name);
			Parameters.Add("name", name);
			if ((position != null) && position.IsValid)
				Parameters.Add("pos", position);
			Parameters.Add("idCard", Id);
			var checkList = EntityRepository.Download<CheckList>(EntityRequestType.Card_Write_AddChecklist, Parameters);
			checkList.Owner = this;
			UpdateDependencies(checkList);
			return checkList;
		}
		/// <summary>
		/// Adds a comment to the card.
		/// </summary>
		/// <param name="comment"></param>
		public void AddComment(string comment)
		{
			if (_isDeleted) return;
			Validator.Writable();
			Validator.NonEmptyString(comment);
			Parameters["_id"] = Id;
			Parameters.Add("text", comment);
			EntityRepository.Upload(EntityRequestType.Card_Write_AddComment, Parameters);
		}
		/// <summary>
		/// Applies a lable to the card.
		/// </summary>
		/// <param name="color">The color of the label.</param>
		public void ApplyLabel(LabelColor color)
		{
			if (_isDeleted) return;
			Validator.Writable();
			Parameters["_id"] = Id;
			Parameters.Add("value", color.ToLowerString());
			EntityRepository.Upload(EntityRequestType.Card_Write_ApplyLabel, Parameters);
		}
		/// <summary>
		/// Assigns a member to the card.
		/// </summary>
		/// <param name="member">The member to assign.</param>
		public void AssignMember(Member member)
		{
			if (_isDeleted) return;
			Validator.Writable();
			Validator.Entity(member);
			Parameters["_id"] = Id;
			Parameters.Add("value", member.Id);
			EntityRepository.Upload(EntityRequestType.Card_Write_AssignMember, Parameters);
		}
		/// <summary>
		/// Marks all notifications associated to this card as read.
		/// </summary>
		public void ClearNotifications()
		{
			if (_isDeleted) return;
			Validator.Writable();
			Parameters["_id"] = Id;
			EntityRepository.Upload(EntityRequestType.Card_Write_ClearNotifications, Parameters);
		}
		/// <summary>
		/// Deletes the card.  This cannot be undone.
		/// </summary>
		public void Delete()
		{
			if (_isDeleted) return;
			Validator.Writable();
			Parameters["_id"] = Id;
			EntityRepository.Upload(EntityRequestType.Card_Write_Delete, Parameters);
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
			if (_isDeleted) return;
			Validator.Writable();
			Validator.Entity(board);
			Validator.Entity(list);
			Parameters["_id"] = Id;
			if (!board.Lists.Contains(list))
				Log.Error(new InvalidOperationException(string.Format("Board '{0}' does not contain list with ID '{1}'.", board.Name, list.Id)));
			if (_jsonCard.IdBoard != board.Id)
				Parameters.Add("idBoard", board.Id);
			if (_jsonCard.IdList != list.Id)
				Parameters.Add("idList", list.Id);
			if (position != null)
				Parameters.Add("pos", position);
			EntityRepository.Upload(EntityRequestType.Card_Write_Move, Parameters);
		}
		/// <summary>
		/// Removes a label from a card.
		/// </summary>
		/// <param name="color"></param>
		public void RemoveLabel(LabelColor color)
		{
			if (_isDeleted) return;
			Validator.Writable();
			Parameters["_id"] = Id;
			Parameters.Add("_color", color.ToLowerString());
			EntityRepository.Upload(EntityRequestType.Card_Write_RemoveLabel, Parameters);
		}
		/// <summary>
		/// Removes (unassigns) a member from a card.
		/// </summary>
		/// <param name="member"></param>
		public void RemoveMember(Member member)
		{
			if (_isDeleted) return;
			Validator.Writable();
			Validator.Entity(member);
			Parameters["_id"] = Id;
			Parameters.Add("_memberId", member.Id);
			EntityRepository.Upload(EntityRequestType.Card_Write_RemoveMember, Parameters);
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
			return Id.GetHashCode();
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
		public override bool Refresh()
		{
			if (_isDeleted) return false;
			Parameters["_id"] = Id;
			AddDefaultParameters();
			return EntityRepository.Refresh(this, EntityRequestType.Card_Read_Refresh);
		}

		internal override void ApplyJson(object obj)
		{
			_jsonCard = (IJsonCard)obj;
			_position = _jsonCard.Pos.HasValue ? new Position(_jsonCard.Pos.Value) : Position.Unknown;
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}
		internal override void PropagateDependencies()
		{
			UpdateDependencies(_badges);
		}
		internal void ForceDeleted(bool deleted)
		{
			_isDeleted = deleted;
		}

		private void Upload(EntityRequestType requestType)
		{
			Parameters["_id"] = Id;
			EntityRepository.Upload(requestType, Parameters);
		}
	}
}

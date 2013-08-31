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
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	///<summary>
	/// Represents a board.
	///</summary>
	public class Board : ExpiringObject, IEquatable<Board>, IComparable<Board>
	{
		private IJsonBoard _jsonBoard;
		private readonly ExpiringList<Action> _actions;
		private readonly ExpiringList<Card> _archivedCards;
		private readonly ExpiringList<List> _archivedLists;
		private readonly ExpiringList<Member> _invitedMembers;
		private readonly LabelNames _labelNames;
		private readonly ExpiringList<List> _lists;
		private readonly ExpiringList<BoardMembership> _memberships;
		private Organization _organization;
		private readonly BoardPreferences _preferences;
		private readonly BoardPersonalPreferences _personalPreferences;

		///<summary>
		/// Enumerates all actions associated with this board.
		///</summary>
		public IEnumerable<Action> Actions { get { return _actions; } }
		/// <summary>
		/// Enumerates all archived cards on this board.
		/// </summary>
		public IEnumerable<Card> ArchivedCards { get { return _archivedCards; } }
		/// <summary>
		/// Enumerates all archived lists on this board.
		/// </summary>
		public IEnumerable<List> ArchivedLists { get { return _archivedLists; } }
		///<summary>
		/// Gets or sets the board's description.
		///</summary>
		public string Description
		{
			get
			{
				VerifyNotExpired();
				return (_jsonBoard == null) ? null : _jsonBoard.Desc;
			}
			set
			{
				Validator.Writable();
				if (_jsonBoard == null) return;
				if (_jsonBoard.Desc == value) return;
				_jsonBoard.Desc = value ?? string.Empty;
				Parameters.Add("desc", _jsonBoard.Desc);
				Put(EntityRequestType.Board_Write_Description);
			}
		}
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonBoard != null ? _jsonBoard.Id : base.Id; }
			internal set
			{
				if (_jsonBoard != null)
					_jsonBoard.Id = value;
				base.Id = value;
			}
		}
		/// <summary>
		/// Enumerates all members who have received invitations to this board.
		/// </summary>
		public IEnumerable<Member> InvitedMembers { get { return _invitedMembers; } }
		///<summary>
		/// Gets or sets whether this board is closed.
		///</summary>
		public bool? IsClosed
		{
			get
			{
				VerifyNotExpired();
				return (_jsonBoard == null) ? null : _jsonBoard.Closed;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonBoard == null) return;
				if (_jsonBoard.Closed == value) return;
				_jsonBoard.Closed = value;
				Parameters.Add("closed", _jsonBoard.Closed.ToLowerString());
				Put(EntityRequestType.Board_Write_IsClosed);
			}
		}
		///<summary>
		/// Gets or sets whether this board is pinned to the user's Boards menu.
		///</summary>
		public bool? IsPinned
		{
			get
			{
				VerifyNotExpired();
				return (_jsonBoard == null) ? null : _jsonBoard.Pinned;
			}
			private set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonBoard == null) return;
				if (_jsonBoard.Pinned == value) return;
				_jsonBoard.Pinned = value;
				Parameters.Add("pinned", _jsonBoard.Pinned.ToLowerString());
				Put(EntityRequestType.Board_Write_IsPinned);
			}
		}
		///<summary>
		/// Gets or sets whether the user is subscribed to this board.
		///</summary>
		public bool? IsSubscribed
		{
			get
			{
				VerifyNotExpired();
				return (_jsonBoard == null) ? null : _jsonBoard.Subscribed;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonBoard == null) return;
				if (_jsonBoard.Subscribed == value) return;
				_jsonBoard.Subscribed = value;
				Parameters.Add("subscribed", _jsonBoard.Subscribed.ToLowerString());
				Put(EntityRequestType.Board_Write_IsSubscribed);
			}
		}
		///<summary>
		/// Gets the board's set of label names.
		///</summary>
		public LabelNames LabelNames { get { return _labelNames; } }
		///<summary>
		/// Gets the board's open lists.
		///</summary>
		public IEnumerable<List> Lists { get { return _lists; } }
		internal ExpiringList<List> ListsList { get { return _lists; } }
		///<summary>
		/// Gets the board's members and their types.
		///</summary>
		public IEnumerable<BoardMembership> Memberships { get { return _memberships; } }
		///<summary>
		/// Gets or sets the board's name.
		///</summary>
		public string Name
		{
			get
			{
				VerifyNotExpired();
				return (_jsonBoard == null) ? null : _jsonBoard.Name;
			}
			set
			{
				Validator.Writable();
				Validator.NonEmptyString(value);
				if (_jsonBoard == null) return;
				if (_jsonBoard.Name == value) return;
				_jsonBoard.Name = value;
				Parameters.Add("name", _jsonBoard.Name);
				Put(EntityRequestType.Board_Write_Name);
			}
		}
		/// <summary>
		/// Gets or sets the organization, if any, to which this board belongs.
		/// </summary>
		public Organization Organization
		{
			get
			{
				VerifyNotExpired();
				if (_jsonBoard == null) return null;
				return UpdateById(ref _organization, EntityRequestType.Organization_Read_Refresh, _jsonBoard.IdOrganization);
			}
			set
			{
				Validator.Writable();
				Validator.Entity(value, true);
				if (_jsonBoard == null) return;
				if (value == null)
				{
					_jsonBoard.IdOrganization = null;
				}
				else
				{
					if (_jsonBoard.IdOrganization == value.Id) return;
					_jsonBoard.IdOrganization = value.Id;
				}
				Parameters.Add("idOrganization", _jsonBoard.IdOrganization ?? string.Empty);
				Put(EntityRequestType.Board_Write_Organization);
			}
		}
		///<summary>
		/// Gets the set of preferences for this board unique to the current member.
		///</summary>
		public BoardPersonalPreferences PersonalPreferences { get { return _personalPreferences; } }
		///<summary>
		/// Gets the set of preferences for this board.
		///</summary>
		public BoardPreferences Preferences { get { return _preferences; } }
		///<summary>
		/// Gets the URL for this board.
		///</summary>
		public string Url { get { return (_jsonBoard == null) ? null : _jsonBoard.Url; } }

		///<summary>
		/// Creates a new instance of the Board class.
		///</summary>
		public Board()
		{
			_jsonBoard = new InnerJsonBoard();
			_actions = new ExpiringList<Action>(this, EntityRequestType.Board_Read_Actions);
			_archivedCards = new ExpiringList<Card>(this, EntityRequestType.Board_Read_Cards) {Filter = "closed"};
			_archivedLists = new ExpiringList<List>(this, EntityRequestType.Board_Read_Lists) {Filter = "closed"};
			_invitedMembers = new ExpiringList<Member>(this, EntityRequestType.Board_Read_Members);
			_labelNames = new LabelNames(this);
			_lists = new ExpiringList<List>(this, EntityRequestType.Board_Read_Lists);
			_memberships = new ExpiringList<BoardMembership>(this, EntityRequestType.Board_Read_Memberships);
			_personalPreferences = new BoardPersonalPreferences(this);
			_preferences = new BoardPreferences(this);
		}

		///<summary>
		/// Adds a new list to the board in the specified location
		///</summary>
		///<param name="name">The name of the list.</param>
		///<param name="position">The desired position of the list.  Default is Bottom.  Invalid positions are ignored.</param>
		///<returns>The new list.</returns>
		public List AddList(string name, Position position = null)
		{
			Validator.Writable();
			Validator.NonEmptyString(name);
			Parameters.Add("name", name);
			Parameters.Add("idBoard", Id);
			if ((position != null) && position.IsValid)
				Parameters.Add("pos", position);
			var list = EntityRepository.Download<List>(EntityRequestType.Board_Write_AddList, Parameters);
			UpdateDependencies(list);
			_lists.MarkForUpdate();
			_actions.MarkForUpdate();
			return list;
		}
		///<summary>
		/// Adds a member to the board or updates the permissions of a member already on the board.
		///</summary>
		///<param name="member">The member</param>
		///<param name="type">The permission level for the member</param>
		public void AddOrUpdateMember(Member member, BoardMembershipType type = BoardMembershipType.Normal)
		{
			Validator.Writable();
			Validator.Entity(member);
			Parameters.Add("_id", Id);
			Parameters.Add("_memberId", member.Id);
			Parameters.Add("type", type.ToLowerString());
			EntityRepository.Upload(EntityRequestType.Board_Write_AddOrUpdateMember, Parameters);
			_memberships.MarkForUpdate();
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Adds a new or existing member to the board or updates the permissions of a member already on the board.
		/// </summary>
		/// <param name="fullName"></param>
		/// <param name="type">The permission level for the member</param>
		/// <param name="emailAddress"></param>
		public Member AddOrUpdateMember(string emailAddress, string fullName, BoardMembershipType type = BoardMembershipType.Normal)
		{
			throw new NotImplementedException("The functionality to add a non-Trello member to to boards has been temporarily disabled.");
			Validator.Writable();
			Validator.NonEmptyString(emailAddress);
			Validator.NonEmptyString(fullName);
			Member member = null;
			if (member != null)
			{
				AddOrUpdateMember(member, type);
				_memberships.MarkForUpdate();
				_memberships.MarkForUpdate();
				_actions.MarkForUpdate();
			}
			return member;
		}
		///<summary>
		/// Marks the board as viewed by the current member.
		///</summary>
		public void MarkAsViewed()
		{
			Validator.Writable();
			Parameters.Add("_id", Id);
			EntityRepository.Upload(EntityRequestType.Board_Write_MarkAsViewed, Parameters);
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Extends an invitation to the board to another member.
		/// </summary>
		/// <param name="member">The member to invite.</param>
		/// <param name="type">The level of membership offered.</param>
		internal void InviteMember(Member member, BoardMembershipType type = BoardMembershipType.Normal)
		{
			Validator.Writable();
			Validator.Entity(member);
			Log.Error(new NotSupportedException("Inviting members to boards is not yet supported by the Trello API."));
		}
		///<summary>
		/// Removes a member from the board.
		///</summary>
		///<param name="member"></param>
		public void RemoveMember(Member member)
		{
			Validator.Writable();
			Validator.Entity(member);
			Parameters.Add("_id", Id);
			Parameters.Add("_memberId", member.Id);
			EntityRepository.Upload(EntityRequestType.Board_Write_RemoveMember, Parameters);
			_memberships.MarkForUpdate();
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Rescinds an existing invitation to the board.
		/// </summary>
		/// <param name="member"></param>
		internal void RescindInvitation(Member member)
		{
			Validator.Entity(member);
			Log.Error(new NotSupportedException("Inviting members to boards is not yet supported by the Trello API."));
		}
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Board other)
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
			if (!(obj is Board)) return false;
			return Equals((Board) obj);
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
		public int CompareTo(Board other)
		{
			var order = string.Compare(Name, other.Name);
			return order;
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
			Parameters.Add("_id", Id);
			AddDefaultParameters();
			EntityRepository.Refresh(this, EntityRequestType.Board_Read_Refresh);
			return true;

		}

		internal override void ApplyJson(object obj)
		{
			_jsonBoard = (IJsonBoard) obj;
		}
		internal override void PropagateDependencies()
		{
			UpdateDependencies(_actions);
			UpdateDependencies(_archivedCards);
			UpdateDependencies(_archivedLists);
			UpdateDependencies(_invitedMembers);
			UpdateDependencies(_labelNames);
			UpdateDependencies(_lists);
			UpdateDependencies(_memberships);
			UpdateDependencies(_preferences);
			UpdateDependencies(_personalPreferences);
		}

		private void Put(EntityRequestType requestType)
		{
			Parameters.Add("_id", Id);
			EntityRepository.Upload(requestType, Parameters);
			_actions.MarkForUpdate();
		}
	}
}

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
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

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
	///<summary>
	/// Represents a board.
	///</summary>
	public class Board : ExpiringObject, IEquatable<Board>
	{
		private IJsonBoard _jsonBoard;
		private readonly ExpiringList<Action, IJsonAction> _actions;
		private readonly ExpiringList<Card, IJsonCard> _archivedCards;
		private readonly ExpiringList<List, IJsonList> _archivedLists;
		private readonly LabelNames _labelNames;
		private readonly ExpiringList<List, IJsonList> _lists;
		private readonly ExpiringList<BoardMembership, IJsonBoardMembership> _members;
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
				Validate.Writable(Svc);
				if (_jsonBoard == null) return;
				if (_jsonBoard.Desc == value) return;
				_jsonBoard.Desc = value ?? string.Empty;
				Parameters.Add("desc", _jsonBoard.Desc);
				Put();
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
				Validate.Writable(Svc);
				Validate.Nullable(value);
				if (_jsonBoard == null) return;
				if (_jsonBoard.Closed == value) return;
				_jsonBoard.Closed = value;
				Parameters.Add("closed", _jsonBoard.Closed.ToLowerString());
				Put();
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
			set
			{
				Validate.Writable(Svc);
				Validate.Nullable(value);
				if (_jsonBoard == null) return;
				if (_jsonBoard.Pinned == value) return;
				_jsonBoard.Pinned = value;
				Parameters.Add("pinned", _jsonBoard.Pinned.ToLowerString());
				Put();
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
				Validate.Writable(Svc);
				Validate.Nullable(value);
				if (_jsonBoard == null) return;
				if (_jsonBoard.Subscribed == value) return;
				_jsonBoard.Subscribed = value;
				Parameters.Add("subscribed", _jsonBoard.Subscribed.ToLowerString());
				Put();
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
		///<summary>
		/// Gets the board's members and their types.
		///</summary>
		public IEnumerable<BoardMembership> Memberships { get { return _members; } }
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
				Validate.Writable(Svc);
				Validate.NonEmptyString(value);
				if (_jsonBoard == null) return;
				if (_jsonBoard.Name == value) return;
				_jsonBoard.Name = value;
				Parameters.Add("name", _jsonBoard.Name);
				Put();
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
				return ((_organization == null) || (_organization.Id != _jsonBoard.IdOrganization)) && (Svc != null)
				       	? (_organization = Svc.Retrieve<Organization>(_jsonBoard.IdOrganization))
				       	: _organization;
			}
			set
			{
				Validate.Writable(Svc);
				Validate.Entity(value, true);
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
				Put();
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

		internal override string Key { get { return "boards"; } }
		/// <summary>
		/// Gets whether the entity is a cacheable item.
		/// </summary>
		protected override bool Cacheable { get { return true; } }

		///<summary>
		/// Creates a new instance of the Board class.
		///</summary>
		public Board()
		{
			_jsonBoard = new InnerJsonBoard();
			_actions = new ExpiringList<Action, IJsonAction>(this, "actions");
			_archivedCards = new ExpiringList<Card, IJsonCard>(this, "cards") { Filter = "closed" };
			_archivedLists = new ExpiringList<List, IJsonList>(this, "lists") { Filter = "closed" };
			_labelNames = new LabelNames(this);
			_lists = new ExpiringList<List, IJsonList>(this, "lists");
			_members = new ExpiringList<BoardMembership, IJsonBoardMembership>(this, "memberships");
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
			if (Svc == null) return null;
			Validate.Writable(Svc);
			Validate.NonEmptyString(name);
			var list = new List();
			var endpoint = EndpointGenerator.Default.Generate(list);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("name", name);
			request.AddParameter("idBoard", Id);
			if ((position != null) && position.IsValid)
				request.AddParameter("pos", position);
			list.ApplyJson(Api.Post<IJsonList>(request));
			list.Svc = Svc;
			list.Api = Api;
			_lists.MarkForUpdate();
			_actions.MarkForUpdate();
			return list;
		}
		///<summary>
		/// Adds a member to the board or updates the permissions of an existing member.
		///</summary>
		///<param name="member">The member</param>
		///<param name="type">The permission level for the member</param>
		public void AddOrUpdateMember(Member member, BoardMembershipType type = BoardMembershipType.Normal)
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			Validate.Entity(member);
			var endpoint = EndpointGenerator.Default.Generate(this, member);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("type", type.ToLowerString());
			Api.Put<IJsonBoard>(request);
			_members.MarkForUpdate();
			_actions.MarkForUpdate();
		}
		///<summary>
		/// Marks the board as viewed by the current member.
		///</summary>
		public void MarkAsViewed()
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			var endpoint = EndpointGenerator.Default.Generate(this);
			endpoint.Append("markAsViewed");
			var request = Api.RequestProvider.Create(endpoint.ToString());
			Api.Post<IJsonBoard>(request);
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Extends an invitation to the board to another member.
		/// </summary>
		/// <param name="member">The member to invite.</param>
		/// <param name="type">The level of membership offered.</param>
		internal void InviteMember(Member member, BoardMembershipType type = BoardMembershipType.Normal)
		{
			Validate.Writable(Svc);
			Validate.Entity(member);
			throw new NotSupportedException("Inviting members to boards is not yet supported by the Trello API.");
		}
		///<summary>
		/// Removes a member from the board.
		///</summary>
		///<param name="member"></param>
		public void RemoveMember(Member member)
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			Validate.Entity(member);
			var endpoint = EndpointGenerator.Default.Generate(this, member);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			Api.Delete<IJsonBoard>(request);
		}
		/// <summary>
		/// Rescinds an existing invitation to the board.
		/// </summary>
		/// <param name="member"></param>
		internal void RescindInvitation(Member member)
		{
			Validate.Entity(member);
			throw new NotSupportedException("Inviting members to boards is not yet supported by the Trello API.");
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
		protected override void Refresh()
		{
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			ApplyJson(Api.Get<IJsonBoard>(request));
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService()
		{
			_actions.Svc = Svc;
			_archivedCards.Svc = Svc;
			_archivedLists.Svc = Svc;
			_labelNames.Svc = Svc;
			_lists.Svc = Svc;
			_members.Svc = Svc;
			_personalPreferences.Svc = Svc;
			_preferences.Svc = Svc;
			if (_organization != null) _organization.Svc = Svc;
		}

		internal override void ApplyJson(object obj)
		{
			if (obj == null)
				throw new EntityNotOnTrelloException<Board>(this);
			_jsonBoard = (IJsonBoard)obj;
		}

		private void Put()
		{
			if (Svc == null)
			{
				Parameters.Clear();
				return;
			}
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			foreach (var parameter in Parameters)
			{
				request.AddParameter(parameter.Key, parameter.Value);
			}
			Api.Put<IJsonBoard>(request);
			_actions.MarkForUpdate();
		}
	}
}

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
using Manatee.Json.Extensions;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;

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
	public class Board : JsonCompatibleExpiringObject, IEquatable<Board>
	{
		private readonly ExpiringList<Board, Action> _actions;
		private readonly ExpiringList<Board, Card> _archivedCards;
		private readonly ExpiringList<Board, List> _archivedLists;
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
		/// Gets and sets the board's description.
		///</summary>
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
		///<summary>
		/// Gets and sets whether this board is closed.
		///</summary>
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
				if (!value.HasValue)
					throw new ArgumentNullException("value");
				_isClosed = value;
				Parameters.Add("closed", _isClosed.ToLowerString());
				Put();
			}
		}
		///<summary>
		/// Gets and sets whether this board is pinned to the user's Boards menu.
		///</summary>
		public bool? IsPinned
		{
			get
			{
				VerifyNotExpired();
				return _isPinned;
			}
			set
			{
				if (_isPinned == value) return;
				if (!value.HasValue)
					throw new ArgumentNullException("value");
				_isPinned = value;
				Parameters.Add("pinned", _isPinned.ToLowerString());
				Put();
			}
		}
		///<summary>
		/// Gets and sets whether the user is subscribed to this board.
		///</summary>
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
				if (!value.HasValue)
					throw new ArgumentNullException("value");
				_isSubscribed = value;
				Parameters.Add("subscribed", _isSubscribed.ToLowerString());
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
		/// Gets the board's members.
		///</summary>
		public IEnumerable<BoardMembership> Members { get { return _members; } }
		///<summary>
		/// Gets and sets the board's name.
		///</summary>
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
				if (!string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("value");
				_name = value;
				Parameters.Add("name", _name);
				Put();
			}
		}
		/// <summary>
		/// Gets and sets the organization, if any, to which this board belongs.
		/// </summary>
		public Organization Organization
		{
			get
			{
				VerifyNotExpired();
				return ((_organization == null) || (_organization.Id != _organizationId)) && (Svc != null)
				       	? (_organization = Svc.Get(Svc.RequestProvider.Create<Organization>(_organizationId)))
				       	: _organization;
			}
			set
			{
				Validate.Entity(value);
				if (_organizationId == value.Id) return;
				_organizationId = value.Id;
				Parameters.Add("idOrganization", _organizationId);
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
		public string Url { get { return _url; } }

		internal override string Key { get { return "boards"; } }

		///<summary>
		/// Creates a new instance of the Board class.
		///</summary>
		public Board()
		{
			_actions = new ExpiringList<Board, Action>(this);
			_archivedCards = new ExpiringList<Board, Card>(this) {Filter = "closed"};
			_archivedLists = new ExpiringList<Board, List>(this) {Filter = "closed"};
			_labelNames = new LabelNames(null, this);
			_lists = new ExpiringList<Board, List>(this);
			_members = new ExpiringList<Board, BoardMembership>(this);
			_personalPreferences = new BoardPersonalPreferences(null, this);
			_preferences = new BoardPreferences(null, this);
		}
		internal Board(ITrelloRest svc, string id)
			: base(svc, id)
		{
			_actions = new ExpiringList<Board, Action>(svc, this);
			_archivedCards = new ExpiringList<Board, Card>(svc, this) {Filter = "closed"};
			_archivedLists = new ExpiringList<Board, List>(svc, this) {Filter = "closed"};
			_labelNames = new LabelNames(svc, this);
			_lists = new ExpiringList<Board, List>(svc, this);
			_members = new ExpiringList<Board, BoardMembership>(svc, this);
			_personalPreferences = new BoardPersonalPreferences(svc, this);
			_preferences = new BoardPreferences(svc, this);
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
			Validate.NonEmptyString(name);
			var request = Svc.RequestProvider.Create<List>(this);
			Parameters.Add("name", name);
			Parameters.Add("idBoard", Id);
			if ((position != null) && position.IsValid)
				Parameters.Add("pos", position);
			var list = Svc.Post(request);
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
			Validate.Entity(member);
			var request = Svc.RequestProvider.Create<Member>(new ExpiringObject[] { this, member }, this);
			Parameters.Add("type", type.ToLowerString());
			Svc.Put(request);
			_members.MarkForUpdate();
			_actions.MarkForUpdate();
		}
		///<summary>
		/// Marks the board as viewed by the current member.
		///</summary>
		public void MarkAsViewed()
		{
			if (Svc == null) return;
			var request = Svc.RequestProvider.Create<Board>(new ExpiringObject[] { this }, urlExtension: "markAsViewed");
			Svc.Post(request);
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Extends an invitation to the board to another member.
		/// </summary>
		/// <param name="member">The member to invite.</param>
		/// <param name="type">The level of membership offered.</param>
		private void InviteMember(Member member, BoardMembershipType type = BoardMembershipType.Normal)
		{
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
			Validate.Entity(member);
			Svc.Delete(Svc.RequestProvider.Create<Board>(new ExpiringObject[] { this, member }));
		}
		/// <summary>
		/// Rescinds an existing invitation to the board.
		/// </summary>
		/// <param name="member"></param>
		private void RescindInvitation(Member member)
		{
			Validate.Entity(member);
			throw new NotSupportedException("Inviting members to boards is not yet supported by the Trello API.");
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
			_description = obj.TryGetString("desc");
			_isClosed = obj.TryGetBoolean("closed");
			_isPinned = obj.TryGetBoolean("pinned");
			_isSubscribed = obj.TryGetBoolean("subscribed");
			_name = obj.TryGetString("name");
			_organizationId = obj.TryGetString("idOrganization");
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
			_isInitialized = true;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			var entity = Svc.Get(Svc.RequestProvider.Create<Board>(Id));
			Refresh(entity);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateSerivce()
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

		private void Put()
		{
			if (Svc == null)
			{
				Parameters.Clear();
				return;
			}
			var request = Svc.RequestProvider.Create<Board>(this);
			Svc.Put(request);
			_actions.MarkForUpdate();
		}
	}
}

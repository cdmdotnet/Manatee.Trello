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
 
	File Name:		List.cs
	Namespace:		Manatee.Trello
	Class Name:		List
	Purpose:		Represents a list on Trello.com.

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
	//   "id":"5144051cbd0da6681200201f",
	//   "name":"Current Version Backlog",
	//   "closed":false,
	//   "idBoard":"5144051cbd0da6681200201e",
	//   "pos":16384
	//}
	/// <summary>
	/// Represents a list.
	/// </summary>
	public class List : JsonCompatibleExpiringObject, IEquatable<List>
	{
		private readonly ExpiringList<List, Action> _actions;
		private string _boardId;
		private Board _board;
		private readonly ExpiringList<List, Card> _cards;
		private bool? _isClosed;
		private bool? _isSubscribed;
		private string _name;
		private Position _position;

		///<summary>
		/// Enumerates all actions associated with the list.
		///</summary>
		public IEnumerable<Action> Actions { get { return _actions; } }
		/// <summary>
		/// Gets the board which contains the list.
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
		/// Enumerates all cards in the list.
		/// </summary>
		public IEnumerable<Card> Cards { get { return _cards; } }
		/// <summary>
		/// Gets or sets whether the list is archived.
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
		/// Gets or sets whether the current member is subscribed to the list.
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
		/// Gets or sets the name of the list.
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
		/// Gets or sets the position of the list.
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

		internal override string Key { get { return "lists"; } }

		/// <summary>
		/// Creates a new instance of the List class.
		/// </summary>
		public List()
		{
			_actions = new ExpiringList<List, Action>(this);
			_cards = new ExpiringList<List, Card>(this);
		}
		internal List(ITrelloRest svc, string id)
			: base(svc, id)
		{
			_actions = new ExpiringList<List, Action>(svc, this);
			_cards = new ExpiringList<List, Card>(svc, this);
		}

		/// <summary>
		/// Adds a new card to the list.
		/// </summary>
		/// <param name="name">The name of the card.</param>
		/// <param name="description">The description of the card.</param>
		/// <param name="position">The position of the card.  Default is Bottom.  Invalid positions are ignored.</param>
		/// <returns>The card.</returns>
		public Card AddCard(string name, string description = null, Position position = null)
		{
			if (Svc == null) return null;
			Validate.NonEmptyString(name);
			var request = Svc.RequestProvider.Create<Card>(new ExpiringObject[]{new Card()}, this);
			Parameters.Add("name", name);
			Parameters.Add("idList", Id);
			if (description != null)
				Parameters.Add("desc", description);
			if ((position != null) && position.IsValid)
				Parameters.Add("pos", position);
			var card = Svc.Post(request);
			_cards.MarkForUpdate();
			return card;
		}
		/// <summary>
		/// Deletes the list.  This cannot be undone.
		/// </summary>
		internal void Delete()
		{
			throw new NotSupportedException("Deleting lists is not yet supported by Trello.");
		}
		/// <summary>
		/// Moves the list to another board.
		/// </summary>
		/// <param name="board">The destination board.</param>
		/// <param name="position">The position in the board.  Default is Bottom (right).  Invalid positions are ignored.</param>
		public void Move(Board board, Position position = null)
		{
			if (Svc == null) return;
			Validate.Entity(board);
			Parameters.Add("idBoard", board.Id);
			if (position != null)
				Parameters.Add("pos", position);
			Svc.Put(Svc.RequestProvider.Create<List>(this));
			_actions.MarkForUpdate();
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
			_boardId = obj.TryGetString("idBoard");
			_isClosed = obj.TryGetBoolean("closed");
			_isSubscribed = obj.TryGetBoolean("subscribed");
			_name = obj.TryGetString("name");
			_position = new Position(PositionValue.Unknown);
			if (obj.ContainsKey("pos"))
				_position.FromJson(obj["pos"]);
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
			           		{"state", _boardId},
			           		{"closed", _isClosed.HasValue ? _isClosed.Value : JsonValue.Null},
			           		{"subscribed", _isSubscribed.HasValue ? _isSubscribed.Value : JsonValue.Null},
			           		{"name", _name},
			           		{"pos", _position.ToJson()},
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
		public bool Equals(List other)
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
			if (!(obj is List)) return false;
			return Equals((List) obj);
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
			var list = entity as List;
			if (list == null) return;
			_boardId = list._boardId;
			_isClosed = list._isClosed;
			_isSubscribed = list._isSubscribed;
			_name = list._name;
			_position = list._position;
			_isInitialized = true;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			var entity = Svc.Get(Svc.RequestProvider.Create<List>(Id));
			Refresh(entity);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateSerivce()
		{
			_actions.Svc = Svc;
			_cards.Svc = Svc;
			if (_board != null) _board.Svc = Svc;
		}

		private void Put()
		{
			if (Svc == null)
			{
				Parameters.Clear();
				return;
			}
			var request = Svc.RequestProvider.Create<List>(this);
			Svc.Put(request);
			_actions.MarkForUpdate();
		}
	}
}

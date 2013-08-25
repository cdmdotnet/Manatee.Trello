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
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a list.
	/// </summary>
	public class List : ExpiringObject, IEquatable<List>, IComparable<List>
	{
		private IJsonList _jsonList;
		private readonly ExpiringList<Action> _actions;
		private Board _board;
		private readonly ExpiringList<Card> _cards;
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
				if (_jsonList == null) return null;
				return ((_board == null) || (_board.Id != _jsonList.IdBoard)) && (Svc != null)
						? (_board = Svc.Retrieve<Board>(_jsonList.IdBoard))
						: _board;
			}
		}
		/// <summary>
		/// Enumerates all cards in the list.
		/// </summary>
		public IEnumerable<Card> Cards { get { return _cards; } }
		internal ExpiringList<Card> CardsList { get { return _cards; } }
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonList != null ? _jsonList.Id : base.Id; }
			internal set
			{
				if (_jsonList != null)
					_jsonList.Id = value;
				base.Id = value;
			}
		}
		/// <summary>
		/// Gets or sets whether the list is archived.
		/// </summary>
		public bool? IsClosed
		{
			get
			{
				VerifyNotExpired();
				return (_jsonList == null) ? null : _jsonList.Closed;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonList == null) return;
				if (_jsonList.Closed == value) return;
				_jsonList.Closed = value;
				Parameters.Add("closed", _jsonList.Closed.ToLowerString());
				Put(EntityRequestType.List_Write_IsClosed);
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
				return (_jsonList == null) ? null : _jsonList.Subscribed;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonList == null) return;
				if (_jsonList.Subscribed == value) return;
				_jsonList.Subscribed = value;
				Parameters.Add("subscribed", _jsonList.Subscribed.ToLowerString());
				Put(EntityRequestType.List_Write_IsSubscribed);
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
				return (_jsonList == null) ? null : _jsonList.Name;
			}
			set
			{
				Validator.Writable();
				Validator.NonEmptyString(value);
				if (_jsonList == null) return;
				if (_jsonList.Name == value) return;
				_jsonList.Name = value;
				Parameters.Add("name", _jsonList.Name);
				Put(EntityRequestType.List_Write_Name);
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
				Validator.Writable();
				Validator.Position(value);
				if (_jsonList == null) return;
				if (_position == value) return;
				_position = value;
				Parameters.Add("pos", _position);
				Put(EntityRequestType.List_Write_Position);
				MarkForUpdate();
				if (_board != null)
					_board.ListsList.MarkForUpdate();
			}
		}

		/// <summary>
		/// Creates a new instance of the List class.
		/// </summary>
		public List()
		{
			_jsonList = new InnerJsonList();
			_actions = new ExpiringList<Action>(this, EntityRequestType.List_Read_Actions) {Fields = "id"};
			_cards = new ExpiringList<Card>(this, EntityRequestType.List_Read_Cards) {Fields = "id"};
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
			Validator.Writable();
			Validator.NonEmptyString(name);
			Parameters.Add("name", name);
			Parameters.Add("idList", Id);
			if (description != null)
				Parameters.Add("desc", description);
			if ((position != null) && position.IsValid)
				Parameters.Add("pos", position);
			var card = EntityRepository.Download<Card>(EntityRequestType.List_Write_AddCard, Parameters);
			UpdateService(card);
			_cards.MarkForUpdate();
			return card;
		}
		/// <summary>
		/// Deletes the list.  This cannot be undone.
		/// </summary>
		internal void Delete()
		{
			Log.Error(new NotSupportedException("Deleting lists is not yet supported by Trello."));
		}
		/// <summary>
		/// Moves the list to another board.
		/// </summary>
		/// <param name="board">The destination board.</param>
		/// <param name="position">The position in the board.  Default is Bottom (right).  Invalid positions are ignored.</param>
		public void Move(Board board, Position position = null)
		{
			if (Svc == null) return;
			Validator.Writable();
			Validator.Entity(board);
			Parameters.Add("_id", Id);
			Parameters.Add("idBoard", board.Id);
			if (position != null)
				Parameters.Add("pos", position);
			EntityRepository.Upload(EntityRequestType.List_Write_Move, Parameters);
			MarkForUpdate();
			if (_board != null)
				_board.ListsList.MarkForUpdate();
			_actions.MarkForUpdate();
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
		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public int CompareTo(List other)
		{
			var order = Position.Value - other.Position.Value;
			return (int)order;
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
			Parameters.Add("fields", "name,closed,idBoard,pos,subscribed");
			Parameters.Add("cards", "none");
			EntityRepository.Refresh(this, EntityRequestType.List_Read_Refresh);
			return true;
		}

		/// <summary>
		/// Propagates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropagateService()
		{
			UpdateService(_actions);
			UpdateService(_cards);
			UpdateService(_board);
		}

		internal override void ApplyJson(object obj)
		{
			if (obj is IRestResponse)
				_jsonList = ((IRestResponse<IJsonList>)obj).Data;
			else
				_jsonList = (IJsonList)obj;
			_position = ((_jsonList != null) && _jsonList.Pos.HasValue) ? new Position(_jsonList.Pos.Value) : Position.Unknown;
		}

		private void Put(EntityRequestType requestType)
		{
			Parameters.Add("_id", Id);
			EntityRepository.Upload(requestType, Parameters);
			_actions.MarkForUpdate();
		}
	}
}

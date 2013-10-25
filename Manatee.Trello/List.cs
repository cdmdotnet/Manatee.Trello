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

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a list.
	/// </summary>
	public class List : ExpiringObject, IEquatable<List>, IComparable<List>
	{
		private IJsonList _jsonList;
		private Board _board;
		private Position _position;

		///<summary>
		/// Enumerates all actions associated with the list.
		///</summary>
		public IEnumerable<Action> Actions { get { return BuildList<Action>(EntityRequestType.List_Read_Actions); } }
		/// <summary>
		/// Gets the board which contains the list.
		/// </summary>
		public Board Board
		{
			get
			{
				VerifyNotExpired();
				return UpdateById(ref _board, EntityRequestType.Board_Read_Refresh, _jsonList.IdBoard);
			}
			set
			{
				Validator.Writable();
				Validator.Entity(value);
				if (_jsonList.IdBoard == value.Id) return;
				_jsonList.IdBoard = value.Id;
				_board = value;
				Parameters.Add("idBoard", _jsonList.IdBoard);
				Upload(EntityRequestType.List_Write_Board);
			}
		}
		/// <summary>
		/// Enumerates all cards in the list.
		/// </summary>
		public IEnumerable<Card> Cards { get { return BuildList<Card>(EntityRequestType.List_Read_Cards); } }
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonList.Id; }
			internal set { _jsonList.Id = value; }
		}
		/// <summary>
		/// Gets or sets whether the list is archived.
		/// </summary>
		public bool? IsClosed
		{
			get
			{
				VerifyNotExpired();
				return _jsonList.Closed;
			}
			set
			{

				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonList.Closed == value) return;
				_jsonList.Closed = value;
				Parameters.Add("closed", _jsonList.Closed.ToLowerString());
				Upload(EntityRequestType.List_Write_IsClosed);
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
				return _jsonList.Subscribed;
			}
			set
			{

				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonList.Subscribed == value) return;
				_jsonList.Subscribed = value;
				Parameters.Add("subscribed", _jsonList.Subscribed.ToLowerString());
				Upload(EntityRequestType.List_Write_IsSubscribed);
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
				return _jsonList.Name;
			}
			set
			{

				Validator.Writable();
				Validator.NonEmptyString(value);
				if (_jsonList.Name == value) return;
				_jsonList.Name = value;
				Parameters.Add("name", _jsonList.Name);
				Upload(EntityRequestType.List_Write_Name);
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
				if (_position == value) return;
				_position = value;
				Parameters.Add("pos", _position);
				Upload(EntityRequestType.List_Write_Position);
				MarkForUpdate();
			}
		}
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return _jsonList is InnerJsonList; } }

		/// <summary>
		/// Creates a new instance of the List class.
		/// </summary>
		public List()
		{
			_jsonList = new InnerJsonList();
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
			Validator.Writable();
			Validator.NonEmptyString(name);
			Parameters.Add("name", name);
			Parameters.Add("idList", Id);
			if (description != null)
				Parameters.Add("desc", description);
			if ((position != null) && position.IsValid)
				Parameters.Add("pos", position);
			var card = EntityRepository.Download<Card>(EntityRequestType.List_Write_AddCard, Parameters);
			UpdateDependencies(card);
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
			return Id.GetHashCode();
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
			Parameters["_id"] = Id;
			AddDefaultParameters();
			return EntityRepository.Refresh(this, EntityRequestType.List_Read_Refresh);
		}

		internal override void ApplyJson(object obj)
		{
			_jsonList = (IJsonList)obj;
			_position = ((_jsonList != null) && _jsonList.Pos.HasValue) ? new Position(_jsonList.Pos.Value) : Position.Unknown;
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}
		internal override void PropagateDependencies() {}

		private void Upload(EntityRequestType requestType)
		{
			Parameters["_id"] = Id;
			EntityRepository.Upload(requestType, Parameters);
		}
	}
}

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
 
	File Name:		CheckList.cs
	Namespace:		Manatee.Trello
	Class Name:		CheckList
	Purpose:		Represents a checklist on Trello.com.

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
	/// Represents a checklist.
	/// </summary>
	public class CheckList : ExpiringObject, IEquatable<CheckList>, IComparable<CheckList>
	{
		private IJsonCheckList _jsonCheckList;
		private Board _board;
		private Card _card;
		private readonly ExpiringList<CheckItem> _checkItems;
		private Position _position;
		private bool _isDeleted;

		/// <summary>
		/// Gets the board which contains this checklist.
		/// </summary>
		public Board Board
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				if (_jsonCheckList == null) return null;
				return UpdateById(ref _board, EntityRequestType.Board_Read_Refresh, _jsonCheckList.IdBoard);
			}
		}
		/// <summary>
		/// Gets or sets the card which contains this checklist.
		/// </summary>
		public Card Card
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				if (_jsonCheckList == null) return null;
				return UpdateById(ref _card, EntityRequestType.Card_Read_Refresh, _jsonCheckList.IdCard);
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Validator.Entity(value);
				if (_jsonCheckList == null) return;
				if (_jsonCheckList.IdCard == value.Id) return;
				_jsonCheckList.IdCard = value.Id;
				_card = value;
				Parameters.Add("idCard", _jsonCheckList.IdCard);
				Put(EntityRequestType.CheckList_Write_Card);
			}
		}
		/// <summary>
		/// Enumerates the items this checklist contains.
		/// </summary>
		public IEnumerable<CheckItem> CheckItems { get { return _checkItems; } }
		internal ExpiringList<CheckItem> CheckItemsList { get { return _checkItems; } }
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonCheckList != null ? _jsonCheckList.Id : base.Id; }
			internal set
			{
				if (_jsonCheckList != null)
					_jsonCheckList.Id = value;
				base.Id = value;
			}
		}
		/// <summary>
		/// Gets or sets the name of this checklist.
		/// </summary>
		public string Name
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return (_jsonCheckList == null) ? null : _jsonCheckList.Name;
			}
			set
			{
				Validator.Writable();
				Validator.NonEmptyString(value);
				if (_jsonCheckList == null) return;
				if (_jsonCheckList.Name == value) return;
				_jsonCheckList.Name = value;
				Parameters.Add("name", _jsonCheckList.Name);
				Put(EntityRequestType.CheckList_Write_Name);
				MarkForUpdate();
			}
		}
		/// <summary>
		/// Gets or sets the position of this checklist.
		/// </summary>
		public Position Position
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return (_jsonCheckList == null) ? null : _position;
			}
			set
			{
				Validator.Writable();
				Validator.Position(value);
				if (_jsonCheckList == null) return;
				if (_position == value) return;
				_position = value;
				Parameters.Add("pos", _position);
				Put(EntityRequestType.CheckList_Write_Position);
				MarkForUpdate();
			}
		}

		/// <summary>
		/// Creates a new instance of the CheckList class.
		/// </summary>
		public CheckList()
		{
			_jsonCheckList = new InnerJsonCheckList();
			_checkItems = new ExpiringList<CheckItem>(this, EntityRequestType.CheckList_Read_CheckItems);
		}

		/// <summary>
		/// Adds a new item to the checklist.
		/// </summary>
		/// <param name="name">The name of the new item.</param>
		/// <param name="state">The initial state of the new item.</param>
		/// <param name="position">The position of the new item.  Default is Bottom.  Invalid positions are ignored.</param>
		/// <returns>The checkitem.</returns>
		public CheckItem AddCheckItem(string name, CheckItemStateType state = CheckItemStateType.Incomplete, Position position = null)
		{
			if (_isDeleted) return null;
			Validator.Writable();
			Validator.NonEmptyString(name);
			Parameters.Add("_id", Id);
			Parameters.Add("name", name);
			Parameters.Add("checked", (state == CheckItemStateType.Complete).ToLowerString());
			if ((position != null) && position.IsValid)
				Parameters.Add("pos", position);
			var checkItem = EntityRepository.Download<CheckItem>(EntityRequestType.CheckList_Write_AddCheckItem, Parameters);
			checkItem.Owner = this;
			UpdateDependencies(checkItem);
			_checkItems.MarkForUpdate();
			return checkItem;
		}
		/// <summary>
		/// Deletes this checklist.  This cannot be undone.
		/// </summary>
		public void Delete()
		{
			if (_isDeleted) return;
			Validator.Writable();
			Parameters.Add("_id", Id);
			EntityRepository.Upload(EntityRequestType.CheckList_Write_Delete, Parameters);
			if (_card != null)
				_card.CheckListsList.MarkForUpdate();
			_isDeleted = true;
		}
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(CheckList other)
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
			if (!(obj is CheckList)) return false;
			return Equals((CheckList) obj);
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
		public int CompareTo(CheckList other)
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
			AddDefaultParameters();
			EntityRepository.Refresh(this, EntityRequestType.CheckList_Read_Refresh);
			return true;
		}

		internal override void ApplyJson(object obj)
		{
			_jsonCheckList = (IJsonCheckList)obj;
			_position = ((_jsonCheckList != null) && _jsonCheckList.Pos.HasValue)
			            	? new Position(_jsonCheckList.Pos.Value)
			            	: Position.Unknown;
		}
		internal override void PropagateDependencies()
		{
			UpdateDependencies(_checkItems);
		}

		private void Put(EntityRequestType requestType)
		{
			Parameters.Add("_id", Id);
			EntityRepository.Upload(requestType, Parameters);
		}
	}
}

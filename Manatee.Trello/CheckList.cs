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
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a checklist.
	/// </summary>
	public class CheckList : ExpiringObject, IEquatable<CheckList>, IComparable<CheckList>, ICanWebhook
	{
		private IJsonCheckList _jsonCheckList;
		private Board _board;
		private Card _card;
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
				return UpdateById(ref _card, EntityRequestType.Card_Read_Refresh, _jsonCheckList.IdCard);
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Validator.Entity(value);
				if (_jsonCheckList.IdCard == value.Id) return;
				_jsonCheckList.IdCard = value.Id;
				_card = value;
				Parameters.Add("idCard", _jsonCheckList.IdCard);
				Upload(EntityRequestType.CheckList_Write_Card);
			}
		}
		/// <summary>
		/// Enumerates the items this checklist contains.
		/// </summary>
		public IEnumerable<CheckItem> CheckItems { get { return BuildList<CheckItem>(EntityRequestType.CheckList_Read_CheckItems).OrderBy(ci => ci.Position); } }
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonCheckList.Id; }
			internal set { _jsonCheckList.Id = value; }
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
				return _jsonCheckList.Name;
			}
			set
			{
				Validator.Writable();
				Validator.NonEmptyString(value);
				if (_jsonCheckList.Name == value) return;
				_jsonCheckList.Name = value;
				Parameters.Add("name", _jsonCheckList.Name);
				Upload(EntityRequestType.CheckList_Write_Name);
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
				return _position;
			}
			set
			{
				Validator.Writable();
				Validator.Position(value);
				if (_position == value) return;
				_position = value;
				Parameters.Add("pos", _position);
				Upload(EntityRequestType.CheckList_Write_Position);
				MarkForUpdate();
			}
		}
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return _jsonCheckList is InnerJsonCheckList; } }

		/// <summary>
		/// Creates a new instance of the CheckList class.
		/// </summary>
		public CheckList()
		{
			_jsonCheckList = new InnerJsonCheckList();
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
			Parameters["_id"] = Id;
			Parameters.Add("name", name);
			Parameters.Add("checked", (state == CheckItemStateType.Complete).ToLowerString());
			if ((position != null) && position.IsValid)
				Parameters.Add("pos", position);
			var checkItem = EntityRepository.Download<CheckItem>(EntityRequestType.CheckList_Write_AddCheckItem, Parameters);
			checkItem.Owner = this;
			UpdateDependencies(checkItem);
			return checkItem;
		}
		/// <summary>
		/// Converts all CheckItems to cards and optionally deletes the CheckList.
		/// </summary>
		/// <param name="deleteCheckList">true if the CheckList is to be deleted after conversion.</param>
		/// <returns>The Cards which represent the CheckItems.</returns>
		public IEnumerable<Card> ConvertAllCheckItemsToCards(bool deleteCheckList = true)
		{
			var cards = CheckItems.Select(checkItem => checkItem.ConvertToCard());
			if (deleteCheckList)
				Delete();
			return cards;
		}
		/// <summary>
		/// Duplicates all CheckItems to cards, leaving the CheckItems in place.
		/// </summary>
		/// <returns>The Cards which represent the CheckItems.</returns>
		public IEnumerable<Card> DuplicateAllCheckItemsAsCards()
		{
			return CheckItems.Select(checkItem => checkItem.DuplicateAsCard());
		}
		/// <summary>
		/// Deletes this checklist.  This cannot be undone.
		/// </summary>
		public void Delete()
		{
			if (_isDeleted) return;
			Validator.Writable();
			Parameters["_id"] = Id;
			EntityRepository.Upload(EntityRequestType.CheckList_Write_Delete, Parameters);
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
			return Id.GetHashCode();
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
			if (_isDeleted) return false;
			Parameters["_id"] = Id;
			AddDefaultParameters();
			return EntityRepository.Refresh(this, EntityRequestType.CheckList_Read_Refresh);
		}
		/// <summary>
		/// Applies the changes an action represents.
		/// </summary>
		/// <param name="action">The action.</param>
		void ICanWebhook.ApplyAction(Action action)
		{
			if (action.Type != ActionType.UpdateChecklist) return;
			MergeJson(action.Data);
		}

		internal void ForceDeleted(bool deleted)
		{
			_isDeleted = deleted;
		}
		internal override void ApplyJson(object obj)
		{
			_jsonCheckList = (IJsonCheckList)obj;
			_position = _jsonCheckList.Pos.HasValue ? new Position(_jsonCheckList.Pos.Value) : Position.Unknown;
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}
		internal override bool EqualsJson(object obj)
		{
			var json = obj as IJsonCheckList;
			return (json != null) && (json.Id == _jsonCheckList.Id);
		}
		internal override void PropagateDependencies() {}

		private void Upload(EntityRequestType requestType)
		{
			Parameters["_id"] = Id;
			EntityRepository.Upload(requestType, Parameters);
		}
		private void MergeJson(IJsonActionData data)
		{
			_jsonCheckList.Name = data.TryGetString("checkList", "name") ?? _jsonCheckList.Name;
			_jsonCheckList.IdBoard = data.TryGetString("checkList", "idBoard") ?? _jsonCheckList.IdBoard;
			_jsonCheckList.IdCard = data.TryGetString("checkList", "idCard") ?? _jsonCheckList.IdCard;
			_jsonCheckList.Pos = data.TryGetNumber("checkList", "pos") ?? _jsonCheckList.Pos;
		}
	}
}

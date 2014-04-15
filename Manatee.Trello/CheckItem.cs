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
 
	File Name:		CheckItem.cs
	Namespace:		Manatee.Trello
	Class Name:		CheckItem
	Purpose:		Represents an item in a checklist on Trello.com.

***************************************************************************************/
using System;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents an item in a checklist.
	/// </summary>
	public class CheckItem : ExpiringObject, IEquatable<CheckItem>, IComparable<CheckItem>, ICanWebhook
	{
		private static readonly OneToOneMap<CheckItemStateType, string> _stateMap;

		private IJsonCheckItem _jsonCheckItem;
		private Position _position;
		private CheckItemStateType _state = CheckItemStateType.Unknown;
		private bool _isDeleted;

		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonCheckItem.Id; }
			internal set { _jsonCheckItem.Id = value; }
		}
		/// <summary>
		/// Gets or sets the name of the checklist item.
		/// </summary>
		public string Name
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return _jsonCheckItem.Name;
			}
			set
			{
				if (_isDeleted) return;				
				Validator.Writable();
				Validator.NonEmptyString(value);
				if (_jsonCheckItem.Name == value) return;
				_jsonCheckItem.Name = value;
				Parameters.Add("value", _jsonCheckItem.Name);
				Upload(EntityRequestType.CheckItem_Write_Name);
			}
		}
		/// <summary>
		/// Gets or sets the position of the checklist item.
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
				Parameters.Add("value", _position);
				Upload(EntityRequestType.CheckItem_Write_Position);
				MarkForUpdate();
			}
		}
		/// <summary>
		/// Gets or sets the check state of the checklist item.
		/// </summary>
		public CheckItemStateType State
		{
			get
			{
				if (_isDeleted) return CheckItemStateType.Unknown;
				VerifyNotExpired();
				return _state;
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Validator.Enumeration(value);
				if (_state == value) return;
				_state = value;
				UpdateApiState();
				Parameters.Add("value", _jsonCheckItem.State);
				Upload(EntityRequestType.CheckItem_Write_State);
			}
		}
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return _jsonCheckItem is InnerJsonCheckItem; } }

		static CheckItem()
		{
			_stateMap = new OneToOneMap<CheckItemStateType, string>
						{
							{CheckItemStateType.Incomplete, "incomplete"},
							{CheckItemStateType.Complete, "complete"},
						};
		}
		/// <summary>
		/// Creates a new instance of the CheckList class.
		/// </summary>
		public CheckItem()
		{
			_jsonCheckItem = new InnerJsonCheckItem();
		}

		/// <summary>
		/// Converts the CheckItem to a Card and deletes the CheckItem.
		/// </summary>
		/// <returns></returns>
		public Card ConvertToCard()
		{
			if (_isDeleted) return null;
			var card = DuplicateAsCard();
			Delete();
			return card;
		}
		/// <summary>
		/// Converts the CheckItem to a Card, leaving the CheckItem in place.
		/// </summary>
		/// <returns></returns>
		public Card DuplicateAsCard()
		{
			if (_isDeleted) return null;
			var hostChecklist = Owner as CheckList;
			if (hostChecklist == null) return null;
			var hostCard = hostChecklist.Card;
			var hostList = hostCard.List;
			return hostList.AddCard(Name, position: Position.Bottom);
		}
		/// <summary>
		/// Deletes this checklist item.  This cannot be undone.
		/// </summary>
		public void Delete()
		{
			if (_isDeleted) return;
			Validator.Writable();
			Parameters["_id"] = Id;
			Parameters.Add("_checkListId", Owner.Id);
			EntityRepository.Upload(EntityRequestType.CheckItem_Write_Delete, Parameters);
			_isDeleted = true;
		}
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(CheckItem other)
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
			if (!(obj is CheckItem)) return false;
			return Equals((CheckItem) obj);
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
		public int CompareTo(CheckItem other)
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
			return string.Format("{0} : {1}", Name, State);
		}
		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public override bool Refresh()
		{
			if (_isDeleted) return false;
			Parameters.Add("_checkListId", Owner.Id);
			Parameters["_id"] = Id;
			return EntityRepository.Refresh(this, EntityRequestType.CheckItem_Read_Refresh);
		}
		/// <summary>
		/// Applies the changes an action represents.
		/// </summary>
		/// <param name="action">The action.</param>
		void ICanWebhook.ApplyAction(Action action)
		{
			if (action.Type != ActionType.UpdateCheckItemStateOnCard) return;
			MergeJson(action.Data);
		}

		internal override void ApplyJson(object obj)
		{
			_jsonCheckItem = (IJsonCheckItem)obj;
			_position = _jsonCheckItem.Pos.HasValue ? new Position(_jsonCheckItem.Pos.Value) : Position.Unknown;
			UpdateState();
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}
		internal override bool EqualsJson(object obj)
		{
			var json = obj as IJsonCheckItem;
			return (json != null) && (json.Id == _jsonCheckItem.Id);

		}
		internal void ForceDeleted(bool deleted)
		{
			_isDeleted = deleted;
		}

		private void Upload(EntityRequestType requestedType)
		{
			Parameters["_id"] = Id;
			Parameters.Add("_checkListId", Owner.Id);
			Parameters.Add("_cardId", Owner.Owner.Id);
			AddDefaultParameters();
			EntityRepository.Upload(requestedType, Parameters);
		}
		private void UpdateState()
		{
			_state = _stateMap.Any(kvp => kvp.Value == _jsonCheckItem.State) ? _stateMap[_jsonCheckItem.State] : CheckItemStateType.Unknown;
		}
		private void UpdateApiState()
		{
			if (_stateMap.Any(kvp => kvp.Key == _state))
				_jsonCheckItem.State = _stateMap[_state];
		}
		private void MergeJson(IJsonActionData data)
		{
			_jsonCheckItem.State = data.TryGetString("checkItem", "state") ?? _jsonCheckItem.State;
			_jsonCheckItem.Name = data.TryGetString("checkItem", "name") ?? _jsonCheckItem.Name;
			_jsonCheckItem.Pos = data.TryGetNumber("checkItem", "pos") ?? _jsonCheckItem.Pos;
		}
	}
}

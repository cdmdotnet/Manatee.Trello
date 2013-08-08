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
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents an item in a checklist.
	/// </summary>
	public class CheckItem : ExpiringObject, IEquatable<CheckItem>, IComparable<CheckItem>
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
			get { return _jsonCheckItem != null ? _jsonCheckItem.Id : base.Id; }
			internal set
			{
				if (_jsonCheckItem != null)
					_jsonCheckItem.Id = value;
				base.Id = value;
			}
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
				return (_jsonCheckItem == null) ? null : _jsonCheckItem.Name;
			}
			set
			{
				if (_isDeleted) return;				
				Validator.Writable();
				Validator.NonEmptyString(value);
				if (_jsonCheckItem == null) return;
				if (_jsonCheckItem.Name == value) return;
				_jsonCheckItem.Name = value;
				Parameters.Add("value", _jsonCheckItem.Name);
				Put("name");
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
				if (_jsonCheckItem == null) return;
				if (_position == value) return;
				_position = value;
				Parameters.Add("value", _position);
				Put("pos");
				MarkForUpdate();
				((CheckList) Owner).CheckItemsList.MarkForUpdate();
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
				if (_jsonCheckItem == null) return;
				if (_state == value) return;
				_state = value;
				UpdateApiState();
				Parameters.Add("value", _jsonCheckItem.State);
				Put("state");
			}
		}

		internal static string TypeGetKey { get { return "checkItems"; } }
		internal static string TypeKey2 { get { return "checkItem"; } }
		internal override string Key { get { return TypeGetKey; } }
		internal override string Key2 { get { return TypeKey2; } }

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
		internal CheckItem(IJsonCheckItem jsonCheckItem, CheckList owner)
			: this()
		{
			_jsonCheckItem = jsonCheckItem;
			Owner = owner;
			if (Cache != null)
				Cache.Add(this);
		}

		/// <summary>
		/// Deletes this checklist item.  This cannot be undone.
		/// </summary>
		public void Delete()
		{
			if (_isDeleted) return;
			if (Svc == null) return;
			Validator.Writable();
			var endpoint = EndpointGenerator.Default.Generate(Owner, this);
			var request = RequestProvider.Create(endpoint.ToString());
			Api.Delete<IJsonCheckItem>(request);
			((CheckList)Owner).CheckItemsList.MarkForUpdate();
			if (Cache != null)
				Cache.Remove(this);
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
			return base.GetHashCode();
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
			var endpoint = EndpointGenerator.Default.Generate(Owner, this);
			var request = RequestProvider.Create(endpoint.ToString());
			var obj = Api.Get<IJsonCheckItem>(request);
			if (obj == null) return false;
			ApplyJson(obj);
			return true;
		}

		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService() {}

		internal override void ApplyJson(object obj)
		{
			if (obj is IRestResponse)
				_jsonCheckItem = ((IRestResponse<IJsonCheckItem>)obj).Data;
			else
				_jsonCheckItem = (IJsonCheckItem)obj;
			_position = _jsonCheckItem.Pos.HasValue ? new Position(_jsonCheckItem.Pos.Value) : Position.Unknown;
			UpdateState();
		}

		private void Put(string extension)
		{
			if (Svc == null)
			{
				Parameters.Clear();
				return;
			}
			var endpoint = EndpointGenerator.Default.Generate2(((CheckList) Owner).Card, Owner, this);
			endpoint.Append(extension);
			var request = RequestProvider.Create(endpoint.ToString());
			foreach (var parameter in Parameters)
			{
				request.AddParameter(parameter.Key, parameter.Value);
			}
			Api.Put<IJsonCheckItem>(request);
			Parameters.Clear();
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
	}
}

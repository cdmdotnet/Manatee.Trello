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
	//{
	//   "id":"514463bce0807abe320028a2",
	//   "name":"Contribution Areas:",
	//   "idBoard":"5144051cbd0da6681200201e",
	//   "idCard":"5144071650af56251f001927",
	//   "pos":16384,
	//   "checkItems":[
	//      {
	//         "state":"incomplete",
	//         "id":"514463bfd02ebee350000d1c",
	//         "name":"Test development",
	//         "pos":16703
	//      },
	//      {
	//         "state":"incomplete",
	//         "id":"514463c25d9ff5651200248a",
	//         "name":"Debugging",
	//         "pos":33564
	//      },
	//      {
	//         "state":"incomplete",
	//         "id":"514463cf9aead40c4b002126",
	//         "name":"Admiration for a job well done",
	//         "pos":50378
	//      },
	//      {
	//         "state":"incomplete",
	//         "id":"514463f46fb8113b4b0026ba",
	//         "name":"Documentation",
	//         "pos":41971
	//      }
	//   ]
	//}
	/// <summary>
	/// Represents a checklist.
	/// </summary>
	public class CheckList : ExpiringObject, IEquatable<CheckList>
	{
		private IJsonCheckList _jsonCheckList;
		private Board _board;
		private Card _card;
		private readonly ExpiringList<CheckItem, IJsonCheckItem> _checkItems;
		private Position _position;

		/// <summary>
		/// Gets the board which contains this checklist.
		/// </summary>
		public Board Board
		{
			get
			{
				VerifyNotExpired();
				if (_jsonCheckList == null) return null;
				return ((_board == null) || (_board.Id != _jsonCheckList.IdBoard)) && (Svc != null)
						? (_board = Svc.Retrieve<Board>(_jsonCheckList.IdBoard))
						: _board;
			}
		}
		/// <summary>
		/// Gets or sets the card which contains this checklist.
		/// </summary>
		public Card Card
		{
			get
			{
				VerifyNotExpired();
				if (_jsonCheckList == null) return null;
				return ((_card == null) || (_card.Id != _jsonCheckList.IdCard)) && (Svc != null)
				       	? (_card = Svc.Retrieve<Card>(_jsonCheckList.IdCard))
				       	: _card;
			}
			set
			{
				Validate.Writable(Svc);
				Validate.Entity(value);
				if (_jsonCheckList == null) return;
				if (_jsonCheckList.IdCard == value.Id) return;
				_jsonCheckList.IdCard = value.Id;
				Parameters.Add("idCard", _jsonCheckList.IdCard);
				Put();
			}
		}
		/// <summary>
		/// Enumerates the items this checklist contains.
		/// </summary>
		public IEnumerable<CheckItem> CheckItems { get { return _checkItems; } }
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
				VerifyNotExpired();
				return (_jsonCheckList == null) ? null : _jsonCheckList.Name;
			}
			set
			{
				Validate.Writable(Svc);
				Validate.NonEmptyString(value);
				if (_jsonCheckList == null) return;
				if (_jsonCheckList.Name == value) return;
				_jsonCheckList.Name = value;
				Parameters.Add("name", _jsonCheckList.Name);
				Put();
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
				VerifyNotExpired();
				return (_jsonCheckList == null) ? null : _position;
			}
			set
			{
				Validate.Writable(Svc);
				Validate.Position(value);
				if (_jsonCheckList == null) return;
				if (_position == value) return;
				_position = value;
				Parameters.Add("pos", _position);
				Put();
			}
		}
		internal override string Key { get { return "checklists"; } }
		/// <summary>
		/// Gets whether the entity is a cacheable item.
		/// </summary>
		protected override bool Cacheable { get { return true; } }

		/// <summary>
		/// Creates a new instance of the CheckList class.
		/// </summary>
		public CheckList()
		{
			_jsonCheckList = new InnerJsonCheckList();
			_checkItems = new ExpiringList<CheckItem, IJsonCheckItem>(this, "checkItems");
		}

		/// <summary>
		/// Adds a new item to the checklist.
		/// </summary>
		/// <param name="name">The name of the new item.</param>
		/// <param name="isChecked">The initial state of the new item.</param>
		/// <param name="position">The position of the new item.  Default is Bottom.  Invalid positions are ignored.</param>
		/// <returns>The checkitem.</returns>
		public CheckItem AddCheckItem(string name, bool isChecked = false, Position position = null)
		{
			if (Svc == null) return null;
			Validate.Writable(Svc);
			Validate.NonEmptyString(name);
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("name", name);
			request.AddParameter("checked", isChecked);
			if ((position != null) && position.IsValid)
				request.AddParameter("pos", position);
			var jsonCheckItem = Api.Post<IJsonCheckItem>(request);
			var checkItem = new CheckItem(jsonCheckItem, this);
			_checkItems.MarkForUpdate();
			return checkItem;
		}
		/// <summary>
		/// Deletes this checklist.  This cannot be undone.
		/// </summary>
		public void Delete()
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			Api.Delete<IJsonCheckList>(request);	
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
			ApplyJson(Api.Get<IJsonCheckList>(request));
		}

		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService()
		{
			_checkItems.Svc = Svc;
			if (_board != null) _board.Svc = Svc;
			if (_card != null) _card.Svc = Svc;
		}

		internal override void ApplyJson(object obj)
		{
			_jsonCheckList = (IJsonCheckList)obj;
			_position = ((_jsonCheckList != null) && _jsonCheckList.Pos.HasValue)
			            	? new Position(_jsonCheckList.Pos.Value)
			            	: Position.Unknown;
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
			Api.Put<IJsonCheckList>(request);
		}
	}
}

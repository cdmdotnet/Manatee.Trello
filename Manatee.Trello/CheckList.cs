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
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;

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
	public class CheckList : JsonCompatibleExpiringObject, IEquatable<CheckList>
	{
		private string _boardId;
		private Board _board;
		private string _cardId;
		private Card _card;
		private readonly ExpiringList<CheckList, CheckItem> _checkItems;
		private string _name;
		private Position _position;

		/// <summary>
		/// Gets the board which contains this checklist.
		/// </summary>
		public Board Board
		{
			get
			{
				VerifyNotExpired();
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null) ? (_board = Svc.Retrieve<Board>(_boardId)) : _board;
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
				return ((_card == null) || (_card.Id != _cardId)) && (Svc != null) ? (_card = Svc.Retrieve<Card>(_cardId)) : _card;
			}
			set
			{
				Validate.Entity(value);
				if (_cardId == value.Id) return;
				_cardId = value.Id;
				Parameters.Add("idCard", _cardId);
				Put();
			}
		}
		/// <summary>
		/// Enumerates the items this checklist contains.
		/// </summary>
		public IEnumerable<CheckItem> CheckItems { get { return _checkItems; } }
		/// <summary>
		/// Gets or sets the name of this checklist.
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

		internal override string Key { get { return "checklists"; } }

		/// <summary>
		/// Creates a new instance of the CheckList class.
		/// </summary>
		public CheckList()
		{
			_checkItems = new ExpiringList<CheckList, CheckItem>(this);
		}
		internal CheckList(TrelloService svc, string id)
			: base(svc, id)
		{
			_checkItems = new ExpiringList<CheckList, CheckItem>(svc, this);
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
			Validate.NonEmptyString(name);
			var request = Svc.RequestProvider.Create<CheckItem>(new[] {Owner, this}, this);
			Parameters.Add("name", name);
			Parameters.Add("checked", isChecked);
			if ((position != null) && position.IsValid)
				Parameters.Add("pos", position);
			var checkItem = Svc.PostAndCache(request);
			_checkItems.MarkForUpdate();
			return checkItem;
		}
		/// <summary>
		/// Deletes this checklist.  This cannot be undone.
		/// </summary>
		public void Delete()
		{
			if (Svc == null) return;
			Svc.DeleteFromCache(Svc.RequestProvider.Create<CheckList>(Id));	
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
			_cardId = obj.TryGetString("idCard");
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
			           		{"idBoard", _boardId},
			           		{"idCard", _cardId},
			           		{"checkItems", _checkItems.Select(c => c.Id).ToJson()},
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
		public bool Equals(CheckList other)
		{
			return Id == other.Id;
		}

		internal override bool Match(string id)
		{
			return Id == id;
		}
		internal override void Refresh(ExpiringObject entity)
		{
			var checkList = entity as CheckList;
			if (checkList == null) return;
			_boardId = checkList._boardId;
			_cardId = checkList._cardId;
			_name = checkList._name;
			_position = checkList._position;
			_isInitialized = true;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			var entity = Svc.Api.Get(Svc.RequestProvider.Create<CheckList>(Id));
			Refresh(entity);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateSerivce()
		{
			_checkItems.Svc = Svc;
			if (_board != null) _board.Svc = Svc;
			if (_card != null) _card.Svc = Svc;
		}

		private void Put()
		{
			if (Svc == null)
			{
				Parameters.Clear();
				return;
			}
			var request = Svc.RequestProvider.Create<CheckItem>(new[] { Owner, this }, this);
			Svc.PutAndCache(request);
		}
	}
}

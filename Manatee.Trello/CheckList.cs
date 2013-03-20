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
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

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
	public class CheckList : EntityBase, IEquatable<CheckList>
	{
		private string _boardId;
		private Board _board;
		private string _cardId;
		private Card _card;
		private readonly ExpiringList<CheckList, CheckItem> _checkItems;
		private string _name;
		private int? _pos;

		public Board Board
		{
			get
			{
				VerifyNotExpired();
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null) ? (_board = Svc.Retrieve<Board>(_boardId)) : _board;
			}
		}
		public Card Card
		{
			get
			{
				VerifyNotExpired();
				return ((_card == null) || (_card.Id != _cardId)) && (Svc != null) ? (_card = Svc.Retrieve<Card>(_cardId)) : _card;
			}
		}
		public IEnumerable<CheckItem> CheckItems { get { return _checkItems; } }
		public string Name
		{
			get
			{
				VerifyNotExpired();
				return _name;
			}
			set { _name = value; }
		}
		public int? Pos
		{
			get
			{
				VerifyNotExpired();
				return _pos;
			}
			set { _pos = value; }
		}

		public CheckList()
		{
			_checkItems = new ExpiringList<CheckList, CheckItem>(this);
		}
		internal CheckList(TrelloService svc, string id)
			: base(svc, id)
		{
			_checkItems = new ExpiringList<CheckList, CheckItem>(svc, this);
		}
		
		//public CheckItem AddCheckItem(string name)
		//{
		//    var request = new AddCheckItemToCheckListRequest(this, name);
		//    var checkItem = Svc.PostAndCache<CheckList, CheckItem, AddCheckItemToCheckListRequest>(request);
		//    checkItem.Svc = Svc;
		//    _checkItems.MarkForUpdate();
		//    return checkItem;
		//}
		public void Delete()
		{
			
		}
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			_boardId = obj.TryGetString("idBoard");
			_cardId = obj.TryGetString("idCard");
			_name = obj.TryGetString("name");
			_pos = (int?) obj.TryGetNumber("pos");
		}
		public override JsonValue ToJson()
		{
			VerifyNotExpired();
			var json = new JsonObject
			           	{
			           		{"id", Id},
			           		{"idBoard", _boardId},
			           		{"idCard", _cardId},
			           		{"checkItems", _checkItems.Select(c => c.Id).ToJson()},
			           		{"name", _name},
			           		{"pos", _pos.HasValue ? _pos.Value : JsonValue.Null}
			           	};
			return json;
		}
		public  bool Equals(CheckList other)
		{
			return Id == other.Id;
		}

		internal override void Refresh(ExpiringObject entity)
		{
			var checkList = entity as CheckList;
			if (checkList == null) return;
			_boardId = checkList._boardId;
			_cardId = checkList._cardId;
			_name = checkList._name;
			_pos = checkList._pos;
		}
		internal override bool Match(string id)
		{
			return Id == id;
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.Get(new Request<CheckList>(Id));
			Refresh(entity);
		}
		protected override void PropigateSerivce()
		{
			_checkItems.Svc = Svc;
			if (_board != null) _board.Svc = Svc;
			if (_card != null) _card.Svc = Svc;
		}
	}
}

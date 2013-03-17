using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
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
	public class CheckList : EntityBase
	{
		private string _boardId;
		private Board _board;
		private string _cardId;
		private Card _card;
		private readonly ExpiringList<CheckList, CheckItem> _checkItems;
		private string _name;
		private int _pos;

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
		public IEntityCollection<CheckItem> CheckItems
		{
			get { return _checkItems; }
		}
		public string Name
		{
			get
			{
				VerifyNotExpired();
				return _name;
			}
			set { _name = value; }
		}
		public int Pos
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

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			_boardId = obj.TryGetString("idBoard");
			_cardId = obj.TryGetString("idCard");
			_name = obj.TryGetString("name");
			_pos = (int)obj.TryGetNumber("pos");
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
			           		{"pos", _pos}
			           	};
			return json;
		}
		public override bool Equals(EquatableExpiringObject other)
		{
			var checkList = other as CheckList;
			if (checkList == null) return false;
			return Id == checkList.Id;
		}

		internal override void Refresh(EquatableExpiringObject entity)
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
			var entity = Svc.Api.GetEntity<CheckList>(Id);
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

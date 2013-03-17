using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Serialization;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	//   "checkItems":[
	//      {
	//         "state":"incomplete",
	//         "id":"514463bfd02ebee350000d1c",
	//         "name":"Test development",
	//         "pos":16703
	//      },
	public class CheckItem : OwnedEntityBase<CheckList>
	{
		private static readonly OneToOneMap<CheckItemStates, string> _stateMap;

		private string _apiState;
		private string _name;
		private int _pos;
		private CheckItemStates _state;

		public string Id { get; private set; }
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
		public CheckItemStates State
		{
			get { return _state; }
			set
			{
				_state = value;
				UpdateApiState();
			}
		}

		static CheckItem()
		{
			_stateMap = new OneToOneMap<CheckItemStates, string>
			           	{
			           		{CheckItemStates.Incomplete, "incomplete"},
			           		{CheckItemStates.Complete, "complete"},
			           	};
		}
		public CheckItem() {}
		internal CheckItem(TrelloService svc, CheckList owner)
			: base(svc, owner) {}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Name = obj.TryGetString("name");
			Pos = (int) obj.TryGetNumber("pos");
			_apiState = obj.TryGetString("state");
			UpdateState();
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"id", Id},
			           		{"name", Name},
			           		{"pos", Pos},
			           		{"state", _apiState}
			           	};
			return json;
		}
		public override bool Equals(EquatableExpiringObject other)
		{
			var checkItem = other as CheckItem;
			if (checkItem == null) return false;
			return Id == checkItem.Id;
		}

		internal override void Refresh(EquatableExpiringObject entity)
		{
			var checkItem = entity as CheckItem;
			if (checkItem == null) return;
		}
		internal override bool Match(string id)
		{
			return Id == id;
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.GetOwnedEntity<CheckList, CheckItem>(Owner.Id);
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}

		private void UpdateState()
		{
			_state = _stateMap.Any(kvp => kvp.Value == _apiState) ? _stateMap[_apiState] : CheckItemStates.Unknown;
		}
		private void UpdateApiState()
		{
			if (_stateMap.Any(kvp => kvp.Key == _state))
				_apiState = _stateMap[_state];
		}
	}
}

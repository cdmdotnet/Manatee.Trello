using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Serialization;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//   "checkItemStates":[
	//      {
	//         "idCheckItem":"514463bfd02ebee350000d1c",
	//         "state":"complete"
	//      }
	public class CheckItemState : OwnedEntityBase<Card>
	{
		private static readonly OneToOneMap<CheckItemStates, string> _stateMap;

		private string _apiState;
		private string _checkItemId;
		private CheckItemStates _state;

		public string CheckItemId
		{
			get
			{
				VerifyNotExpired();
				return _checkItemId;
			}
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

		static CheckItemState()
		{
			_stateMap = new OneToOneMap<CheckItemStates, string>
			           	{
			           		{CheckItemStates.Incomplete, "incomplete"},
			           		{CheckItemStates.Complete, "complete"},
			           	};
		}
		public CheckItemState() {}
		internal CheckItemState(TrelloService svc, Card owner)
			: base(svc, owner) {}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_checkItemId = obj.TryGetString("idCheckItem");
			_apiState = obj.TryGetString("state");
			UpdateState();
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"idCheckItem", _checkItemId},
			           		{"state", _apiState}
			           	};
			return json;
		}
		public override bool Equals(EquatableExpiringObject other)
		{
			var state = other as CheckItemState;
			if (state == null) return false;
			return (Owner == state.Owner) && (_checkItemId == state._checkItemId);
		}

		internal override void Refresh(EquatableExpiringObject entity)
		{
			var checkItemState = entity as CheckItemState;
			if (checkItemState == null) return;
			_checkItemId = checkItemState._checkItemId;
			_apiState = checkItemState._apiState;
			UpdateState();
		}
		internal override bool Match(string id)
		{
			return false;
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.GetOwnedEntity<Card, Badges>(Owner.Id);
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}

		private void UpdateState()
		{
			_state = _stateMap[_apiState];
		}
		private void UpdateApiState()
		{
			_apiState = _stateMap[_state];
		}
	}
}

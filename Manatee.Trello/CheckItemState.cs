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
 
	File Name:		CheckItemState.cs
	Namespace:		Manatee.Trello
	Class Name:		CheckItemState
	Purpose:		Represents a the state of a check item contained within a
					card on Trello.com.

***************************************************************************************/
using System;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	//   "checkItemStates":[
	//      {
	//         "idCheckItem":"514463bfd02ebee350000d1c",
	//         "state":"complete"
	//      }
	public class CheckItemState : OwnedEntityBase<Card>, IEquatable<CheckItemState>
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
		public bool Equals(CheckItemState other)
		{
			return (Owner == other.Owner) && (_checkItemId == other._checkItemId);
		}

		internal override void Refresh(ExpiringObject entity) {}
		internal override bool Match(string id)
		{
			return false;
		}

		protected override void Refresh() {}
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

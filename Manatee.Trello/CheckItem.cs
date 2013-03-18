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
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//   "checkItems":[
	//      {
	//         "state":"incomplete",
	//         "id":"514463bfd02ebee350000d1c",
	//         "name":"Test development",
	//         "pos":16703
	//      },
	public class CheckItem : OwnedEntityBase<CheckList>, IEquatable<CheckItem>
	{
		private static readonly OneToOneMap<CheckItemStates, string> _stateMap;

		private string _apiState;
		private string _name;
		private int? _pos;
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
		public int? Pos
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
			Pos = (int?) obj.TryGetNumber("pos");
			_apiState = obj.TryGetString("state");
			UpdateState();
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"id", Id},
			           		{"name", Name},
			           		{"pos", Pos.HasValue ? Pos.Value : JsonValue.Null},
			           		{"state", _apiState}
			           	};
			return json;
		}
		public bool Equals(CheckItem other)
		{
			return Id == other.Id;
		}

		internal override void Refresh(ExpiringObject entity)
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

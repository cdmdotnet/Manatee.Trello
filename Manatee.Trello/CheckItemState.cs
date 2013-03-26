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
using Manatee.Json.Extensions;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//   "checkItemStates":[
	//      {
	//         "idCheckItem":"514463bfd02ebee350000d1c",
	//         "state":"complete"
	//      }
	/// <summary>
	/// Represents a the state of a check item contained within a card.
	/// </summary>
	public class CheckItemState : JsonCompatibleExpiringObject, IEquatable<CheckItemState>
	{
		private static readonly OneToOneMap<CheckItemStateType, string> _stateMap;

		private string _apiState;
		private CheckItemStateType _state;

		/// <summary>
		/// Gets the checked state of the checklist item.
		/// </summary>
		/// <remarks>
		/// To set this, use the State property on the CheckItem object.  This class is intended
		/// for reporting on the Card object.
		/// </remarks>
		public CheckItemStateType State
		{
			get
			{
				VerifyNotExpired();
				return _state;
			}
		}

		static CheckItemState()
		{
			_stateMap = new OneToOneMap<CheckItemStateType, string>
			           	{
			           		{CheckItemStateType.Incomplete, "incomplete"},
			           		{CheckItemStateType.Complete, "complete"},
			           	};
		}
		/// <summary>
		/// Creates a new instance of the CheckItemState object.
		/// </summary>
		public CheckItemState() {}
		internal CheckItemState(TrelloService svc, Card owner)
			: base(svc, owner) {}

		/// <summary>
		/// Builds an object from a JsonValue.
		/// </summary>
		/// <param name="json">The JsonValue representation of the object.</param>
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("idCheckItem");
			_apiState = obj.TryGetString("state");
			UpdateState();
		}
		/// <summary>
		/// Converts an object to a JsonValue.
		/// </summary>
		/// <returns>
		/// The JsonValue representation of the object.
		/// </returns>
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"idCheckItem", Id},
			           		{"state", _apiState}
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
		public bool Equals(CheckItemState other)
		{
			return (Owner == other.Owner) && (Id == other.Id);
		}

		internal override bool Match(string id)
		{
			return false;
		}
		internal override void Refresh(ExpiringObject entity) { }

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get() {}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
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

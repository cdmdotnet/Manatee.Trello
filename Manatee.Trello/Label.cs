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
 
	File Name:		Label.cs
	Namespace:		Manatee.Trello
	Class Name:		Label
	Purpose:		Represents a label as applied to a card on Trello.com.

***************************************************************************************/
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//   "labels":[
	//      {
	//         "color":"green",
	//         "name":""
	//      },
	public class Label : OwnedEntityBase<Card>
	{
		private string _color;
		private string _name;

		public string Color
		{
			get
			{
				VerifyNotExpired();
				return _color;
			}
			set { _color = value; }
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

		public Label() {}
		internal Label(TrelloService svc, Card owner)
			: base(svc, owner) {}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_color = obj.TryGetString("color");
			_name = obj.TryGetString("name");
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"color", _color},
			           		{"name", _name}
			           	};
			return json;
		}
		public override bool Equals(EquatableExpiringObject other)
		{
			var label = other as Label;
			if (label == null) return false;
			return (_color == label._color) && (_name == label._name);
		}

		internal override void Refresh(EquatableExpiringObject entity)
		{
			var label = entity as Label;
			if (label == null) return;
			_color = label._color;
			_name = label._name;
		}
		internal override bool Match(string id)
		{
			return false;
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.GetOwnedEntity<Card, Label>(Owner.Id);
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}
	}
}

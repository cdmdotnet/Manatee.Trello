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
using System;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	//   "labels":[
	//      {
	//         "color":"green",
	//         "name":""
	//      },
	public class Label : OwnedEntityBase<Card>, IEquatable<Label>
	{
		private static readonly OneToOneMap<LabelColor, string> _colorMap;

		private string _apicolor;
		private LabelColor _color;
		private string _name;

		public LabelColor Color
		{
			get
			{
				VerifyNotExpired();
				return _color;
			}
		}
		public string Name
		{
			get
			{
				VerifyNotExpired();
				return _name;
			}
		}

		static Label()
		{
			_colorMap = new OneToOneMap<LabelColor, string>
			            	{
			            		{LabelColor.Green, "green"},
			            		{LabelColor.Yellow, "yellow"},
			            		{LabelColor.Orange, "orange"},
			            		{LabelColor.Red, "red"},
			            		{LabelColor.Purple, "purple"},
			            		{LabelColor.Blue, "blue"},
			            	};
		}
		public Label() {}
		internal Label(TrelloService svc, Card owner)
			: base(svc, owner) {}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_apicolor = obj.TryGetString("color");
			_name = obj.TryGetString("name");
			UpdateColor();
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"color", _apicolor},
			           		{"name", _name}
			           	};
			return json;
		}
		public bool Equals(Label other)
		{
			return _apicolor == other._apicolor;
		}

		internal override void Refresh(ExpiringObject entity)
		{
			var label = entity as Label;
			if (label == null) return;
			_apicolor = label._apicolor;
			_name = label._name;
			UpdateColor();
		}
		internal override bool Match(string id)
		{
			return false;
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.Get(new Request<Card, Label>(Owner.Id));
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}

		private void UpdateColor()
		{
			_color = _colorMap.Any(kvp => kvp.Value == _apicolor) ? _colorMap[_apicolor] : LabelColor.Unknown;
		}
		private void UpdateApiColor()
		{
			if (_colorMap.Any(kvp => kvp.Key == _color))
				_apicolor = _colorMap[_color];
		}
	}
}

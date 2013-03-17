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

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
 
	File Name:		LabelNames.cs
	Namespace:		Manatee.Trello
	Class Name:		LabelNames
	Purpose:		Defines a set of labels for a board on Trello.com.

***************************************************************************************/
using System;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	//   "labelNames":{
	//      "red":"",
	//      "orange":"",
	//      "yellow":"",
	//      "green":"",
	//      "blue":"",
	//      "purple":""
	//   }
	public class LabelNames : JsonCompatibleExpiringObject
	{
		private string _red;
		private string _orange;
		private string _yellow;
		private string _green;
		private string _blue;
		private string _purple;

		public string Red
		{
			get
			{
				VerifyNotExpired();
				return _red;
			}
			set
			{
				_red = value;
				Parameters.Add("value", value);
				Put("red");
			}
		}
		public string Orange
		{
			get
			{
				VerifyNotExpired();
				return _orange;
			}
			set
			{
				_orange = value;
				Parameters.Add("value", value);
				Put("orange");
			}
		}
		public string Yellow
		{
			get
			{
				VerifyNotExpired();
				return _yellow;
			}
			set
			{
				_yellow = value;
				Parameters.Add("value", value);
				Put("yellow");
			}
		}
		public string Green
		{
			get
			{
				VerifyNotExpired();
				return _green;
			}
			set
			{
				_green = value;
				Parameters.Add("value", value);
				Put("green");
			}
		}
		public string Blue
		{
			get
			{
				VerifyNotExpired();
				return _blue;
			}
			set
			{
				_blue = value;
				Parameters.Add("value", value);
				Put("blue");
			}
		}
		public string Purple
		{
			get
			{
				VerifyNotExpired();
				return _purple;
			}
			set
			{
				_purple = value;
				Parameters.Add("value", value);
				Put("purple");
			}
		}

		public LabelNames() {}
		public LabelNames(TrelloService svc, Board owner)
			: base(svc, owner) {}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_red = obj.TryGetString("red");
			_orange = obj.TryGetString("orange");
			_yellow = obj.TryGetString("yellow");
			_green = obj.TryGetString("green");
			_blue = obj.TryGetString("blue");
			_purple = obj.TryGetString("purple");
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"red", _red},
			           		{"orange", _orange},
			           		{"yellow", _yellow},
			           		{"green", _green},
			           		{"blue", _blue},
			           		{"purple", _purple}
			           	};
			return json;
		}

		internal override void Refresh(ExpiringObject entity)
		{
			var labels = entity as LabelNames;
			if (labels == null) return;
			_red = labels._red;
			_orange = labels._orange;
			_yellow = labels._yellow;
			_green = labels._green;
			_blue = labels._blue;
			_purple = labels._purple;
		}
		internal override bool Match(string id)
		{
			return false;
		}

		protected override void Get()
		{
			var entity = Svc.Api.Get(new Request<LabelNames>(new[] {Owner, this}));
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}

		private void Put(string extension)
		{
			var request = new Request<LabelNames>(new[] {Owner, this}, this, extension);
			Svc.PutAndCache(request);
		}
	}
}

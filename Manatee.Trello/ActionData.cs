﻿/***************************************************************************************

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
 
	File Name:		ActionData.cs
	Namespace:		Manatee.Trello
	Class Name:		ActionData
	Purpose:		Represents relevant data for an action on Trello.com.

***************************************************************************************/
using System;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	//  "data":{
	//     "board":{
	//        "name":"Manatee.Json",
	//        "id":"50d227239c7b29575f000f99"
	//     },
	//     "idMember":"514464db3fa062da6e00254f"
	//  },
	public class ActionData : JsonCompatibleExpiringObject
	{
		public JsonObject Data { get; set; }

		public ActionData() {}
		public ActionData(TrelloService svc, Action owner)
			: base(svc, owner) {}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Data = obj;
		}
		public override JsonValue ToJson()
		{
			return Data;
		}

		internal override void Refresh(ExpiringObject entity)
		{
			var data = entity as ActionData;
			if (data == null) return;
			Data = data.Data;
		}
		internal override bool Match(string id)
		{
			return false;
		}

		protected override void Get()
		{
			var entity = Svc.Api.Get(new Request<ActionData>(new[] {Owner, this}));
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}
	}
}
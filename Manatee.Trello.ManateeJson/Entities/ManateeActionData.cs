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
 
	File Name:		ManateeActionData.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeActionData
	Purpose:		Implements IJsonActionData for Manatee.Json.

***************************************************************************************/

using System;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeActionData : IJsonActionData, IJsonSerializable
	{
		public IJsonAttachment Attachment { get; set; }
		public IJsonBoard Board { get; set; }
		public IJsonBoard BoardSource { get; set; }
		public IJsonBoard BoardTarget { get; set; }
		public IJsonCard Card { get; set; }
		public IJsonCard CardSource { get; set; }
		public IJsonCheckItem CheckItem { get; set; }
		public IJsonCheckList CheckList { get; set; }
		public DateTime? DateLastEdited { get; set; }
		public IJsonList List { get; set; }
		public IJsonList ListAfter { get; set; }
		public IJsonList ListBefore { get; set; }
		public IJsonMember Member { get; set; }
		public IJsonOrganization Org { get; set; }
		public IJsonActionOldData Old { get; set; }
		public string Text { get; set; }
		public string Value { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Attachment = obj.Deserialize<IJsonAttachment>(serializer, "attachment");
			Board = obj.Deserialize<IJsonBoard>(serializer, "board");
			BoardSource = obj.Deserialize<IJsonBoard>(serializer, "boardSource");
			BoardTarget = obj.Deserialize<IJsonBoard>(serializer, "boardTarget");
			Card = obj.Deserialize<IJsonCard>(serializer, "card");
			CheckItem = obj.Deserialize<IJsonCheckItem>(serializer, "checkItem");
			CheckList = obj.Deserialize<IJsonCheckList>(serializer, "checklist");
			DateLastEdited = obj.Deserialize<DateTime?>(serializer, "dateLastEdited");
			List = obj.Deserialize<IJsonList>(serializer, "list");
			ListAfter = obj.Deserialize<IJsonList>(serializer, "listAfter");
			ListBefore = obj.Deserialize<IJsonList>(serializer, "listBefore");
			Member = obj.Deserialize<IJsonMember>(serializer, "member") ??
			         obj.Deserialize<IJsonMember>(serializer, "idMember") ??
			         obj.Deserialize<IJsonMember>(serializer, "idMemberAdded");
			Old = obj.Deserialize<IJsonActionOldData>(serializer, "old");
			Org = obj.Deserialize<IJsonOrganization>(serializer, "org") ??
			      obj.Deserialize<IJsonOrganization>(serializer, "organization");
			Text = obj.TryGetString("text");
			Value = obj.TryGetString("value");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}

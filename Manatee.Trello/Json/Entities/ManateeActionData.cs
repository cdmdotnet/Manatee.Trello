﻿using System;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
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
		public IJsonCustomFieldDefinition CustomField { get; set; }
		public IJsonLabel Label { get; set; }
		public DateTime? DateLastEdited { get; set; }
		public IJsonList List { get; set; }
		public IJsonList ListAfter { get; set; }
		public IJsonList ListBefore { get; set; }
		public IJsonCollaborator Collaborator { get; set; }
		public IJsonMember Member { get; set; }
		public IJsonOrganization Org { get; set; }
		public IJsonActionOldData Old { get; set; }
		public IJsonPowerUp Plugin { get; set; }
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
			CustomField = obj.Deserialize<IJsonCustomFieldDefinition>(serializer, "customField");
			Label = obj.Deserialize<IJsonLabel>(serializer, "label");
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
			Plugin = obj.Deserialize<IJsonPowerUp>(serializer, "plugin");
			Text = obj.TryGetString("text");
			Value = obj.TryGetString("value");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}

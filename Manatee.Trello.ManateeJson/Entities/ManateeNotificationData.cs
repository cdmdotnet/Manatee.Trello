using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeNotificationData : IJsonNotificationData, IJsonSerializable
	{
		public IJsonAttachment Attachment { get; set; }
		public IJsonBoard Board { get; set; }
		public IJsonBoard BoardSource { get; set; }
		public IJsonBoard BoardTarget { get; set; }
		public IJsonCard Card { get; set; }
		public IJsonCard CardSource { get; set; }
		public IJsonCheckItem CheckItem { get; set; }
		public IJsonCheckList CheckList { get; set; }
		public IJsonList List { get; set; }
		public IJsonList ListAfter { get; set; }
		public IJsonList ListBefore { get; set; }
		public IJsonMember Member { get; set; }
		public IJsonOrganization Org { get; set; }
		public IJsonNotificationOldData Old { get; set; }
		public string Text { get; set; }

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
			List = obj.Deserialize<IJsonList>(serializer, "list");
			ListAfter = obj.Deserialize<IJsonList>(serializer, "listAfter");
			ListBefore = obj.Deserialize<IJsonList>(serializer, "listBefore");
			Member = obj.Deserialize<IJsonMember>(serializer, obj.ContainsKey("member") ? "member" : "idMember");
			Old = obj.Deserialize<IJsonNotificationOldData>(serializer, "old");
			Org = obj.Deserialize<IJsonOrganization>(serializer, "org");
			Text = obj.TryGetString("text");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}
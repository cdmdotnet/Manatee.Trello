using System;
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeAction : IJsonAction, IJsonSerializable
	{
		public string Id { get; set; }
		public IJsonMember MemberCreator { get; set; }
		public IJsonActionData Data { get; set; }
		public ActionType? Type { get; set; }
		public DateTime? Date { get; set; }
		public string Text { get; set; }
		public List<IJsonCommentReaction> Reactions { get; set; }

		public virtual void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			MemberCreator = obj.Deserialize<IJsonMember>(serializer, "memberCreator") ??
							obj.Deserialize<IJsonMember>(serializer, "idMemberCreator");
			Data = obj.Deserialize<IJsonActionData>(serializer, "data");
			Type = obj.Deserialize<ActionType?>(serializer, "type");
			Date = obj.Deserialize<DateTime?>(serializer, "date");
			Reactions = obj.Deserialize<List<IJsonCommentReaction>>(serializer, "reactions");
		}
		public virtual JsonValue ToJson(JsonSerializer serializer)
		{
			return new JsonObject {{"text", Text}};
		}
	}
}

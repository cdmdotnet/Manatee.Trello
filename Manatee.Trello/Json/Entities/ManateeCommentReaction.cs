using System;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeCommentReaction : IJsonCommentReaction, IJsonSerializable
	{
		public string Id { get; set; }
		public IJsonMember Member { get; set; }
		public IJsonAction Comment { get; set; }
		public Emoji Emoji { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Member = obj.Deserialize<IJsonMember>(serializer, "member") ?? obj.Deserialize<IJsonMember>(serializer, "idMember");
			Comment = obj.Deserialize<IJsonAction>(serializer, "action") ?? obj.Deserialize<IJsonAction>(serializer, "idAction");
			Emoji = obj.Deserialize<Emoji>(serializer, "emoji");
		}

		public JsonValue ToJson(JsonSerializer serializer)
		{
			return serializer.Serialize(Emoji);
		}
	}
}
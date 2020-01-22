using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json
{
	internal class EmojiSerializer : Manatee.Json.Serialization.ISerializer
	{
		public bool ShouldMaintainReferences => true;

		public bool Handles(SerializationContextBase context)
		{
			return context.InferredType == typeof(Emoji);
		}

		public JsonValue Serialize(SerializationContext context)
		{
			var emoji = (Emoji) context.Source;
			return new JsonObject
				{
					["unified"] = emoji.Unified
				};
		}

		public object Deserialize(DeserializationContext context)
		{
			return Emojis.GetByUnicodeId(context.LocalValue.Object.TryGetString("unified"));
		}
	}
}
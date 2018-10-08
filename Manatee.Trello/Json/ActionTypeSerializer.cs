using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json
{
	internal class ActionTypeSerializer : Manatee.Json.Serialization.ISerializer
	{
		public bool ShouldMaintainReferences => false;

		public bool Handles(SerializationContext context)
		{
			return context.InferredType == typeof(ActionType);
		}

		public JsonValue Serialize(SerializationContext context)
		{
			return context.Source?.ToString();
		}

		public object Deserialize(SerializationContext context)
		{
			return ActionType.TryParse(context.LocalValue.String, out var actionType) ? actionType : (ActionType?)null;
		}
	}
}
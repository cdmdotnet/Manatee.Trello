using System;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Internal.Licensing
{
	internal class ByteArraySerializer : ISerializer
	{
		public bool ShouldMaintainReferences => false;

		public bool Handles(SerializationContext context)
		{
			return context.InferredType == typeof(byte[]);
		}

		public JsonValue Serialize(SerializationContext context)
		{
			return Convert.ToBase64String((byte[]) context.Source);
		}

		public object Deserialize(SerializationContext context)
		{
			return Convert.FromBase64String(context.LocalValue.String);
		}
	}
}
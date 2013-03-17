using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Implementation
{
	public abstract class JsonCompatibleEquatableExpiringObject : EquatableExpiringObject, IJsonCompatible
	{
		public JsonCompatibleEquatableExpiringObject() {}
		internal JsonCompatibleEquatableExpiringObject(TrelloService svc)
			: base(svc) {}

		public abstract void FromJson(JsonValue json);
		public abstract JsonValue ToJson();
	}
}
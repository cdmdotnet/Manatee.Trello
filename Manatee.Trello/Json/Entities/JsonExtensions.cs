using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal static class JsonExtensions
	{
		public static T Deserialize<T>(this JsonObject obj, JsonSerializer serializer, string key)
		{
			if (!obj.ContainsKey(key)) return default(T);
			return serializer.Deserialize<T>(obj[key]);
		}
		public static void Serialize<T>(this T obj, JsonObject json, JsonSerializer serializer, string key, bool force = false)
		{
			var isDefault = Equals(obj, default(T));
			if (force || !isDefault)
			{
				json[key] = isDefault ? string.Empty : serializer.Serialize(obj);
			}
		}
		public static void SerializeId<T>(this T obj, JsonObject json, string key)
			where T : IJsonCacheable
		{
			if (!Equals(obj, default(T)))
				json[key] = obj.Id;
		}
	}
}

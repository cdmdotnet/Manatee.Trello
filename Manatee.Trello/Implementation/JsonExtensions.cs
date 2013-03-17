using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Implementation
{
	internal static class JsonExtensions
	{
		public static string TryGetString(this JsonObject obj, string key)
		{
			return obj.ContainsKey(key) && (obj[key].Type != JsonValueType.Null) ? obj[key].String : null;
		}
		public static double? TryGetNumber(this JsonObject obj, string key)
		{
			return obj.ContainsKey(key) && (obj[key].Type != JsonValueType.Null) ? obj[key].Number : (double?) null;
		}
		public static bool? TryGetBoolean(this JsonObject obj, string key)
		{
			return obj.ContainsKey(key) && (obj[key].Type != JsonValueType.Null) ? obj[key].Boolean : (bool?) null;
		}
		public static JsonArray TryGetArray(this JsonObject obj, string key)
		{
			return obj.ContainsKey(key) && (obj[key].Type != JsonValueType.Null) ? obj[key].Array : null;
		}
		public static JsonObject TryGetObject(this JsonObject obj, string key)
		{
			return obj.ContainsKey(key) && (obj[key].Type != JsonValueType.Null) ? obj[key].Object : null;
		}

		public static List<T> FromJson<T>(this JsonArray json) where T : IJsonCompatible, new()
		{
			if (json == null) return null;
			var list = new List<T>();
			foreach (var value in json)
			{
				T item = new T();
				item.FromJson(value);
				list.Add(item);
			}
			return list;
		}
		public static List<string> StringsFromJson(this JsonArray json)
		{
			if (json == null) return null;
			return json.Select(v => v.Type == JsonValueType.Null ? null : v.String).ToList();
		}
		public static T FromJson<T>(this JsonObject json) where T : IJsonCompatible, new()
		{
			if (json == null) return default(T);
			T obj = new T();
			obj.FromJson(json);
			return obj;
		}

		public static JsonValue ToJson(this IEnumerable<string> list)
		{
			if (list == null) return JsonValue.Null;
			var json = new JsonArray();
			json.AddRange(list.Select(j => new JsonValue(j)));
			return json;
		}
		public static JsonValue ToJson(this IEnumerable<bool> list)
		{
			if (list == null) return JsonValue.Null;
			var json = new JsonArray();
			json.AddRange(list.Select(j => new JsonValue(j)));
			return json;
		}
		public static JsonValue ToJson(this IEnumerable<bool?> list)
		{
			if (list == null) return JsonValue.Null;
			var json = new JsonArray();
			json.AddRange(list.Select(b => b.HasValue ? new JsonValue(b.Value) : JsonValue.Null));
			return json;
		}
		public static JsonValue ToJson(this IEnumerable<JsonArray> list)
		{
			if (list == null) return JsonValue.Null;
			var json = new JsonArray();
			json.AddRange(list.Select(j => new JsonValue(j)));
			return json;
		}
		public static JsonValue ToJson(this IEnumerable<JsonObject> list)
		{
			if (list == null) return JsonValue.Null;
			var json = new JsonArray();
			json.AddRange(list.Select(j => new JsonValue(j)));
			return json;
		}
		//public static JsonValue ToJson(this IEnumerable<number> list)
		//{
		//    if (list == null) return JsonValue.Null;
		//    var json = new JsonArray();
		//    json.AddRange(list.Select(j => new JsonValue(j)));
		//    return json;
		//}
		public static JsonValue ToJson(this IEnumerable<IJsonCompatible> list)
		{
			if (list == null) return JsonValue.Null;
			var json = new JsonArray();
			json.AddRange(list.Select(j => j == null ? JsonValue.Null : j.ToJson()));
			return json;
		}
	}
}

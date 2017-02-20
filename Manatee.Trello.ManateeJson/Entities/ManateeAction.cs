using System;
using System.Globalization;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeAction : IJsonAction, IJsonSerializable
	{
		public string Id { get; set; }
		public IJsonMember MemberCreator { get; set; }
		public IJsonActionData Data { get; set; }
		public ActionType? Type { get; set; }
		public DateTime? Date { get; set; }
		public string Text { get; set; }

		public virtual void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			MemberCreator = obj.Deserialize<IJsonMember>(serializer, "idMemberCreator");
			Data = obj.Deserialize<IJsonActionData>(serializer, "data");
			Type = obj.Deserialize<ActionType?>(serializer, "type");
#if IOS
			var dateString = obj.TryGetString("date");
			DateTime date;
			if (DateTime.TryParseExact(dateString, "yyyy-MM-ddThh:mm:ss.fffZ", CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
				Date = date.ToLocalTime();
#else
			Date = obj.Deserialize<DateTime?>(serializer, "date");
#endif
		}
		public virtual JsonValue ToJson(JsonSerializer serializer)
		{
			return new JsonObject {{"text", Text}};
		}
	}
}

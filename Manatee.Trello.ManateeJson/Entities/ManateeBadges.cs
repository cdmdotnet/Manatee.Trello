using System;
using System.Globalization;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeBadges : IJsonBadges, IJsonSerializable
	{
		public int? Votes { get; set; }
		public bool? ViewingMemberVoted { get; set; }
		public bool? Subscribed { get; set; }
		public string Fogbugz { get; set; }
		public DateTime? Due { get; set; }
		public bool? Description { get; set; }
		public int? Comments { get; set; }
		public int? CheckItemsChecked { get; set; }
		public int? CheckItems { get; set; }
		public int? Attachments { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Votes = (int?) obj.TryGetNumber("votes");
			ViewingMemberVoted = obj.TryGetBoolean("viewingMemberVoted");
			Subscribed = obj.TryGetBoolean("subscribed");
			Fogbugz = obj.TryGetString("fogbugz");
#if IOS
			var dateString = obj.TryGetString("due");
			DateTime date;
			if (DateTime.TryParseExact(dateString, "yyyy-MM-ddThh:mm:ss.fffZ", CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
				Due = date.ToLocalTime();
#else
			Due = obj.Deserialize<DateTime?>(serializer, "due");
#endif
			Description = obj.TryGetBoolean("description");
			Comments = (int?)obj.TryGetNumber("comments");
			CheckItemsChecked = (int?)obj.TryGetNumber("checkItemsChecked");
			CheckItems = (int?)obj.TryGetNumber("checkItems");
			Attachments = (int?)obj.TryGetNumber("attachments");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}

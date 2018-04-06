using System;
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeBoard : IJsonBoard, IJsonSerializable
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Desc { get; set; }
		public bool? Closed { get; set; }
		public IJsonOrganization Organization { get; set; }
		public bool? Pinned { get; set; }
		public IJsonBoardPreferences Prefs { get; set; }
		public string Url { get; set; }
		public bool? Subscribed { get; set; }
		public IJsonBoard BoardSource { get; set; }
		public bool? Starred { get; set; }
		public DateTime? DateLastActivity { get; set; }
		public DateTime? DateLastView { get; set; }
		public string ShortLink { get; set; }
		public string ShortUrl { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("id");
					Name = obj.TryGetString("name");
					Desc = obj.TryGetString("desc");
					Closed = obj.TryGetBoolean("closed");
					Organization = obj.Deserialize<IJsonOrganization>(serializer, "idOrganization");
					Pinned = obj.TryGetBoolean("pinned");
					Prefs = obj.Deserialize<IJsonBoardPreferences>(serializer, "prefs");
					Url = obj.TryGetString("url");
					Subscribed = obj.TryGetBoolean("subscribed");
					Starred = obj.TryGetBoolean("starred");
					DateLastActivity = obj.Deserialize<DateTime?>(serializer, "dateLastActivity");
					DateLastView = obj.Deserialize<DateTime?>(serializer, "dateLastView");
					ShortLink = obj.TryGetString("shortLink");
					ShortUrl = obj.TryGetString("shortUrl");
					break;
				case JsonValueType.String:
					Id = json.String;
					break;
			}
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();
			Id.Serialize(json, serializer, "id");
			Name.Serialize(json, serializer, "name");
			Desc.Serialize(json, serializer, "desc");
			Closed.Serialize(json, serializer, "closed");
			Pinned.Serialize(json, serializer, "pinned");
			Starred.Serialize(json, serializer, "starred");
			Subscribed.Serialize(json, serializer, "subscribed");
			Organization.SerializeId(json, "idOrganization");
			BoardSource.SerializeId(json, "idBoardSource");
			// Don't serialize the Preferences collection because Trello wants individual properties.
			if (Prefs != null)
			{
				Prefs.PermissionLevel.Serialize(json, serializer, "prefs/permissionLevel");
				Prefs.SelfJoin.Serialize(json, serializer, "prefs/selfJoin");
				Prefs.CardCovers.Serialize(json, serializer, "prefs/cardCovers");
				Prefs.Invitations.Serialize(json, serializer, "prefs/invitations");
				Prefs.Voting.Serialize(json, serializer, "prefs/voting");
				Prefs.Comments.Serialize(json, serializer, "prefs/comments");
				Prefs.CardAging.Serialize(json, serializer, "prefs/cardAging");
				Prefs.CalendarFeed.Serialize(json, serializer, "prefs/calendarFeedEnabled ");
				Prefs.Background.Serialize(json, serializer, "prefs/background");
			}
			return json;
		}
	}
}

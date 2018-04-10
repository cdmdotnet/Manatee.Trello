using System;
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeCard : IJsonCard, IJsonSerializable
	{
		public string Id { get; set; }
		public List<IJsonAction> Actions { get; set; }
		public List<IJsonAttachment> Attachments { get; set; }
		public IJsonBadges Badges { get; set; }
		public List<IJsonCheckList> CheckLists { get; set; }
		public bool? Closed { get; set; }
		public List<IJsonAction> Comments { get; set; }
		public List<IJsonCustomField> CustomFields { get; set; }
		public DateTime? DateLastActivity { get; set; }
		public string Desc { get; set; }
		public DateTime? Due { get; set; }
		public bool? DueComplete { get; set; }
		public IJsonBoard Board { get; set; }
		public IJsonList List { get; set; }
		public int? IdShort { get; set; }
		public string IdAttachmentCover { get; set; }
		public List<IJsonLabel> Labels { get; set; }
		public bool? ManualCoverAttachment { get; set; }
		public List<IJsonMember> Members { get; set; }
		public string Name { get; set; }
		public IJsonPosition Pos { get; set; }
		public List<IJsonPowerUpData> PowerUpData { get; set; }
		public string Url { get; set; }
		public string ShortUrl { get; set; }
		public bool? Subscribed { get; set; }
		public IJsonCard CardSource { get; set; }
		public object UrlSource { get; set; }
		public bool ForceDueDate { get; set; }
		public string IdMembers { get; set; }
		public string IdLabels { get; set; }
		public List<IJsonSticker> Stickers { get; set; }
		public List<IJsonMember> MembersVoted { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("id");
					Attachments = obj.Deserialize<List<IJsonAttachment>>(serializer, "attachments");
					Badges = obj.Deserialize<IJsonBadges>(serializer, "badges");
					CheckLists = obj.Deserialize<List<IJsonCheckList>>(serializer, "checkLists");
					Closed = obj.TryGetBoolean("closed");
					Comments = obj.Deserialize<List<IJsonAction>>(serializer, "comments");
					CustomFields = obj.Deserialize<List<IJsonCustomField>>(serializer, "customFieldItems");
					DateLastActivity = obj.Deserialize<DateTime?>(serializer, "dateLastActivity");
					Due = obj.Deserialize<DateTime?>(serializer, "due");
					DueComplete = obj.TryGetBoolean("dueComplete");
					Desc = obj.TryGetString("desc");
					Board = obj.Deserialize<IJsonBoard>(serializer, "idBoard");
					List = obj.Deserialize<IJsonList>(serializer, "idList");
					IdShort = (int?) obj.TryGetNumber("idShort");
					IdAttachmentCover = obj.TryGetString("idAttachmentCover");
					Labels = obj.Deserialize<List<IJsonLabel>>(serializer, "idLabels");
					ManualCoverAttachment = obj.TryGetBoolean("manualAttachmentCover");
					Members = obj.Deserialize<List<IJsonMember>>(serializer, "members");
					Name = obj.TryGetString("name");
					Pos = obj.Deserialize<IJsonPosition>(serializer, "pos");
					Url = obj.TryGetString("url");
					ShortUrl = obj.TryGetString("shortUrl") ?? obj.TryGetString("shortLink");
					Subscribed = obj.TryGetBoolean("subscribed");
					Stickers = obj.Deserialize<List<IJsonSticker>>(serializer, "stickers");
					MembersVoted = obj.Deserialize<List<IJsonMember>>(serializer, "membersVoted");
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
			Board.SerializeId(json, "idBoard");
			Closed.Serialize(json, serializer, "closed");
			Desc.Serialize(json, serializer, "desc");
			Due.Serialize(json, serializer, "due", ForceDueDate);
			DueComplete.Serialize(json, serializer, "dueComplete");
			List.SerializeId(json, "idList");
			Name.Serialize(json, serializer, "name");
			Pos.Serialize(json, serializer, "pos");
			Subscribed.Serialize(json, serializer, "subscribed");
			CardSource.SerializeId(json, "idCardSource");
			UrlSource.Serialize(json, serializer, "urlSource");
			IdMembers.Serialize(json, serializer, "idMembers");
			IdLabels.Serialize(json, serializer, "idLabels");
			return json;
		}
	}
}

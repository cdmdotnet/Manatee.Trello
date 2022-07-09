using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeCollaborator : IJsonCollaborator, IJsonSerializable
	{
		public string Id { get; set; }
		public string AvatarHash { get; set; }
		public string AvatarUrl { get; set; }
		public string Bio { get; set; }
		public string FullName { get; set; }
		public string Initials { get; set; }
		public string MemberType { get; set; }
		public MemberStatus? Status { get; set; }
		public string Url { get; set; }
		public string Username { get; set; }
		public List<IJsonOrganization> Organizations { get; set; }
		public bool ValidForMerge { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("id");
					AvatarUrl = obj.TryGetString("avatarUrl");
					Bio = obj.TryGetString("bio");
					FullName = obj.TryGetString("fullName");
					Initials = obj.TryGetString("initials");
					MemberType = obj.TryGetString("memberType");
					Status = obj.Deserialize<MemberStatus?>(serializer, "status");
					Url = obj.TryGetString("url");
					Username = obj.TryGetString("username");
					Organizations = obj.Deserialize<List<IJsonOrganization>>(serializer, "organizations");
					ValidForMerge = true;
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
			Bio.Serialize(json, serializer, "bio");
			FullName.Serialize(json, serializer, "fullName");
			Initials.Serialize(json, serializer, "initials");
			Username.Serialize(json, serializer, "username");
			return json;
		}
	}
}

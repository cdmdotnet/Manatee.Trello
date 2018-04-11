using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeMember : IJsonMember, IJsonSerializable
	{
		public string Id { get; set; }
		public string AvatarHash { get; set; }
		public string Bio { get; set; }
		public string FullName { get; set; }
		public string Initials { get; set; }
		public string MemberType { get; set; }
		public MemberStatus? Status { get; set; }
		public string Url { get; set; }
		public string Username { get; set; }
		public AvatarSource? AvatarSource { get; set; }
		public bool? Confirmed { get; set; }
		public string Email { get; set; }
		public string GravatarHash { get; set; }
		public List<string> LoginTypes { get; set; }
		public List<string> Trophies { get; set; }
		public string UploadedAvatarHash { get; set; }
		public List<string> OneTimeMessagesDismissed { get; set; }
		public int? Similarity { get; set; }
		public IJsonMemberPreferences Prefs { get; set; }
		public List<IJsonAction> Actions { get; set; }
		public List<IJsonBoard> Boards { get; set; }
		public List<IJsonOrganization> Organizations { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("id");
					AvatarHash = obj.TryGetString("avatarHash");
					Bio = obj.TryGetString("bio");
					FullName = obj.TryGetString("fullName");
					Initials = obj.TryGetString("initials");
					MemberType = obj.TryGetString("memberType");
					Status = obj.Deserialize<MemberStatus?>(serializer, "status");
					Url = obj.TryGetString("url");
					Username = obj.TryGetString("username");
					AvatarSource = obj.Deserialize<AvatarSource?>(serializer, "avatarSource");
					Confirmed = obj.TryGetBoolean("confirmed");
					Email = obj.TryGetString("email");
					GravatarHash = obj.TryGetString("gravatarHash");
					LoginTypes = obj.Deserialize<List<string>>(serializer, "loginTypes");
					Trophies = obj.Deserialize<List<string>>(serializer, "trophies");
					UploadedAvatarHash = obj.TryGetString("uploadedAvatarHash");
					OneTimeMessagesDismissed = obj.Deserialize<List<string>>(serializer, "oneTimeMessagesDismissed");
					Similarity = (int?) obj.TryGetNumber("similarity");
					Prefs = obj.Deserialize<IJsonMemberPreferences>(serializer, "prefs");
					Actions = obj.Deserialize<List<IJsonAction>>(serializer, "actions");
					Boards = obj.Deserialize<List<IJsonBoard>>(serializer, "boards");
					Organizations = obj.Deserialize<List<IJsonOrganization>>(serializer, "organizations");
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
			AvatarSource.Serialize(json, serializer, "avatarSource");
			Email.Serialize(json, serializer, "email");
			OneTimeMessagesDismissed.Serialize(json, serializer, "oneTimeMessagesDismissed");
			if (Prefs != null)
			{
				json.Add("prefs/minutesBetweenSummaries", Prefs.MinutesBetweenSummaries);
				json.Add("prefs/colorBlind", Prefs.ColorBlind);
			}
			return json;
		}
	}
}

/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ManateeMember.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeMember
	Purpose:		Implements IJsonMember for Manatee.Json.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeMember : IJsonMember, IJsonSerializable
	{
		public string Id { get; set; }
		public string AvatarHash { get; set; }
		public string Bio { get; set; }
		public string FullName { get; set; }
		public string Initials { get; set; }
		public string MemberType { get; set; }
		public MemberStatus Status { get; set; }
		public string Url { get; set; }
		public string Username { get; set; }
		public AvatarSource AvatarSource { get; set; }
		public bool? Confirmed { get; set; }
		public string Email { get; set; }
		public string GravatarHash { get; set; }
		public List<string> LoginTypes { get; set; }
		public List<string> Trophies { get; set; }
		public string UploadedAvatarHash { get; set; }
		public List<string> OneTimeMessagesDismissed { get; set; }
		public int? Similarity { get; set; }

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
					Status = serializer.Deserialize<MemberStatus>(obj.TryGetString("status"));
					Url = obj.TryGetString("url");
					Username = obj.TryGetString("username");
					AvatarSource = serializer.Deserialize<AvatarSource>(obj.TryGetString("avatarSource"));
					Confirmed = obj.TryGetBoolean("confirmed");
					Email = obj.TryGetString("email");
					GravatarHash = obj.TryGetString("gravatarHash");
					LoginTypes = obj.Deserialize<List<string>>(serializer, "loginTypes");
					Trophies = obj.Deserialize<List<string>>(serializer, "trophies");
					UploadedAvatarHash = obj.TryGetString("uploadedAvatarHash");
					OneTimeMessagesDismissed = obj.Deserialize<List<string>>(serializer, "oneTimeMessagesDismissed");
					Similarity = (int?) obj.TryGetNumber("similarity");
					break;
				case JsonValueType.String:
					Id = json.String;
					break;
			}
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return new JsonObject
			       	{
						// TODO: remove read-only properties from serialization
			       		{"id", Id},
			       		{"avatarHash", AvatarHash},
			       		{"bio", Bio},
			       		{"fullName", FullName},
			       		{"initials", Initials},
			       		{"memberType", MemberType},
			       		{"status", serializer.Serialize(Status)},
			       		{"url", Url},
			       		{"username", Username},
			       		{"avatarSource", serializer.Serialize(AvatarSource)},
			       		{"confirmed", Confirmed},
			       		{"email", Email},
			       		{"gravatarHash", GravatarHash},
			       		{"loginTypes", serializer.Serialize(LoginTypes)},
			       		{"trophies", serializer.Serialize(Trophies)},
			       		{"uploadedAvatarHash", UploadedAvatarHash},
			       		{"oneTimeMessagesDismissed", serializer.Serialize(OneTimeMessagesDismissed)},
			       	};
		}
	}
}

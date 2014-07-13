﻿/***************************************************************************************

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
using System.Linq;
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
		public string Status { get; set; }
		public string Url { get; set; }
		public string Username { get; set; }
		public string AvatarSource { get; set; }
		public bool? Confirmed { get; set; }
		public string Email { get; set; }
		public string GravatarHash { get; set; }
		public List<string> LoginTypes { get; set; }
		public List<string> Trophies { get; set; }
		public string UploadedAvatarHash { get; set; }
		public List<string> OneTimeMessagesDismissed { get; set; }

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
					Status = obj.TryGetString("status");
					Url = obj.TryGetString("url");
					Username = obj.TryGetString("username");
					AvatarSource = obj.TryGetString("avatarSource");
					Confirmed = obj.TryGetBoolean("confirmed");
					Email = obj.TryGetString("email");
					GravatarHash = obj.TryGetString("gravatarHash");
					var loginTypes = obj.TryGetArray("loginTypes");
					if (loginTypes != null)
						LoginTypes = loginTypes.Select(j => j.String).ToList();
					var trophies = obj.TryGetArray("trophies");
					if (loginTypes != null)
						Trophies = trophies.Select(j => j.String).ToList();
					UploadedAvatarHash = obj.TryGetString("uploadedAvatarHash");
					var messagesDismissed = obj.TryGetArray("oneTimeMessagesDismissed");
					if (messagesDismissed != null)
						OneTimeMessagesDismissed = messagesDismissed.Select(j => j.String).ToList();
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
			       		{"id", Id},
			       		{"avatarHash", AvatarHash},
			       		{"bio", Bio},
			       		{"fullName", FullName},
			       		{"initials", Initials},
			       		{"memberType", MemberType},
			       		{"status", Status},
			       		{"url", Url},
			       		{"username", Username},
			       		{"avatarSource", AvatarSource},
			       		{"confirmed", Confirmed},
			       		{"email", Email},
			       		{"gravatarHash", GravatarHash},
			       		{"loginTypes", LoginTypes.ToJson()},
			       		{"trophies", Trophies.ToJson()},
			       		{"uploadedAvatarHash", UploadedAvatarHash},
			       		{"oneTimeMessagesDismissed", OneTimeMessagesDismissed.ToJson()},
			       	};
		}
	}
}

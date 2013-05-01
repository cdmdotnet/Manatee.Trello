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
	Namespace:		Manatee.Trello.Json.Manatee.Entities
	Class Name:		ManateeMember
	Purpose:		Implements IJsonMember for Manatee.Json.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Manatee.Entities
{
	internal class ManateeMember : IJsonMember, IJsonCompatible
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

		public void FromJson(JsonValue json)
		{
			if (json.Type != JsonValueType.Object) return;
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
			LoginTypes = obj.TryGetArray("loginTypes").Select(j => j.String).ToList();
			Trophies = obj.TryGetArray("trophies").Select(j => j.String).ToList();
			UploadedAvatarHash = obj.TryGetString("uploadedAvatarHash");
		}
		public JsonValue ToJson()
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
			       	};
		}
	}
}

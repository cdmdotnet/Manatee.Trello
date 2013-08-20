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
 
	File Name:		NewtonsoftMember.cs
	Namespace:		Manatee.Trello.NewtonsoftJson.Entities
	Class Name:		NewtonsoftMember
	Purpose:		Implements IJsonMember for Newtonsoft's Json.Net.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Json;
using Newtonsoft.Json;

namespace Manatee.Trello.NewtonsoftJson.Entities
{
	internal class NewtonsoftMember : IJsonMember
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("avatarHash")]
		public string AvatarHash { get; set; }
		[JsonProperty("bio")]
		public string Bio { get; set; }
		[JsonProperty("fullName")]
		public string FullName { get; set; }
		[JsonProperty("initials")]
		public string Initials { get; set; }
		[JsonProperty("memberType")]
		public string MemberType { get; set; }
		[JsonProperty("status")]
		public string Status { get; set; }
		[JsonProperty("url")]
		public string Url { get; set; }
		[JsonProperty("username")]
		public string Username { get; set; }
		[JsonProperty("avatarSource")]
		public string AvatarSource { get; set; }
		[JsonProperty("confirmed")]
		public bool? Confirmed { get; set; }
		[JsonProperty("email")]
		public string Email { get; set; }
		[JsonProperty("gravatarHash")]
		public string GravatarHash { get; set; }
		[JsonProperty("loginTypes")]
		public List<string> LoginTypes { get; set; }
		[JsonProperty("trophies")]
		public List<string> Trophies { get; set; }
		[JsonProperty("uploadedAvatarHash")]
		public string UploadedAvatarHash { get; set; }
	}
}

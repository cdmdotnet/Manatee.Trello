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
 
	File Name:		NewtonsoftMemberSession.cs
	Namespace:		Manatee.Trello.Json.Newtonsoft.Entities
	Class Name:		NewtonsoftMemberSession
	Purpose:		Implements IJsonMemberSession for Newtonsoft's Json.Net.

***************************************************************************************/
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Manatee.Trello.Json.Newtonsoft.Entities
{
	internal class NewtonsoftMemberSession : IJsonMemberSession
	{
		[JsonProperty("isCurrent")]
		public bool? IsCurrent { get; set; }
		[JsonProperty("isRecent")]
		public bool? IsRecent { get; set; }
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("dateCreated")]
		[JsonConverter(typeof(IsoDateTimeConverter))]
		public DateTime? DateCreated { get; set; }
		[JsonProperty("dateExpires")]
		[JsonConverter(typeof(IsoDateTimeConverter))]
		public DateTime? DateExpires { get; set; }
		[JsonProperty("dateLastUsed")]
		[JsonConverter(typeof(IsoDateTimeConverter))]
		public DateTime? DateLastUsed { get; set; }
		[JsonProperty("ipAddress")]
		public string IpAddress { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("userAgent")]
		public string UserAgent { get; set; }
	}
}
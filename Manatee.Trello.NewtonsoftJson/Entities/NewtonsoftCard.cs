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
 
	File Name:		NewtonsoftCard.cs
	Namespace:		Manatee.Trello.NewtonsoftJson.Entities
	Class Name:		NewtonsoftCard
	Purpose:		Implements IJsonCard for Newtonsoft's Json.Net.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Trello.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Manatee.Trello.NewtonsoftJson.Entities
{
	internal class NewtonsoftCard : IJsonCard
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("closed")]
		public bool? Closed { get; set; }
		[JsonProperty("dateLastActivity")]
		[JsonConverter(typeof(IsoDateTimeConverter))]
		public DateTime? DateLastActivity { get; set; }
		[JsonProperty("desc")]
		public string Desc { get; set; }
		[JsonProperty("due")]
		[JsonConverter(typeof(IsoDateTimeConverter))]
		public DateTime? Due { get; set; }
		[JsonProperty("idBoard")]
		public string IdBoard { get; set; }
		[JsonProperty("idList")]
		public string IdList { get; set; }
		[JsonProperty("idShort")]
		public int? IdShort { get; set; }
		[JsonProperty("idAttachmentCover")]
		public string IdAttachmentCover { get; set; }
		[JsonProperty("manualCoverAttachment")]
		public bool? ManualCoverAttachment { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("pos")]
		public double? Pos { get; set; }
		[JsonProperty("url")]
		public string Url { get; set; }
		[JsonProperty("shortUrl")]
		public string ShortUrl { get; set; }

		[JsonProperty("subscribed")]
		public bool? Subscribed { get; set; }
	}
}

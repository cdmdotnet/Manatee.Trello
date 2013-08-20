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
 
	File Name:		NewtonsoftBadges.cs
	Namespace:		Manatee.Trello.NewtonsoftJson.Entities
	Class Name:		NewtonsoftBadges
	Purpose:		Implements IJsonBadges for Newtonsoft's Json.Net.

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
	internal class NewtonsoftBadges : IJsonBadges
	{
		[JsonProperty("votes")]
		public int? Votes { get; set; }
		[JsonProperty("viewingMemberVoted")]
		public bool? ViewingMemberVoted { get; set; }
		[JsonProperty("subscribed")]
		public bool? Subscribed { get; set; }
		[JsonProperty("fogbugz")]
		public string Fogbugz { get; set; }
		[JsonProperty("due")]
		[JsonConverter(typeof(IsoDateTimeConverter))]
		public DateTime? Due { get; set; }
		[JsonProperty("descriptions")]
		public bool? Description { get; set; }
		[JsonProperty("comments")]
		public int? Comments { get; set; }
		[JsonProperty("checkItemsChecked")]
		public int? CheckItemsChecked { get; set; }
		[JsonProperty("checkItems")]
		public int? CheckItems { get; set; }
		[JsonProperty("attachments")]
		public int? Attachments { get; set; }
	}
}

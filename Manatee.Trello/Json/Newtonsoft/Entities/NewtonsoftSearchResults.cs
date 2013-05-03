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
 
	File Name:		NewtonsoftSearchResults.cs
	Namespace:		Manatee.Trello.Json.Newtonsoft.Entities
	Class Name:		NewtonsoftSearchResults
	Purpose:		Implements IJsonCheckItem for Newtonsoft's Json.Net.

***************************************************************************************/
using System.Collections.Generic;
using Manatee.Trello.Json.Newtonsoft.Converters;
using Newtonsoft.Json;

namespace Manatee.Trello.Json.Newtonsoft.Entities
{
	internal class NewtonsoftSearchResults : IJsonSearchResults
	{
		[JsonProperty("actions")]
		[JsonConverter(typeof(IdListConverter))]
		public List<string> ActionIds { get; set; }
		[JsonProperty("boards")]
		[JsonConverter(typeof(IdListConverter))]
		public List<string> BoardIds { get; set; }
		[JsonProperty("cards")]
		[JsonConverter(typeof(IdListConverter))]
		public List<string> CardIds { get; set; }
		[JsonProperty("members")]
		[JsonConverter(typeof(IdListConverter))]
		public List<string> MemberIds { get; set; }
		[JsonProperty("organizations")]
		[JsonConverter(typeof(IdListConverter))]
		public List<string> OrganizationIds { get; set; }
	}
}
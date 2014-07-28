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
 
	File Name:		NewtonsoftAction.cs
	Namespace:		Manatee.Trello.NewtonsoftJson.Entities
	Class Name:		NewtonsoftAction
	Purpose:		Implements IJsonAction for Newtonsoft's Json.Net.

***************************************************************************************/
using System;
using Manatee.Trello.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Manatee.Trello.NewtonsoftJson.Entities
{
	internal class NewtonsoftAction : IJsonAction
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("idMember")]
		public IJsonMember MemberCreator { get; set; }
		[JsonProperty("data")]
		public IJsonActionData Data { get; set; }
		[JsonProperty("type")]
		public ActionType Type { get; set; }
		[JsonProperty("date")]
		[JsonConverter(typeof(IsoDateTimeConverter))]
		public DateTime? Date { get; set; }
	}
}

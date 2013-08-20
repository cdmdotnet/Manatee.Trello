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
 
	File Name:		NewtonsoftToken.cs
	Namespace:		Manatee.Trello.NewtonsoftJson.Entities
	Class Name:		NewtonsoftToken
	Purpose:		Implements IJsonToken for Newtonsoft's Json.Net.

***************************************************************************************/
using System;
using System.Collections.Generic;
using Manatee.Trello.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Manatee.Trello.NewtonsoftJson.Entities
{
	internal class NewtonsoftToken : IJsonToken
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("identifier")]
		public string Identifier { get; set; }
		[JsonProperty("idMember")]
		public string IdMember { get; set; }
		[JsonProperty("dateCreated")]
		[JsonConverter(typeof(IsoDateTimeConverter))]
		public DateTime? DateCreated { get; set; }
		[JsonProperty("dateExpires")]
		[JsonConverter(typeof(IsoDateTimeConverter))]
		public DateTime? DateExpires { get; set; }
		[JsonProperty("permissions")]
		public List<IJsonTokenPermission> Permissions { get; set; }
	}
}
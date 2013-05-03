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
 
	File Name:		NewtonsoftOrganization.cs
	Namespace:		Manatee.Trello.Json.Newtonsoft.Entities
	Class Name:		NewtonsoftOrganization
	Purpose:		Implements IJsonOrganization for Newtonsoft's Json.Net.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Manatee.Trello.Json.Newtonsoft.Entities
{
	internal class NewtonsoftOrganization : IJsonOrganization
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("displayName")]
		public string DisplayName { get; set; }
		[JsonProperty("desc")]
		public string Desc { get; set; }
		[JsonProperty("url")]
		public string Url { get; set; }
		[JsonProperty("website")]
		public string Website { get; set; }
		[JsonProperty("logoHash")]
		public string LogoHash { get; set; }
		[JsonProperty("powerUps")]
		public List<int> PowerUps { get; set; }
	}
}

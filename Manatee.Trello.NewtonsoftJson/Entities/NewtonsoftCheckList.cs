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
 
	File Name:		NewtonsoftCheckList.cs
	Namespace:		Manatee.Trello.NewtonsoftJson.Entities
	Class Name:		NewtonsoftCheckList
	Purpose:		Implements IJsonCheckList for Newtonsoft's Json.Net.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Trello.Json;
using Newtonsoft.Json;

namespace Manatee.Trello.NewtonsoftJson.Entities
{
	internal class NewtonsoftCheckList : IJsonCheckList
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("idBoard")]
		public string IdBoard { get; set; }
		[JsonProperty("idCard")]
		public string IdCard { get; set; }
		[JsonProperty("pos")]
		public double? Pos { get; set; }
	}
}

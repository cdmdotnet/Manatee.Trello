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
 
	File Name:		NewtonsoftBoardPersonalPreferences.cs
	Namespace:		Manatee.Trello.Json.Newtonsoft.Entities
	Class Name:		NewtonsoftBoardPersonalPreferences
	Purpose:		Implements IJsonBoardPersonalPreferences for Newtonsoft's Json.Net.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Manatee.Trello.Json.Newtonsoft.Entities
{
	internal class NewtonsoftBoardPersonalPreferences : IJsonBoardPersonalPreferences
	{
		[JsonProperty("showSidebar")]
		public bool? ShowSidebar { get; set; }
		[JsonProperty("showSidebarMembers")]
		public bool? ShowSidebarMembers { get; set; }
		[JsonProperty("showSidebarBoardActions")]
		public bool? ShowSidebarBoardActions { get; set; }
		[JsonProperty("showSidebarActivity")]
		public bool? ShowSidebarActivity { get; set; }
		[JsonProperty("showListGuide")]
		public bool? ShowListGuide { get; set; }
	}
}

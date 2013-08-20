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
 
	File Name:		NewtonsoftBoardPreferences.cs
	Namespace:		Manatee.Trello.NewtonsoftJson.Entities
	Class Name:		NewtonsoftBoardPreferences
	Purpose:		Implements IJsonBoardPreferences for Newtonsoft's Json.Net.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Trello.Json;
using Newtonsoft.Json;

namespace Manatee.Trello.NewtonsoftJson.Entities
{
	internal class NewtonsoftBoardPreferences : IJsonBoardPreferences
	{
		[JsonProperty("permissionLevel")]
		public string PermissionLevel { get; set; }
		[JsonProperty("voting")]
		public string Voting { get; set; }
		[JsonProperty("comments")]
		public string Comments { get; set; }
		[JsonProperty("invitations")]
		public string Invitations { get; set; }
		[JsonProperty("selfJoin")]
		public bool? SelfJoin { get; set; }
		[JsonProperty("cardCovers")]
		public bool? CardCovers { get; set; }
	}
}

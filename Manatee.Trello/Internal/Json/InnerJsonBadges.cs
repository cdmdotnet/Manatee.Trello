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
 
	File Name:		InnerJsonBadges.cs
	Namespace:		Manatee.Trello.Internal.Json
	Class Name:		InnerJsonBadges
	Purpose:		Internal implementation of IJsonBadges.

***************************************************************************************/
using System;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Json
{
	public class InnerJsonBadges : IJsonBadges
	{
		public int? Votes { get; set; }
		public bool? ViewingMemberVoted { get; set; }
		public bool? Subscribed { get; set; }
		public string Fogbugz { get; set; }
		public DateTime? Due { get; set; }
		public bool? Description { get; set; }
		public int? Comments { get; set; }
		public int? CheckItemsChecked { get; set; }
		public int? CheckItems { get; set; }
		public int? Attachments { get; set; }
	}
}
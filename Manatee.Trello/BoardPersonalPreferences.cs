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
 
	File Name:		BoardPersonalPreferences.cs
	Namespace:		Manatee.Trello
	Class Name:		BoardPersonalPreferences
	Purpose:		Represents a member's viewing preferences for a board
					on Trello.com.

***************************************************************************************/
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	//{
	//   "showSidebar":true,
	//   "showSidebarMembers":true,
	//   "showSidebarBoardActions":true,
	//   "showSidebarActivity":true,
	//   "showListGuide":false
	//}
	public class BoardPersonalPreferences : JsonCompatibleExpiringObject
	{
		private bool? _showListGuide;
		private bool? _showSidebar;
		private bool? _showSidebarActivity;
		private bool? _showSidebarBoardActions;
		private bool? _showSidebarMembers;

		public bool? ShowListGuide
		{
			get
			{
				VerifyNotExpired();
				return _showListGuide;
			}
			set
			{
				_showListGuide = value;
				Parameters.Add("showListGuide", _showListGuide.ToLowerString());
				Post();
			}
		}
		public bool? ShowSidebar
		{
			get
			{
				VerifyNotExpired();
				return _showSidebar;
			}
			set
			{
				_showListGuide = value;
				Parameters.Add("showListGuide", _showListGuide.ToLowerString());
				Post();
			}
		}
		public bool? ShowSidebarActivity
		{
			get
			{
				VerifyNotExpired();
				return _showSidebarActivity;
			}
			set
			{
				_showListGuide = value;
				Parameters.Add("showListGuide", _showListGuide.ToLowerString());
				Post();
			}
		}
		public bool? ShowSidebarBoardActions
		{
			get
			{
				VerifyNotExpired();
				return _showSidebarBoardActions;
			}
			set
			{
				_showListGuide = value;
				Parameters.Add("showListGuide", _showListGuide.ToLowerString());
				Post();
			}
		}
		public bool? ShowSidebarMembers
		{
			get
			{
				VerifyNotExpired();
				return _showSidebarMembers;
			}
			set
			{
				_showListGuide = value;
				Parameters.Add("showSidebarMembers", _showSidebarMembers.ToLowerString());
				Post();
			}
		}

		public BoardPersonalPreferences() {}
		public BoardPersonalPreferences(TrelloService svc, Board owner)
			: base(svc, owner) {}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_showListGuide = obj.TryGetBoolean("showListGuide");
			_showSidebar = obj.TryGetBoolean("showSidebar");
			_showSidebarActivity = obj.TryGetBoolean("showSidebarActivity");
			_showSidebarBoardActions = obj.TryGetBoolean("showSidebarBoardActions");
			_showSidebarMembers = obj.TryGetBoolean("showSidebarMembers");
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"showListGuide", _showListGuide.HasValue ? _showListGuide.Value : JsonValue.Null},
			           		{"showSidebar", _showSidebar.HasValue ? _showSidebar.Value : JsonValue.Null},
			           		{"showSidebarActivity", _showSidebarActivity.HasValue ? _showSidebarActivity.Value : JsonValue.Null},
			           		{"showSidebarBoardActions", _showSidebarBoardActions.HasValue ? _showSidebarBoardActions.Value : JsonValue.Null},
			           		{"showSidebarMembers", _showSidebarMembers.HasValue ? _showSidebarMembers.Value : JsonValue.Null}
			           	};
			return json;
		}

		internal override void Refresh(ExpiringObject entity)
		{
			var prefs = entity as BoardPersonalPreferences;
			if (prefs == null) return;
			_showListGuide = prefs._showListGuide;
			_showSidebar = prefs._showSidebar;
			_showSidebarActivity = prefs._showSidebarActivity;
			_showSidebarBoardActions = prefs._showSidebarBoardActions;
			_showSidebarMembers = prefs._showSidebarMembers;
		}
		internal override bool Match(string id)
		{
			return false;
		}

		protected override void Get()
		{
			var entity = Svc.Api.Get(new Request<BoardPersonalPreferences>(new[] {Owner, this}));
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}

		private void Post()
		{
			Svc.Api.Post(new Request<BoardPersonalPreferences>(new[] {Owner, this}, this));
		}
	}
}

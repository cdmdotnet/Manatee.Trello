using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//{
	//   "showSidebar":true,
	//   "showSidebarMembers":true,
	//   "showSidebarBoardActions":true,
	//   "showSidebarActivity":true,
	//   "showListGuide":false
	//}
	public class BoardPersonalPreferences : OwnedEntityBase<Board>
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
			set { _showListGuide = value; }
		}
		public bool? ShowSidebar
		{
			get
			{
				VerifyNotExpired();
				return _showSidebar;
			}
			set { _showSidebar = value; }
		}
		public bool? ShowSidebarActivity
		{
			get
			{
				VerifyNotExpired();
				return _showSidebarActivity;
			}
			set { _showSidebarActivity = value; }
		}
		public bool? ShowSidebarBoardActions
		{
			get
			{
				VerifyNotExpired();
				return _showSidebarBoardActions;
			}
			set { _showSidebarBoardActions = value; }
		}
		public bool? ShowSidebarMembers
		{
			get
			{
				VerifyNotExpired();
				return _showSidebarMembers;
			}
			set { _showSidebarMembers = value; }
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
		public override bool Equals(EquatableExpiringObject other)
		{
			return true;
		}

		internal override void Refresh(EquatableExpiringObject entity)
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

		protected override void Refresh()
		{
			var entity = Svc.Api.GetOwnedEntity<Board, BoardPersonalPreferences>(Owner.Id);
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}
	}
}

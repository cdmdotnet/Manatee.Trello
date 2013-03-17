using System;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//   "badges":{
	//      "votes":0,
	//      "viewingMemberVoted":false,
	//      "subscribed":false,
	//      "fogbugz":"",
	//      "due":null,
	//      "description":true,
	//      "comments":0,
	//      "checkItemsChecked":0,
	//      "checkItems":4,
	//      "attachments":0
	//   },
	public class Badges : OwnedEntityBase<Card>
	{
		private int? _attachments;
		private int? _checkItems;
		private int? _checkItemsChecked;
		private int? _comments;
		private DateTime? _dueDate;
		private string _fogBugz;
		private bool? _hasDescription;
		private bool? _isSubscribed;
		private bool? _viewingMemberVoted;
		private int? _votes;

		public int? Attachments
		{
			get
			{
				VerifyNotExpired();
				return _attachments;
			}
		}
		public int? CheckItems
		{
			get
			{
				VerifyNotExpired();
				return _checkItems;
			}
		}
		public int? CheckItemsChecked
		{
			get
			{
				VerifyNotExpired();
				return _checkItemsChecked;
			}
		}
		public int? Comments
		{
			get
			{
				VerifyNotExpired();
				return _comments;
			}
		}
		public DateTime? DueDate
		{
			get
			{
				VerifyNotExpired();
				return _dueDate;
			}
		}
		public string FogBugz
		{
			get
			{
				VerifyNotExpired();
				return _fogBugz;
			}
		}
		public bool? HasDescription
		{
			get
			{
				VerifyNotExpired();
				return _hasDescription;
			}
		}
		public bool? IsSubscribed
		{
			get
			{
				VerifyNotExpired();
				return _isSubscribed;
			}
		}
		public bool? ViewingMemberVoted
		{
			get
			{
				VerifyNotExpired();
				return _viewingMemberVoted;
			}
		}
		public int? Votes
		{
			get
			{
				VerifyNotExpired();
				return _votes;
			}
		}

		public Badges() {}
		public Badges(TrelloService svc, Card owner)
			: base(svc, owner) {}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_attachments = (int?) obj.TryGetNumber("attachments");
			_checkItems = (int?) obj.TryGetNumber("checkItems");
			_checkItemsChecked = (int?) obj.TryGetNumber("checkItemsChecked");
			_comments = (int?) obj.TryGetNumber("comments");
			var due = obj.TryGetString("due");
			_dueDate = string.IsNullOrWhiteSpace(due) ? (DateTime?) null : DateTime.Parse(due);
			_fogBugz = obj.TryGetString("fogBugz");
			_hasDescription = obj.TryGetBoolean("desc");
			_isSubscribed = obj.TryGetBoolean("subscribed");
			_viewingMemberVoted = obj.TryGetBoolean("viewingMemberVoted");
			_votes = (int?) obj.TryGetNumber("votes");
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"attachments", _attachments.HasValue ? _attachments.Value : JsonValue.Null},
			           		{"checkItems", _checkItems.HasValue ? _checkItems.Value : JsonValue.Null},
			           		{"checkItemsChecked", _checkItemsChecked.HasValue ? _checkItemsChecked.Value : JsonValue.Null},
			           		{"comments", _comments.HasValue ? _comments.Value : JsonValue.Null},
			           		{"due", _dueDate.HasValue ? _dueDate.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			           		{"fogBugz", _fogBugz},
			           		{"desc", _hasDescription.HasValue ? _hasDescription.Value : JsonValue.Null},
			           		{"subscribed", _isSubscribed.HasValue ? _isSubscribed.Value : JsonValue.Null},
			           		{"viewingMemberVoted", _viewingMemberVoted.HasValue ? _viewingMemberVoted.Value : JsonValue.Null},
			           		{"votes", _votes.HasValue ? _votes.Value : JsonValue.Null},
			           	};
			return json;
		}
		public override bool Equals(EquatableExpiringObject other)
		{
			return true;
		}

		internal override void Refresh(EquatableExpiringObject entity)
		{
			var badges = entity as Badges;
			if (badges == null) return;
			_attachments = badges._attachments;
			_checkItems = badges._checkItems;
			_checkItemsChecked = badges._checkItemsChecked;
			_comments = badges._comments;
			_dueDate = badges._dueDate;
			_fogBugz = badges._fogBugz;
			_hasDescription = badges._hasDescription;
			_isSubscribed = badges._isSubscribed;
			_viewingMemberVoted = badges._viewingMemberVoted;
			_votes = badges._votes;
		}
		internal override bool Match(string id)
		{
			return false;
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.GetOwnedEntity<Card, Badges>(Owner.Id);
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}
	}
}
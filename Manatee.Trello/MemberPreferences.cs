using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//   "prefs":{
	//      "sendSummaries":true,
	//      "minutesBetweenSummaries":60,
	//      "minutesBeforeDeadlineToNotify":1440,
	//      "colorBlind":false
	//   },
	public class MemberPreferences : OwnedEntityBase<Member>
	{
		private bool? _colorBlind;
		private int? _minutesBetweenSummaries;
		private bool? _sendSummaries;
		private int? _minutesBeforeDeadlineToNotify;

		public bool? ColorBlind
		{
			get
			{
				VerifyNotExpired();
				return _colorBlind;
			}
			set { _colorBlind = value; }
		}
		public int? MinutesBetweenSummaries
		{
			get
			{
				VerifyNotExpired();
				return _minutesBetweenSummaries;
			}
			set { _minutesBetweenSummaries = value; }
		}
		public bool? SendSummaries
		{
			get
			{
				VerifyNotExpired();
				return _sendSummaries;
			}
			set { _sendSummaries = value; }
		}
		public int? MinutesBeforeDeadlineToNotify
		{
			get
			{
				VerifyNotExpired();
				return _minutesBeforeDeadlineToNotify;
			}
			set { _minutesBeforeDeadlineToNotify = value; }
		}

		public MemberPreferences() {}
		public MemberPreferences(TrelloService svc, Member owner)
			: base(svc, owner) {}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_colorBlind = obj.TryGetBoolean("colorBlind");
			_minutesBetweenSummaries = (int?) obj.TryGetNumber("minutesBetweenSummaries");
			_sendSummaries = obj.TryGetBoolean("sendSummaries");
			_minutesBeforeDeadlineToNotify = (int?) obj.TryGetNumber("minutesBeforeDeadlineToNotify");
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"colorBlind", _colorBlind.HasValue ? _colorBlind.Value : JsonValue.Null},
			           		{"minutesBetweenSummaries", _minutesBetweenSummaries.HasValue ? _minutesBetweenSummaries.Value : JsonValue.Null},
			           		{"sendSummaries", _sendSummaries.HasValue ? _sendSummaries.Value : JsonValue.Null},
			           		{"minutesBeforeDeadlineToNotify", _minutesBeforeDeadlineToNotify.HasValue ? _minutesBeforeDeadlineToNotify.Value : JsonValue.Null},
			           	};
			return json;
		}
		public override bool Equals(EquatableExpiringObject other)
		{
			return true;
		}

		internal override void Refresh(EquatableExpiringObject entity)
		{
			var prefs = entity as MemberPreferences;
			if (prefs == null) return;
			_colorBlind = prefs._colorBlind;
			_minutesBetweenSummaries = prefs._minutesBetweenSummaries;
			_sendSummaries = prefs._sendSummaries;
			_minutesBeforeDeadlineToNotify = prefs._minutesBeforeDeadlineToNotify;
		}
		internal override bool Match(string id)
		{
			return false;
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.GetOwnedEntity<Member, MemberPreferences>(Owner.Id);
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}
	}
}
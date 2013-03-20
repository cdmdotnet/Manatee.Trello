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
 
	File Name:		MemberPreferences.cs
	Namespace:		Manatee.Trello
	Class Name:		MemberPreferences
	Purpose:		Represents available preference settings for a member
					on Trello.com.

***************************************************************************************/
using System;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

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
			set
			{
				_colorBlind = value;
				//Update(colorBlind: _colorBlind);
			}
		}
		public int? MinutesBetweenSummaries
		{
			get
			{
				VerifyNotExpired();
				return _minutesBetweenSummaries;
			}
			set
			{
				_minutesBetweenSummaries = value;
				//Update(minutesBetweenSummaries: _minutesBetweenSummaries);
			}
		}
		public bool? SendSummaries
		{
			get
			{
				VerifyNotExpired();
				return _sendSummaries;
			}
			set
			{
				_sendSummaries = value;
				//Update(sendSummaries: _sendSummaries);
			}
		}
		public int? MinutesBeforeDeadlineToNotify
		{
			get
			{
				VerifyNotExpired();
				return _minutesBeforeDeadlineToNotify;
			}
			set
			{
				_minutesBeforeDeadlineToNotify = value;
			//	Update(minutesBeforeDeadlineToNotify: _minutesBeforeDeadlineToNotify);
			}
		}

		public MemberPreferences() {}
		public MemberPreferences(TrelloService svc, Member owner)
			: base(svc, owner) {}

		//public void Update(bool? colorBlind = null,
		//                   int? minutesBetweenSummaries = null,
		//                   bool? sendSummaries = null,
		//                   int? minutesBeforeDeadlineToNotify = null)
		//{
		//    var request = new UpdateMemberPreferencesRequest(Owner, colorBlind, minutesBetweenSummaries,
		//                                                     sendSummaries, minutesBeforeDeadlineToNotify);
		//    Svc.PutAndCache<Member, MemberPreferences, UpdateMemberPreferencesRequest>(request);
		//}
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

		internal override void Refresh(ExpiringObject entity)
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
			var entity = Svc.Api.Get(new Request<Member, MemberPreferences>(Owner.Id));
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}
	}
}
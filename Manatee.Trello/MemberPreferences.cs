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
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
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
	/// <summary>
	/// Represents available preference settings for a member.
	/// </summary>
	public class MemberPreferences : JsonCompatibleExpiringObject
	{
		private bool? _colorBlind;
		private int? _minutesBetweenSummaries;
		private bool? _sendSummaries;
		private int? _minutesBeforeDeadlineToNotify;

		/// <summary>
		/// Enables/disables color-blind mode.
		/// </summary>
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
				Parameters.Add("value", value.ToLowerString());
				Put("colorBlind");
			}
		}
		/// <summary>
		/// Gets or sets the number of minutes between summary emails.
		/// </summary>
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
				Parameters.Add("value", value);
				Put("minutesBetweenSummaries");
			}
		}
		/// <summary>
		/// Enables/disables summary emails.
		/// </summary>
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
				Parameters.Add("value", value.ToLowerString());
				Put("sendSummaries");
			}
		}
		/// <summary>
		/// Gets or sets the number of minutes before a deadline to notify the member.
		/// </summary>
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
				Parameters.Add("value", value);
				Put("minutesBeforeDeadlineToNotify");
			}
		}

		/// <summary>
		/// Creates a new instance of the MemberPreferences class.
		/// </summary>
		public MemberPreferences() {}
		internal MemberPreferences(TrelloService svc, Member owner)
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

		protected override void Get()
		{
			var entity = Svc.Api.Get(new Request<MemberPreferences>(new[] {Owner, this}));
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}
		
		private void Put(string extension)
		{
			Svc.PutAndCache(new Request<MemberPreferences>(new[] {Owner, this}, this, extension));
		}
	}
}
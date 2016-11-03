using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class MemberPreferencesContext : LinkedSynchronizationContext<IJsonMemberPreferences>
	{
		static MemberPreferencesContext()
		{
			_properties = new Dictionary<string, Property<IJsonMemberPreferences>>
				{
					{"EnableColorBlindMode", new Property<IJsonMemberPreferences, bool?>((d, a) => d.ColorBlind, (d, o) => d.ColorBlind = o)},
					{"MinutesBetweenSummaries", new Property<IJsonMemberPreferences, int?>((d, a) => d.MinutesBetweenSummaries, (d, o) => d.MinutesBetweenSummaries = o)},
				};
		}
		public MemberPreferencesContext(TrelloAuthorization auth)
			: base(auth) {}
	}
}
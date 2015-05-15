/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		MemberPreferencesContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		MemberPreferencesContext
	Purpose:		Provides a data context for a member's preferences.

***************************************************************************************/
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
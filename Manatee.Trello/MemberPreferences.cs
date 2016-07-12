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
 
	File Name:		MemberPreferences.cs
	Namespace:		Manatee.Trello
	Class Name:		MemberPreferences
	Purpose:		Represents preferences for a member.

***************************************************************************************/

using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents preferences for a member.
	/// </summary>
	public class MemberPreferences
	{
		private readonly Field<bool?> _enableColorBlindMode;
		private readonly Field<int?> _minutesBetweenSummaries;
		private MemberPreferencesContext _context;

		/// <summary>
		/// Gets or sets whether color-blind mode is enabled.
		/// </summary>
		public bool? EnableColorBlindMode
		{
			get { return _enableColorBlindMode.Value; }
			set { _enableColorBlindMode.Value = value; }
		}
		/// <summary>
		/// Gets or sets the time between email summaries.
		/// </summary>
		public int? MinutesBetweenSummaries
		{
			get { return _minutesBetweenSummaries.Value; }
			set { _minutesBetweenSummaries.Value = value; }
		}

		internal MemberPreferences(MemberPreferencesContext context)
		{
			_context = context;

			_enableColorBlindMode = new Field<bool?>(_context, nameof(EnableColorBlindMode));
			_enableColorBlindMode.AddRule(NullableHasValueRule<bool>.Instance);
			_minutesBetweenSummaries = new Field<int?>(_context, nameof(MinutesBetweenSummaries));
			_minutesBetweenSummaries.AddRule(NullableHasValueRule<int>.Instance);
		}
	}
}
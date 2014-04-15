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
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents available preference settings for a member.
	/// </summary>
	public class MemberPreferences : ExpiringObject
	{
		private IJsonMemberPreferences _jsonMemberPreferences;

		/// <summary>
		/// Enables/disables color-blind mode.
		/// </summary>
		public bool? ColorBlind
		{
			get
			{
				VerifyNotExpired();
				return _jsonMemberPreferences.ColorBlind;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonMemberPreferences.ColorBlind == value) return;
				_jsonMemberPreferences.ColorBlind = value;
				Parameters.Add("value", _jsonMemberPreferences.ColorBlind.ToLowerString());
				Upload(EntityRequestType.MemberPreferences_Write_ColorBlind);
			}
		}
		/// <summary>
		/// Gets or sets the number of minutes between summary emails.
		/// </summary>
		public MemberPreferenceSummaryPeriodType? MinutesBetweenSummaries
		{
			get
			{
				VerifyNotExpired();
				return (MemberPreferenceSummaryPeriodType?) _jsonMemberPreferences.MinutesBetweenSummaries;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				Validator.Enumeration(value.Value);
				if (_jsonMemberPreferences.MinutesBetweenSummaries == (int?) value) return;
				_jsonMemberPreferences.MinutesBetweenSummaries = (int?) value;
				Parameters.Add("value", _jsonMemberPreferences.MinutesBetweenSummaries);
				Upload(EntityRequestType.MemberPreferences_Write_MinutesBetweenSummaries);
			}
		}
		/// <summary>
		/// Enables/disables summary emails.
		/// </summary>
		internal bool? SendSummaries
		{
			get
			{
				VerifyNotExpired();
				return _jsonMemberPreferences.SendSummaries;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonMemberPreferences.SendSummaries == value) return;
				_jsonMemberPreferences.SendSummaries = value;
				Parameters.Add("value", _jsonMemberPreferences.SendSummaries.ToLowerString());
				Upload(EntityRequestType.MemberPreferences_Write_SendSummaries);
			}
		}
		/// <summary>
		/// Gets or sets the number of minutes before a deadline to notify the member.
		/// </summary>
		internal int? MinutesBeforeDeadlineToNotify
		{
			get
			{
				VerifyNotExpired();
				return _jsonMemberPreferences.MinutesBeforeDeadlineToNotify;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonMemberPreferences.MinutesBeforeDeadlineToNotify == value) return;
				_jsonMemberPreferences.MinutesBeforeDeadlineToNotify = value;
				Parameters.Add("value", _jsonMemberPreferences.MinutesBeforeDeadlineToNotify);
				Upload(EntityRequestType.MemberPreferences_Write_MinutesBeforeDeadlineToNotify);
			}
		}
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return _jsonMemberPreferences is InnerJsonMemberPreferences; } }

		/// <summary>
		/// Creates a new instance of the MemberPreferences class.
		/// </summary>
		public MemberPreferences()
		{
			_jsonMemberPreferences = new InnerJsonMemberPreferences();
		}
		internal MemberPreferences(Member owner)
			: this()
		{
			Owner = owner;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public override bool Refresh()
		{
			Parameters.Add("_memberId", Owner.Id);
			AddDefaultParameters();
			return EntityRepository.Refresh(this, EntityRequestType.MemberPreferences_Read_Refresh);
		}

		internal override void ApplyJson(object obj)
		{
			_jsonMemberPreferences = (IJsonMemberPreferences)obj;
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}
		internal override bool EqualsJson(object obj)
		{
			return false;
		}

		private void Upload(EntityRequestType requestType)
		{
			Parameters.Add("_memberId", Owner.Id);
			EntityRepository.Upload(requestType, Parameters);
		}
	}
}
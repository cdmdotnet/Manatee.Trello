/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ManateeFactory.cs
	Namespace:		Manatee.Trello.ManateeJson
	Class Name:		ManateeFactory
	Purpose:		Implements IJsonFactory using Manatee.Json.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Json;
using Manatee.Trello.ManateeJson.Entities;

namespace Manatee.Trello.ManateeJson
{
	/// <summary>
	/// Creates instances of JSON interfaces.
	/// </summary>
	public class ManateeFactory : IJsonFactory
	{
		private static readonly Dictionary<Type, Func<object>> _factory;

		static ManateeFactory()
		{
			_factory = new Dictionary<Type, Func<object>>
				{
					{typeof (IJsonAction), () => new ManateeAction()},
					{typeof (IJsonActionData), () => new ManateeActionData()},
					{typeof (IJsonActionOldData), () => new ManateeActionOldData()},
					{typeof (IJsonAttachment), () => new ManateeAttachment()},
					{typeof (IJsonBadges), () => new ManateeBadges()},
					{typeof (IJsonBoard), () => new ManateeBoard()},
					{typeof (IJsonBoardBackground), () => new ManateeBoardBackground()},
					{typeof (IJsonBoardMembership), () => new ManateeBoardMembership()},
					{typeof (IJsonBoardPersonalPreferences), () => new ManateeBoardPersonalPreferences()},
					{typeof (IJsonBoardPreferences), () => new ManateeBoardPreferences()},
					{typeof (IJsonBoardVisibilityRestrict), () => new ManateeBoardVisibilityRestrict()},
					{typeof (IJsonCard), () => new ManateeCard()},
					{typeof (IJsonCheckItem), () => new ManateeCheckItem()},
					{typeof (IJsonCheckList), () => new ManateeCheckList()},
					{typeof (IJsonComment), () => new ManateeComment()},
					{typeof (IJsonImagePreview), () => new ManateeImagePreview()},
					{typeof (IJsonLabel), () => new ManateeLabel()},
					{typeof (IJsonList), () => new ManateeList()},
					{typeof (IJsonMember), () => new ManateeMember()},
					{typeof (IJsonMemberPreferences), () => new ManateeMemberPreferences()},
					{typeof (IJsonMemberSearch), () => new ManateeMemberSearch()},
					{typeof (IJsonMemberSession), () => new ManateeMemberSession()},
					{typeof (IJsonNotification), () => new ManateeNotification()},
					{typeof (IJsonNotificationData), () => new ManateeNotificationData()},
					{typeof (IJsonNotificationOldData), () => new ManateeNotificationOldData()},
					{typeof (IJsonOrganization), () => new ManateeOrganization()},
					{typeof (IJsonOrganizationMembership), () => new ManateeOrganizationMembership()},
					{typeof (IJsonOrganizationPreferences), () => new ManateeOrganizationPreferences()},
					{typeof (IJsonParameter), () => new ManateeParameter()},
					{typeof (IJsonPosition), () => new ManateePosition()},
					{typeof (IJsonSearch), () => new ManateeSearch()},
					{typeof (IJsonSticker), () => new ManateeSticker()},
					{typeof (IJsonToken), () => new ManateeToken()},
					{typeof (IJsonTokenPermission), () => new ManateeTokenPermission()},
					{typeof (IJsonWebhook), () => new ManateeWebhook()},
					{typeof (IJsonWebhookNotification), () => new ManateeWebhookNotification()},
				};
		}

		/// <summary>
		/// Creates an instance of the requested JSON interface.
		/// </summary>
		/// <typeparam name="T">The type to create.</typeparam>
		/// <returns>An instance of the requested type.</returns>
		public T Create<T>()
		{
			return (T) _factory[typeof (T)]();
		}
	}
}
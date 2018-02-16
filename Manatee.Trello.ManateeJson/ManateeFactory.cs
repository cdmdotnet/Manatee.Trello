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
					{typeof (IJsonImagePreview), () => new ManateeImagePreview()},
					{typeof (IJsonLabel), () => new ManateeLabel()},
					{typeof (IJsonList), () => new ManateeList()},
					{typeof (IJsonMember), () => new ManateeMember()},
					{typeof (IJsonMemberPreferences), () => new ManateeMemberPreferences()},
					{typeof (IJsonMemberSearch), () => new ManateeMemberSearch()},
					{typeof (IJsonNotification), () => new ManateeNotification()},
					{typeof (IJsonNotificationData), () => new ManateeNotificationData()},
					{typeof (IJsonNotificationOldData), () => new ManateeNotificationOldData()},
					{typeof (IJsonOrganization), () => new ManateeOrganization()},
					{typeof (IJsonOrganizationMembership), () => new ManateeOrganizationMembership()},
					{typeof (IJsonOrganizationPreferences), () => new ManateeOrganizationPreferences()},
					{typeof (IJsonParameter), () => new ManateeParameter()},
					{typeof (IJsonPosition), () => new ManateePosition()},
					{typeof (IJsonPowerUp), () => new ManateePowerUp()},
					{typeof (IJsonPowerUpData), () => new ManateePowerUpData()},
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
			where T : class
		{
			return (T) _factory[typeof (T)]();
		}
	}
}
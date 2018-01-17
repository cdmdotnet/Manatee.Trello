using Manatee.Trello.Json;
using Moq;

namespace Manatee.Trello.Moq
{
	public static class TrelloMock
	{
		private static readonly object _lock = new object();
		private static Mock<IJsonFactory> _jsonFactory;

		public static void Initialize()
		{
			if (_jsonFactory != null) return;

			lock (_lock)
			{
				if (_jsonFactory != null) return;

				_jsonFactory = new Mock<IJsonFactory>();
				_jsonFactory.Setup(f => f.Create<IJsonAction>()).Returns(() => new Mock<IJsonAction>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonActionData>()).Returns(() => new Mock<IJsonActionData>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonActionOldData>()).Returns(() => new Mock<IJsonActionOldData>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonAttachment>()).Returns(() => new Mock<IJsonAttachment>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonBadges>()).Returns(() => new Mock<IJsonBadges>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonBoardBackground>()).Returns(() => new Mock<IJsonBoardBackground>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonBoardMembership>()).Returns(() => new Mock<IJsonBoardMembership>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonBoardPersonalPreferences>()).Returns(() => new Mock<IJsonBoardPersonalPreferences>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonBoardPreferences>()).Returns(() => new Mock<IJsonBoardPreferences>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonBoardVisibilityRestrict>()).Returns(() => new Mock<IJsonBoardVisibilityRestrict>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonCard>()).Returns(() => new Mock<IJsonCard>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonCheckItem>()).Returns(() => new Mock<IJsonCheckItem>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonCheckList>()).Returns(() => new Mock<IJsonCheckList>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonImagePreview>()).Returns(() => new Mock<IJsonImagePreview>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonLabel>()).Returns(() => new Mock<IJsonLabel>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonList>()).Returns(() => new Mock<IJsonList>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonMember>()).Returns(() => new Mock<IJsonMember>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonMemberPreferences>()).Returns(() => new Mock<IJsonMemberPreferences>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonMemberSearch>()).Returns(() => new Mock<IJsonMemberSearch>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonMemberSession>()).Returns(() => new Mock<IJsonMemberSession>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonNotification>()).Returns(() => new Mock<IJsonNotification>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonNotificationData>()).Returns(() => new Mock<IJsonNotificationData>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonNotificationOldData>()).Returns(() => new Mock<IJsonNotificationOldData>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonOrganization>()).Returns(() => new Mock<IJsonOrganization>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonOrganizationMembership>()).Returns(() => new Mock<IJsonOrganizationMembership>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonOrganizationPreferences>()).Returns(() => new Mock<IJsonOrganizationPreferences>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonParameter>()).Returns(() => new Mock<IJsonParameter>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonPosition>()).Returns(() => new Mock<IJsonPosition>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonPowerUp>()).Returns(() => new Mock<IJsonPowerUp>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonPowerUpData>()).Returns(() => new Mock<IJsonPowerUpData>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonSearch>()).Returns(() => new Mock<IJsonSearch>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonSticker>()).Returns(() => new Mock<IJsonSticker>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonToken>()).Returns(() => new Mock<IJsonToken>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonTokenPermission>()).Returns(() => new Mock<IJsonTokenPermission>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonWebhook>()).Returns(() => new Mock<IJsonWebhook>().Object);
				_jsonFactory.Setup(f => f.Create<IJsonWebhookNotification>()).Returns(() => new Mock<IJsonWebhookNotification>().Object);

				TrelloConfiguration.JsonFactory = _jsonFactory.Object;
			}
		}
	}
}
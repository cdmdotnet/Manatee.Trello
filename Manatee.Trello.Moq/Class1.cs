using System;
using Manatee.Trello.Contracts;
using Moq;

namespace Manatee.Trello.Moq
{
	public class ActionMock : Mock<Action>
	{
		public ActionMock()
			: base(string.Empty, null)
		{

		}
	}

	public class ActionDataMock : Mock<ActionData>
	{
		public ActionDataMock()
			: base(string.Empty, null)
		{

		}
	}

	public class AttachmentMock : Mock<Attachment>
	{
		public AttachmentMock()
			: base(string.Empty, null)
		{

		}
	}

	public class AttachmentCollectionMock : Mock<AttachmentCollection>
	{
		public AttachmentCollectionMock()
			: base(string.Empty, null)
		{

		}
	}

	public class BadgesMock : Mock<Badges>
	{
		public BadgesMock()
			: base(string.Empty, null)
		{

		}
	}

	public class BoardMock : Mock<Board>
	{
		public BoardMock()
			: base(string.Empty, null)
		{
		}
	}

	public class BoardBackgroundMock : Mock<BoardBackground>
	{
		public BoardBackgroundMock()
			: base(string.Empty, null)
		{
		}
	}

	public class BoardCollectionMock : Mock<BoardCollection>
	{
		public BoardCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class BoardLabelCollectionMock : Mock<BoardLabelCollection>
	{
		public BoardLabelCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class BoardMembershipMock : Mock<BoardMembership>
	{
		public BoardMembershipMock()
			: base(string.Empty, null)
		{
		}
	}

	public class BoardMembershipCollectionMock : Mock<BoardMembershipCollection>
	{
		public BoardMembershipCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class BoardPersonalPreferencesMock : Mock<BoardPersonalPreferences>
	{
		public BoardPersonalPreferencesMock()
			: base(string.Empty, null)
		{
		}
	}

	public class BoardPreferencesMock : Mock<BoardPreferences>
	{
		public BoardPreferencesMock()
			: base(string.Empty, null)
		{
		}
	}

	public class CardMock : Mock<Card>
	{
		private ReadOnlyActionCollectionMock _actions;
		private BoardMock _board;
		private CommentCollectionMock _comments;

		public ReadOnlyActionCollectionMock Actions => _actions ?? (_actions = new ReadOnlyActionCollectionMock());
		public BoardMock Board => _board ?? (_board = new BoardMock());
		public CommentCollectionMock Comments => _comments ?? (_comments = new CommentCollectionMock());

		public CardMock()
			: base(string.Empty, null)
		{
			SetupGet(c => c.Actions).Returns(() => Actions.Object);
			SetupGet(c => c.Board).Returns(() => Board.Object);
		}
	}

	public class CardCollectionMock : Mock<CardCollection>
	{
		public CardCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class CardLabelCollectionMock : Mock<CardLabelCollection>
	{
		public CardLabelCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class CardStickerCollectionMock : Mock<CardStickerCollection>
	{
		public CardStickerCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class CheckItemMock : Mock<CheckItem>
	{
		public CheckItemMock()
			: base(string.Empty, null)
		{
		}
	}

	public class CheckItemCollectionMock : Mock<CheckItemCollection>
	{
		public CheckItemCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class CheckListMock : Mock<CheckList>
	{
		public CheckListMock()
			: base(string.Empty, null)
		{
		}
	}

	public class CheckListCollectionMock : Mock<CheckListCollection>
	{
		public CheckListCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class CommentCollectionMock : Mock<CommentCollection>
	{
		public CommentCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ImagePreviewMock : Mock<ImagePreview>
	{
		public ImagePreviewMock()
			: base(string.Empty, null)
		{
		}
	}

	public class LabelMock : Mock<Label>
	{
		public LabelMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ListMock : Mock<List>
	{
		public ListMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ListCollectionMock : Mock<ListCollection>
	{
		public ListCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class MemberMock : Mock<Member>
	{
		public MemberMock()
			: base(string.Empty, null)
		{
		}
	}

	public class MemberCollectionMock : Mock<MemberCollection>
	{
		public MemberCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class MemberPreferencesMock : Mock<MemberPreferences>
	{
		public MemberPreferencesMock()
			: base(string.Empty, null)
		{
		}
	}

	public class MemberSearchMock : Mock<MemberSearch>
	{
		public MemberSearchMock()
			: base(string.Empty, null)
		{
		}
	}

	public class MemberSearchResultMock : Mock<MemberSearchResult>
	{
		public MemberSearchResultMock()
			: base(string.Empty, null)
		{
		}
	}

	public class MemberStickerCollectionMock : Mock<MemberStickerCollection>
	{
		public MemberStickerCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class NotificationMock : Mock<Notification>
	{
		public NotificationMock()
			: base(string.Empty, null)
		{
		}
	}

	public class NotificationDataMock : Mock<NotificationData>
	{
		public NotificationDataMock()
			: base(string.Empty, null)
		{
		}
	}

	public class OrganizationMock : Mock<Organization>
	{
		public OrganizationMock()
			: base(string.Empty, null)
		{
		}
	}

	public class OrganizationCollectionMock : Mock<OrganizationCollection>
	{
		public OrganizationCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class OrganizationMembershipMock : Mock<OrganizationMembership>
	{
		public OrganizationMembershipMock()
			: base(string.Empty, null)
		{
		}
	}

	public class OrganizationMembershipCollectionMock : Mock<OrganizationMembershipCollection>
	{
		public OrganizationMembershipCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class OrganizationPreferencesMock : Mock<OrganizationPreferences>
	{
		public OrganizationPreferencesMock()
			: base(string.Empty, null)
		{
		}
	}

	public class PositionMock : Mock<Position>
	{
		public PositionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class PowerUpBaseMock : Mock<PowerUpBase>
	{
		public PowerUpBaseMock()
			: base(string.Empty, null)
		{
		}
	}

	public class PowerUpDataMock : Mock<PowerUpData>
	{
		public PowerUpDataMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyActionCollectionMock : Mock<ReadOnlyActionCollection>
	{
		public ReadOnlyActionCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyAttachmentCollectionMock : Mock<ReadOnlyAttachmentCollection>
	{
		public ReadOnlyAttachmentCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyAttachmentPreviewCollectionMock : Mock<ReadOnlyAttachmentPreviewCollection>
	{
		public ReadOnlyAttachmentPreviewCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyBoardBackgroundScalesCollectionMock : Mock<ReadOnlyBoardBackgroundScalesCollection>
	{
		public ReadOnlyBoardBackgroundScalesCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyBoardCollectionMock : Mock<ReadOnlyBoardCollection>
	{
		public ReadOnlyBoardCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyBoardMembershipCollectionMock : Mock<ReadOnlyBoardMembershipCollection>
	{
		public ReadOnlyBoardMembershipCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyCardCollectionMock : Mock<ReadOnlyCardCollection>
	{
		public ReadOnlyCardCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyCheckItemCollectionMock : Mock<ReadOnlyCheckItemCollection>
	{
		public ReadOnlyCheckItemCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyCheckListCollectionMock : Mock<ReadOnlyCheckListCollection>
	{
		public ReadOnlyCheckListCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyListCollectionMock : Mock<ReadOnlyListCollection>
	{
		public ReadOnlyListCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyMemberCollectionMock : Mock<ReadOnlyMemberCollection>
	{
		public ReadOnlyMemberCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyNotificationCollectionMock : Mock<ReadOnlyNotificationCollection>
	{
		public ReadOnlyNotificationCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyOrganizationCollectionMock : Mock<ReadOnlyOrganizationCollection>
	{
		public ReadOnlyOrganizationCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyOrganizationMembershipCollectionMock : Mock<ReadOnlyOrganizationMembershipCollection>
	{
		public ReadOnlyOrganizationMembershipCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyPowerUpCollectionMock : Mock<ReadOnlyPowerUpCollection>
	{
		public ReadOnlyPowerUpCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyPowerUpDataCollectionMock : Mock<ReadOnlyPowerUpDataCollection>
	{
		public ReadOnlyPowerUpDataCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyStickerCollectionMock : Mock<ReadOnlyStickerCollection>
	{
		public ReadOnlyStickerCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ReadOnlyStickerPreviewCollectionMock : Mock<ReadOnlyStickerPreviewCollection>
	{
		public ReadOnlyStickerPreviewCollectionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class SearchMock : Mock<Search>
	{
		public SearchMock()
			: base(string.Empty, null)
		{
		}
	}

	public class StickerMock : Mock<Sticker>
	{
		public StickerMock()
			: base(string.Empty, null)
		{
		}
	}

	public class TokenMock : Mock<Token>
	{
		public TokenMock()
			: base(string.Empty, null)
		{
		}
	}

	public class TokenPermissionMock : Mock<TokenPermission>
	{
		public TokenPermissionMock()
			: base(string.Empty, null)
		{
		}
	}

	public class WebColorMock : Mock<WebColor>
	{
		public WebColorMock()
			: base(string.Empty, null)
		{
		}
	}

	public class WebhookMock<T> : Mock<Webhook<T>>
		where T : class, ICanWebhook
	{
		public WebhookMock()
			: base(string.Empty, null)
		{
		}
	}
}

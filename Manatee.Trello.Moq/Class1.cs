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
			: base(null)
		{

		}
	}

	public class AttachmentMock : Mock<Attachment>
	{
		public AttachmentMock()
			: base(null)
		{

		}
	}

	public class AttachmentCollectionMock : Mock<AttachmentCollection>
	{
		public AttachmentCollectionMock()
			: base(null)
		{

		}
	}

	public class BadgesMock : Mock<Badges>
	{
		public BadgesMock()
			: base(null)
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
			: base(null)
		{
		}
	}

	public class BoardCollectionMock : Mock<BoardCollection>
	{
		public BoardCollectionMock()
			: base(null)
		{
		}
	}

	public class BoardLabelCollectionMock : Mock<BoardLabelCollection>
	{
		public BoardLabelCollectionMock()
			: base(null)
		{
		}
	}

	public class BoardMembershipMock : Mock<BoardMembership>
	{
		public BoardMembershipMock()
			: base(null)
		{
		}
	}

	public class BoardMembershipCollectionMock : Mock<BoardMembershipCollection>
	{
		public BoardMembershipCollectionMock()
			: base(null)
		{
		}
	}

	public class BoardPersonalPreferencesMock : Mock<BoardPersonalPreferences>
	{
		public BoardPersonalPreferencesMock()
			: base(null)
		{
		}
	}

	public class BoardPreferencesMock : Mock<BoardPreferences>
	{
		public BoardPreferencesMock()
			: base(null)
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
			: base(null)
		{
		}
	}

	public class CardLabelCollectionMock : Mock<CardLabelCollection>
	{
		public CardLabelCollectionMock()
			: base(null)
		{
		}
	}

	public class CardStickerCollectionMock : Mock<CardStickerCollection>
	{
		public CardStickerCollectionMock()
			: base(null)
		{
		}
	}

	public class CheckItemMock : Mock<CheckItem>
	{
		public CheckItemMock()
			: base(null)
		{
		}
	}

	public class CheckItemCollectionMock : Mock<CheckItemCollection>
	{
		public CheckItemCollectionMock()
			: base(null)
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
			: base(null)
		{
		}
	}

	public class CommentCollectionMock : Mock<CommentCollection>
	{
		public CommentCollectionMock()
			: base(null)
		{
		}
	}

	public class ImagePreviewMock : Mock<ImagePreview>
	{
		public ImagePreviewMock()
			: base(null)
		{
		}
	}

	public class LabelMock : Mock<Label>
	{
		public LabelMock()
			: base(null)
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
			: base(null)
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
			: base(null)
		{
		}
	}

	public class MemberPreferencesMock : Mock<MemberPreferences>
	{
		public MemberPreferencesMock()
			: base(null)
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
			: base(string.Empty, null, null, null, null, null)
		{
		}
	}

	public class MemberStickerCollectionMock : Mock<MemberStickerCollection>
	{
		public MemberStickerCollectionMock()
			: base(null)
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
			: base(null)
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
			: base(null)
		{
		}
	}

	public class OrganizationMembershipMock : Mock<OrganizationMembership>
	{
		public OrganizationMembershipMock()
			: base(null)
		{
		}
	}

	public class OrganizationMembershipCollectionMock : Mock<OrganizationMembershipCollection>
	{
		public OrganizationMembershipCollectionMock()
			: base(null)
		{
		}
	}

	public class OrganizationPreferencesMock : Mock<OrganizationPreferences>
	{
		public OrganizationPreferencesMock()
			: base(null)
		{
		}
	}

	public class PositionMock : Mock<Position>
	{
		public PositionMock()
			: base(0.0)
		{
		}
	}

	public class PowerUpMock : Mock<UnknownPowerUp>
	{
		public PowerUpMock()
			: base(null)
		{
		}
	}

	public class PowerUpDataMock : Mock<PowerUpData>
	{
		public PowerUpDataMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyActionCollectionMock : Mock<ReadOnlyActionCollection>
	{
		public ReadOnlyActionCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyAttachmentCollectionMock : Mock<ReadOnlyAttachmentCollection>
	{
		public ReadOnlyAttachmentCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyAttachmentPreviewCollectionMock : Mock<ReadOnlyAttachmentPreviewCollection>
	{
		public ReadOnlyAttachmentPreviewCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyBoardBackgroundScalesCollectionMock : Mock<ReadOnlyBoardBackgroundScalesCollection>
	{
		public ReadOnlyBoardBackgroundScalesCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyBoardCollectionMock : Mock<ReadOnlyBoardCollection>
	{
		public ReadOnlyBoardCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyBoardMembershipCollectionMock : Mock<ReadOnlyBoardMembershipCollection>
	{
		public ReadOnlyBoardMembershipCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyCardCollectionMock : Mock<ReadOnlyCardCollection>
	{
		public ReadOnlyCardCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyCheckItemCollectionMock : Mock<ReadOnlyCheckItemCollection>
	{
		public ReadOnlyCheckItemCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyCheckListCollectionMock : Mock<ReadOnlyCheckListCollection>
	{
		public ReadOnlyCheckListCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyListCollectionMock : Mock<ReadOnlyListCollection>
	{
		public ReadOnlyListCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyMemberCollectionMock : Mock<ReadOnlyMemberCollection>
	{
		public ReadOnlyMemberCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyNotificationCollectionMock : Mock<ReadOnlyNotificationCollection>
	{
		public ReadOnlyNotificationCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyOrganizationCollectionMock : Mock<ReadOnlyOrganizationCollection>
	{
		public ReadOnlyOrganizationCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyOrganizationMembershipCollectionMock : Mock<ReadOnlyOrganizationMembershipCollection>
	{
		public ReadOnlyOrganizationMembershipCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyPowerUpCollectionMock : Mock<ReadOnlyPowerUpCollection>
	{
		public ReadOnlyPowerUpCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyPowerUpDataCollectionMock : Mock<ReadOnlyPowerUpDataCollection>
	{
		public ReadOnlyPowerUpDataCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyStickerCollectionMock : Mock<ReadOnlyStickerCollection>
	{
		public ReadOnlyStickerCollectionMock()
			: base(null)
		{
		}
	}

	public class ReadOnlyStickerPreviewCollectionMock : Mock<ReadOnlyStickerPreviewCollection>
	{
		public ReadOnlyStickerPreviewCollectionMock()
			: base(null)
		{
		}
	}

	public class SearchMock : Mock<Search>
	{
		public SearchMock()
			: base((string)null, null,SearchModelType.All, null, null, false)
		{
		}
	}

	public class StickerMock : Mock<Sticker>
	{
		public StickerMock()
			: base(null)
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
			: base(null)
		{
		}
	}

	public class WebColorMock : Mock<WebColor>
	{
		public WebColorMock()
			: base(string.Empty)
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

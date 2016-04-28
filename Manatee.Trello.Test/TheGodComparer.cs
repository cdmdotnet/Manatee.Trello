using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Json;

namespace Manatee.Trello.Test
{
	public class TheGodComparer : IEqualityComparer<IJsonAttachment>,
	                              IEqualityComparer<IJsonBadges>,
	                              IEqualityComparer<IJsonBoard>,
	                              IEqualityComparer<IJsonBoardBackground>,
	                              IEqualityComparer<IJsonBoardPreferences>,
	                              IEqualityComparer<IJsonCard>,
	                              IEqualityComparer<IJsonCheckItem>,
	                              IEqualityComparer<IJsonCheckList>,
	                              IEqualityComparer<IJsonImagePreview>,
	                              IEqualityComparer<IJsonList>,
	                              IEqualityComparer<IJsonMember>,
	                              IEqualityComparer<IJsonMemberPreferences>,
	                              IEqualityComparer<IJsonNotification>,
	                              IEqualityComparer<IJsonNotificationData>,
	                              IEqualityComparer<IJsonNotificationOldData>,
	                              IEqualityComparer<IJsonOrganization>,
	                              IEqualityComparer<IJsonOrganizationPreferences>,
	                              IEqualityComparer<IJsonPosition>,
	                              IEqualityComparer<IJsonLabel>
	{
		public static TheGodComparer Instance { get; }

		static TheGodComparer()
		{
			Instance = new TheGodComparer();
		}
		private TheGodComparer() {}

		private static int GetCollectionHashCode<T>(IEnumerable<T> objs)
		{
			unchecked
			{
				var hashCode = 0;
				foreach (var obj in objs)
				{
					hashCode = (hashCode*397) ^ obj?.GetHashCode() ?? 0;
				}
				return hashCode;
			}
		}

		private static int GetCollectionHashCode<T>(IEnumerable<T> objs, IEqualityComparer<T> comparer)
		{
			unchecked
			{
				var hashCode = 0;
				foreach (var obj in objs)
				{
					hashCode = (hashCode*397) ^ comparer.GetHashCode(obj);
				}
				return hashCode;
			}
		}

		#region IJsonNotification

		public bool Equals(IJsonNotification x, IJsonNotification y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.Type == y.Type &&
			       Instance.Equals(x.Data, y.Data) &&
			       x.Date == y.Date &&
			       Instance.Equals(x.MemberCreator, y.MemberCreator) &&
			       x.Unread == y.Unread &&
			       x.Id == y.Id;
		}
		public int GetHashCode(IJsonNotification obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = obj.Id?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ obj.Unread.GetHashCode();
				hashCode = (hashCode*397) ^ obj.Type.GetHashCode();
				hashCode = (hashCode*397) ^ obj.Date.GetHashCode();
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Data);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.MemberCreator);
				return hashCode;
			}
		}

		#endregion

		#region IJsonMember

		public bool Equals(IJsonMember x, IJsonMember y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.AvatarHash == y.AvatarHash &&
			       x.AvatarSource == y.AvatarSource &&
			       x.Bio == y.Bio &&
			       x.Confirmed == y.Confirmed &&
			       x.Email == y.Email &&
			       x.FullName == y.FullName &&
			       x.GravatarHash == y.GravatarHash &&
			       x.Id == y.Id &&
			       x.Initials == y.Initials &&
			       (x.LoginTypes?.SequenceEqual(y.LoginTypes) ?? true) &&
			       x.MemberType == y.MemberType &&
			       (x.OneTimeMessagesDismissed?.SequenceEqual(y.OneTimeMessagesDismissed) ?? true) &&
			       Instance.Equals(x.Prefs, y.Prefs) &&
			       x.Similarity == y.Similarity &&
			       x.Status == y.Status &&
			       (x.Trophies?.SequenceEqual(y.Trophies) ?? true) &&
			       x.UploadedAvatarHash == y.UploadedAvatarHash &&
			       x.Url == y.Url &&
			       x.Username == y.Username;
		}
		public int GetHashCode(IJsonMember obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = obj.Id?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ (obj.AvatarHash?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.Bio?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.FullName?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.Initials?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.MemberType?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ obj.Status.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.Url?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.Username?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ obj.AvatarSource.GetHashCode();
				hashCode = (hashCode*397) ^ obj.Confirmed.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.Email?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.GravatarHash?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ GetCollectionHashCode(obj.LoginTypes);
				hashCode = (hashCode*397) ^ GetCollectionHashCode(obj.Trophies);
				hashCode = (hashCode*397) ^ (obj.UploadedAvatarHash?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ GetCollectionHashCode(obj.OneTimeMessagesDismissed);
				hashCode = (hashCode*397) ^ obj.Similarity.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.Prefs?.GetHashCode() ?? 0);
				return hashCode;
			}
		}

		#endregion

		#region IJsonBoard

		public bool Equals(IJsonBoard x, IJsonBoard y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.Id == y.Id &&
			       x.BoardSource == y.BoardSource &&
			       x.Closed == y.Closed &&
			       x.Desc == y.Desc &&
			       x.Name == y.Name &&
			       Instance.Equals(x.Organization, y.Organization) &&
			       Instance.Equals(x.Prefs, y.Prefs) &&
			       x.Subscribed == y.Subscribed &&
			       x.Url == y.Url;
		}
		public int GetHashCode(IJsonBoard obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = obj.Id?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ (obj.Name?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.Desc?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ obj.Closed.GetHashCode();
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Organization);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Prefs);
				hashCode = (hashCode*397) ^ (obj.Url?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ obj.Subscribed.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.BoardSource?.GetHashCode() ?? 0);
				return hashCode;
			}
		}

		#endregion

		#region IJsonNotificationData

		public bool Equals(IJsonNotificationData x, IJsonNotificationData y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return Instance.Equals(x.Attachment, y.Attachment) &&
			       Instance.Equals(x.Board, y.Board) &&
			       Instance.Equals(x.BoardSource, y.BoardSource) &&
			       Instance.Equals(x.BoardTarget, y.BoardTarget) &&
			       Instance.Equals(x.Card, y.Card) &&
			       Instance.Equals(x.CardSource, y.CardSource) &&
			       Instance.Equals(x.CheckItem, y.CheckItem) &&
			       Instance.Equals(x.CheckList, y.CheckList) &&
			       Instance.Equals(x.List, y.List) &&
			       Instance.Equals(x.ListAfter, y.ListAfter) &&
			       Instance.Equals(x.ListBefore, y.ListBefore) &&
			       Instance.Equals(x.Member, y.Member) &&
			       Instance.Equals(x.Org, y.Org) &&
			       Instance.Equals(x.Old, y.Old) &&
			       x.Text == y.Text;
		}
		public int GetHashCode(IJsonNotificationData obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = Instance.GetHashCode(obj.Attachment);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Board);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.BoardSource);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.BoardTarget);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Card);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.CardSource);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.CheckItem);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.CheckList);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.List);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.ListAfter);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.ListBefore);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Member);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Org);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Old);
				hashCode = (hashCode*397) ^ (obj.Text?.GetHashCode() ?? 0);
				return hashCode;
			}
		}

		#endregion

		#region IJsonMemberPreferences

		public bool Equals(IJsonMemberPreferences x, IJsonMemberPreferences y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.MinutesBetweenSummaries == y.MinutesBetweenSummaries &&
			       x.ColorBlind == y.ColorBlind;
		}
		public int GetHashCode(IJsonMemberPreferences obj)
		{
			if (obj == null) return 0;

			return (obj.MinutesBetweenSummaries.GetHashCode()*397) ^ obj.ColorBlind.GetHashCode();
		}

		#endregion

		#region IJsonBoardPreferences

		public bool Equals(IJsonBoardPreferences x, IJsonBoardPreferences y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.PermissionLevel == y.PermissionLevel &&
			       x.Voting == y.Voting &&
			       x.Comments == y.Comments &&
			       x.Invitations == y.Invitations &&
			       x.SelfJoin == y.SelfJoin &&
			       x.CardCovers == y.CardCovers &&
			       x.CalendarFeed == y.CalendarFeed &&
			       x.CardAging == y.CardAging &&
			       Instance.Equals(x.Background, y.Background);
		}
		public int GetHashCode(IJsonBoardPreferences obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = obj.PermissionLevel.GetHashCode();
				hashCode = (hashCode*397) ^ obj.Voting.GetHashCode();
				hashCode = (hashCode*397) ^ obj.Comments.GetHashCode();
				hashCode = (hashCode*397) ^ obj.Invitations.GetHashCode();
				hashCode = (hashCode*397) ^ obj.SelfJoin.GetHashCode();
				hashCode = (hashCode*397) ^ obj.CardCovers.GetHashCode();
				hashCode = (hashCode*397) ^ obj.CalendarFeed.GetHashCode();
				hashCode = (hashCode*397) ^ obj.CardAging.GetHashCode();
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Background);
				return hashCode;
			}
		}

		#endregion

		#region IJsonOrganization

		public bool Equals(IJsonOrganization x, IJsonOrganization y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.Id == y.Id &&
			       x.Name == y.Name &&
			       x.DisplayName == y.DisplayName &&
			       x.Desc == y.Desc &&
			       x.Url == y.Url &&
			       x.Website == y.Website &&
			       x.LogoHash == y.LogoHash &&
			       (x.PowerUps?.SequenceEqual(y.PowerUps) ?? true) &&
			       x.PaidAccount == y.PaidAccount &&
			       (x.PremiumFeatures?.SequenceEqual(y.PremiumFeatures) ?? true) &&
			       Instance.Equals(x.Prefs, y.Prefs);
		}
		public int GetHashCode(IJsonOrganization obj)
		{
			unchecked
			{
				var hashCode = obj.Id?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ (obj.Name?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.DisplayName?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.Desc?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.Url?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.Website?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.LogoHash?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ GetCollectionHashCode(obj.PowerUps);
				hashCode = (hashCode*397) ^ obj.PaidAccount.GetHashCode();
				hashCode = (hashCode*397) ^ GetCollectionHashCode(obj.PremiumFeatures);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Prefs);
				return hashCode;
			}
		}

		#endregion

		#region IJsonAttachment

		public bool Equals(IJsonAttachment x, IJsonAttachment y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.Id == y.Id &&
			       x.Bytes == y.Bytes &&
			       x.Date == y.Date &&
			       Instance.Equals(x.Member, y.Member) &&
			       x.IsUpload == y.IsUpload &&
			       x.MimeType == y.MimeType &&
			       x.Name == y.Name &&
			       (x.Previews?.SequenceEqual(y.Previews, Instance) ?? true) &&
			       x.Url == y.Url;
		}
		public int GetHashCode(IJsonAttachment obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = obj.Id?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ obj.Bytes.GetHashCode();
				hashCode = (hashCode*397) ^ obj.Date.GetHashCode();
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Member);
				hashCode = (hashCode*397) ^ obj.IsUpload.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.MimeType?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.Name?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ GetCollectionHashCode(obj.Previews);
				hashCode = (hashCode*397) ^ (obj.Url?.GetHashCode() ?? 0);
				return hashCode;
			}
		}

		#endregion

		#region IJsonCard

		public bool Equals(IJsonCard x, IJsonCard y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.Id == y.Id &&
			       Instance.Equals(x.Badges, y.Badges) &&
			       x.Closed == y.Closed &&
			       x.DateLastActivity == y.DateLastActivity &&
			       x.Desc == y.Desc &&
			       x.Due == y.Due &&
			       Instance.Equals(x.Board, y.Board) &&
			       Instance.Equals(x.List, y.List) &&
			       x.IdShort == y.IdShort &&
			       x.IdAttachmentCover == y.IdAttachmentCover &&
			       (x.Labels?.SequenceEqual(y.Labels, Instance) ?? true) &&
			       x.ManualCoverAttachment == y.ManualCoverAttachment &&
			       x.Name == y.Name &&
			       Instance.Equals(x.Pos, y.Pos) &&
			       x.Url == y.Url &&
			       x.ShortUrl == y.ShortUrl &&
			       x.Subscribed == y.Subscribed &&
			       Instance.Equals(x.CardSource, y.CardSource) &&
			       Equals(x.UrlSource, y.UrlSource) &&
			       x.ForceDueDate == y.ForceDueDate;
		}
		public int GetHashCode(IJsonCard obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = obj.Id?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Badges);
				hashCode = (hashCode*397) ^ obj.Closed.GetHashCode();
				hashCode = (hashCode*397) ^ obj.DateLastActivity.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.Desc?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ obj.Due.GetHashCode();
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Board);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.List);
				hashCode = (hashCode*397) ^ obj.IdShort.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.IdAttachmentCover?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ GetCollectionHashCode(obj.Labels);
				hashCode = (hashCode*397) ^ obj.ManualCoverAttachment.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.Name?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Pos);
				hashCode = (hashCode*397) ^ (obj.Url?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.ShortUrl?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ obj.Subscribed.GetHashCode();
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.CardSource);
				hashCode = (hashCode*397) ^ (obj.UrlSource?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ obj.ForceDueDate.GetHashCode();
				return hashCode;
			}
		}

		#endregion

		#region IJsonCheckItem

		public bool Equals(IJsonCheckItem x, IJsonCheckItem y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.Id == y.Id &&
			       x.State == y.State &&
			       x.Name == y.Name &&
			       Instance.Equals(x.Pos, y.Pos);
		}
		public int GetHashCode(IJsonCheckItem obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = obj.Id?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ obj.State.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.Name?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Pos);
				return hashCode;
			}
		}

		#endregion

		#region IJsonCheckList

		public bool Equals(IJsonCheckList x, IJsonCheckList y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.Id == y.Id &&
			       string.Equals(x.Name, y.Name) &&
			       Instance.Equals(x.Board, y.Board) &&
			       Instance.Equals(x.Card, y.Card) &&
			       (x.CheckItems?.SequenceEqual(y.CheckItems, Instance) ?? true) &&
			       Instance.Equals(x.Pos, y.Pos) &&
			       Instance.Equals(x.CheckListSource, y.CheckListSource);
		}
		public int GetHashCode(IJsonCheckList obj)
		{
			unchecked
			{
				var hashCode = obj.Id?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ (obj.Name?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Board);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Card);
				hashCode = (hashCode*397) ^ GetCollectionHashCode(obj.CheckItems, Instance);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Pos);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.CheckListSource);
				return hashCode;
			}
		}

		#endregion

		#region IJsonList

		public bool Equals(IJsonList x, IJsonList y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.Id == y.Id &&
			       x.Name == y.Name &&
			       x.Closed == y.Closed &&
			       Instance.Equals(x.Board, y.Board) &&
			       Instance.Equals(x.Pos, y.Pos) &&
			       x.Subscribed == y.Subscribed;
		}
		public int GetHashCode(IJsonList obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = obj.Id?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ (obj.Name?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ obj.Closed.GetHashCode();
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Board);
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.Pos);
				hashCode = (hashCode*397) ^ obj.Subscribed.GetHashCode();
				return hashCode;
			}
		}

		#endregion

		#region IJsonNotificationOldData

		public bool Equals(IJsonNotificationOldData x, IJsonNotificationOldData y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.Desc == y.Desc &&
			       Instance.Equals(x.List, y.List) &&
			       x.Pos == y.Pos &&
			       x.Text == y.Text &&
			       x.Closed == y.Closed;
		}
		public int GetHashCode(IJsonNotificationOldData obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = obj.Desc?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ Instance.GetHashCode(obj.List);
				hashCode = (hashCode*397) ^ obj.Pos.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.Text?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ obj.Closed.GetHashCode();
				return hashCode;
			}
		}

		#endregion

		#region IJsonBoardBackground

		public bool Equals(IJsonBoardBackground x, IJsonBoardBackground y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.Id == y.Id &&
			       x.Color == y.Color &&
			       x.Image == y.Image &&
			       (x.ImageScaled?.SequenceEqual(y.ImageScaled) ?? true) &&
			       x.Tile == y.Tile;
		}
		public int GetHashCode(IJsonBoardBackground obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = obj.Id?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ (obj.Color?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.Image?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ GetCollectionHashCode(obj.ImageScaled, Instance);
				hashCode = (hashCode*397) ^ obj.Tile.GetHashCode();
				return hashCode;
			}
		}

		#endregion

		#region IJsonOrganizationPreferences

		public bool Equals(IJsonOrganizationPreferences x, IJsonOrganizationPreferences y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.PermissionLevel == y.PermissionLevel &&
			       (x.OrgInviteRestrict?.SequenceEqual(y.OrgInviteRestrict) ?? true) &&
			       x.ExternalMembersDisabled == y.ExternalMembersDisabled &&
			       x.AssociatedDomain == y.AssociatedDomain &&
			       x.BoardVisibilityRestrict == y.BoardVisibilityRestrict;
		}
		public int GetHashCode(IJsonOrganizationPreferences obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = obj.PermissionLevel.GetHashCode();
				hashCode = (hashCode*397) ^ GetCollectionHashCode(obj.OrgInviteRestrict);
				hashCode = (hashCode*397) ^ obj.ExternalMembersDisabled.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.AssociatedDomain?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.BoardVisibilityRestrict?.GetHashCode() ?? 0);
				return hashCode;
			}
		}

		#endregion

		#region IJsonImagePreview

		public bool Equals(IJsonImagePreview x, IJsonImagePreview y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.Width == y.Width &&
			       x.Height == y.Height &&
			       x.Url == y.Url &&
			       x.Id == y.Id &&
			       x.Scaled == y.Scaled;
		}
		public int GetHashCode(IJsonImagePreview obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = obj.Width.GetHashCode();
				hashCode = (hashCode*397) ^ obj.Height.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.Url?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.Id?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ obj.Scaled.GetHashCode();
				return hashCode;
			}
		}

		#endregion

		#region IJsonBadges

		public bool Equals(IJsonBadges x, IJsonBadges y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.Votes == y.Votes &&
			       x.ViewingMemberVoted == y.ViewingMemberVoted &&
			       x.Subscribed == y.Subscribed &&
			       x.Fogbugz == y.Fogbugz &&
			       x.Due == y.Due &&
			       x.Description == y.Description &&
			       x.Comments == y.Comments &&
			       x.CheckItemsChecked == y.CheckItemsChecked &&
			       x.CheckItems == y.CheckItems &&
			       x.Attachments == y.Attachments;
		}
		public int GetHashCode(IJsonBadges obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = obj.Votes.GetHashCode();
				hashCode = (hashCode*397) ^ obj.ViewingMemberVoted.GetHashCode();
				hashCode = (hashCode*397) ^ obj.Subscribed.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.Fogbugz?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ obj.Due.GetHashCode();
				hashCode = (hashCode*397) ^ obj.Description.GetHashCode();
				hashCode = (hashCode*397) ^ obj.Comments.GetHashCode();
				hashCode = (hashCode*397) ^ obj.CheckItemsChecked.GetHashCode();
				hashCode = (hashCode*397) ^ obj.CheckItems.GetHashCode();
				hashCode = (hashCode*397) ^ obj.Attachments.GetHashCode();
				return hashCode;
			}
		}

		#endregion

		#region IJsonPosition

		public bool Equals(IJsonPosition x, IJsonPosition y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return x.Explicit == y.Explicit &&
			       x.Named == y.Named;
		}
		public int GetHashCode(IJsonPosition obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				return (obj.Explicit.GetHashCode()*397) ^ (obj.Named?.GetHashCode() ?? 0);
			}
		}

		#endregion

		#region IJsonLabel

		public bool Equals(IJsonLabel x, IJsonLabel y)
		{
			if (x == null && y != null) return false;
			if (x != null && y == null) return false;
			if (x == null) return true;

			return Instance.Equals(x.Board, y.Board) &&
			       x.Color == y.Color &&
			       x.Id == y.Id &&
			       x.Name == y.Name &&
			       x.Uses == y.Uses;
		}
		public int GetHashCode(IJsonLabel obj)
		{
			if (obj == null) return 0;

			unchecked
			{
				var hashCode = Instance.GetHashCode(obj.Board);
				hashCode = (hashCode*397) ^ obj.Color.GetHashCode();
				hashCode = (hashCode*397) ^ (obj.Id?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (obj.Name?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ obj.Uses.GetHashCode();
				return hashCode;
			}
		}

		#endregion
	}
}

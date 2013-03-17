using System;
using System.Collections.Generic;
using Manatee.Trello.Implementation;

namespace Manatee.Trello.Rest
{
	internal class EntityService
	{
		private static readonly Dictionary<Type, string> SectionStrings =
			new Dictionary<Type, string>
				{
					{typeof (Action), "actions"},
					{typeof (ActionData), "data"},
					{typeof (Attachment), "attachments"},
					{typeof (Badges), "badges"},
					{typeof (Board), "boards"},
					{typeof (BoardMembership), "memberships"},
					{typeof (BoardPersonalPreferences), "myPrefs"},
					{typeof (BoardPreferences), "prefs"},
					{typeof (Card), "cards"},
					{typeof (CheckItem), "checkItems"},
					{typeof (CheckItemState), "checkItemStates"},
					{typeof (CheckList), "checklists"},
					{typeof (InvitedBoard), "idBoardsInvited"},
					{typeof (InvitedOrganization), "idOrganizationsInvited"},
					{typeof (Label), "labels"},
					{typeof (LabelNames), "labelNames"},
					{typeof (List), "lists"},
					{typeof (Member), "members"},
					{typeof (MemberPreferences), "prefs"},
					{typeof (Notification), "notifications"},
					{typeof (Organization), "organizations"},
					{typeof (OrganizationPreferences), "prefs"},
					{typeof (PinnedBoard), "idPinnedBoards"},
					{typeof (PremiumOrganization), "idPremOrgsAdmin"},
					{typeof (VotingMember), "membersVoted"},
				};

		private readonly TrelloApi _api;

		public EntityService(string authKey, string authToken)
		{
			_api = new TrelloApi(authKey, authToken);
		}

		public T GetEntity<T>(string id) where T : JsonCompatibleEquatableExpiringObject, new()
		{
			string section = SectionStrings[typeof (T)];
			return _api.GetRequest<T>("{0}/{1}/", section, id);
		}
		public TContent GetOwnedEntity<TSource, TContent>(string id)
			where TSource : EntityBase
			where TContent : OwnedEntityBase<TSource>, new()
		{
			string section = SectionStrings[typeof(TSource)],
				   itemType = SectionStrings[typeof(TContent)];
			return _api.GetRequest<TContent>("{0}/{1}/{2}", section, id, itemType);
		}
		public List<TContent> GetContents<TSource, TContent>(string id) where TSource : EntityBase
		{
			string section = SectionStrings[typeof (TSource)],
			       itemType = SectionStrings[typeof (TContent)];
			return _api.GetRequest<List<TContent>>("{0}/{1}/{2}", section, id, itemType);
		}
		//public List<TContent> GetContents<TContent>(Type sourceType, string id)
		//{
		//    string section = SectionStrings[sourceType],
		//           itemType = SectionStrings[typeof(TContent)];
		//    return _api.GetRequest<List<TContent>>("{0}/{1}/{2}", section, id, itemType);
		//}
	}
}

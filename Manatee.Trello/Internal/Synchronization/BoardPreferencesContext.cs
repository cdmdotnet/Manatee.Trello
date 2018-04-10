using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BoardPreferencesContext : LinkedSynchronizationContext<IJsonBoardPreferences>
	{
		static BoardPreferencesContext()
		{
			Properties = new Dictionary<string, Property<IJsonBoardPreferences>>
				{
					{
						nameof(BoardPreferences.PermissionLevel),
						new Property<IJsonBoardPreferences, BoardPermissionLevel?>((d, a) => d.PermissionLevel,
						                                                           (d, o) => d.PermissionLevel = o)
					},
					{
						nameof(BoardPreferences.Voting),
						new Property<IJsonBoardPreferences, BoardVotingPermission?>((d, a) => d.Voting, (d, o) => d.Voting = o)
					},
					{
						nameof(BoardPreferences.Commenting),
						new Property<IJsonBoardPreferences, BoardCommentPermission?>((d, a) => d.Comments, (d, o) => d.Comments = o)
					},
					{
						nameof(BoardPreferences.Invitations),
						new Property<IJsonBoardPreferences, BoardInvitationPermission?>((d, a) => d.Invitations,
						                                                                (d, o) => d.Invitations = o)
					},
					{
						nameof(BoardPreferences.AllowSelfJoin),
						new Property<IJsonBoardPreferences, bool?>((d, a) => d.SelfJoin, (d, o) => d.SelfJoin = o)
					},
					{
						nameof(BoardPreferences.ShowCardCovers),
						new Property<IJsonBoardPreferences, bool?>((d, a) => d.CardCovers, (d, o) => d.CardCovers = o)
					},
					{
						nameof(BoardPreferences.IsCalendarFeedEnabled),
						new Property<IJsonBoardPreferences, bool?>((d, a) => d.CalendarFeed, (d, o) => d.CalendarFeed = o)
					},
					{
						nameof(BoardPreferences.CardAgingStyle),
						new Property<IJsonBoardPreferences, CardAgingStyle?>((d, a) => d.CardAging, (d, o) => d.CardAging = o)
					},
					{
						nameof(BoardPreferences.Background),
						new Property<IJsonBoardPreferences, BoardBackground>((d, a) => d.Background?.GetFromCache<BoardBackground>(a),
						                                                     (d, o) => d.Background = o?.Json)
					}
				};
		}
		public BoardPreferencesContext(TrelloAuthorization auth)
			: base(auth) {}
	}
}
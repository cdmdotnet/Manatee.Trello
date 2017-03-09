using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BoardPreferencesContext : LinkedSynchronizationContext<IJsonBoardPreferences>
	{
		static BoardPreferencesContext()
		{
			_properties = new Dictionary<string, Property<IJsonBoardPreferences>>
				{
					{"PermissionLevel", new Property<IJsonBoardPreferences, BoardPermissionLevel?>((d, a) => d.PermissionLevel, (d, o) => d.PermissionLevel = o)},
					{"Voting", new Property<IJsonBoardPreferences, BoardVotingPermission?>((d, a) => d.Voting, (d, o) => d.Voting = o)},
					{"Commenting", new Property<IJsonBoardPreferences, BoardCommentPermission?>((d, a) => d.Comments, (d, o) => d.Comments = o)},
					{"Invitations", new Property<IJsonBoardPreferences, BoardInvitationPermission?>((d, a) => d.Invitations, (d, o) => d.Invitations = o)},
					{"AllowSelfJoin", new Property<IJsonBoardPreferences, bool?>((d, a) => d.SelfJoin, (d, o) => d.SelfJoin = o)},
					{"ShowCardCovers", new Property<IJsonBoardPreferences, bool?>((d, a) => d.CardCovers, (d, o) => d.CardCovers = o)},
					{"IsCalendarFeedEnabled", new Property<IJsonBoardPreferences, bool?>((d, a) => d.CalendarFeed, (d, o) => d.CalendarFeed = o)},
					{"CardAgingStyle", new Property<IJsonBoardPreferences, CardAgingStyle?>((d, a) => d.CardAging, (d, o) => d.CardAging = o)},
					{
						"Background", new Property<IJsonBoardPreferences, BoardBackground>((d, a) => d.Background?.GetFromCache<BoardBackground>(a),
																						   (d, o) => d.Background = o?.Json)
					}
				};
		}
		public BoardPreferencesContext(TrelloAuthorization auth)
			: base(auth) {}
	}
}
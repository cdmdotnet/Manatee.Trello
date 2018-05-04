using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeBoardPreferences : IJsonBoardPreferences, IJsonSerializable
	{
		public BoardPermissionLevel? PermissionLevel { get; set; }
		public BoardVotingPermission? Voting { get; set; }
		public BoardCommentPermission? Comments { get; set; }
		public BoardInvitationPermission? Invitations { get; set; }
		public bool? SelfJoin { get; set; }
		public bool? CardCovers { get; set; }
		public bool? CalendarFeed { get; set; }
		public CardAgingStyle? CardAging { get; set; }
		public IJsonBoardBackground Background { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			PermissionLevel = obj.Deserialize<BoardPermissionLevel?>(serializer, "permissionLevel");
			Voting = obj.Deserialize<BoardVotingPermission?>(serializer, "voting");
			Comments = obj.Deserialize<BoardCommentPermission?>(serializer, "comments");
			Invitations = obj.Deserialize<BoardInvitationPermission?>(serializer, "invitations");
			SelfJoin = obj.TryGetBoolean("selfJoin");
			CardCovers = obj.TryGetBoolean("cardCovers");
			CalendarFeed = obj.TryGetBoolean("calendarFeed");
			CardAging = obj.Deserialize<CardAgingStyle?>(serializer, "cardAging");
			var background = serializer.Deserialize<IJsonBoardBackground>(obj);
			if (background.Id != null)
				Background = background;
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}

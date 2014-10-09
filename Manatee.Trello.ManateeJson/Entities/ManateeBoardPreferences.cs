/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ManateeBoardPreferences.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeBoardPreferences
	Purpose:		Implements IJsonBoardPreferences for Manatee.Json.

***************************************************************************************/

using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeBoardPreferences : IJsonBoardPreferences, IJsonSerializable
	{
		public BoardPermissionLevel PermissionLevel { get; set; }
		public BoardVotingPermission Voting { get; set; }
		public BoardCommentPermission Comments { get; set; }
		public BoardInvitationPermission Invitations { get; set; }
		public bool? SelfJoin { get; set; }
		public bool? CardCovers { get; set; }
		public bool? CalendarFeed { get; set; }
		public CardAgingStyle CardAging { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			PermissionLevel = obj.Deserialize<BoardPermissionLevel>(serializer, "permissionLevel");
			Voting = obj.Deserialize<BoardVotingPermission>(serializer, "voting");
			Comments = obj.Deserialize<BoardCommentPermission>(serializer, "comments");
			Invitations = obj.Deserialize<BoardInvitationPermission>(serializer, "invitations");
			SelfJoin = obj.TryGetBoolean("selfJoin");
			CardCovers = obj.TryGetBoolean("cardCovers");
			CalendarFeed = obj.TryGetBoolean("calendarFeed");
			CardAging = obj.Deserialize<CardAgingStyle>(serializer, "cardAging");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}

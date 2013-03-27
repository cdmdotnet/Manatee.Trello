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
 
	File Name:		InvitedBoard.cs
	Namespace:		Manatee.Trello.Implementation
	Class Name:		InvitedBoard
	Purpose:		Represents a single board to which a user is invited on Trello.com.

***************************************************************************************/
using System;
using Manatee.Json.Enumerations;

namespace Manatee.Trello.Implementation
{
	internal class InvitedBoard : Board, IEquatable<InvitedBoard>
	{
		public InvitedBoard() {}
		internal InvitedBoard(TrelloService svc, string id)
			: base(svc, id) {}

		public bool Equals(InvitedBoard other)
		{
			return base.Equals(this);
		}
		public override void FromJson(Manatee.Json.JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.String) return;
			Id = json.String;
		}
	}
}
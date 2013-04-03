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
 
	File Name:		PinnedBoard.cs
	Namespace:		Manatee.Trello.Implementation
	Class Name:		PinnedBoard
	Purpose:		Represents a single board that is pinned to a user's "Boards"
					menu on Trello.com.

***************************************************************************************/
using System;
using Manatee.Json.Enumerations;

namespace Manatee.Trello.Implementation
{
	internal class PinnedBoard : Board, IEquatable<PinnedBoard>
	{
		internal override string Key { get { return "idBoardsPinned"; } }

		public PinnedBoard() { }
		internal PinnedBoard(ITrelloRest svc, string id)
			: base(svc, id) {}

		public bool Equals(PinnedBoard other)
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
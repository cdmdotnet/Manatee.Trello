﻿/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		BoardExtensions.cs
	Namespace:		Manatee.Trello.Extensions
	Class Name:		BoardExtensions
	Purpose:		Extension methods for boards.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Internal.DataAccess;

namespace Manatee.Trello.Extensions
{
	/// <summary>
	/// Extension methods for boards.
	/// </summary>
	public static class BoardExtensions
	{
		public static ReadOnlyCardCollection CardsForMember(this Board board, Member member)
		{
			return new ReadOnlyCardCollection(EntityRequestType.Board_Read_CardsForMember, board.Id, new Dictionary<string, object> {{"_idMember", member.Id}});
		}
	}
}
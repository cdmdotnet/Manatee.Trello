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
 
	File Name:		CardExtensions.cs
	Namespace:		Manatee.Trello
	Class Name:		CardExtensions
	Purpose:		Exposes extension methods for the Card entity.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes extension methods for the Card entity.
	/// </summary>
	public static class CardExtensions
	{
		/// <summary>
		/// Retrieves all actions performed on a card by a specified member.
		/// </summary>
		/// <param name="card">The card</param>
		/// <param name="member">The member</param>
		/// <returns>A collection of actions.</returns>
		public static IEnumerable<Action> ActionsByMember(this Card card, Member member)
		{
			return card.Actions.Where(a => a.MemberCreator.Equals(member));
		}
		/// <summary>
		/// Retrieves all checklist items contained within a card.
		/// </summary>
		/// <param name="card">The card</param>
		/// <returns>A collection of checklist items.</returns>
		public static IEnumerable<CheckItem> AllCheckItems(this Card card)
		{
			return card.CheckLists.SelectMany(l => l.CheckItems);
		}
		/// <summary>
		/// Retrieves all completed checklist items contained within a card.
		/// </summary>
		/// <param name="card">The card</param>
		/// <returns>A collection of checklist items.</returns>
		public static IEnumerable<CheckItem> CompleteCheckItems(this Card card)
		{
			return card.AllCheckItems().Where(i => i.State == CheckItemStateType.Complete);
		}
		/// <summary>
		/// Retrieves all incomplete checklist items contained within a card.
		/// </summary>
		/// <param name="card">The card</param>
		/// <returns>A collection of checklist items.</returns>
		public static IEnumerable<CheckItem> IncompleteCheckItems(this Card card)
		{
			return card.AllCheckItems().Where(i => i.State == CheckItemStateType.Incomplete);
		}
	}
}
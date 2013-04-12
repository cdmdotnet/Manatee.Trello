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
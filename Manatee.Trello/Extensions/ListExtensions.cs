using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes extension methods for the List entity.
	/// </summary>
	public static class ListExtensions
	{
		/// <summary>
		/// Retrieves all actions on a list or its contents performed by a specified member.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="member">The member.</param>
		/// <returns>A collection of actions.</returns>
		public static IEnumerable<Action> ActionsByMember(this List list, Member member)
		{
			return list.Actions.Where(a => a.MemberCreator.Equals(member));
		}
		/// <summary>
		/// Retrieves all active cards contained within a list which are assigned to a specified member.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="member">The member.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> CardsAssignedToMember(this List list, Member member)
		{
			return list.Cards.Where(c => c.Members.Contains(member));
		}
		/// <summary>
		/// Retrieves all active cards within a list with due dates within a specified time span of DateTime.Now.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="timeSpan">The time span.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> CardsDueSoon(this List list, TimeSpan timeSpan)
		{
			return list.Cards.Where(c => DateTime.Now > c.DueDate - timeSpan);
		}
		/// <summary>
		/// Retrieves all active cards within a list with names or descriptions which match a specified Regex.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="regex">The Regex.</param>
		/// <returns>A collection of cards.</returns>
		/// <remarks>Description matching does not account for Markdown syntax.</remarks>
		public static IEnumerable<Card> CardsMatching(this List list, Regex regex)
		{
			return list.Cards.Where(c => regex.IsMatch(c.Description) || regex.IsMatch(c.Name));
		}
		/// <summary>
		/// Retrieves all active cards within a list with names or descriptions which contain a specified string.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="search">The string.</param>
		/// <returns>A collection of cards.</returns>
		/// <remarks>Description searching does not account for Markdown syntax.</remarks>
		public static IEnumerable<Card> CardsContaining(this List list, string search)
		{
			return list.Cards.Where(c => c.Description.Contains(search) || c.Name.Contains(search));
		}
		/// <summary>
		/// Retrieves all active cards within a list with names or descriptions which have a specified label color applied.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="color">The label color.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> CardsWithLabel(this List list, LabelColor color)
		{
			return list.Cards.Where(c => c.Labels.Any(l => l.Color == color));
		}
	}
}

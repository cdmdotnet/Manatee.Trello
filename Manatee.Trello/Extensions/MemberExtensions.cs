using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes extension methods for the Member entity.
	/// </summary>
	public static class MemberExtensions
	{
		/// <summary>
		/// Retrieves all cards assigned to a member, both archived and active.
		/// </summary>
		/// <param name="member">The member</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> AllCards(this Member member)
		{
			return member.Boards.SelectMany(b => b.AllCards())
								.Where(c => c.Members.Contains(member));
		}
		/// <summary>
		/// Retrieves all active cards assigned to a member.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> ActiveCards(this Member member)
		{
			return member.Boards.SelectMany(b => b.CardsAssignedToMember(member));
		}
		/// <summary>
		/// Returns only the boards which are owned by a member.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <returns>A collection of boards.</returns>
		public static IEnumerable<Board> PersonalBoards(this Member member)
		{
			return member.Boards.Where(b => b.Organization == null);
		}
		/// <summary>
		/// Retrieves all active cards assigned to a member with due dates within a specified time span of DateTime.Now.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <param name="timeSpan">The time span.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> CardsDueSoon(this Member member, TimeSpan timeSpan)
		{
			return member.ActiveCards().Where(c => DateTime.Now > c.DueDate - timeSpan);
		}
		/// <summary>
		/// Retrieves all active cards assigned to a member with names or descriptions which match a specified Regex.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <param name="regex">The Regex.</param>
		/// <returns>A collection of cards.</returns>
		/// <remarks>Description matching does not account for Markdown syntax.</remarks>
		public static IEnumerable<Card> CardsMatching(this Member member, Regex regex)
		{
			return member.ActiveCards().Where(c => regex.IsMatch(c.Description) || regex.IsMatch(c.Name));
		}
		/// <summary>
		/// Retrieves all active cards assigned to a member with names or descriptions which contain a specified string.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <param name="search">The string.</param>
		/// <returns>A collection of cards.</returns>
		/// <remarks>Description searching does not account for Markdown syntax.</remarks>
		public static IEnumerable<Card> CardsContaining(this Member member, string search)
		{
			return member.ActiveCards().Where(c => c.Description.Contains(search) || c.Name.Contains(search));
		}
	}
}
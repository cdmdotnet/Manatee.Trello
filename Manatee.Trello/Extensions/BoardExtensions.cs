using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes extension methods for Board entities.
	/// </summary>
	public static class BoardExtensions
	{
		/// <summary>
		/// Retrieves all actions on a board or its contents performed by a specified member.
		/// </summary>
		/// <param name="board">The board.</param>
		/// <param name="member">The member.</param>
		/// <returns>A collection of actions.</returns>
		public static IEnumerable<Action> ActionsByMember(this Board board, Member member)
		{
			return board.Actions.Where(a => a.MemberCreator.Equals(member));
		}
		/// <summary>
		/// Retrieves all members of the board who have Admin privileges.
		/// </summary>
		/// <param name="board">The board.</param>
		/// <returns>A collection of members.</returns>
		public static IEnumerable<Member> Admins(this Board board)
		{
			return board.Memberships.Where(m => m.MembershipType == BoardMembershipType.Admin)
									.Select(m => m.Member);
		}
		/// <summary>
		/// Retrieves all cards contained within a board, both archived and active.
		/// </summary>
		/// <param name="board">The board.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> AllCards(this Board board)
		{
			return board.Lists.SelectMany(l => l.Cards)
							  .Union(board.ArchivedCards);
		}
		/// <summary>
		/// Retrieves all lists contained within a board, both archived and active.
		/// </summary>
		/// <param name="board">The board.</param>
		/// <returns>A collection of lists.</returns>
		public static IEnumerable<List> AllLists(this Board board)
		{
			return board.Lists.Union(board.ArchivedLists);
		}
		/// <summary>
		/// Retrieves all active cards contained within a board.
		/// </summary>
		/// <param name="board">The board.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> Cards(this Board board)
		{
			return board.Lists.SelectMany(l => l.Cards);
		}
		/// <summary>
		/// Retrieves all active cards contained within a board which are assigned to a specified member.
		/// </summary>
		/// <param name="board">The board.</param>
		/// <param name="member">The member.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> CardsAssignedToMember(this Board board, Member member)
		{
			return board.Cards().Where(c => c.Members.Contains(member));
		}
		/// <summary>
		/// Retrieves all active cards within a board with due dates within a specified time span of DateTime.Now.
		/// </summary>
		/// <param name="board">The board.</param>
		/// <param name="timeSpan">The time span.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> CardsDueSoon(this Board board, TimeSpan timeSpan)
		{
			return board.Cards().Where(c => DateTime.Now > c.DueDate - timeSpan);
		}
		/// <summary>
		/// Retrieves all active cards within a board with names or descriptions which match a specified Regex.
		/// </summary>
		/// <param name="board">The board.</param>
		/// <param name="regex">The Regex.</param>
		/// <returns>A collection of cards.</returns>
		/// <remarks>Description matching does not account for Markdown syntax.</remarks>
		public static IEnumerable<Card> CardsMatching(this Board board, Regex regex)
		{
			return board.Cards().Where(c => regex.IsMatch(c.Description) || regex.IsMatch(c.Name));
		}
		/// <summary>
		/// Retrieves all active cards within a board with names or descriptions which contain a specified string.
		/// </summary>
		/// <param name="board">The board.</param>
		/// <param name="search">The string.</param>
		/// <returns>A collection of cards.</returns>
		/// <remarks>Description searching does not account for Markdown syntax.</remarks>
		public static IEnumerable<Card> CardsContaining(this Board board, string search)
		{
			return board.Cards().Where(c => c.Description.Contains(search) || c.Name.Contains(search));
		}
		/// <summary>
		/// Retrieves all active cards within a board with names or descriptions which have a specified label color applied.
		/// </summary>
		/// <param name="board">The board.</param>
		/// <param name="color">The label color.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> CardsWithLabel(this Board board, LabelColor color)
		{
			return board.Cards().Where(c => c.Labels.Any(l => l.Color == color));
		}
		/// <summary>
		/// Retrieves all members of a board without specifying their permission level.
		/// </summary>
		/// <param name="board">The board.</param>
		/// <returns>A collection of members.</returns>
		public static IEnumerable<Member> Members(this Board board)
		{
			return board.Memberships.Select(m => m.Member);
		}
	}
}

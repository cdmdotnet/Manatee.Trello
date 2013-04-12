using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes extension methods for the Organization entity.
	/// </summary>
	public static class OrganizationExtensions
	{
		/// <summary>
		/// Retrieves all active cards contained within a organization which are assigned to a specified member.
		/// </summary>
		/// <param name="organization">The organization.</param>
		/// <param name="member">The member.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> CardsAssignedToMember(this Organization organization, Member member)
		{
			return organization.Boards.SelectMany(b => b.Cards())
									  .Where(c => c.Members.Contains(member));
		}
		/// <summary>
		/// Retrieves all active cards within a organization with due dates within a specified time span of DateTime.Now.
		/// </summary>
		/// <param name="organization">The organization.</param>
		/// <param name="timeSpan">The time span.</param>
		/// <returns>A collection of cards.</returns>
		public static IEnumerable<Card> CardsDueSoon(this Organization organization, TimeSpan timeSpan)
		{
			return organization.Boards.SelectMany(b => b.Cards())
									  .Where(c => DateTime.Now > c.DueDate - timeSpan);
		}
		/// <summary>
		/// Retrieves all active cards within a organization with names or descriptions which match a specified Regex.
		/// </summary>
		/// <param name="organization">The organization.</param>
		/// <param name="regex">The Regex.</param>
		/// <returns>A collection of cards.</returns>
		/// <remarks>Description matching does not account for Markdown syntax.</remarks>
		public static IEnumerable<Card> CardsMatching(this Organization organization, Regex regex)
		{
			return organization.Boards.SelectMany(b => b.Cards())
									  .Where(c => regex.IsMatch(c.Description) || regex.IsMatch(c.Name));
		}
		/// <summary>
		/// Retrieves all active cards within a organization with names or descriptions which contain a specified string.
		/// </summary>
		/// <param name="organization">The organization.</param>
		/// <param name="search">The string.</param>
		/// <returns>A collection of cards.</returns>
		/// <remarks>Description searching does not account for Markdown syntax.</remarks>
		public static IEnumerable<Card> CardsContaining(this Organization organization, string search)
		{
			return organization.Boards.SelectMany(b => b.Cards())
									  .Where(c => c.Description.Contains(search) || c.Name.Contains(search));
		}
	}
}
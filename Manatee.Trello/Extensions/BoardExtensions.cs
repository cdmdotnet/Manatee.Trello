using System.Collections.Generic;
using Manatee.Trello.Internal.DataAccess;

namespace Manatee.Trello
{
	/// <summary>
	/// Extension methods for boards.
	/// </summary>
	public static class BoardExtensions
	{
		/// <summary>
		/// Gets all open cards for a member on a specific board.
		/// </summary>
		/// <param name="board">The board.</param>
		/// <param name="member">The member.</param>
		/// <returns>A <see cref="ReadOnlyCardCollection"/> containing the member's cards.</returns>
		public static ReadOnlyCardCollection CardsForMember(this Board board, Member member)
		{
			return new ReadOnlyCardCollection(EntityRequestType.Board_Read_CardsForMember, () => board.Id, board.Auth, new Dictionary<string, object> {{"_idMember", member.Id}});
		}
	}
}
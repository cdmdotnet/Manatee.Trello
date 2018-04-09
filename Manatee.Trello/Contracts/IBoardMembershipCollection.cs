using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of board memberships.
	/// </summary>
	public interface IBoardMembershipCollection : IReadOnlyBoardMembershipCollection
	{
		/// <summary>
		/// Adds a member to a board with specified privileges.
		/// </summary>
		/// <param name="member">The member to add.</param>
		/// <param name="membership">The membership type.</param>
		Task<IBoardMembership> Add(IMember member, BoardMembershipType membership);

		/// <summary>
		/// Removes a member from a board.
		/// </summary>
		/// <param name="member">The member to remove.</param>
		Task Remove(IMember member);
	}
}
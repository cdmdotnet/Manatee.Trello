using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Associates a <see cref="IMember"/> to a <see cref="IBoard"/> and indicates any permissions the member has on the board.
	/// </summary>
	public interface IBoardMembership : ICacheable
	{
		/// <summary>
		/// Gets the creation date of the membership.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets whether the member has accepted the invitation to join Trello.
		/// </summary>
		bool? IsDeactivated { get; }

		/// <summary>
		/// Gets the member.
		/// </summary>
		IMember Member { get; }

		/// <summary>
		/// Gets the membership's permission level.
		/// </summary>
		BoardMembershipType? MemberType { get; set; }

		/// <summary>
		/// Raised when data on the membership is updated.
		/// </summary>
		event Action<IBoardMembership, IEnumerable<string>> Updated;

		/// <summary>
		/// Marks the board membership to be refreshed the next time data is accessed.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Refresh(CancellationToken ct = default(CancellationToken));
	}
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Associates a <see cref="Member"/> to an <see cref="Organization"/> and indicates any permissions the member has in the organization.
	/// </summary>
	public interface IOrganizationMembership : ICacheable
	{
		/// <summary>
		/// Gets the creation date of the membership.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets whether the member has accepted the invitation to join Trello.
		/// </summary>
		bool? IsUnconfirmed { get; }

		/// <summary>
		/// Gets the member.
		/// </summary>
		IMember Member { get; }

		/// <summary>
		/// Gets the membership's permission level.
		/// </summary>
		OrganizationMembershipType? MemberType { get; set; }

		/// <summary>
		/// Raised when data on the membership is updated.
		/// </summary>
		event Action<IOrganizationMembership, IEnumerable<string>> Updated;

		/// <summary>
		/// Refreshes the organization membership data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken));
	}
}
namespace Manatee.Trello
{
	/// <summary>
	/// A collection of organization memberships.
	/// </summary>
	public interface IOrganizationMembershipCollection : IReadOnlyOrganizationMembershipCollection
	{
		/// <summary>
		/// Adds a member to an organization with specified privileges.
		/// </summary>
		/// <param name="member">The member to add.</param>
		/// <param name="membership">The membership type.</param>
		void Add(IMember member, OrganizationMembershipType membership);

		/// <summary>
		/// Removes a member from an organization.
		/// </summary>
		/// <param name="member">The member to remove.</param>
		void Remove(IMember member);
	}
}
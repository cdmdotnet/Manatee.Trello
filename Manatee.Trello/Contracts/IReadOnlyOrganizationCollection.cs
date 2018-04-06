namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of organizations.
	/// </summary>
	public interface IReadOnlyOrganizationCollection : IReadOnlyCollection<IOrganization>
	{
		/// <summary>
		/// Retrieves a organization which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching organization, or null if none found.</returns>
		/// <remarks>
		/// Matches on Organization.Id, Organization.Name, and Organization.DisplayName.  Comparison is case-sensitive.
		/// </remarks>
		IOrganization this[string key] { get; }

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		void Filter(OrganizationFilter filter);
	}
}
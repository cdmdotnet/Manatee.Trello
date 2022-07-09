using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of organization collaborator.
	/// </summary>
	public interface IReadOnlyCollaboratorCollection : IReadOnlyCollection<ICollaborator>
	{
		/// <summary>
		/// Retrieves a collaborator which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching list, or null if none found.</returns>
		/// <remarks>
		/// Matches on OrganizationCollaborator.Id, OrganizationCollaborator.Member.Id, OrganizationCollaborator.Member.FullName, and OrganizationCollaborator.Member.Username. Comparison is case-sensitive.
		/// </remarks>
		ICollaborator this[string key] { get; }

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		void Filter(CollaboratorFilter filter);
		/// <summary>
		/// Adds a set of filters to the collection.
		/// </summary>
		/// <param name="filters">The filter values.</param>
		void Filter(IEnumerable<CollaboratorFilter> filters);
	}
}
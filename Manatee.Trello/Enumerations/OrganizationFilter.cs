using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the filter options for organization collections.
	/// </summary>
	public enum OrganizationFilter
	{
		/// <summary>
		/// Filters to organizations that only members can access.
		/// </summary>
		[Display(Description="members")]
		Members,
		/// <summary>
		/// Filters to organizations that are publicly accessible.
		/// </summary>
		[Display(Description="public")]
		Public,
	}
}

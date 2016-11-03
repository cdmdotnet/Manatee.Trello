using System.ComponentModel;

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
		[Description("members")]
		Members,
		/// <summary>
		/// Filters to organizations that are publicly accessible.
		/// </summary>
		[Description("public")]
		Public,
	}
}

using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the filter options for collaborator collections.
	/// </summary>
	public enum CollaboratorFilter
	{
		/// <summary>
		/// Filters to only normal collaborators.
		/// </summary>
		[Display(Description="normal")]
		Normal,
		/// <summary>
		/// Filters to only ghosts.
		/// </summary>
		[Display(Description="ghost")]
		Ghost,
	}
}

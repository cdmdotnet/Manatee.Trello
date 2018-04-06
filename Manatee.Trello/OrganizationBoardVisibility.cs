using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates known visibility levels for board in orgainzations.
	/// </summary>
	public enum OrganizationBoardVisibility
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that a board within an organization will not be visible.
		/// </summary>
		[Display(Description="none")]
		None,
		/// <summary>
		/// Indicates that a board within an organization will be visible to the organization admins.
		/// </summary>
		[Display(Description="admin")]
		Admin,
		/// <summary>
		/// Indicates that a board within an organization will be visible to the organization members.
		/// </summary>
		[Display(Description="org")]
		Org
	}
}
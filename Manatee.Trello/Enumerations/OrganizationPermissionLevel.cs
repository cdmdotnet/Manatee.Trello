using System.ComponentModel;

namespace Manatee.Trello
{
	///<summary>
	/// Enumerates known values for organization permission levels
	///</summary>
	public enum OrganizationPermissionLevel
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates that the organization can only be viewed by its members.
		/// </summary>
		[Description("private")]
		Private,
		/// <summary>
		/// Indicates that anyone (even non-Trello users) may view the organization.
		/// </summary>
		[Description("public")]
		Public
	}
}
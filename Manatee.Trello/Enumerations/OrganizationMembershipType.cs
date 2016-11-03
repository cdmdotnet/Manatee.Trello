using System.ComponentModel;

namespace Manatee.Trello
{
	///<summary>
	/// Enumerates known board membership types.
	///</summary>
	public enum OrganizationMembershipType
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates the member is an admin of the organization.
		/// </summary>
		[Description("admin")]
		Admin,
		/// <summary>
		/// Indicates the member is a normal member of the organization.
		/// </summary>
		[Description("normal")]
		Normal,
		/// <summary>
		/// Indicates the member is may only view the organization.
		/// </summary>
		[Description("observer")]
		Observer,
		/// <summary>
		/// Indicates the member has been invited, but has not yet joined Trello.
		/// </summary>
		[Description("ghost")]
		Ghost
	}
}

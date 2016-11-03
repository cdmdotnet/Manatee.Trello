using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the OrganizationPreferences object.
	/// </summary>
	public interface IJsonOrganizationPreferences
	{
		/// <summary>
		/// Gets or sets the permission level.
		/// </summary>
		[JsonDeserialize]
		OrganizationPermissionLevel? PermissionLevel { get; set; }
		/// <summary>
		/// Gets or sets organization invitation restrictions.
		/// </summary>
		[JsonDeserialize]
		List<object> OrgInviteRestrict { get; set; }
		/// <summary>
		/// Gets or sets whether external members are disabled.
		/// </summary>
		[JsonDeserialize]
		bool? ExternalMembersDisabled { get; set; }
		/// <summary>
		/// Gets or sets the Google Apps domain.
		/// </summary>
		[JsonDeserialize]
		string AssociatedDomain { get; set; }
		/// <summary>
		/// Gets or sets the visibility of boards owned by the organization.
		/// </summary>
		[JsonDeserialize]
		IJsonBoardVisibilityRestrict BoardVisibilityRestrict { get; set; }
	}
}
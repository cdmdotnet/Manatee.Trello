namespace Manatee.Trello
{
	/// <summary>
	/// Represents the preferences for an organization.
	/// </summary>
	public interface IOrganizationPreferences
	{
		/// <summary>
		/// Gets or sets the general visibility of the organization.
		/// </summary>
		OrganizationPermissionLevel? PermissionLevel { get; set; }

		/// <summary>
		/// Gets or sets whether external members are disabled.
		/// </summary>
		/// <remarks>
		/// Still researching what this means.
		/// </remarks>
		// TODO: What does ExternalMembersDisabled do?
		bool? ExternalMembersDisabled { get; set; }

		/// <summary>
		/// Gets or sets a domain to associate with the organization.
		/// </summary>
		/// <remarks>
		/// Still researching what this means.
		/// </remarks>
		// TODO: What does AssociatedDomain do?
		string AssociatedDomain { get; set; }

		/// <summary>
		/// Gets or sets the visibility of public-viewable boards owned by the organizations.
		/// </summary>
		OrganizationBoardVisibility? PublicBoardVisibility { get; set; }

		/// <summary>
		/// Gets or sets the visibility of organization-viewable boards owned by the organization.
		/// </summary>
		OrganizationBoardVisibility? OrganizationBoardVisibility { get; set; }

		/// <summary>
		/// Gets or sets the visibility of private-viewable boards owned by the organization.
		/// </summary>
		OrganizationBoardVisibility? PrivateBoardVisibility { get; set; }
	}
}
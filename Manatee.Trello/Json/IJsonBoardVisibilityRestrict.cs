namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the BoardVisibilityRestrict object.
	/// </summary>
	public interface IJsonBoardVisibilityRestrict
	{
		/// <summary>
		/// Gets or sets the visibility of publicly-visible boards owned by the organization.
		/// </summary>
		[JsonDeserialize]
		OrganizationBoardVisibility? Public { get; set; }
		/// <summary>
		/// Gets or sets the visibility of Org-visible boards owned by the organization.
		/// </summary>
		[JsonDeserialize]
		OrganizationBoardVisibility? Org { get; set; }
		/// <summary>
		/// Gets or sets the visibility of private boards owned by the organization.
		/// </summary>
		[JsonDeserialize]
		OrganizationBoardVisibility? Private { get; set; }
	}
}
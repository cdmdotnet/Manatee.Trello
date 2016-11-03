namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the OrganizationMembership object.
	/// </summary>
	public interface IJsonOrganizationMembership : IJsonCacheable
	{
		///<summary>
		/// Gets or sets the ID of the member.
		///</summary>
		[JsonDeserialize]
		IJsonMember Member { get; set; }
		///<summary>
		/// Gets or sets the membership type.
		///</summary>
		[JsonDeserialize]
		[JsonSerialize(IsRequired = true)]
		OrganizationMembershipType? MemberType { get; set; }
		///<summary>
		/// Gets or sets whether the membership is unconfirmed.
		///</summary>
		[JsonDeserialize]
		bool? Unconfirmed { get; set; }
	}
}
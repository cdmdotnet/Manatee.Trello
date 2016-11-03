namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the BoardMembership object.
	/// </summary>
	public interface IJsonBoardMembership : IJsonCacheable
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
		BoardMembershipType? MemberType { get; set; }
		///<summary>
		/// Gets or sets whether the membership is deactivated.
		///</summary>
		[JsonDeserialize]
		bool? Deactivated { get; set; }
	}
}
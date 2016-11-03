using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for a member search.
	/// </summary>
	public interface IJsonMemberSearch
	{
		/// <summary>
		/// Gets or sets a list of members.
		/// </summary>
		[JsonDeserialize]
		List<IJsonMember> Members { get; set; }
		/// <summary>
		/// Gets or sets a board within which the search should run.
		/// </summary>
		[JsonDeserialize]
		IJsonBoard Board { get; set; }
		/// <summary>
		/// Gets or sets the number of results to return.
		/// </summary>
		[JsonSerialize]
		int? Limit { get; set; }
		/// <summary>
		/// Gets or sets whether only organization members should be returned.
		/// </summary>
		[JsonSerialize]
		bool? OnlyOrgMembers { get; set; }
		/// <summary>
		/// Gets or sets an organization within which the search should run.
		/// </summary>
		[JsonSerialize]
		IJsonOrganization Organization { get; set; }
		/// <summary>
		/// Gets or sets the search query.
		/// </summary>
		[JsonSerialize(IsRequired = true)]
		string Query { get; set; }
	}
}
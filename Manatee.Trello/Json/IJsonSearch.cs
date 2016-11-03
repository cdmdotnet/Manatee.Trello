using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Search object.
	/// </summary>
	public interface IJsonSearch
	{
		/// <summary>
		/// Lists the IDs of actions which match the query.
		/// </summary>
		[JsonDeserialize]
		List<IJsonAction> Actions { get; set; }
		/// <summary>
		/// Lists the IDs of boards which match the query.
		/// </summary>
		[JsonDeserialize]
		List<IJsonBoard> Boards { get; set; }
		/// <summary>
		/// Lists the IDs of cards which match the query.
		/// </summary>
		[JsonDeserialize]
		List<IJsonCard> Cards { get; set; }
		/// <summary>
		/// Lists the IDs of members which match the query.
		/// </summary>
		[JsonDeserialize]
		List<IJsonMember> Members { get; set; }
		/// <summary>
		/// Lists the IDs of organizations which match the query.
		/// </summary>
		[JsonDeserialize]
		List<IJsonOrganization> Organizations { get; set; }
		/// <summary>
		/// Gets or sets the search query.
		/// </summary>
		[JsonSerialize(IsRequired = true)]
		string Query { get; set; }
		/// <summary>
		/// Gets or sets a collection of boards, cards, and organizations within
		/// which the search should run.
		/// </summary>
		[JsonSerialize]
		List<IJsonCacheable> Context { get; set; }
		/// <summary>
		/// Gets or sets which types of objects should be returned.
		/// </summary>
		[JsonSerialize]
		SearchModelType? Types { get; set; }
		/// <summary>
		/// Gets or sets how many results to return;
		/// </summary>
		[JsonSerialize]
		int? Limit { get; set; }
		/// <summary>
		/// Gets or sets whether the search should match on partial words.
		/// </summary>
		[JsonSerialize]
		bool Partial { get; set; }
	}
}
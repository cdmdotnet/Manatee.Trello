namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines properties required for TrelloService to cache an item.
	/// </summary>
	public interface IJsonCacheable
	{
		/// <summary>
		/// Gets or sets a unique identifier (not necessarily a GUID).
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize(IsRequired = true)]
		string Id { get; set; }
	}

	public interface IAcceptId
	{
		bool ValidForMerge { get; set; }
	}
}
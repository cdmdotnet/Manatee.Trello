namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the List object.
	/// </summary>
	public interface IJsonList : IJsonCacheable
	{
		/// <summary>
		/// Gets or sets the name of the list.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Name { get; set; }
		/// <summary>
		/// Gets or sets whether the list is archived.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		bool? Closed { get; set; }
		/// <summary>
		/// Gets or sets the ID of the board which contains the list.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonBoard Board { get; set; }
		/// <summary>
		/// Gets or sets the position of the list.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonPosition Pos { get; set; }
		/// <summary>
		/// Gets or sets whether the current member is subscribed to the list.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		bool? Subscribed { get; set; }
	}
}
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the ActionOldData object.
	/// </summary>
	public interface IJsonNotificationOldData
	{
		/// <summary>
		/// Gets or sets an old description.
		/// </summary>
		[JsonDeserialize]
		string Desc { get; set; }
		/// <summary>
		/// Gets or sets an old list.
		/// </summary>
		[JsonDeserialize]
		IJsonList List { get; set; }
		/// <summary>
		/// Gets or sets an old position.
		/// </summary>
		[JsonDeserialize]
		double? Pos { get; set; }
		/// <summary>
		/// Gets or sets old text.
		/// </summary>
		[JsonDeserialize]
		string Text { get; set; }
		/// <summary>
		/// Gets or sets whether an item was closed.
		/// </summary>
		[JsonDeserialize]
		bool? Closed { get; set; }
	}
}
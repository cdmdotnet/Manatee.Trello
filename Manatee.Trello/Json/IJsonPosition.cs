namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Position object.
	/// </summary>
	public interface IJsonPosition
	{
		/// <summary>
		/// Gets or sets an explicit numeric value for the position.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		double? Explicit { get; set; }
		/// <summary>
		/// Gets or sets a named value for the position.
		/// </summary>
		/// <remarks>
		/// Valid values are "top" and "bottom".
		/// </remarks>
		[JsonDeserialize]
		[JsonSerialize]
		string Named { get; set; }
	}
}

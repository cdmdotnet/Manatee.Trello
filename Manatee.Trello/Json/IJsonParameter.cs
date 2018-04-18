namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for a single-value parameter.
	/// </summary>
	public interface IJsonParameter
	{
		/// <summary>
		/// Gets or sets a string parameter value.
		/// </summary>
		[JsonSerialize]
		string String { get; set; }
		/// <summary>
		/// Gets or sets a boolean parameter value.
		/// </summary>
		[JsonSerialize]
		bool? Boolean { get; set; }
		/// <summary>
		/// Gets or sets an object parameter value.
		/// </summary>
		[JsonSerialize]
		object Object { get; set; }
	}
}
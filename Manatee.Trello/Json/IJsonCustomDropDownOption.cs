namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for a custom field drop down option.
	/// </summary>
	public interface IJsonCustomDropDownOption : IJsonCacheable, IAcceptId
	{
		/// <summary>
		/// Gets or sets the custom field definition.
		/// </summary>
		[JsonDeserialize]
		IJsonCustomFieldDefinition Field { get; set; }
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Text { get; set; }
		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		LabelColor? Color { get; set; }
		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonPosition Pos { get; set; }
	}
}
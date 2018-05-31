namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for information regarding the display of custom fields.
	/// </summary>
	public interface IJsonCustomFieldDisplayInfo
	{
		/// <summary>
		/// Gets or sets whether the field should be displayed on the card front.
		/// </summary>
		bool? CardFront { get; set; }
	}
}
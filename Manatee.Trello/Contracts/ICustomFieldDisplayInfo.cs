namespace Manatee.Trello
{
	/// <summary>
	/// Represents the display information for a custom field.
	/// </summary>
	public interface ICustomFieldDisplayInfo
	{
		/// <summary>
		/// Gets or sets whether the custom field will appear on the front of the card.
		/// </summary>
		bool? CardFront { get; set; }
	}
}
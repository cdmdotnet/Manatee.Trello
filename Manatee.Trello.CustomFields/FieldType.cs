namespace Manatee.Trello.CustomFields
{
	/// <summary>
	/// Defines intended data types for custom fields.  Custom field data is always string-encoded.
	/// </summary>
	public enum FieldType
	{
		/// <summary>
		/// Specifies an unknown type.
		/// </summary>
		Unknown = -1,
		/// <summary>
		/// Specifies that data is a string.
		/// </summary>
		Text,
		/// <summary>
		/// Specifies that data is a number.
		/// </summary>
		Number,
		/// <summary>
		/// Specifies that data is a checkbox (boolean).
		/// </summary>
		Checkbox,
		/// <summary>
		/// Specifies that data is a date.
		/// </summary>
		Date,
		/// <summary>
		/// Specifies that data is one of a collection of predefined text values.
		/// </summary>
		Dropdown
	}
}
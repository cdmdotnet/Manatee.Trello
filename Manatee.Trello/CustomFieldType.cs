using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the types of custom fields available.
	/// </summary>
	public enum CustomFieldType
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates the field data is string text.
		/// </summary>
		[Display(Description = "text")]
		Text,
		/// <summary>
		/// Indicates the field data is one of several selectable options.
		/// </summary>
		[Display(Description = "list")]
		DropDown,
		/// <summary>
		/// Indicates the field data is a boolean value.
		/// </summary>
		[Display(Description = "checkbox")]
		CheckBox,
		/// <summary>
		/// Indicates the field data is a date/time value.
		/// </summary>
		[Display(Description = "date")]
		DateTime,
		/// <summary>
		/// Indicates the field data is numeric.
		/// </summary>
		[Display(Description = "number")]
		Number
	}
}
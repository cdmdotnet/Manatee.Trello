using System;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for a custom field instance.
	/// </summary>
	public interface IJsonCustomField : IJsonCacheable, IAcceptId
	{
		/// <summary>
		/// Gets or sets the custom field definition.
		/// </summary>
		[JsonDeserialize]
		IJsonCustomFieldDefinition Definition { get; set; }
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		[JsonSpecialSerialization]
		string Text { get; set; }
		/// <summary>
		/// Gets or sets the number.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		[JsonSpecialSerialization]
		double? Number { get; set; }
		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		[JsonSpecialSerialization]
		DateTime? Date { get; set; }
		/// <summary>
		/// Gets or sets the checked.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		[JsonSpecialSerialization]
		bool? Checked { get; set; }
		/// <summary>
		/// Gets or sets the selected drop down option.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		[JsonSpecialSerialization]
		IJsonCustomDropDownOption Selected { get; set; }
		/// <summary>
		/// Gets or sets the data type.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		CustomFieldType Type { get; set; }
		/// <summary>
		/// Used to signal the value should be cleared.
		/// </summary>
		bool UseForClear { get; set; }
	}
}
using System.Collections.Generic;

namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for a custom field definition.
	/// </summary>
	public interface IJsonCustomFieldDefinition : IJsonCacheable, IAcceptId
	{
		/// <summary>
		/// Gets or sets the board.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonBoard Board { get; set; }
		/// <summary>
		/// Gets or sets the field group.
		/// </summary>
		[JsonDeserialize]
		string FieldGroup { get; set; }
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonPosition Pos { get; set; }
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		CustomFieldType? Type { get; set; }
		/// <summary>
		/// Gets or sets any drop down options.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		List<IJsonCustomDropDownOption> Options { get; set; }
		/// <summary>
		/// Gets or sets the display information.
		/// </summary>
		[JsonDeserialize]
		[JsonSerialize]
		IJsonCustomFieldDisplayInfo Display { get; set; }
	}
}
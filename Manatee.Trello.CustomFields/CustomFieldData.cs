using System;

namespace Manatee.Trello.CustomFields
{
	/// <summary>
	/// Models a single custom field on a card.
	/// </summary>
	[Obsolete("Custom fields have been integrated into the main Manatee.Trello library as of version 2.4.")]
	public class CustomFieldData
	{
		internal string Id { get; set; }
		/// <summary>
		/// Gets the name of the field.
		/// </summary>
		public string Name { get; internal set; }
		/// <summary>
		/// Gets the intended data type of the field.
		/// </summary>
		public FieldType Type { get; internal set; }
		/// <summary>
		/// Gets the value in the field.  Check the <see cref="Type"/> property for the intended data type.
		/// </summary>
		/// <remarks>
		/// Values for drop-down lists will be the index in the list, not the textual value.  To get the text
		/// value, check the <see cref="CustomFieldDefinition"/> for the options.
		/// </remarks>
		public string Value { get; internal set; }

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return $"{Name}: {Value} ({Type})";
		}
	}
}
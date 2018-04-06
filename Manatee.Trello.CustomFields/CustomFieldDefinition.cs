using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.CustomFields
{
	/// <summary>
	/// Defines meta-data for a single custom field.
	/// </summary>
	[Obsolete("Custom fields have been integrated into the main Manatee.Trello library as of version 2.4.")]
	public class CustomFieldDefinition : IJsonSerializable
	{
		internal string Id { get; private set; }
		/// <summary>
		/// Gets the name of the field.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Gets the intended data type of the field.  Data will be string-encoded.
		/// </summary>
		public FieldType Type { get; private set; }
		/// <summary>
		/// Gets whether the field is configured to appear on the card cover as a badge.
		/// </summary>
		public bool ShowBadge { get; private set; }
		/// <summary>
		/// Gets the drop-down options.  Only available for drop-down field types.
		/// </summary>
		public IEnumerable<string> DropdownOptions { get; private set; }

		/// <summary>
		/// Builds an object from a <see cref="T:Manatee.Json.JsonValue" />.
		/// </summary>
		/// <param name="json">The <see cref="T:Manatee.Json.JsonValue" /> representation of the object.</param>
		/// <param name="serializer">The <see cref="T:Manatee.Json.Serialization.JsonSerializer" /> instance to use for additional
		/// serialization of values.</param>
		void IJsonSerializable.FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Name = obj.TryGetString("n");
			Type = (FieldType?) obj.TryGetNumber("t") ?? FieldType.Unknown;
			ShowBadge = (int?) obj.TryGetNumber("b") == 1;
			DropdownOptions = obj.TryGetArray("o")?.Select(o => o.String);
		}
		/// <summary>
		/// Converts an object to a <see cref="T:Manatee.Json.JsonValue" />.
		/// </summary>
		/// <param name="serializer">The <see cref="T:Manatee.Json.Serialization.JsonSerializer" /> instance to use for additional
		/// serialization of values.</param>
		/// <returns>The <see cref="T:Manatee.Json.JsonValue" /> representation of the object.</returns>
		JsonValue IJsonSerializable.ToJson(JsonSerializer serializer)
		{
			throw new System.NotImplementedException();
		}
	}
}
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.CustomFields
{
	/// <summary>
	/// Models meta data for Custom Fields defined on a board.
	/// </summary>
	public class CustomFieldsSettings : IJsonSerializable
	{
		/// <summary>
		/// Gets the text that appears on the power-up button inside a card.
		/// </summary>
		public string ButtonText { get; private set; }
		/// <summary>
		/// Gets the field definitions.
		/// </summary>
		public IEnumerable<CustomFieldDefinition> Fields { get; private set; }

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
			ButtonText = obj.TryGetString("btn");
			Fields = serializer.Deserialize<List<CustomFieldDefinition>>(obj.TryGetArray("fields"));
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
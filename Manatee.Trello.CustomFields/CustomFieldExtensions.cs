using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.CustomFields
{
	/// <summary>
	/// Extensions to retrieve Custom Fields data.
	/// </summary>
	public static class CustomFieldExtensions
	{
		private static readonly JsonSerializer Serializer = new JsonSerializer();

		/// <summary>
		/// Gets meta-data about the custom fields.
		/// </summary>
		/// <param name="board">The board that defines the fields.</param>
		/// <returns>The custom field settings.</returns>
		public static CustomFieldsSettings CustomFieldsSettings(this Board board)
		{
			CustomFieldsPowerUp.Register();
			var data = board.PowerUpData.FirstOrDefault(d => d.PluginId == CustomFieldsPowerUp.PluginId);
			if (data == null) return null;

			var json = JsonValue.Parse(data.Value);
			var settings = Serializer.Deserialize<CustomFieldsSettings>(json);
			return settings;
		}

		/// <summary>
		/// Gets custom field data for a card.
		/// </summary>
		/// <param name="card">The card.</param>
		/// <returns>All custom fields defined for the card.</returns>
		public static IEnumerable<CustomFieldData> CustomFields(this Card card)
		{
			CustomFieldsPowerUp.Register();
			var data = card.PowerUpData.FirstOrDefault(d => d.PluginId == CustomFieldsPowerUp.PluginId);
			if (data == null) return null;

			// This will return null if the power-up isn't registered.
			var powerUp = card.Board.TryGetPowerUp();
			if (powerUp == null) return null;

			var json = JsonValue.Parse(data.Value);
			var fieldData = json.Object.TryGetObject("fields").Select(d => new CustomFieldData
				                                                          {
					                                                          Id = d.Key,
					                                                          Value = d.Value.Type == JsonValueType.String
						                                                                  ? d.Value.String
						                                                                  : d.Value.ToString()
				                                                          });
			var fieldSettings = card.Board.CustomFieldsSettings();

			fieldData = fieldSettings.Fields.Join(fieldData,
			                                      f => f.Id,
			                                      d => d.Id,
			                                      (f, d) =>
				                                      {
					                                      d.Name = f.Name;
					                                      d.Type = f.Type;
					                                      return d;
				                                      });

			return fieldData;
		}
	}
}
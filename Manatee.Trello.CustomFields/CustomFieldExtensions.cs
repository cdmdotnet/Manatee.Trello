using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.CustomFields
{
	public static class CustomFieldExtensions
	{
		private static readonly JsonSerializer Serializer = new JsonSerializer();

		public static CustomFieldsSettings CustomFieldsSettings(this Board board)
		{
			var data = board.PowerUpData.FirstOrDefault(d => d.PluginId == CustomFieldsPowerUp.PluginId);
			if (data == null) return null;

			var json = JsonValue.Parse(data.Value);
			var settings = Serializer.Deserialize<CustomFieldsSettings>(json);
			return settings;
		}

		public static IEnumerable<CustomFieldData> CustomFields(this Card card)
		{
			var data = card.PowerUpData.FirstOrDefault(d => d.PluginId == CustomFieldsPowerUp.PluginId);
			if (data == null) return null;

			var powerUp = card.Board.TryGetPowerUp();
			// The data check should catch this, but that's dependent upon Trello's data being right...
			if (powerUp == null) return null;

			var json = JsonValue.Parse(data.Value);
			var fieldData = json.Object.TryGetObject("fields")?.Select(d => new CustomFieldData
				                                                           {
					                                                           Id = d.Key,
					                                                           Value = d.Value.String
				                                                           });
			var fieldSettings = card.Board.CustomFieldsSettings();

			// We just want the side effect from this join.
			// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
			fieldData = fieldSettings.Fields.Join(fieldData,
			                                      f => f.Id,
			                                      d => d.Id,
			                                      (f, d) =>
				                                      {
					                                      d.Name = f.Name;
					                                      return d;
				                                      });

			return fieldData;
		}
	}
}
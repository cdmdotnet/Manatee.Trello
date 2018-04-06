using System;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeCustomField : IJsonCustomField, IJsonSerializable
	{
		public string Id { get; set; }
		public IJsonCustomFieldDefinition Definition { get; set; }
		public string Text { get; set; }
		public double? Number { get; set; }
		public DateTime? Date { get; set; }
		public bool? Checked { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("id");
					Definition = obj.Deserialize<IJsonCustomFieldDefinition>(serializer, "idCustomField");
					var value = obj.TryGetObject("value");
					if (value != null)
					{
						Text = value.TryGetString("text");
						var numberString = value.TryGetString("number");
						Number = numberString != null && !double.TryParse(numberString, out var number)
							         ? number
							         : (double?) null;
						var boolString = value.TryGetString("checked");
						Checked = boolString != null && !bool.TryParse(boolString, out var boolean)
							          ? boolean
							          : (bool?) null;
						var dateString = value.TryGetString("number");
						Date = dateString != null && !DateTime.TryParse(dateString, out var date)
							       ? date
							       : (DateTime?) null;
					}
					break;
				case JsonValueType.String:
					Id = json.String;
					break;
			}
		}

		public JsonValue ToJson(JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
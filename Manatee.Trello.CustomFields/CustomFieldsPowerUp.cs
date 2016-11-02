using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello.CustomFields
{
	public class CustomFieldsPowerUp : PowerUpBase
	{
		public static string PluginId = "56d5e249a98895a9797bebb9";

		public CustomFieldsPowerUp(IJsonPowerUp json, TrelloAuthorization auth)
			: base(json, auth) {}
	}

	public static class CustomFieldExtensions
	{
		public static Dictionary<string, string> CustomFields(this Card card)
		{
			var data = card.PowerUpData.FirstOrDefault(d => d.PluginId == CustomFieldsPowerUp.PluginId);
			if (data == null) return null;

			var json = JsonValue.Parse(data.Value);
			return json.Object["fields"].Object.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.String);
		}
	}
}

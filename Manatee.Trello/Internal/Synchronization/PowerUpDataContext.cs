using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class PowerUpDataContext : SynchronizationContext<IJsonPowerUpData>
	{
		static PowerUpDataContext()
		{
			_properties = new Dictionary<string, Property<IJsonPowerUpData>>
				{
					{"Id", new Property<IJsonPowerUpData, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"PluginId", new Property<IJsonPowerUpData, string>((d, a) => d.PluginId, (d, o) => d.PluginId = o)},
					{"Value", new Property<IJsonPowerUpData, string>((d, a) => d.Value, (d, o) => d.Value = o)},
				};
		}
		public PowerUpDataContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
		}
	}
}

using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	// TODO: This class should be refreshable.
	internal class PowerUpDataContext : SynchronizationContext<IJsonPowerUpData>
	{
		static PowerUpDataContext()
		{
			Properties = new Dictionary<string, Property<IJsonPowerUpData>>
				{
					{
						nameof(PowerUpData.Id),
						new Property<IJsonPowerUpData, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(PowerUpData.PluginId),
						new Property<IJsonPowerUpData, string>((d, a) => d.PluginId, (d, o) => d.PluginId = o)
					},
					{
						nameof(PowerUpData.Value),
						new Property<IJsonPowerUpData, string>((d, a) => d.Value, (d, o) => d.Value = o)
					},
				};
		}
		public PowerUpDataContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
		}
	}
}

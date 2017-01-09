using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class PowerUpContext : SynchronizationContext<IJsonPowerUp>
	{
		static PowerUpContext()
		{
			_properties = new Dictionary<string, Property<IJsonPowerUp>>
				{
					{"Id", new Property<IJsonPowerUp, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"Name", new Property<IJsonPowerUp, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					{"Public", new Property<IJsonPowerUp, bool?>((d, a) => d.Public, (d, o) => d.Public = o)},
					{"AdditionalInfo", new Property<IJsonPowerUp, string>((d, a) => d.Url, (d, o) => d.Url = o)},
				};
		}
		public PowerUpContext(IJsonPowerUp json, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = json.Id;

			Merge(json);
		}
	}
}
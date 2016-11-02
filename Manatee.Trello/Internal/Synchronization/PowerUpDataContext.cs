using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
					//{"Name", new Property<IJsonPowerUpData, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					//{"Public", new Property<IJsonPowerUpData, bool?>((d, a) => d.Public, (d, o) => d.Public = o)},
					//{"AdditionalInfo", new Property<IJsonPowerUpData, string>((d, a) => d.Url, (d, o) => d.Url = o)},
				};
		}
		public PowerUpDataContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
		}
	}
}

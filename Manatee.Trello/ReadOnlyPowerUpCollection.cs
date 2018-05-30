using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of power-ups.
	/// </summary>
	/// <remarks>
	/// If a power-up hasn't been registered, it will be instantiated using <see cref="UnknownPowerUp"/>.
	/// </remarks>
	public class ReadOnlyPowerUpCollection : ReadOnlyCollection<IPowerUp>
	{
		internal ReadOnlyPowerUpCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth) {}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_PowerUps, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonPowerUp>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jn =>
				{
					var powerUp = jn.GetFromCache<IPowerUp>(Auth);
					if (powerUp is PowerUpBase contexted)
						contexted.Json = jn;
					return powerUp;
				}));
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of power-up data.
	/// </summary>
	public class ReadOnlyPowerUpDataCollection : ReadOnlyCollection<IPowerUpData>
	{
		private readonly EntityRequestType _requestType;

		internal ReadOnlyPowerUpDataCollection(EntityRequestType requestType, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_requestType = requestType;
		}
		internal ReadOnlyPowerUpDataCollection(ReadOnlyPowerUpDataCollection source, TrelloAuthorization auth)
			: this(source._requestType, () => source.OwnerId, auth) {}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		public sealed override async Task Refresh()
		{
			var endpoint = EndpointFactory.Build(_requestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonPowerUpData>>(Auth, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(jn =>
				{
					var powerUp = jn.GetFromCache<PowerUpData>(Auth);
					powerUp.Json = jn;
					return powerUp;
				}));
		}
	}
}
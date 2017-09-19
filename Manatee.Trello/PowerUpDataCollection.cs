using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of actions.
	/// </summary>
	public class ReadOnlyPowerUpDataCollection : ReadOnlyCollection<PowerUpData>
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
		protected override void Update()
		{
			var endpoint = EndpointFactory.Build(_requestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonPowerUpData>>(Auth, endpoint);

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
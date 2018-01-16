using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of power-ups.
	/// </summary>
	public class ReadOnlyPowerUpCollection : ReadOnlyCollection<IPowerUp>
	{
		[Obsolete("This constructor is only for mocking purposes.")]
		public ReadOnlyPowerUpCollection(ReadOnlyPowerUpCollection doNotUse)
			: base(() => string.Empty, null)
		{
		}
		internal ReadOnlyPowerUpCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth) {}
		internal ReadOnlyPowerUpCollection(ReadOnlyPowerUpCollection source, TrelloAuthorization auth)
			: this(() => source.OwnerId, auth) {}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected override void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_PowerUps, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonPowerUp>>(Auth, endpoint);

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
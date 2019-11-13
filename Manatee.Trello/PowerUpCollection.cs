using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of power-ups.
	/// </summary>
	public class PowerUpCollection : ReadOnlyPowerUpCollection, IPowerUpCollection
	{
		internal PowerUpCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}

		/// <summary>
		/// Enables a power-up for a board.
		/// </summary>
		/// <param name="powerUp">The power-up to enable.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public async Task EnablePowerUp(IPowerUp powerUp, CancellationToken ct = default)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonPowerUp>();
			json.Id = powerUp.Id;

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_EnablePowerUp,
			                                     new Dictionary<string, object>
				                                     {
					                                     {"_id", OwnerId}
				                                     });
			await JsonRepository.Execute(Auth, endpoint, json, ct);
		}

		/// <summary>
		/// Disables a power-up for a board.
		/// </summary>
		/// <param name="powerUp">The power-up to disble.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public async Task DisablePowerUp(IPowerUp powerUp, CancellationToken ct = default)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_DisablePowerUp,
			                                     new Dictionary<string, object>
				                                     {
					                                     {"_boardId", OwnerId},
					                                     {"_id", OwnerId}
				                                     });
			await JsonRepository.Execute(Auth, endpoint, ct, AdditionalParameters);
		}
	}
}
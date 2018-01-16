using System;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides an implementation for an unknown power-up.
	/// </summary>
	public class UnknownPowerUp : PowerUpBase
	{
		[Obsolete("This constructor is only for mocking purposes.")]
		public UnknownPowerUp(UnknownPowerUp doNotUse)
			: base(TrelloConfiguration.JsonFactory.Create<IJsonPowerUp>(), null)
		{
		}
		internal UnknownPowerUp(IJsonPowerUp json, TrelloAuthorization auth)
			: base(json, auth) {}
	}
}
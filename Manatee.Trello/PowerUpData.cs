using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class PowerUpData : ICacheable
	{
		private readonly PowerUpDataContext _context;

		public string Id { get; }

		internal IJsonPowerUpData Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		internal PowerUpData(IJsonPowerUpData json, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new PowerUpDataContext(Id, auth);
		}
	}
}

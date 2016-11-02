using System.Collections.Generic;
using System.Linq;

namespace Manatee.Trello.CustomFields
{
	internal static class PowerUpCache
	{
		private static readonly Dictionary<string, CustomFieldsPowerUp> Cache = new Dictionary<string, CustomFieldsPowerUp>();

		public static CustomFieldsPowerUp TryGetPowerUp(this Board board)
		{
			CustomFieldsPowerUp powerUp;
			if (!Cache.TryGetValue(board.Id, out powerUp))
			{
				powerUp = board.PowerUps.OfType<CustomFieldsPowerUp>().FirstOrDefault();
				if (powerUp == null) return null;

				Cache[board.Id] = powerUp;
			}
			return powerUp;
		}
	}
}
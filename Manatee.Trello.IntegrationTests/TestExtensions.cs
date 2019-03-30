using System.Linq;
using System.Threading.Tasks;

namespace Manatee.Trello.IntegrationTests
{
	public static class TestExtensions
	{
		public static async Task EnsurePowerUp(this IBoard board, IPowerUp powerUp)
		{
			await board.PowerUps.Refresh(true);
			if (board.PowerUps.Any(p => p.Id == powerUp.Id)) return;

			await board.PowerUps.EnablePowerUp(powerUp);
			await board.PowerUps.Refresh();
		}
	}
}
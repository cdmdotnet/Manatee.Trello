using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of power-ups.
	/// </summary>
	public interface IPowerUpCollection : IReadOnlyCollection<IPowerUp>
	{
		/// <summary>
		/// Enables a power-up for a board.
		/// </summary>
		/// <param name="powerUp">The power-up to enable.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task EnablePowerUp(IPowerUp powerUp, CancellationToken ct = default(CancellationToken));
		/// <summary>
		/// Disables a power-up for a board.
		/// </summary>
		/// <param name="powerUp">The power-up to disble.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task DisablePowerUp(IPowerUp powerUp, CancellationToken ct = default(CancellationToken));
	}
}
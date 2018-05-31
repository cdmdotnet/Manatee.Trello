using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the Voting power-up.
	/// </summary>
	public sealed class VotingPowerUp : IPowerUp
	{
		/// <summary>
		/// Gets an ID on which matching can be performed.
		/// </summary>
		public string Id => "55a5d917446f517774210013";

		/// <summary>
		/// Gets the power-up name.
		/// </summary>
		public string Name => "Voting";

		/// <summary>
		/// Gets whether the power-up is public. (Really, I don't know what this is, and Trello's not talking.)
		/// </summary>
		public bool? IsPublic => true;

		/// <summary>
		/// Refreshes the power-up data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
#if NET45
			return Task.Run(() => { }, ct);
#else
			return Task.CompletedTask;
#endif
		}
	}
}
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Defines the basis of a power-up.
	/// </summary>
	public interface IPowerUp : ICacheable
	{
		/// <summary>
		/// Gets the power-up name.
		/// </summary>
		string Name { get; }
		/// <summary>
		/// Gets whether the power-up is public. (Really, I don't know what this is, and Trello's not talking.)
		/// </summary>
		bool? IsPublic { get; }

		/// <summary>
		/// Refreshes the power-up data.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Refresh(CancellationToken ct = default(CancellationToken));
	}
}

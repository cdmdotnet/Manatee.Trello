using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the data associated with a power-up.
	/// </summary>
	public interface IPowerUpData : ICacheable
	{
		/// <summary>
		/// Gets the ID for the plugin with which this data is associated.
		/// </summary>
		string PluginId { get; }

		/// <summary>
		/// Gets the data as a string.  This data will be JSON-encoded.
		/// </summary>
		string Value { get; }

		/// <summary>
		/// Refreshes the power-up data... data.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Refresh(CancellationToken ct = default(CancellationToken));
	}
}
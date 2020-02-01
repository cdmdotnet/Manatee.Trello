using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of board backgrounds.
	/// </summary>
	public interface IBoardBackgroundCollection : IReadOnlyCollection<IBoardBackground>
	{
                /// <summary>
                /// Adds a custom board background.
                /// </summary>
                /// <param name="data">The byte data of the file to attach.</param>
                /// <param name="ct">(Optional) A cancellation token for async processing.</param>
                /// <returns>The newly created <see cref="IBoardBackground"/>.</returns>
                Task<IBoardBackground> Add(byte[] data, CancellationToken ct = default);

                /// <summary>
                /// Adds a custom board background.
                /// </summary>
                /// <param name="path">The path of the file to attach.</param>
                /// <param name="ct">(Optional) A cancellation token for async processing.</param>
                /// <returns>The newly created <see cref="IBoardBackground"/>.</returns>
                Task<IBoardBackground> Add(string path, CancellationToken ct = default);

    }
}
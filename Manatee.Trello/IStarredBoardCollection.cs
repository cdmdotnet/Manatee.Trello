using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	public interface IStarredBoardCollection : IReadOnlyCollection<IStarredBoard>
	{
		Task<IStarredBoard> Add(IBoard board, Position position = null, CancellationToken ct = default(CancellationToken));
	}
}
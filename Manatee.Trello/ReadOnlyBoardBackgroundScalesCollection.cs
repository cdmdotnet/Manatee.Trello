using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.Synchronization;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of scaled versions of board backgrounds.
	/// </summary>
	public class ReadOnlyBoardBackgroundScalesCollection : ReadOnlyCollection<IImagePreview>
	{
		private readonly BoardBackgroundContext _context;

		internal ReadOnlyBoardBackgroundScalesCollection(BoardBackgroundContext context, TrelloAuthorization auth)
			: base(() => context.Data.Id, auth)
		{
			_context = context;
		}

		/// <summary>
		/// Manually updates the collection's data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public sealed override async Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
			await _context.Synchronize(ct);
			if (_context.Data.ImageScaled == null) return;
			Items.Clear();
			foreach (var jsonPreview in _context.Data.ImageScaled)
			{
				var preview = jsonPreview.GetFromCache<ImagePreview>(Auth);
				Items.Add(preview);
			}
		}
	}
}
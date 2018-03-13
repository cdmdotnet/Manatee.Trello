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
		/// Implement to provide data to the collection.
		/// </summary>
		protected sealed override void Update()
		{
			_context.Synchronize();
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
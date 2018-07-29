using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.Eventing;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of checklist items.
	/// </summary>
	public class ReadOnlyCheckItemCollection : ReadOnlyCollection<ICheckItem>,
	                                           IReadOnlyCheckItemCollection,
	                                           IHandle<EntityUpdatedEvent<IJsonCheckItem>>,
	                                           IHandle<EntityDeletedEvent<IJsonCheckItem>>
	{
		private readonly CheckListContext _context;

		/// <summary>
		/// Retrieves a check list item which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching check list item, or null if none found.</returns>
		/// <remarks>
		/// Matches on check item ID and name.  Comparison is case-sensitive.
		/// </remarks>
		public ICheckItem this[string key] => GetByKey(key);

		internal ReadOnlyCheckItemCollection(CheckListContext context, TrelloAuthorization auth)
			: base(() => context.Data.Id, auth)
		{
			_context = context;

			EventAggregator.Subscribe(this);
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			await _context.Synchronize(force, ct);
			if (_context.Data.CheckItems == null) return;
			foreach (var jsonCheckItem in _context.Data.CheckItems)
			{
				var checkItem = Items.SingleOrDefault(ci => ci.Id == jsonCheckItem.Id);
				if (checkItem == null)
					Items.Add(new CheckItem(jsonCheckItem, _context.Data.Id));
				else
					((CheckItem) checkItem).Json = jsonCheckItem;
			}

			foreach (var checkItem in Items.ToList())
			{
				if (_context.Data.CheckItems.All(jci => jci.Id != checkItem.Id))
					Items.Remove(checkItem);
			}
		}

		private ICheckItem GetByKey(string key)
		{
			return this.FirstOrDefault(ci => key.In(ci.Id, ci.Name));
		}

		void IHandle<EntityUpdatedEvent<IJsonCheckItem>>.Handle(EntityUpdatedEvent<IJsonCheckItem> message)
		{
			if (!message.Properties.Contains(nameof(CheckItem.CheckList))) return;
			var checkItem = Items.FirstOrDefault(b => b.Id == message.Data.Id);
			if (message.Data.CheckList?.Id != OwnerId && checkItem != null)
				Items.Remove(checkItem);
			else if (message.Data.CheckList?.Id == OwnerId && checkItem == null)
				Items.Add(message.Data.GetFromCache<CheckItem>(Auth));
		}

		void IHandle<EntityDeletedEvent<IJsonCheckItem>>.Handle(EntityDeletedEvent<IJsonCheckItem> message)
		{
			var item = Items.FirstOrDefault(c => c.Id == message.Data.Id);
			Items.Remove(item);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Eventing;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of lists.
	/// </summary>
	public class ReadOnlyListCollection : ReadOnlyCollection<IList>,
	                                      IReadOnlyListCollection,
	                                      IHandle<EntityUpdatedEvent<IJsonList>>
	{
		/// <summary>
		/// Retrieves a list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching list, or null if none found.</returns>
		/// <remarks>
		/// Matches on list ID and name.  Comparison is case-sensitive.
		/// </remarks>
		public IList this[string key] => GetByKey(key);

		internal ReadOnlyListCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			EventAggregator.Subscribe(this);
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		public void Filter(ListFilter filter)
		{
			AdditionalParameters["filter"] = filter.GetDescription();
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			IncorporateLimit();

			var allParameters = AdditionalParameters.Concat(ListContext.CurrentParameters)
			                                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_Lists, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonList>>(Auth, endpoint, ct, allParameters);

			Items.Clear();
			EventAggregator.Unsubscribe(this);
			Items.AddRange(newData.Select(jl =>
				{
					var list = jl.GetFromCache<List, IJsonList>(Auth);
					list.Json = jl;
					return list;
				}));
			EventAggregator.Subscribe(this);
		}

		private IList GetByKey(string key)
		{
			return this.FirstOrDefault(l => key.In(l.Id, l.Name));
		}

		void IHandle<EntityUpdatedEvent<IJsonList>>.Handle(EntityUpdatedEvent<IJsonList> message)
		{
			if (!message.Properties.Contains(nameof(List.Board))) return;
			var list = Items.FirstOrDefault(l => l.Id == message.Data.Id);
			if (message.Data.Board?.Id != OwnerId && list != null)
				Items.Remove(list);
			else if (message.Data.Board?.Id == OwnerId && list == null)
				Items.Add(message.Data.GetFromCache<List>(Auth));
		}
	}
}
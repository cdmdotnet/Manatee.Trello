using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of lists.
	/// </summary>
	public class ReadOnlyListCollection : ReadOnlyCollection<IList>, IReadOnlyListCollection
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

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_Lists, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonList>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jl =>
				{
					var list = jl.GetFromCache<List, IJsonList>(Auth);
					list.Json = jl;
					return list;
				}));
		}

		private IList GetByKey(string key)
		{
			return this.FirstOrDefault(l => key.In(l.Id, l.Name));
		}
	}
}
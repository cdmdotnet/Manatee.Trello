using System;
using System.Collections.Generic;
using System.Linq;
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
		private Dictionary<string, object> _additionalParameters;

		/// <summary>
		/// Retrieves a list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching list, or null if none found.</returns>
		/// <remarks>
		/// Matches on <see cref="List.Id"/> and <see cref="List.Name"/>.  Comparison is case-sensitive.
		/// </remarks>
		public IList this[string key] => GetByKey(key);

		internal ReadOnlyListCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth) {}
		internal ReadOnlyListCollection(ReadOnlyListCollection source, TrelloAuthorization auth)
			: this(() => source.OwnerId, auth)
		{
			if (source._additionalParameters != null)
				_additionalParameters = new Dictionary<string, object>(source._additionalParameters);
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		public void Filter(ListFilter filter)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object>();
			_additionalParameters["filter"] = filter.GetDescription();
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected sealed override void Update()
		{
			IncorporateLimit(_additionalParameters);

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_Lists, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonList>>(Auth, endpoint, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jl =>
				{
					var list = CachingObjectFactory.GetFromCache<List>(jl, Auth);
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
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
	/// A read-only collection of cards.
	/// </summary>
	public class ReadOnlyCardCollection : ReadOnlyCollection<ICard>, IReadOnlyCardCollection
	{
		private readonly EntityRequestType _updateRequestType;
		private readonly Dictionary<string, object> _requestParameters;
		private Dictionary<string, object> _additionalParameters;

		/// <summary>
		/// Retrieves a card which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching card, or null if none found.</returns>
		/// <remarks>
		/// Matches on <see cref="ICard.Id"/> and <see cref="ICard.Name"/>.  Comparison is case-sensitive.
		/// </remarks>
		public ICard this[string key] => GetByKey(key);

		internal ReadOnlyCardCollection(Type type, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_updateRequestType = type == typeof (List)
				                     ? EntityRequestType.List_Read_Cards
				                     : EntityRequestType.Board_Read_Cards;
			_requestParameters = new Dictionary<string, object>();
		}
		internal ReadOnlyCardCollection(EntityRequestType requestType, Func<string> getOwnerId, TrelloAuthorization auth, Dictionary<string, object> requestParameters = null)
			: base(getOwnerId, auth)
		{
			_updateRequestType = requestType;
			_requestParameters = requestParameters ?? new Dictionary<string, object>();
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		public void Filter(CardFilter filter)
		{
			// NOTE: See issue 109.  /1/lists/{listId}/cards does not support filter=visible
			if (_updateRequestType == EntityRequestType.List_Read_Cards && filter == CardFilter.Visible)
			{
				_additionalParameters?.Remove("filter");
				return;
			}

			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object>();
			_additionalParameters["filter"] = filter.GetDescription();
		}

		/// <summary>
		/// Adds a filter to the collection based on start and end date.
		/// </summary>
		/// <param name="start">The start date.</param>
		/// <param name="end">The end date.</param>
		public void Filter(DateTime? start, DateTime? end)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object>();
			if (start != null)
				_additionalParameters["since"] = start;
			if (end != null)
				_additionalParameters["before"] = end;
		}

		/// <summary>
		/// Manually updates the collection's data.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public sealed override async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			IncorporateLimit(_additionalParameters);

			_requestParameters["_id"] = OwnerId;
			var endpoint = EndpointFactory.Build(_updateRequestType, _requestParameters);
			var newData = await JsonRepository.Execute<List<IJsonCard>>(Auth, endpoint, ct, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jc =>
				{
					var card = jc.GetFromCache<Card, IJsonCard>(Auth);
					card.Json = jc;
					return card;
				}));
		}

		private ICard GetByKey(string key)
		{
			return this.FirstOrDefault(c => key.In(c.Id, c.Name));
		}
	}
}
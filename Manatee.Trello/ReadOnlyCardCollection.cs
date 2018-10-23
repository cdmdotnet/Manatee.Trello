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
	/// A read-only collection of cards.
	/// </summary>
	public class ReadOnlyCardCollection : ReadOnlyCollection<ICard>,
	                                      IReadOnlyCardCollection,
	                                      IHandle<EntityUpdatedEvent<IJsonCard>>,
	                                      IHandle<EntityDeletedEvent<IJsonCard>>
	{
		private readonly EntityRequestType _updateRequestType;
		private readonly Dictionary<string, object> _requestParameters;

		/// <summary>
		/// Retrieves a card which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching card, or null if none found.</returns>
		/// <remarks>
		/// Matches on card ID and name.  Comparison is case-sensitive.
		/// </remarks>
		public ICard this[string key] => GetByKey(key);

		internal ReadOnlyCardCollection(Type type, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_updateRequestType = type == typeof(List)
				                     ? EntityRequestType.List_Read_Cards
				                     : EntityRequestType.Board_Read_Cards;
			_requestParameters = new Dictionary<string, object>();

			EventAggregator.Subscribe(this);
		}

		internal ReadOnlyCardCollection(EntityRequestType requestType, Func<string> getOwnerId, TrelloAuthorization auth, Dictionary<string, object> requestParameters = null)
			: base(getOwnerId, auth)
		{
			_updateRequestType = requestType;
			_requestParameters = requestParameters ?? new Dictionary<string, object>();

			EventAggregator.Subscribe(this);
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
				AdditionalParameters.Remove("filter");
				return;
			}

			AdditionalParameters["filter"] = filter.GetDescription();
		}

		/// <summary>
		/// Adds a filter to the collection based on start and end date.
		/// </summary>
		/// <param name="start">The start date.</param>
		/// <param name="end">The end date.</param>
		public void Filter(DateTime? start, DateTime? end)
		{
			if (start != null)
				AdditionalParameters["since"] = start.Value.ToUniversalTime().ToString("O");
			if (end != null)
				AdditionalParameters["before"] = end.Value.ToUniversalTime().ToString("O");
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			IncorporateLimit();

			_requestParameters["_id"] = OwnerId;
			var allParameters = AdditionalParameters.Concat(CardContext.CurrentParameters)
			                                        .ToDictionary(kvp => kvp.Key.In("filter", "since", "before")
				                                                             ? kvp.Key
				                                                             : $"cards_{kvp.Key}",
			                                                      kvp => kvp.Value);
			var endpoint = EndpointFactory.Build(_updateRequestType, _requestParameters);
			var newData = await JsonRepository.Execute<List<IJsonCard>>(Auth, endpoint, ct, allParameters);

			var previousItems = new List<ICard>(Items);
			Items.Clear();
			Items.AddRange(newData.Select(jc =>
				{
					var card = jc.GetFromCache<Card, IJsonCard>(Auth);
					card.Json = jc;
					return card;
				}));
			var removedItems = previousItems.Except(Items, CacheableComparer.Get<ICard>()).OfType<Card>().ToList();
			foreach (var item in removedItems)
			{
				item.Json.List = null;
			}
		}

		private ICard GetByKey(string key)
		{
			return this.FirstOrDefault(c => key.In(c.Id, c.Name));
		}

		void IHandle<EntityUpdatedEvent<IJsonCard>>.Handle(EntityUpdatedEvent<IJsonCard> message)
		{
			ICard card;
			switch (_updateRequestType)
			{
				case EntityRequestType.Board_Read_Cards:
					if (!message.Properties.Contains(nameof(Card.Board))) return;
					card = Items.FirstOrDefault(c => c.Id == message.Data.Id);
					if (message.Data.Board?.Id != OwnerId && card != null)
						Items.Remove(card);
					else if (message.Data.Board?.Id == OwnerId && card == null)
						Items.Add(message.Data.GetFromCache<Card>(Auth));
					break;
				case EntityRequestType.List_Read_Cards:
					if (!message.Properties.Contains(nameof(Card.List))) return;
					card = Items.FirstOrDefault(c => c.Id == message.Data.Id);
					if (message.Data.List?.Id != OwnerId && card != null)
						Items.Remove(card);
					else if (message.Data.List?.Id == OwnerId && card == null)
						Items.Add(message.Data.GetFromCache<Card>(Auth));
					break;
				case EntityRequestType.Member_Read_Cards:
					if (!message.Properties.Contains(nameof(Card.Members))) return;
					card = Items.FirstOrDefault(c => c.Id == message.Data.Id);
					var memberIds = message.Data.Members.Select(m => m.Id).ToList();
					if (!memberIds.Contains(OwnerId) && card != null)
						Items.Remove(card);
					else if (memberIds.Contains(OwnerId) && card == null)
						Items.Add(message.Data.GetFromCache<Card>(Auth));
					break;
			}
		}

		void IHandle<EntityDeletedEvent<IJsonCard>>.Handle(EntityDeletedEvent<IJsonCard> message)
		{
			var item = Items.FirstOrDefault(c => c.Id == message.Data.Id);
			Items.Remove(item);
		}
	}
}

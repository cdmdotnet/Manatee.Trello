using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Eventing;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of boards.
	/// </summary>
	public class ReadOnlyBoardCollection : ReadOnlyCollection<IBoard>,
	                                       IReadOnlyBoardCollection,
	                                       IHandle<EntityUpdatedEvent<IJsonBoard>>,
	                                       IHandle<EntityDeletedEvent<IJsonBoard>>
	{
		private readonly EntityRequestType _updateRequestType;

		/// <summary>
		/// Retrieves a board which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching board, or null if none found.</returns>
		/// <remarks>
		/// Matches on board ID and name.  Comparison is case-sensitive.
		/// </remarks>
		public IBoard this[string key] => GetByKey(key);

		internal ReadOnlyBoardCollection(Type type, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_updateRequestType = type == typeof(Organization)
				                     ? EntityRequestType.Organization_Read_Boards
				                     : EntityRequestType.Member_Read_Boards;

			EventAggregator.Subscribe(this);
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		public void Filter(BoardFilter filter)
		{
			AdditionalParameters["filter"] = filter.GetDescription();
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			IncorporateLimit();

			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonBoard>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jb =>
				{
					var board = jb.GetFromCache<Board, IJsonBoard>(Auth);
					board.Json = jb;
					return board;
				}));
		}

		private IBoard GetByKey(string key)
		{
			return this.FirstOrDefault(b => key.In(b.Id, b.Name));
		}

		void IHandle<EntityUpdatedEvent<IJsonBoard>>.Handle(EntityUpdatedEvent<IJsonBoard> message)
		{
			IBoard board;
			switch (_updateRequestType)
			{
				case EntityRequestType.Organization_Read_Boards:
					if (!message.Properties.Contains(nameof(Board.Organization))) return;
					board = Items.FirstOrDefault(b => b.Id == message.Data.Id);
					if (message.Data.Organization?.Id != OwnerId && board != null)
						Items.Remove(board);
					else if (message.Data.Organization?.Id == OwnerId && board == null)
						Items.Add(message.Data.GetFromCache<Board>(Auth));
					break;
				case EntityRequestType.Member_Read_Boards:
					if (!message.Properties.Contains(nameof(Board.Members))) return;
					board = Items.FirstOrDefault(b => b.Id == message.Data.Id);
					var memberIds = message.Data.Members.Select(m => m.Id).ToList();
					if (!memberIds.Contains(OwnerId) && board != null)
						Items.Remove(board);
					else if (memberIds.Contains(OwnerId) && board == null)
						Items.Add(message.Data.GetFromCache<Board>(Auth));
					break;
			}
		}

		void IHandle<EntityDeletedEvent<IJsonBoard>>.Handle(EntityDeletedEvent<IJsonBoard> message)
		{
			var item = Items.FirstOrDefault(c => c.Id == message.Data.Id);
			Items.Remove(item);
		}
	}
}
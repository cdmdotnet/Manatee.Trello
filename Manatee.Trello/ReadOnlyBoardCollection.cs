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
	/// A read-only collectin of boards.
	/// </summary>
	public class ReadOnlyBoardCollection : ReadOnlyCollection<IBoard>, IReadOnlyBoardCollection
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
			_updateRequestType = type == typeof (Organization)
				                     ? EntityRequestType.Organization_Read_Boards
				                     : EntityRequestType.Member_Read_Boards;
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		public void Filter(BoardFilter filter)
		{
			AdditionalParameters["filter"] = filter.GetDescription();
		}

		/// <summary>
		/// Manually updates the collection's data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public sealed override async Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
			IncorporateLimit();

			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> { { "_id", OwnerId } });
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
	}
}
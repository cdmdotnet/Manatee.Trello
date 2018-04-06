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
	/// A read-only collectin of boards.
	/// </summary>
	public class ReadOnlyBoardCollection : ReadOnlyCollection<IBoard>, IReadOnlyBoardCollection
	{
		private readonly EntityRequestType _updateRequestType;
		private Dictionary<string, object> _additionalParameters;

		/// <summary>
		/// Retrieves a board which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching board, or null if none found.</returns>
		/// <remarks>
		/// Matches on <see cref="Board.Id"/> and <see cref="Board.Name"/>.  Comparison is case-sensitive.
		/// </remarks>
		public IBoard this[string key] => GetByKey(key);

		internal ReadOnlyBoardCollection(Type type, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_updateRequestType = type == typeof (Organization)
				                     ? EntityRequestType.Organization_Read_Boards
				                     : EntityRequestType.Member_Read_Boards;
		}
		internal ReadOnlyBoardCollection(ReadOnlyBoardCollection source, TrelloAuthorization auth)
			: base(() => source.OwnerId, auth)
		{
			_updateRequestType = source._updateRequestType;
			if (source._additionalParameters != null)
				_additionalParameters = new Dictionary<string, object>(source._additionalParameters);
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		public void Filter(BoardFilter filter)
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

			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> { { "_id", OwnerId } });
			var newData = JsonRepository.Execute<List<IJsonBoard>>(Auth, endpoint, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jb =>
				{
					var board = CachingObjectFactory.GetFromCache<Board>(jb, Auth);
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
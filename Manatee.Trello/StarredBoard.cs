using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a member's board star.
	/// </summary>
	public class StarredBoard : IStarredBoard, IMergeJson<IJsonStarredBoard>, IBatchRefresh, IHandleSynchronization
	{
		private readonly Field<IBoard> _board;
		private readonly Field<Position> _position;
		private readonly StarredBoardContext _context;

		/// <summary>
		/// Gets the board that is starred.
		/// </summary>
		public IBoard Board => _board.Value;

		/// <summary>
		/// Gets an ID on which matching can be performed.
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// Gets or sets the position in the member's starred boards list.
		/// </summary>
		public Position Position
		{
			get { return _position.Value; }
			set { _position.Value = value; }
		}

		internal IJsonStarredBoard Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}
		TrelloAuthorization IBatchRefresh.Auth => _context.Auth;

		/// <summary>
		/// Raised when data on the star is updated.
		/// </summary>
		public event Action<IStarredBoard, IEnumerable<string>> Updated;

		internal StarredBoard(string memberId, IJsonStarredBoard json, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new StarredBoardContext(memberId, Id, auth ?? TrelloAuthorization.Default);
			_context.Synchronized.Add(this);

			_board = new Field<IBoard>(_context, nameof(Board));
			_position = new Field<Position>(_context, nameof(Position));

			if (auth != TrelloAuthorization.Null)
				TrelloConfiguration.Cache.Add(this);
		}

		void IMergeJson<IJsonStarredBoard>.Merge(IJsonStarredBoard json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}

		/// <summary>
		/// Deletes the star.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the star from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		public Task Delete(CancellationToken ct = default)
		{
			return _context.Delete(ct);
		}

		/// <summary>
		/// Refreshes the star data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public Task Refresh(bool force = false, CancellationToken ct = default)
		{
			return _context.Synchronize(force, ct);
		}

		Endpoint IBatchRefresh.GetRefreshEndpoint()
		{
			return _context.GetRefreshEndpoint();
		}

		void IBatchRefresh.Apply(string content)
		{
			var json = TrelloConfiguration.Deserializer.Deserialize<IJsonStarredBoard>(content);
			_context.Merge(json);
		}

		void IHandleSynchronization.HandleSynchronized(IEnumerable<string> properties)
		{
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public interface IStarredBoard : ICacheable
	{
		IBoard Board { get; set; }
		Position Position { get; set; }

		/// <summary>
		/// Raised when data on the star is updated.
		/// </summary>
		event Action<IStarredBoard, IEnumerable<string>> Updated;

		/// <summary>
		/// Deletes the star.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the star from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		Task Delete(CancellationToken ct = default(CancellationToken));

		/// <summary>
		/// Refreshes the star data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken));
	}

	public class StarredBoard : IStarredBoard, IMergeJson<IJsonStarredBoard>
	{
		private readonly Field<IBoard> _board;
		private readonly Field<Position> _position;
		private readonly StarredBoardContext _context;

		public IBoard Board
		{
			get { return _board.Value; }
			set { _board.Value = value; }
		}

		public string Id { get; }

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

		public event Action<IStarredBoard, IEnumerable<string>> Updated;

		internal StarredBoard(string memberId, IJsonStarredBoard json, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new StarredBoardContext(memberId, Id, auth ?? TrelloAuthorization.Default);
			_context.Synchronized += Synchronized;

			_board = new Field<IBoard>(_context, nameof(Board));
			_position = new Field<Position>(_context, nameof(Position));
		}

		public void Merge(IJsonStarredBoard json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}

		public Task Delete(CancellationToken ct = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		public Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
			return _context.Synchronize(force, ct);
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}
}

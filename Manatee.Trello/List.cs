﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a list.
	/// </summary>
	public class List : IList, IMergeJson<IJsonList>, IBatchRefresh, IHandleSynchronization
	{
		/// <summary>
		/// Enumerates the data which can be pulled for lists.
		/// </summary>
		[Flags]
		public enum Fields
		{
			/// <summary>
			/// Indicates the Name property should be populated.
			/// </summary>
			[Display(Description="name")]
			Name = 1,
			/// <summary>
			/// Indicates the IsClosed property should be populated.
			/// </summary>
			[Display(Description="closed")]
			IsClosed = 1 << 1,
			/// <summary>
			/// Indicates the Board property should be populated.
			/// </summary>
			[Display(Description="board")]
			Board = 1 << 2,
			/// <summary>
			/// Indicates the Position property should be populated.
			/// </summary>
			[Display(Description="pos")]
			Position = 1 << 3,
			/// <summary>
			/// Indicates the Susbcribed property should be populated.
			/// </summary>
			[Display(Description="subscribed")]
			IsSubscribed = 1 << 4,
			/// <summary>
			/// Indicates the actions will be downloaded.
			/// </summary>
			Actions = 1 << 5,
			/// <summary>
			/// Indicates the cards will be downloaded.
			/// </summary>
			Cards = 1 << 6
		}

		private readonly Field<Board> _board;
		private readonly Field<bool?> _isArchived;
		private readonly Field<bool?> _isSubscribed;
		private readonly Field<string> _name;
		private readonly Field<Position> _position;
		private readonly ListContext _context;

		private string _id;
		private DateTime? _creation;
		private static Fields _downloadedFields;

		/// <summary>
		/// Specifies which fields should be downloaded.
		/// </summary>
		public static Fields DownloadedFields
		{
			get { return _downloadedFields; }
			set
			{
				_downloadedFields = value;
				ListContext.UpdateParameters();
			}
		}

		/// <summary>
		/// Gets the collection of actions performed on the list.
		/// </summary>
		public IReadOnlyActionCollection Actions => _context.Actions;
		/// <summary>
		/// Gets or sets the board on which the list belongs.
		/// </summary>
		public IBoard Board
		{
			get { return _board.Value; }
			set { _board.Value = (Board) value; }
		}
		/// <summary>
		/// Gets the collection of cards contained in the list.
		/// </summary>
		public ICardCollection Cards => _context.Cards;
		/// <summary>
		/// Gets the creation date of the list.
		/// </summary>
		public DateTime CreationDate
		{
			get
			{
				if (_creation == null)
					_creation = Id.ExtractCreationDate();
				return _creation.Value;
			}
		}
		/// <summary>
		/// Gets the list's ID.
		/// </summary>
		public string Id
		{
			get
			{
				if (!_context.HasValidId)
					Task.Run(async () => { await _context.Synchronize(true, CancellationToken.None); }).Wait();
				return _id;
			}
			private set { _id = value; }
		}
		/// <summary>
		/// Gets or sets whether the list is archived.
		/// </summary>
		public bool? IsArchived
		{
			get { return _isArchived.Value; }
			set { _isArchived.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether the current member is subscribed to the list.
		/// </summary>
		public bool? IsSubscribed
		{
			get { return _isSubscribed.Value; }
			set { _isSubscribed.Value = value; }
		}
		/// <summary>
		/// Gets the list's name.
		/// </summary>
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		/// <summary>
		/// Gets the list's position.
		/// </summary>
		public Position Position
		{
			get { return _position.Value; }
			set { _position.Value = value; }
		}

		/// <summary>
		/// Retrieves a card which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching card, or null if none found.</returns>
		/// <remarks>
		/// Matches on card ID and name.  Comparison is case-sensitive.
		/// </remarks>
		public ICard this[string key] => Cards[key];
		/// <summary>
		/// Retrieves the card at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The card.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		public ICard this[int index] => Cards[index];

		internal IJsonList Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}
		TrelloAuthorization IBatchRefresh.Auth => _context.Auth;

		/// <summary>
		/// Raised when data on the list is updated.
		/// </summary>
		public event Action<IList, IEnumerable<string>> Updated;

		static List()
		{
			DownloadedFields = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();
		}

		/// <summary>
		/// Creates a new instance of the <see cref="List"/> object.
		/// </summary>
		/// <param name="id">The list's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided, <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public List(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new ListContext(id, auth);
			_context.Synchronized.Add(this);

			_board = new Field<Board>(_context, nameof(Board));
			_board.AddRule(NotNullRule<Board>.Instance);
			_isArchived = new Field<bool?>(_context, nameof(IsArchived));
			_isArchived.AddRule(NullableHasValueRule<bool>.Instance);
			_isSubscribed = new Field<bool?>(_context, nameof(IsSubscribed));
			_isSubscribed.AddRule(NullableHasValueRule<bool>.Instance);
			_name = new Field<string>(_context, nameof(Name));
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, nameof(Position));
			_position.AddRule(NotNullRule<Position>.Instance);
			_position.AddRule(PositionRule.Instance);

			if (auth != TrelloAuthorization.Null)
				TrelloConfiguration.Cache.Add(this);
		}
		internal List(IJsonList json, TrelloAuthorization auth)
			: this(json.Id, auth)
		{
			_context.Merge(json);
		}

		/// <summary>
		/// Applies the changes an action represents.
		/// </summary>
		/// <param name="action">The action.</param>
		public void ApplyAction(IAction action)
		{
			var localAction = action as Action;

			if (action.Type != ActionType.UpdateList ||
			    localAction?.Json?.Data?.List == null ||
			    localAction.Json.Data.List.Id != Id) return;

			_context.Merge(localAction.Json.Data.List, false);
		}

		/// <summary>
		/// Refreshes the label data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public Task Refresh(bool force = false, CancellationToken ct = default)
		{
			return _context.Synchronize(force, ct);
		}

		void IMergeJson<IJsonList>.Merge(IJsonList json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return Name;
		}

		Endpoint IBatchRefresh.GetRefreshEndpoint()
		{
			return _context.GetRefreshEndpoint();
		}

		void IBatchRefresh.Apply(string content)
		{
			var json = TrelloConfiguration.Deserializer.Deserialize<IJsonList>(content);
			_context.Merge(json);
		}

		void IHandleSynchronization.HandleSynchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}
}
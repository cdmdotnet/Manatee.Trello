using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a checklist.
	/// </summary>
	public class CheckList : ICheckList, IMergeJson<IJsonCheckList>
	{
		/// <summary>
		/// Enumerates the data which can be pulled for check lists.
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
			/// Indicates the Board property should be populated.
			/// </summary>
			[Display(Description="idBoard")]
			Board = 1 << 1,
			/// <summary>
			/// Indicates the Card property should be populated.
			/// </summary>
			[Display(Description="idCard")]
			Card = 1 << 2,
			/// <summary>
			/// Indicates the CheckItems property should be populated.
			/// </summary>
			[Display(Description="checkItems")]
			CheckItems = 1 << 3,
			/// <summary>
			/// Indicates the Position property should be populated.
			/// </summary>
			[Display(Description="pos")]
			Position = 1 << 4
		}

		private readonly Field<Board> _board;
		private readonly Field<Card> _card;
		private readonly Field<string> _name;
		private readonly Field<Position> _position;
		private readonly CheckListContext _context;
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
				CheckListContext.UpdateParameters();
			}
		}

		/// <summary>
		/// Gets the board on which the checklist belongs.
		/// </summary>
		public IBoard Board => _board.Value;
		/// <summary>
		/// Gets or sets the card on which the checklist belongs.
		/// </summary>
		public ICard Card
		{
			get { return _card.Value; }
			set { _card.Value = (Card) value; }
		}

		/// <summary>
		/// Gets the collection of items in the checklist.
		/// </summary>
		public ICheckItemCollection CheckItems => _context.CheckItems;
		/// <summary>
		/// Gets the creation date of the checklist.
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
		/// Gets the checklist's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets the checklist's name.
		/// </summary>
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		/// <summary>
		/// Gets the checklist's position.
		/// </summary>
		public Position Position
		{
			get { return _position.Value; }
			set { _position.Value = (Position) value; }
		}

		/// <summary>
		/// Retrieves a check list item which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching check list item, or null if none found.</returns>
		/// <remarks>
		/// Matches on <see cref="ICheckItem.Id"/> and <see cref="ICheckItem.Name"/>.  Comparison is case-sensitive.
		/// </remarks>
		public ICheckItem this[string key] => CheckItems[key];
		/// <summary>
		/// Retrieves the check list item at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The check list item.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		public ICheckItem this[int index] => CheckItems[index];

		internal IJsonCheckList Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		/// <summary>
		/// Raised when data on the check list is updated.
		/// </summary>
		public event Action<ICheckList, IEnumerable<string>> Updated;

		/// <summary>
		/// Creates a new instance of the <see cref="CheckList"/> object.
		/// </summary>
		/// <param name="id">The check list's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided, <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public CheckList(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new CheckListContext(id, auth);
			_context.Synchronized += Synchronized;

			_board = new Field<Board>(_context, nameof(Board));
			_card = new Field<Card>(_context, nameof(Card));
			_card.AddRule(NotNullRule<Card>.Instance);
			_name = new Field<string>(_context, nameof(Name));
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, nameof(Position));
			_position.AddRule(NotNullRule<Position>.Instance);
			_position.AddRule(PositionRule.Instance);

			TrelloConfiguration.Cache.Add(this);
		}
		internal CheckList(IJsonCheckList json, TrelloAuthorization auth)
			: this(json.Id, auth)
		{
			_context.Merge(json);
		}

		/// <summary>
		/// Permanently deletes the check list from Trello.
		/// </summary>
		/// <remarks>
		/// This instance will remain in memory and all properties will remain accessible.
		/// </remarks>
		public async Task Delete(CancellationToken ct = default(CancellationToken))
		{
			await _context.Delete(ct);
			TrelloConfiguration.Cache.Remove(this);
		}
		/// <summary>
		/// Marks the check list to be refreshed the next time data is accessed.
		/// </summary>
		public async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			await _context.Synchronize(ct);
		}

		void IMergeJson<IJsonCheckList>.Merge(IJsonCheckList json)
		{
			_context.Merge(json);
		}

		/// <summary>
		/// Returns the <see cref="Name"/>.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		public override string ToString()
		{
			return Name;
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}
}
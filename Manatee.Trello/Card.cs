using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a card.
	/// </summary>
	public class Card : ICard, IMergeJson<IJsonCard>
	{
		/// <summary>
		/// Enumerates the data which can be pulled for cards.
		/// </summary>
		[Flags]
		public enum Fields
		{
			/// <summary>
			/// Indicates the Badges property should be populated.
			/// </summary>
			[Display(Description = "badges")]
			Badges = 1,

			/// <summary>
			/// Indicates the Board property should be populated.
			/// </summary>
			[Display(Description = "board")]
			Board = 1 << 1,

			/// <summary>
			/// Indicates the Checklists property should be populated.
			/// </summary>
			[Display(Description = "checkLists")]
			Checklists = 1 << 2,

			/// <summary>
			/// Indicates the DateLastActivity property should be populated.
			/// </summary>
			[Display(Description = "dateLastActivity")]
			DateLastActivity = 1 << 3,

			/// <summary>
			/// Indicates the Description property should be populated.
			/// </summary>
			[Display(Description = "desc")]
			Description = 1 << 4,

			/// <summary>
			/// Indicates the Due property should be populated.
			/// </summary>
			[Display(Description = "due")]
			Due = 1 << 5,

			/// <summary>
			/// Indicates the IsArchived property should be populated.
			/// </summary>
			[Display(Description = "closed")]
			IsArchived = 1 << 6,

			/// <summary>
			/// Indicates the IsComplete property should be populated.
			/// </summary>
			[Display(Description = "dueComplete")]
			IsComplete = 1 << 7,

			/// <summary>
			/// Indicates the IsSubscribed property should be populated.
			/// </summary>
			[Display(Description = "subscribed")]
			IsSubscribed = 1 << 8,

			/// <summary>
			/// Indicates that the labels should downloaded.
			/// </summary>
			[Display(Description = "labels")]
			Labels = 1 << 9,

			/// <summary>
			/// Indicates the List property should be populated.
			/// </summary>
			[Display(Description = "list")]
			List = 1 << 10,

			/// <summary>
			/// Indicates the ManualCoverAttachment property should be populated.
			/// </summary>
			[Display(Description = "manualCoverAttachment")]
			ManualCoverAttachment = 1 << 11,

			/// <summary>
			/// Indicates the Name property should be populated.
			/// </summary>
			[Display(Description = "name")]
			Name = 1 << 12,

			/// <summary>
			/// Indicates the Position property should be populated.
			/// </summary>
			[Display(Description = "pos")]
			Position = 1 << 13,

			/// <summary>
			/// Indicates the ShortId property should be populated.
			/// </summary>
			[Display(Description = "idShort")]
			ShortId = 1 << 14,

			/// <summary>
			/// Indicates the ShortUrl property should be populated.
			/// </summary>
			[Display(Description = "shortUrl")]
			ShortUrl = 1 << 15,

			/// <summary>
			/// Indicates the Url property should be populated.
			/// </summary>
			[Display(Description = "url")]
			Url = 1 << 16,
			/// <summary>
			/// Indicates that the actions should downloaded.
			/// </summary>
			Actions = 1 << 17,
			/// <summary>
			/// Indicates that the attachments should downloaded.
			/// </summary>
			Attachments = 1 << 18,
			/// <summary>
			/// Indicates that the custom field instances should downloaded.
			/// </summary>
			CustomFields = 1 << 19,
			/// <summary>
			/// Indicates that the comments should downloaded. Overrides Actions. Not included by default.
			/// </summary>
			Comments = 1 << 20,
			/// <summary>
			/// Indicates that the members should downloaded.
			/// </summary>
			Members = 1 << 21,
			/// <summary>
			/// Indicates that the stickers should downloaded.
			/// </summary>
			Stickers = 1 << 22,
			/// <summary>
			/// Indicates that the voting members should downloaded.
			/// </summary>
			VotingMembers = 1 << 23,
		}

		private readonly Field<Board> _board;
		private readonly Field<string> _description;
		private readonly Field<DateTime?> _dueDate;
		private readonly Field<bool?> _isArchived;
		private readonly Field<bool?> _isComplete;
		private readonly Field<bool?> _isSubscribed;
		private readonly Field<DateTime?> _lastActivity;
		private readonly Field<List> _list;
		private readonly Field<string> _name;
		private readonly Field<Position> _position;
		private readonly Field<int?> _shortId;
		private readonly Field<string> _shortUrl;
		private readonly Field<string> _url;
		private readonly CardContext _context;

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
				CardContext.UpdateParameters();
			}
		}

		/// <summary>
		/// Gets the collection of actions performed on this card.
		/// </summary>
		/// <remarks>By default imposed by Trello, this contains actions of type <see cref="ActionType.CommentCard"/>.</remarks>
		public IReadOnlyCollection<IAction> Actions { get; }

		/// <summary>
		/// Gets the collection of attachments contained in the card.
		/// </summary>
		public IAttachmentCollection Attachments => _context.Attachments;

		/// <summary>
		/// Gets the badges summarizing the card's content.
		/// </summary>
		public IBadges Badges { get; }

		/// <summary>
		/// Gets the board to which the card belongs.
		/// </summary>
		public IBoard Board => _board.Value;

		/// <summary>
		/// Gets the collection of checklists contained in the card.
		/// </summary>
		public ICheckListCollection CheckLists => _context.CheckLists;

		/// <summary>
		/// Gets the collection of comments made on the card.
		/// </summary>
		public ICommentCollection Comments => _context.Comments;

		/// <summary>
		/// Gets the creation date of the card.
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
		/// Gets the collection of custom field values for the card.
		/// </summary>
		public IReadOnlyCollection<ICustomField> CustomFields => _context.CustomFields;

		/// <summary>
		/// Gets or sets the card's description.
		/// </summary>
		public string Description
		{
			get { return _description.Value; }
			set { _description.Value = value; }
		}

		/// <summary>
		/// Gets or sets the card's due date.
		/// </summary>
		public DateTime? DueDate
		{
			get { return _dueDate.Value; }
			set { _dueDate.Value = value; }
		}

		/// <summary>
		/// Gets the card's ID.
		/// </summary>
		public string Id
		{
			get
			{
				if (!_context.HasValidId)
					_context.Synchronize(CancellationToken.None).Wait();
				return _id;
			}
			private set { _id = value; }
		}

		/// <summary>
		/// Gets or sets whether the card is archived.
		/// </summary>
		public bool? IsArchived
		{
			get { return _isArchived.Value; }
			set { _isArchived.Value = value; }
		}

		/// <summary>
		/// Gets or sets whether the card is complete.  Associated with <see cref="DueDate"/>.
		/// </summary>
		public bool? IsComplete
		{
			get { return _isComplete.Value; }
			set { _isComplete.Value = value; }
		}

		/// <summary>
		/// Gets or sets whether the current member is subscribed to the card.
		/// </summary>
		public bool? IsSubscribed
		{
			get { return _isSubscribed.Value; }
			set { _isSubscribed.Value = value; }
		}

		/// <summary>
		/// Gets the collection of labels on the card.
		/// </summary>
		public ICardLabelCollection Labels => _context.Labels;

		/// <summary>
		/// Gets the most recent date of activity on the card.
		/// </summary>
		public DateTime? LastActivity => _lastActivity.Value;

		/// <summary>
		/// Gets or sets the list to the card belongs.
		/// </summary>
		public IList List
		{
			get { return _list.Value; }
			set
			{
				_list.Value = (List) value;
				_board.Value = (Board) value.Board;
			}
		}

		/// <summary>
		/// Gets the collection of members who are assigned to the card.
		/// </summary>
		public IMemberCollection Members => _context.Members;

		/// <summary>
		/// Gets or sets the card's name.
		/// </summary>
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}

		/// <summary>
		/// Gets or sets the card's position.
		/// </summary>
		public Position Position
		{
			get { return _position.Value; }
			set { _position.Value = value; }
		}

		/// <summary>
		/// Gets card-specific power-up data.
		/// </summary>
		public IReadOnlyCollection<IPowerUpData> PowerUpData => _context.PowerUpData;

		/// <summary>
		/// Gets the card's short ID.
		/// </summary>
		public int? ShortId => _shortId.Value;

		/// <summary>
		/// Gets the card's short URL.
		/// </summary>
		/// <remarks>
		/// Because this value does not change, it can be used as a permalink.
		/// </remarks>
		public string ShortUrl => _shortUrl.Value;

		/// <summary>
		/// Gets the collection of stickers which appear on the card.
		/// </summary>
		public ICardStickerCollection Stickers => _context.Stickers;

		/// <summary>
		/// Gets the card's full URL.
		/// </summary>
		/// <remarks>
		/// Trello will likely change this value as the name changes.  You can use <see cref="ShortUrl"/> for permalinks.
		/// </remarks>
		public string Url => _url.Value;

		/// <summary>
		/// Gets all members who have voted for this card.
		/// </summary>
		public IReadOnlyCollection<IMember> VotingMembers => _context.VotingMembers;

		/// <summary>
		/// Retrieves a check list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching check list, or null if none found.</returns>
		/// <remarks>
		/// Matches on checklist ID and name.  Comparison is case-sensitive.
		/// </remarks>
		public ICheckList this[string key] => CheckLists[key];

		/// <summary>
		/// Retrieves the check list at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The check list.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		public ICheckList this[int index] => CheckLists[index];

		internal IJsonCard Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		/// <summary>
		/// Raised when data on the card is updated.
		/// </summary>
		public event Action<ICard, IEnumerable<string>> Updated;

		static Card()
		{
			DownloadedFields = (Fields) Enum.GetValues(typeof(Fields)).Cast<int>().Sum() &
			                   ~Fields.Comments;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Card"/> object.
		/// </summary>
		/// <param name="id">The card's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided, <see cref="TrelloAuthorization.Default"/> will be used.</param>
		/// <remarks>
		/// The supplied ID can be either the full or short ID.
		/// </remarks>
		public Card(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new CardContext(id, auth);
			_context.Synchronized += Synchronized;

			Actions = new ReadOnlyActionCollection(typeof(Card), () => id, auth);
			Badges = new Badges(_context.BadgesContext);
			_board = new Field<Board>(_context, nameof(Board));
			_board.AddRule(NotNullRule<Board>.Instance);
			_description = new Field<string>(_context, nameof(Description));
			_dueDate = new Field<DateTime?>(_context, nameof(DueDate));
			_isComplete = new Field<bool?>(_context, nameof(IsComplete));
			_isArchived = new Field<bool?>(_context, nameof(IsArchived));
			_isArchived.AddRule(NullableHasValueRule<bool>.Instance);
			_isSubscribed = new Field<bool?>(_context, nameof(IsSubscribed));
			_isSubscribed.AddRule(NullableHasValueRule<bool>.Instance);
			_lastActivity = new Field<DateTime?>(_context, nameof(LastActivity));
			_list = new Field<List>(_context, nameof(List));
			_list.AddRule(NotNullRule<IList>.Instance);
			_name = new Field<string>(_context, nameof(Name));
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, nameof(Position));
			_position.AddRule(PositionRule.Instance);
			_shortId = new Field<int?>(_context, nameof(ShortId));
			_shortUrl = new Field<string>(_context, nameof(ShortUrl));
			_url = new Field<string>(_context, nameof(Url));

			if (_context.HasValidId)
				TrelloConfiguration.Cache.Add(this);
		}

		internal Card(IJsonCard json, TrelloAuthorization auth)
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
			if (action.Type != ActionType.UpdateCard || action.Data.Card == null || action.Data.Card.Id != Id) return;

			_context.Merge(((Card) action.Data.Card).Json);
		}

		/// <summary>
		/// Deletes the card.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the card from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		public async Task Delete(CancellationToken ct = default(CancellationToken))
		{
			await _context.Delete(ct);
			if (TrelloConfiguration.RemoveDeletedItemsFromCache)
				TrelloConfiguration.Cache.Remove(this);
		}

		/// <summary>
		/// Refreshes the card data.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			await _context.Synchronize(ct);
		}

		void IMergeJson<IJsonCard>.Merge(IJsonCard json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return Name ?? $"#{ShortId}";
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			if (Id != _context.Data.Id)
			{
				TrelloConfiguration.Cache.Add(this);
				Id = _context.Data.Id;
			}

			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}
}
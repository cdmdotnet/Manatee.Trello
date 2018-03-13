﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;
using IQueryable = Manatee.Trello.Contracts.IQueryable;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a card.
	/// </summary>
	public interface ICard : ICanWebhook, IQueryable
	{
		/// <summary>
		/// Gets the collection of actions performed on this card.
		/// </summary>
		/// <remarks>By default imposed by Trello, this contains actions of types <see cref="ActionType.CommentCard"/> and <see cref="ActionType.UpdateCardIdList"/>.</remarks>
		IReadOnlyCollection<IAction> Actions { get; }

		/// <summary>
		/// Gets the collection of attachments contained in the card.
		/// </summary>
		IAttachmentCollection Attachments { get; }

		/// <summary>
		/// Gets the badges summarizing the content of the card.
		/// </summary>
		IBadges Badges { get; }

		/// <summary>
		/// Gets the board to which the card belongs.
		/// </summary>
		IBoard Board { get; }

		/// <summary>
		/// Gets the collection of checklists contained in the card.
		/// </summary>
		ICheckListCollection CheckLists { get; }

		/// <summary>
		/// Gets the collection of comments made on the card.
		/// </summary>
		ICommentCollection Comments { get; }

		/// <summary>
		/// Gets the creation date of the card.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets or sets the card's description.
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Gets or sets the card's due date.
		/// </summary>
		DateTime? DueDate { get; set; }

		/// <summary>
		/// Gets or sets whether the card is archived.
		/// </summary>
		bool? IsArchived { get; set; }

		/// <summary>
		/// Gets or sets whether the card is complete.  Associated with <see cref="DueDate"/>.
		/// </summary>
		bool? IsComplete { get; set; }

		/// <summary>
		/// Gets or sets whether the current member is subscribed to the card.
		/// </summary>
		bool? IsSubscribed { get; set; }

		/// <summary>
		/// Gets the collection of labels on the card.
		/// </summary>
		ICardLabelCollection Labels { get; }

		/// <summary>
		/// Gets the most recent date of activity on the card.
		/// </summary>
		DateTime? LastActivity { get; }

		/// <summary>
		/// Gets or sets the list to the card belongs.
		/// </summary>
		IList List { get; set; }

		/// <summary>
		/// Gets the collection of members who are assigned to the card.
		/// </summary>
		IMemberCollection Members { get; }

		/// <summary>
		/// Gets or sets the card's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the card's position.
		/// </summary>
		Position Position { get; set; }

		/// <summary>
		/// Gets specific data regarding power-ups.
		/// </summary>
		IReadOnlyCollection<IPowerUpData> PowerUpData { get; }

		/// <summary>
		/// Gets the card's short ID.
		/// </summary>
		int? ShortId { get; }

		/// <summary>
		/// Gets the card's short URL.
		/// </summary>
		/// <remarks>
		/// Because this value does not change, it can be used as a permalink.
		/// </remarks>
		string ShortUrl { get; }

		/// <summary>
		/// Gets the collection of stickers which appear on the card.
		/// </summary>
		ICardStickerCollection Stickers { get; }

		/// <summary>
		/// Gets the card's full URL.
		/// </summary>
		/// <remarks>
		/// Trello will likely change this value as the name changes.  You can use <see cref="ShortUrl"/> for permalinks.
		/// </remarks>
		string Url { get; }

		/// <summary>
		/// Gets all members who have voted for this card.
		/// </summary>
		IReadOnlyCollection<IMember> VotingMembers { get; }

		/// <summary>
		/// Retrieves a check list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching check list, or null if none found.</returns>
		/// <remarks>
		/// Matches on CheckList.Id and CheckList.Name.  Comparison is case-sensitive.
		/// </remarks>
		ICheckList this[string key] { get; }

		/// <summary>
		/// Retrieves the check list at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The check list.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		ICheckList this[int index] { get; }

		/// <summary>
		/// Raised when data on the card is updated.
		/// </summary>
		event Action<ICard, IEnumerable<string>> Updated;

		/// <summary>
		/// Deletes the card.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the card from Trello's server, however, this object will
		/// remain in memory and all properties will remain accessible.
		/// </remarks>
		void Delete();

		/// <summary>
		/// Marks the card to be refreshed the next time data is accessed.
		/// </summary>
		void Refresh();
	}

	/// <summary>
	/// Represents a card.
	/// </summary>
	public class Card : ICard
	{
		/// <summary>
		/// Defines fetchable fields for <see cref="Card"/>s.
		/// </summary>
		[Flags]
		public enum Fields
		{
			/// <summary>
			/// Indicates that <see cref="Card.Badges"/> should be fetched.
			/// </summary>
			[Display(Description="badges")]
			Badges = 1,
			/// <summary>
			/// Indicates that <see cref="Card.Board"/> should be fetched.
			/// </summary>
			[Display(Description="idBoard")]
			Board = 1 << 1,
			/// <summary>
			/// Indicates that <see cref="Card.CheckLists"/> should be fetched.
			/// </summary>
			[Display(Description="idCheckLists")]
			Checklists = 1 << 2,
			/// <summary>
			/// Indicates that <see cref="Card.LastActivity"/> should be fetched.
			/// </summary>
			[Display(Description="dateLastActivity")]
			DateLastActivity = 1 << 3,
			/// <summary>
			/// Indicates that <see cref="Card.Description"/> should be fetched.
			/// </summary>
			[Display(Description="desc")]
			Description = 1 << 4,
			/// <summary>
			/// Indicates that <see cref="Card.DueDate"/> should be fetched.
			/// </summary>
			[Display(Description="due")]
			Due = 1 << 5,
			/// <summary>
			/// Indicates that <see cref="Card.IsArchived"/> should be fetched.
			/// </summary>
			[Display(Description="closed")]
			IsArchived = 1 << 6,
			/// <summary>
			/// Indicates that <see cref="Card.IsComplete"/> should be fetched.
			/// </summary>
			[Display(Description="dueComplete")]
			IsComplete = 1 << 7,
			/// <summary>
			/// Indicates that <see cref="Card.IsSubscribed"/> should be fetched.
			/// </summary>
			[Display(Description="subscribed")]
			IsSubscribed = 1 << 8,
			/// <summary>
			/// Indicates that <see cref="Card.Labels"/> should be fetched.
			/// </summary>
			[Display(Description="idLabels")]
			Labels = 1 << 9,
			/// <summary>
			/// Indicates that <see cref="Card.List"/> should be fetched.
			/// </summary>
			[Display(Description="idList")]
			List = 1 << 10,
			/// <summary>
			/// Not Implemented
			/// </summary>
			[Display(Description="manualCoverAttachment")]
			ManualCoverAttachment = 1 << 11,
			/// <summary>
			/// Indicates that <see cref="Card.Name"/> should be fetched.
			/// </summary>
			[Display(Description="name")]
			Name = 1 << 12,
			/// <summary>
			/// Indicates that <see cref="Card.Position"/> should be fetched.
			/// </summary>
			[Display(Description="pos")]
			Position = 1 << 13,
			/// <summary>
			/// Indicates that <see cref="Card.ShortId"/> should be fetched.
			/// </summary>
			[Display(Description="idShort")]
			ShortId = 1 << 14,
			/// <summary>
			/// Indicates that <see cref="Card.ShortUrl"/> should be fetched.
			/// </summary>
			[Display(Description="shortUrl")]
			ShortUrl = 1 << 15,
			/// <summary>
			/// Indicates that <see cref="Card.Url"/> should be fetched.
			/// </summary>
			[Display(Description="url")]
			Url = 1 << 16,
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

		/// <summary>
		/// Gets and sets the fields to fetch.
		/// </summary>
		public static Fields DownloadedFields { get; set; } = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();

		/// <summary>
		/// Gets the collection of actions performed on this card.
		/// </summary>
		/// <remarks>By default imposed by Trello, this contains actions of types <see cref="ActionType.CommentCard"/> and <see cref="ActionType.UpdateCardIdList"/>.</remarks>
		public IReadOnlyCollection<IAction> Actions { get; }
		/// <summary>
		/// Gets the collection of attachments contained in the card.
		/// </summary>
		public IAttachmentCollection Attachments { get; }
		/// <summary>
		/// Gets the badges summarizing the content of the card.
		/// </summary>
		public IBadges Badges { get; }
		/// <summary>
		/// Gets the board to which the card belongs.
		/// </summary>
		public IBoard Board => _board.Value;
		/// <summary>
		/// Gets the collection of checklists contained in the card.
		/// </summary>
		public ICheckListCollection CheckLists { get; }
		/// <summary>
		/// Gets the collection of comments made on the card.
		/// </summary>
		public ICommentCollection Comments { get; }
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
					_context.Synchronize();
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
		public ICardLabelCollection Labels { get; }
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
		public IMemberCollection Members { get; }
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
			set { _position.Value = (Position) value; }
		}
		/// <summary>
		/// Gets specific data regarding power-ups.
		/// </summary>
		public IReadOnlyCollection<IPowerUpData> PowerUpData { get; }
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
		public ICardStickerCollection Stickers { get; }
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
		public IReadOnlyCollection<IMember> VotingMembers { get; }

		/// <summary>
		/// Retrieves a check list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching check list, or null if none found.</returns>
		/// <remarks>
		/// Matches on CheckList.Id and CheckList.Name.  Comparison is case-sensitive.
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

		/// <summary>
		/// Creates a new instance of the <see cref="Card"/> object.
		/// </summary>
		/// <param name="id">The card's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		/// <remarks>
		/// The supplied ID can be either the full or short ID.
		/// </remarks>
		public Card(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new CardContext(id, auth);
			_context.Synchronized += Synchronized;

			Actions = new ReadOnlyActionCollection(typeof(Card), () => id, auth);
			Attachments = new AttachmentCollection(() => Id, auth);
			Badges = new Badges(_context.BadgesContext);
			_board = new Field<Board>(_context, nameof(Board));
			_board.AddRule(NotNullRule<Board>.Instance);
			CheckLists = new CheckListCollection(this, auth);
			Comments = new CommentCollection(() => Id, auth);
			_description = new Field<string>(_context, nameof(Description));
			_dueDate = new Field<DateTime?>(_context, nameof(DueDate));
			_isComplete = new Field<bool?>(_context, nameof(IsComplete));
			_isArchived = new Field<bool?>(_context, nameof(IsArchived));
			_isArchived.AddRule(NullableHasValueRule<bool>.Instance);
			_isSubscribed = new Field<bool?>(_context, nameof(IsSubscribed));
			_isSubscribed.AddRule(NullableHasValueRule<bool>.Instance);
			Labels = new CardLabelCollection(_context, auth);
			_lastActivity = new Field<DateTime?>(_context, nameof(LastActivity));
			_list = new Field<List>(_context, nameof(List));
			_list.AddRule(NotNullRule<IList>.Instance);
			Members = new MemberCollection(EntityRequestType.Card_Read_Members, () => Id, auth);
			_name = new Field<string>(_context, nameof(Name));
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, nameof(Position));
			_position.AddRule(PositionRule.Instance);
			PowerUpData = new ReadOnlyPowerUpDataCollection(EntityRequestType.Card_Read_PowerUpData, () => Id, auth);
			_shortId = new Field<int?>(_context, nameof(ShortId));
			_shortUrl = new Field<string>(_context, nameof(ShortUrl));
			Stickers = new CardStickerCollection(() => Id, auth);
			_url = new Field<string>(_context, nameof(Url));
			VotingMembers = new ReadOnlyMemberCollection(EntityRequestType.Card_Read_MembersVoted, () => Id, auth);

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
			_context.Merge(((Card)action.Data.Card).Json);
		}
		/// <summary>
		/// Deletes the card.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the card from Trello's server, however, this object will
		/// remain in memory and all properties will remain accessible.
		/// </remarks>
		public void Delete()
		{
			_context.Delete();
			TrelloConfiguration.Cache.Remove(this);
		}
		/// <summary>
		/// Marks the card to be refreshed the next time data is accessed.
		/// </summary>
		public void Refresh()
		{
			_context.Expire();
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return Name ?? $"#{ShortId}";
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}
}
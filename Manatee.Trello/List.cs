﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a list.
	/// </summary>
	public interface IList : ICanWebhook
	{
		/// <summary>
		/// Gets the collection of actions performed on the list.
		/// </summary>
		IReadOnlyCollection<IAction> Actions { get; }

		/// <summary>
		/// Gets or sets the board on which the list belongs.
		/// </summary>
		IBoard Board { get; set; }

		/// <summary>
		/// Gets the collection of cards contained in the list.
		/// </summary>
		ICardCollection Cards { get; }

		/// <summary>
		/// Gets the creation date of the list.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets or sets whether the list is archived.
		/// </summary>
		bool? IsArchived { get; set; }

		/// <summary>
		/// Gets or sets whether the current member is subscribed to the list.
		/// </summary>
		bool? IsSubscribed { get; set; }

		/// <summary>
		/// Gets the list's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets the list's position.
		/// </summary>
		Position Position { get; set; }

		/// <summary>
		/// Retrieves a card which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching card, or null if none found.</returns>
		/// <remarks>
		/// Matches on Card.Id and Card.Name.  Comparison is case-sensitive.
		/// </remarks>
		ICard this[string key] { get; }

		/// <summary>
		/// Retrieves the card at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The card.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		ICard this[int index] { get; }

		/// <summary>
		/// Raised when data on the list is updated.
		/// </summary>
		event Action<IList, IEnumerable<string>> Updated;

		/// <summary>
		/// Marks the list to be refreshed the next time data is accessed.
		/// </summary>
		void Refresh();
	}

	/// <summary>
	/// Represents a list.
	/// </summary>
	public class List : IList
	{
		/// <summary>
		/// Defines fetchable fields for <see cref="List"/>s.
		/// </summary>
		[Flags]
		public enum Fields
		{
			/// <summary>
			/// Indicates that <see cref="List.Name"/> should be fetched.
			/// </summary>
			[Display(Description="name")]
			Name = 1,
			/// <summary>
			/// Indicates that <see cref="List.IsArchived"/> should be fetched.
			/// </summary>
			[Display(Description="closed")]
			IsClosed = 1 << 1,
			/// <summary>
			/// Indicates that <see cref="List.Board"/> should be fetched.
			/// </summary>
			[Display(Description="idBoard")]
			Board = 1 << 2,
			/// <summary>
			/// Indicates that <see cref="List.Position"/> should be fetched.
			/// </summary>
			[Display(Description="pos")]
			Position = 1 << 3,
			/// <summary>
			/// Indicates that <see cref="List.IsSubscribed"/> should be fetched.
			/// </summary>
			[Display(Description="subscribed")]
			Subscribed = 1 << 4
		}

		private readonly Field<Board> _board;
		private readonly Field<bool?> _isArchived;
		private readonly Field<bool?> _isSubscribed;
		private readonly Field<string> _name;
		private readonly Field<Position> _position;
		private readonly ListContext _context;

		private string _id;
		private DateTime? _creation;

		/// <summary>
		/// Gets and sets the fields to fetch.
		/// </summary>
		public static Fields DownloadedFields { get; set; } = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();

		/// <summary>
		/// Gets the collection of actions performed on the list.
		/// </summary>
		public IReadOnlyCollection<IAction> Actions { get; }
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
		public ICardCollection Cards { get; }
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
					_context.Synchronize();
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
			set { _position.Value = (Position) value; }
		}

		/// <summary>
		/// Retrieves a card which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching card, or null if none found.</returns>
		/// <remarks>
		/// Matches on Card.Id and Card.Name.  Comparison is case-sensitive.
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

		/// <summary>
		/// Raised when data on the list is updated.
		/// </summary>
		public event Action<IList, IEnumerable<string>> Updated;

		/// <summary>
		/// Creates a new instance of the <see cref="List"/> object.
		/// </summary>
		/// <param name="id">The list's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public List(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new ListContext(id, auth);
			_context.Synchronized += Synchronized;

			Actions = new ReadOnlyActionCollection(typeof(List), () => Id, auth);
			_board = new Field<Board>(_context, nameof(Board));
			_board.AddRule(NotNullRule<Board>.Instance);
			Cards = new CardCollection(() => Id, auth);
			_isArchived = new Field<bool?>(_context, nameof(IsArchived));
			_isArchived.AddRule(NullableHasValueRule<bool>.Instance);
			_isSubscribed = new Field<bool?>(_context, nameof(IsSubscribed));
			_isSubscribed.AddRule(NullableHasValueRule<bool>.Instance);
			_name = new Field<string>(_context, nameof(Name));
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, nameof(Position));
			_position.AddRule(NotNullRule<Position>.Instance);
			_position.AddRule(PositionRule.Instance);

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
			if (action.Type != ActionType.UpdateList || action.Data.List == null || action.Data.List.Id != Id) return;
			_context.Merge(((List) action.Data.List).Json);
		}
		/// <summary>
		/// Marks the list to be refreshed the next time data is accessed.
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
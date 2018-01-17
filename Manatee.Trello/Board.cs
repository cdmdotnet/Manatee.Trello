using System;
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
	public interface IBoard : ICanWebhook, IQueryable
	{
		/// <summary>
		/// Gets the collection of actions performed on and within this board.
		/// </summary>
		IReadOnlyCollection<IAction> Actions { get; }

		/// <summary>
		/// Gets the collection of cards contained within this board.
		/// </summary>
		/// <remarks>
		/// This property only exposes unarchived cards.
		/// </remarks>
		IReadOnlyCollection<ICard> Cards { get; }

		/// <summary>
		/// Gets the creation date of the board.
		/// </summary>
		DateTime CreationDate { get; }

		/// <summary>
		/// Gets or sets the board's description.
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Gets or sets whether this board is closed.
		/// </summary>
		bool? IsClosed { get; set; }

		/// <summary>
		/// Gets or sets whether the current member is subscribed to this board.
		/// </summary>
		bool? IsSubscribed { get; set; }

		/// <summary>
		/// Gets the collection of labels for this board.
		/// </summary>
		IBoardLabelCollection Labels { get; }

		/// <summary>
		/// Gets the collection of lists on this board.
		/// </summary>
		/// <remarks>
		/// This property only exposes unarchived lists.
		/// </remarks>
		IListCollection Lists { get; }

		/// <summary>
		/// Gets the collection of members on this board.
		/// </summary>
		IReadOnlyCollection<IMember> Members { get; }

		/// <summary>
		/// Gets the collection of members and their privileges on this board.
		/// </summary>
		IBoardMembershipCollection Memberships { get; }

		/// <summary>
		/// Gets or sets the board's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the organization to which this board belongs.
		/// </summary>
		/// <remarks>
		/// Setting null makes the board's first admin the owner.
		/// </remarks>
		IOrganization Organization { get; set; }

		/// <summary>
		/// Gets metadata about any active power-ups.
		/// </summary>
		IReadOnlyCollection<IPowerUp> PowerUps { get; }

		/// <summary>
		/// Gets specific data regarding power-ups.
		/// </summary>
		IReadOnlyCollection<IPowerUpData> PowerUpData { get; }

		/// <summary>
		/// Gets the set of preferences for the board.
		/// </summary>
		IBoardPreferences Preferences { get; }

		/// <summary>
		/// Gets the set of preferences for the board.
		/// </summary>
		IBoardPersonalPreferences PersonalPreferences { get; }

		/// <summary>
		/// Gets the board's URI.
		/// </summary>
		string Url { get; }

		/// <summary>
		/// Retrieves a list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching list, or null if none found.</returns>
		/// <remarks>
		/// Matches on List.Id and List.Name.  Comparison is case-sensitive.
		/// </remarks>
		IList this[string key] { get; }

		/// <summary>
		/// Retrieves the list at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The list.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		IList this[int index] { get; }

		/// <summary>
		/// Raised when data on the board is updated.
		/// </summary>
		event Action<IBoard, IEnumerable<string>> Updated;

		/// <summary>
		/// Marks the board to be refreshed the next time data is accessed.
		/// </summary>
		void Refresh();
	}

	/// <summary>
	/// Represents a board.
	/// </summary>
	public class Board : IBoard
	{
		[Flags]
		public enum Fields
		{
			[Display(Description="name")]
			Name = 1,
			[Display(Description="desc")]
			Description = 1 << 1,
			[Display(Description="closed")]
			Closed = 1 << 2,
			[Display(Description="idOrganization")]
			Organization = 1 << 3,
			[Display(Description="prefs")]
			Preferencess = 1 << 4,
			[Display(Description="url")]
			Url = 1 << 5,
			[Display(Description="subscribed")]
			Subscribed = 1 << 6
		}

		private readonly Field<string> _description;
		private readonly Field<bool?> _isClosed;
		private readonly Field<bool?> _isSubscribed;
		private readonly Field<string> _name;
		private readonly Field<Organization> _organization;
		private readonly Field<string> _url;
		private readonly BoardContext _context;

		private string _id;
		private DateTime? _creation;

		public static Fields DownloadedFields { get; set; } = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();

		/// <summary>
		/// Gets the collection of actions performed on and within this board.
		/// </summary>
		public IReadOnlyCollection<IAction> Actions { get; }
		/// <summary>
		/// Gets the collection of cards contained within this board.
		/// </summary>
		/// <remarks>
		/// This property only exposes unarchived cards.
		/// </remarks>
		public IReadOnlyCollection<ICard> Cards { get; }
		/// <summary>
		/// Gets the creation date of the board.
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
		/// Gets or sets the board's description.
		/// </summary>
		public string Description
		{
			get { return _description.Value; }
			set { _description.Value = value; }
		}
		/// <summary>
		/// Gets the board's ID.
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
		/// Gets or sets whether this board is closed.
		/// </summary>
		public bool? IsClosed
		{
			get { return _isClosed.Value; }
			set { _isClosed.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether the current member is subscribed to this board.
		/// </summary>
		public bool? IsSubscribed
		{
			get { return _isSubscribed.Value; }
			set { _isSubscribed.Value = value; }
		}
		/// <summary>
		/// Gets the collection of labels for this board.
		/// </summary>
		public IBoardLabelCollection Labels { get; }
		/// <summary>
		/// Gets the collection of lists on this board.
		/// </summary>
		/// <remarks>
		/// This property only exposes unarchived lists.
		/// </remarks>
		public IListCollection Lists { get; }
		/// <summary>
		/// Gets the collection of members on this board.
		/// </summary>
		public IReadOnlyCollection<IMember> Members { get; }
		/// <summary>
		/// Gets the collection of members and their privileges on this board.
		/// </summary>
		public IBoardMembershipCollection Memberships { get; }
		/// <summary>
		/// Gets or sets the board's name.
		/// </summary>
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		/// <summary>
		/// Gets or sets the organization to which this board belongs.
		/// </summary>
		/// <remarks>
		/// Setting null makes the board's first admin the owner.
		/// </remarks>
		public IOrganization Organization
		{
			get { return _organization.Value; }
			set { _organization.Value = (Organization) value; }
		}
		/// <summary>
		/// Gets metadata about any active power-ups.
		/// </summary>
		public IReadOnlyCollection<IPowerUp> PowerUps { get; }
		/// <summary>
		/// Gets specific data regarding power-ups.
		/// </summary>
		public IReadOnlyCollection<IPowerUpData> PowerUpData { get; }
		/// <summary>
		/// Gets the set of preferences for the board.
		/// </summary>
		public IBoardPreferences Preferences { get; }
		/// <summary>
		/// Gets the set of preferences for the board.
		/// </summary>
		public IBoardPersonalPreferences PersonalPreferences { get; }
		/// <summary>
		/// Gets the board's URI.
		/// </summary>
		public string Url => _url.Value;

		/// <summary>
		/// Retrieves a list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching list, or null if none found.</returns>
		/// <remarks>
		/// Matches on List.Id and List.Name.  Comparison is case-sensitive.
		/// </remarks>
		public IList this[string key] => Lists[key];
		/// <summary>
		/// Retrieves the list at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The list.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		public IList this[int index] => Lists[index];

		internal IJsonBoard Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}
		internal TrelloAuthorization Auth { get; }

		/// <summary>
		/// Raised when data on the board is updated.
		/// </summary>
		public event Action<IBoard, IEnumerable<string>> Updated;

		/// <summary>
		/// Creates a new instance of the <see cref="Board"/> object.
		/// </summary>
		/// <param name="id">The board's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public Board(string id, TrelloAuthorization auth = null)
		{
			Auth = auth;
			_context = new BoardContext(id, auth);
			_context.Synchronized += Synchronized;
			Id = id;

			Actions = new ReadOnlyActionCollection(typeof(Board), () => Id, auth);
			Cards = new ReadOnlyCardCollection(typeof(Board), () => Id, auth);
			_description = new Field<string>(_context, nameof(Description));
			_isClosed = new Field<bool?>(_context, nameof(IsClosed));
			_isClosed.AddRule(NullableHasValueRule<bool>.Instance);
			_isSubscribed = new Field<bool?>(_context, nameof(IsSubscribed));
			_isSubscribed.AddRule(NullableHasValueRule<bool>.Instance);
			Labels = new BoardLabelCollection(() => Id, auth);
			Lists = new ListCollection(() => Id, auth);
			Members = new ReadOnlyMemberCollection(EntityRequestType.Board_Read_Members, () => Id, auth);
			Memberships = new BoardMembershipCollection(() => Id, auth);
			_name = new Field<string>(_context, nameof(Name));
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_organization = new Field<Organization>(_context, nameof(Organization));
			PowerUps = new ReadOnlyPowerUpCollection(() => Id, auth);
			PowerUpData = new ReadOnlyPowerUpDataCollection(EntityRequestType.Board_Read_PowerUpData, () => Id, auth);
			Preferences = new BoardPreferences(_context.BoardPreferencesContext);
			PersonalPreferences = new BoardPersonalPreferences(() => Id, auth);
			_url = new Field<string>(_context, nameof(Url));

			TrelloConfiguration.Cache.Add(this);
		}
		internal Board(IJsonBoard json, TrelloAuthorization auth)
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
			if (action.Type != ActionType.UpdateBoard || action.Data.Board == null || action.Data.Board.Id != Id) return;
			_context.Merge(((Board) action.Data.Board).Json);
		}
		/// <summary>
		/// Marks the board to be refreshed the next time data is accessed.
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
/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Card.cs
	Namespace:		Manatee.Trello
	Class Name:		Card
	Purpose:		Represents a card.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a card.
	/// </summary>
	public class Card : ICanWebhook, IQueryable
	{
		private readonly Field<Board> _board;
		private readonly Field<string> _description;
		private readonly Field<DateTime?> _dueDate;
		private readonly Field<bool?> _isArchived;
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
		/// Gets the collection of actions performed on this card.
		/// </summary>
		/// <remarks>By default imposed by Trello, this contains actions of types <see cref="ActionType.CommentCard"/> and <see cref="ActionType.UpdateCardIdList"/>.</remarks>
		public ReadOnlyActionCollection Actions { get; }
		/// <summary>
		/// Gets the collection of attachments contained in the card.
		/// </summary>
		public AttachmentCollection Attachments { get; }
		/// <summary>
		/// Gets the badges summarizing the content of the card.
		/// </summary>
		public Badges Badges { get; }
		/// <summary>
		/// Gets the board to which the card belongs.
		/// </summary>
		public Board Board => _board.Value;
		/// <summary>
		/// Gets the collection of checklists contained in the card.
		/// </summary>
		public CheckListCollection CheckLists { get; }
		/// <summary>
		/// Gets the collection of comments made on the card.
		/// </summary>
		public CommentCollection Comments { get; }
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
		public CardLabelCollection Labels { get; }
		/// <summary>
		/// Gets the most recent date of activity on the card.
		/// </summary>
		public DateTime? LastActivity => _lastActivity.Value;
		/// <summary>
		/// Gets or sets the list to the card belongs.
		/// </summary>
		public List List
		{
			get { return _list.Value; }
			set
			{
				_list.Value = value;
				_board.Value = value.Board;
			}
		}
		/// <summary>
		/// Gets the collection of members who are assigned to the card.
		/// </summary>
		public MemberCollection Members { get; }
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
		public CardStickerCollection Stickers { get; }
		/// <summary>
		/// Gets the card's full URL.
		/// </summary>
		/// <remarks>
		/// Trello will likely change this value as the name changes.  You can use <see cref="ShortUrl"/> for permalinks.
		/// </remarks>
		public string Url => _url.Value;
		public ReadOnlyMemberCollection VotingMembers { get; }

		/// <summary>
		/// Retrieves a check list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching check list, or null if none found.</returns>
		/// <remarks>
		/// Matches on CheckList.Id and CheckList.Name.  Comparison is case-sensitive.
		/// </remarks>
		public CheckList this[string key] => CheckLists[key];
		/// <summary>
		/// Retrieves the check list at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The check list.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		public CheckList this[int index] => CheckLists[index];

		internal IJsonCard Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

#if IOS
		private Action<Card, IEnumerable<string>> _updatedInvoker;

		/// <summary>
		/// Raised when data on the card is updated.
		/// </summary>
		public event Action<Card, IEnumerable<string>> Updated
		{
			add { _updatedInvoker += value; }
			remove { _updatedInvoker -= value; }
		}
#else
		/// <summary>
		/// Raised when data on the card is updated.
		/// </summary>
		public event Action<Card, IEnumerable<string>> Updated;
#endif

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
			_isArchived = new Field<bool?>(_context, nameof(IsArchived));
			_isArchived.AddRule(NullableHasValueRule<bool>.Instance);
			_isSubscribed = new Field<bool?>(_context, nameof(IsSubscribed));
			_isSubscribed.AddRule(NullableHasValueRule<bool>.Instance);
			Labels = new CardLabelCollection(_context, auth);
			_lastActivity = new Field<DateTime?>(_context, nameof(LastActivity));
			_list = new Field<List>(_context, nameof(List));
			_list.AddRule(NotNullRule<List>.Instance);
			Members = new MemberCollection(EntityRequestType.Card_Read_Members, () => Id, auth);
			_name = new Field<string>(_context, nameof(Name));
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, nameof(Position));
			_position.AddRule(PositionRule.Instance);
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
		public void ApplyAction(Action action)
		{
			if (action.Type != ActionType.UpdateCard || action.Data.Card == null || action.Data.Card.Id != Id) return;
			_context.Merge(action.Data.Card.Json);
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
#if IOS
			var handler = _updatedInvoker;
#else
			var handler = Updated;
#endif
			handler?.Invoke(this, properties);
		}
	}
}
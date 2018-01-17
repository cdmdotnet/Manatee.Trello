using System;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a collection of badges which summarize the contents of a card.
	/// </summary>
	public class Badges
	{
		private readonly Field<int?> _attachments;
		private readonly Field<int?> _checkItems;
		private readonly Field<int?> _checkItemsChecked;
		private readonly Field<int?> _comments;
		private readonly Field<DateTime?> _dueDate;
		private readonly Field<string> _fogBugz;
		private readonly Field<bool?> _hasDescription;
		private readonly Field<bool?> _hasVoted;
		private readonly Field<bool?> _isComplete;
		private readonly Field<bool?> _isSubscribed;
		private readonly Field<int?> _votes;
		private readonly BadgesContext _context;

		/// <summary>
		/// Gets the number of attachments on this card.
		/// </summary>
		public virtual int? Attachments => _attachments.Value;
		/// <summary>
		/// Gets the number of check items on this card.
		/// </summary>
		public virtual int? CheckItems => _checkItems.Value;
		/// <summary>
		/// Gets the number of check items on this card which are checked.
		/// </summary>
		public virtual int? CheckItemsChecked => _checkItemsChecked.Value;
		/// <summary>
		/// Gets the number of comments on this card.
		/// </summary>
		public virtual int? Comments => _comments.Value;
		/// <summary>
		/// Gets the due date for this card.
		/// </summary>
		public virtual DateTime? DueDate => _dueDate.Value;
		/// <summary>
		/// Gets some FogBugz information.
		/// </summary>
		public virtual string FogBugz => _fogBugz.Value;
		/// <summary>
		/// Gets whether this card has a description.
		/// </summary>
		public virtual bool? HasDescription => _hasDescription.Value;
		/// <summary>
		/// Gets whether the current member has voted for this card.
		/// </summary>
		public virtual bool? HasVoted => _hasVoted.Value;
		/// <summary>
		/// Gets wheterh this card has been marked complete.
		/// </summary>
		public virtual bool? IsComplete => _isComplete.Value;
		/// <summary>
		/// Gets whether the current member is subscribed to this card.
		/// </summary>
		public virtual bool? IsSubscribed => _isSubscribed.Value;
		/// <summary>
		/// Gets the number of votes for this card.
		/// </summary>
		public virtual int? Votes => _votes.Value;

		[Obsolete("This constructor is only for mocking purposes.")]
		public Badges(Badges doNotUse)
		{
		}
		internal Badges(BadgesContext context)
		{
			_context = context;

			_attachments = new Field<int?>(_context, nameof(Attachments));
			_checkItems = new Field<int?>(_context, nameof(CheckItems));
			_checkItemsChecked = new Field<int?>(_context, nameof(CheckItemsChecked));
			_comments = new Field<int?>(_context, nameof(Comments));
			_dueDate = new Field<DateTime?>(_context, nameof(DueDate));
			_fogBugz = new Field<string>(_context, nameof(FogBugz));
			_hasDescription = new Field<bool?>(_context, nameof(HasDescription));
			_hasVoted = new Field<bool?>(_context, nameof(HasVoted));
			_isComplete = new Field<bool?>(_context, nameof(IsComplete));
			_isSubscribed = new Field<bool?>(_context, nameof(IsSubscribed));
			_votes = new Field<int?>(_context, nameof(Votes));
		}
	}
}
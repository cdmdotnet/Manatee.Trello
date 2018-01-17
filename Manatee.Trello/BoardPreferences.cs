using System;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the preferences for a board.
	/// </summary>
	public class BoardPreferences
	{
		private readonly Field<BoardPermissionLevel?> _permissionLevel;
		private readonly Field<BoardVotingPermission?> _voting;
		private readonly Field<BoardCommentPermission?> _commenting;
		private readonly Field<BoardInvitationPermission?> _invitations;
		private readonly Field<bool?> _allowSelfJoin;
		private readonly Field<bool?> _showCardCovers;
		private readonly Field<bool?> _isCalendarFeedEnabled;
		private readonly Field<CardAgingStyle?> _cardAgingStyle;
		private readonly Field<BoardBackground> _background;
		private readonly BoardPreferencesContext _context;

		/// <summary>
		/// Gets or sets the general visibility of the board.
		/// </summary>
		public virtual BoardPermissionLevel? PermissionLevel
		{
			get { return _permissionLevel.Value; }
			set { _permissionLevel.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether voting is enabled and which members are allowed
		/// to vote. 
		/// </summary>
		public virtual BoardVotingPermission? Voting
		{
			get { return _voting.Value; }
			set { _voting.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether commenting is enabled and which members are
		/// allowed to add comments.
		/// </summary>
		public virtual BoardCommentPermission? Commenting
		{
			get { return _commenting.Value; }
			set { _commenting.Value = value; }
		}
		/// <summary>
		/// Gets or sets which members may invite others to the board.
		/// </summary>
		public virtual BoardInvitationPermission? Invitations
		{
			get { return _invitations.Value; }
			set { _invitations.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether any Trello member may join the board themselves
		/// or if an invitation must be sent.
		/// </summary>
		public virtual bool? AllowSelfJoin
		{
			get { return _allowSelfJoin.Value; }
			set { _allowSelfJoin.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether card covers are shown.
		/// </summary>
		public virtual bool? ShowCardCovers
		{
			get { return _showCardCovers.Value; }
			set { _showCardCovers.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether the calendar feed is enabled.
		/// </summary>
		public virtual bool? IsCalendarFeedEnabled
		{
			get { return _isCalendarFeedEnabled.Value; }
			set { _isCalendarFeedEnabled.Value = value; }
		}
		/// <summary>
		/// Gets or sets the card aging style for the Card Aging power up.
		/// </summary>
		public virtual CardAgingStyle? CardAgingStyle
		{
			get { return _cardAgingStyle.Value; }
			set { _cardAgingStyle.Value = value; }
		}
		/// <summary>
		/// Gets or sets the background of the board.
		/// </summary>
		public virtual BoardBackground Background
		{
			get { return _background.Value; }
			set { _background.Value = value; }
		}

		[Obsolete("This constructor is only for mocking purposes.")]
		public BoardPreferences(BoardPreferences doNotUse)
		{
		}
		internal BoardPreferences(BoardPreferencesContext context)
		{
			_context = context;

			_permissionLevel = new Field<BoardPermissionLevel?>(_context, nameof(PermissionLevel));
			_permissionLevel.AddRule(NullableHasValueRule<BoardPermissionLevel>.Instance);
			_permissionLevel.AddRule(EnumerationRule<BoardPermissionLevel?>.Instance);
			_voting = new Field<BoardVotingPermission?>(_context, nameof(Voting));
			_voting.AddRule(NullableHasValueRule<BoardVotingPermission>.Instance);
			_voting.AddRule(EnumerationRule<BoardVotingPermission?>.Instance);
			_commenting = new Field<BoardCommentPermission?>(_context, nameof(Commenting));
			_commenting.AddRule(NullableHasValueRule<BoardCommentPermission>.Instance);
			_commenting.AddRule(EnumerationRule<BoardCommentPermission?>.Instance);
			_invitations = new Field<BoardInvitationPermission?>(_context, nameof(Invitations));
			_invitations.AddRule(NullableHasValueRule<BoardInvitationPermission>.Instance);
			_invitations.AddRule(EnumerationRule<BoardInvitationPermission?>.Instance);
			_allowSelfJoin = new Field<bool?>(_context, nameof(AllowSelfJoin));
			_allowSelfJoin.AddRule(NullableHasValueRule<bool>.Instance);
			_showCardCovers = new Field<bool?>(_context, nameof(ShowCardCovers));
			_showCardCovers.AddRule(NullableHasValueRule<bool>.Instance);
			_isCalendarFeedEnabled = new Field<bool?>(_context, nameof(IsCalendarFeedEnabled));
			_isCalendarFeedEnabled.AddRule(NullableHasValueRule<bool>.Instance);
			_cardAgingStyle = new Field<CardAgingStyle?>(_context, nameof(CardAgingStyle));
			_cardAgingStyle.AddRule(NullableHasValueRule<CardAgingStyle>.Instance);
			_cardAgingStyle.AddRule(EnumerationRule<CardAgingStyle?>.Instance);
			_background = new Field<BoardBackground>(_context, nameof(Background));
			_background.AddRule(NotNullRule<BoardBackground>.Instance);
		}
	}
}
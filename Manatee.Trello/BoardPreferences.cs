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
 
	File Name:		BoardPreferences.cs
	Namespace:		Manatee.Trello
	Class Name:		BoardPreferences
	Purpose:		Represents the preferences for a board.

***************************************************************************************/

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
		public BoardPermissionLevel? PermissionLevel
		{
			get { return _permissionLevel.Value; }
			set { _permissionLevel.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether voting is enabled and which members are allowed
		/// to vote. 
		/// </summary>
		public BoardVotingPermission? Voting
		{
			get { return _voting.Value; }
			set { _voting.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether commenting is enabled and which members are
		/// allowed to add comments.
		/// </summary>
		public BoardCommentPermission? Commenting
		{
			get { return _commenting.Value; }
			set { _commenting.Value = value; }
		}
		/// <summary>
		/// Gets or sets which members may invite others to the board.
		/// </summary>
		public BoardInvitationPermission? Invitations
		{
			get { return _invitations.Value; }
			set { _invitations.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether any Trello member may join the board themselves
		/// or if an invitation must be sent.
		/// </summary>
		public bool? AllowSelfJoin
		{
			get { return _allowSelfJoin.Value; }
			set { _allowSelfJoin.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether card covers are shown.
		/// </summary>
		public bool? ShowCardCovers
		{
			get { return _showCardCovers.Value; }
			set { _showCardCovers.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether the calendar feed is enabled.
		/// </summary>
		public bool? IsCalendarFeedEnabled
		{
			get { return _isCalendarFeedEnabled.Value; }
			set { _isCalendarFeedEnabled.Value = value; }
		}
		/// <summary>
		/// Gets or sets the card aging style for the Card Aging power up.
		/// </summary>
		public CardAgingStyle? CardAgingStyle
		{
			get { return _cardAgingStyle.Value; }
			set { _cardAgingStyle.Value = value; }
		}
		/// <summary>
		/// Gets or sets the background of the board.
		/// </summary>
		public BoardBackground Background
		{
			get { return _background.Value; }
			set { _background.Value = value; }
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
/***************************************************************************************

	Copyright 2014 Greg Dennis

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
	Purpose:		Represents the preferences of a board.

***************************************************************************************/

using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
	public class BoardPreferences
	{
		private readonly Field<BoardPermissionLevel> _permissionLevel;
		private readonly Field<BoardVotingPermission> _voting;
		private readonly Field<BoardCommentPermission> _commenting;
		private readonly Field<BoardInvitationPermission> _invitations;
		private readonly Field<bool?> _allowSelfJoin;
		private readonly Field<bool?> _showCardCovers;
		private BoardPreferencesContext _context;

		public BoardPermissionLevel PermissionLevel
		{
			get { return _permissionLevel.Value; }
			set { _permissionLevel.Value = value; }
		}
		public BoardVotingPermission Voting
		{
			get { return _voting.Value; }
			set { _voting.Value = value; }
		}
		public BoardCommentPermission Commenting
		{
			get { return _commenting.Value; }
			set { _commenting.Value = value; }
		}
		public BoardInvitationPermission Invitations
		{
			get { return _invitations.Value; }
			set { _invitations.Value = value; }
		}
		public bool? AllowSelfJoin
		{
			get { return _allowSelfJoin.Value; }
			set { _allowSelfJoin.Value = value; }
		}
		public bool? ShowCardCovers
		{
			get { return _showCardCovers.Value; }
			set { _showCardCovers.Value = value; }
		}

		internal BoardPreferences(BoardPreferencesContext context)
		{
			_context = context;

			_permissionLevel = new Field<BoardPermissionLevel>(_context, () => PermissionLevel);
			_permissionLevel.AddRule(EnumerationRule<BoardPermissionLevel>.Instance);
			_voting = new Field<BoardVotingPermission>(_context, () => Voting);
			_voting.AddRule(EnumerationRule<BoardVotingPermission>.Instance);
			_commenting = new Field<BoardCommentPermission>(_context, () => Commenting);
			_commenting.AddRule(EnumerationRule<BoardCommentPermission>.Instance);
			_invitations = new Field<BoardInvitationPermission>(_context, () => Invitations);
			_invitations.AddRule(EnumerationRule<BoardInvitationPermission>.Instance);
			_allowSelfJoin = new Field<bool?>(_context, () => AllowSelfJoin);
			_allowSelfJoin.AddRule(NullableHasValueRule<bool>.Instance);
			_showCardCovers = new Field<bool?>(_context, () => ShowCardCovers);
			_showCardCovers.AddRule(NullableHasValueRule<bool>.Instance);
		}
	}
}
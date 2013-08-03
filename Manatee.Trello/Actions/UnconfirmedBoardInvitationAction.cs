/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		UnconfirmedBoardInvitationAction.cs
	Namespace:		Manatee.Trello
	Class Name:		UnconfirmedBoardInvitationAction
	Purpose:		Indicates an unconfirmed meber was invited to a board.

***************************************************************************************/
using System.Diagnostics;

namespace Manatee.Trello
{
	/// <summary>
	/// Indicates an unconfirmed meber was invited to a board.
	/// </summary>
	public class UnconfirmedBoardInvitationAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private readonly string _boardName;
		private Member _member;
		private readonly string _memberId;
		private readonly string _memberName;
		private string _stringFormat;

		/// <summary>
		/// Gets the organization associated with the action.
		/// </summary>
		public Board Board
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null)
						? (_board = Svc.Retrieve<Board>(_boardId))
						: _board;
			}
		}

		/// <summary>
		/// Gets the member associated with the action.
		/// </summary>
		public Member InvitedMember
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return ((_member == null) || (_member.Id != _memberId)) && (Svc != null) ? (_member = Svc.Retrieve<Member>(_memberId)) : _member;
			}
		}

		/// <summary>
		/// Creates a new instance of the UnconfirmedBoardInvitationAction class.
		/// </summary>
		/// <param name="action"></param>
		public UnconfirmedBoardInvitationAction(Action action)
			: base(action.Svc, action.Id)
		{
			VerifyNotExpired();
			_boardId = action.Data.TryGetString("board", "id");
			_boardName = action.Data.TryGetString("board", "name");
			_memberId = action.Data.TryGetString("memberInvited", "id");
			_memberName = action.Data.TryGetString("memberInvited", "id");
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
			return _stringFormat ?? (_stringFormat = string.Format("{0} added unconfirmed user {1} to board {2} on {3}",
			                                                       MemberCreator.FullName,
			                                                       InvitedMember != null ? InvitedMember.FullName : _memberName,
																   Board != null ? Board.Name : _boardName,
																   Date));
		}
	}
}
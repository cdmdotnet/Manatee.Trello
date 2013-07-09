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
 
	File Name:		MakeNormalMemberOfBoardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		MakeNormalMemberOfBoardAction
	Purpose:		Indicates a member was added to a board with normal
					permissions.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a member was added to a board with normal permissions.
	/// </summary>
	public class MakeNormalMemberOfBoardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private Member _member;
		private readonly string _memberId;
		private readonly string _boardName;
		private string _stringFormat;

		/// <summary>
		/// Gets the board associated with the action.
		/// </summary>
		public Board Board
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null) ? (_board = Svc.Retrieve<Board>(_boardId)) : _board;
			}
		}
		/// <summary>
		/// Gets the member associated with the action.
		/// </summary>
		public Member Member
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return ((_member == null) || (_member.Id != _memberId)) && (Svc != null) ? (_member = Svc.Retrieve<Member>(_memberId)) : _member;
			}
		}

		/// <summary>
		/// Creates a new instance of the MakeNormalMemberOfBoardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public MakeNormalMemberOfBoardAction(Action action)
			: base(action.Svc, action.Id)
		{
			VerifyNotExpired();
			_boardId = action.Data.TryGetString("board","id");
			_boardName = action.Data.TryGetString("board","name");
			_memberId = action.Data.TryGetString("idMember");
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
			return _stringFormat ?? (_stringFormat = string.Format("{0} made {1} a normal member of board '{2}' on {3}",
			                                                       MemberCreator.FullName,
			                                                       Member.FullName,
			                                                       Board != null ? Board.Name : _boardName,
			                                                       Date));
		}
	}
}
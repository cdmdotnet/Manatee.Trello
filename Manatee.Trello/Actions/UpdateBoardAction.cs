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
 
	File Name:		UpdateBoardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		UpdateBoardAction
	Purpose:		Indicates a board was updated.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a board was updated.
	/// </summary>
	public class UpdateBoardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private readonly string _boardName;

		/// <summary>
		/// Gets the board associated with the action.
		/// </summary>
		public Board Board
		{
			get
			{
				VerifyNotExpired();
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null) ? (_board = Svc.Retrieve<Board>(_boardId)) : _board;
			}
		}

		/// <summary>
		/// Creates a new instance of the UpdateBoardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public UpdateBoardAction(Action action)
			: base(action.Svc, action.Id)
		{
			VerifyNotExpired();
			_boardId = action.Data.TryGetString("board", "id");
			_boardName = action.Data.TryGetString("board","name");
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
			return string.Format("{0} updated board '{1}' on {2}",
			                     MemberCreator.FullName,
			                     Board != null ? Board.Name : _boardName,
			                     Date);
		}
	}
}
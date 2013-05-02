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
 
	File Name:		MakeAdminOfBoardNotification.cs
	Namespace:		Manatee.Trello
	Class Name:		MakeAdminOfBoardNotification
	Purpose:		Provides notification that the current member was
					made an admin of a board.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Provides notification that the current member was made an admin of a board.
	/// </summary>
	public class MakeAdminOfBoardNotification : Notification
	{
		private Board _board;
		private readonly string _boardId;
		private readonly string _boardName;

		/// <summary>
		/// Gets the board associated with the notification.
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
		/// Creates a new instance of the MakeAdminOfBoardNotification class.
		/// </summary>
		/// <param name="notification">The base notification</param>
		public MakeAdminOfBoardNotification(Notification notification)
			: base(notification.Svc, notification.Id)
		{
			VerifyNotExpired();
			_boardId = notification.Data.TryGetString("board","id");
			_boardName = notification.Data.TryGetString("board","name");
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
			return string.Format("{0} made you an admin of board '{1}'.",
			                     MemberCreator.FullName,
								 Board != null ? Board.Name : _boardName);
		}
	}
}
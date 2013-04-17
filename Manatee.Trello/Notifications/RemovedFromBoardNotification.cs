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
 
	File Name:		RemovedFromBoardNotification.cs
	Namespace:		Manatee.Trello
	Class Name:		RemovedFromBoardNotification
	Purpose:		Provides notification that the current member was
					removed from a board.

***************************************************************************************/
using Manatee.Json.Extensions;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides notification that the current member was removed from a board.
	/// </summary>
	public class RemovedFromBoardNotification : Notification
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
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null) ? (_board = Svc.Get(Svc.RequestProvider.Create<Board>(_boardId))) : _board;
			}
		}

		/// <summary>
		/// Creates a new instance of the RemovedFromBoardNotification class.
		/// </summary>
		/// <param name="notification">The base notification</param>
		public RemovedFromBoardNotification(Notification notification)
			: base(notification.Svc, notification.Id)
		{
			Refresh(notification);
			_boardId = notification.Data.Object.TryGetObject("board").TryGetString("id");
			_boardName = notification.Data.Object.TryGetObject("board").TryGetString("name");
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
			return string.Format("{0} removed you from board '{1}'.",
			                     MemberCreator.FullName,
								 Board != null ? Board.Name : _boardName);
		}
	}
}
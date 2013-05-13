﻿/***************************************************************************************

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
 
	File Name:		CreateCardNotification.cs
	Namespace:		Manatee.Trello
	Class Name:		CreateCardNotification
	Purpose:		Provides notification that another member created a card.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Provides notification that another member created a card.
	/// </summary>
	public class CreateCardNotification : Notification
	{
		private Board _board;
		private readonly string _boardId;
		private readonly string _boardName;
		private List _list;
		private readonly string _listId;
		private readonly string _listName;
		private Card _card;
		private readonly string _cardId;
		private readonly string _cardName;
		private string _stringFormat;

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
		/// Gets the list associated with the notification.
		/// </summary>
		public List List
		{
			get
			{
				VerifyNotExpired();
				return ((_list == null) || (_list.Id != _listId)) && (Svc != null) ? (_list = Svc.Retrieve<List>(_listId)) : _list;
			}
		}
		/// <summary>
		/// Gets the card associated with the notification.
		/// </summary>
		public Card Card
		{
			get
			{
				VerifyNotExpired();
				return ((_card == null) || (_card.Id != _cardId)) && (Svc != null) ? (_card = Svc.Retrieve<Card>(_cardId)) : _card;
			}
		}

		/// <summary>
		/// Creates a new instance of the CreateCardNotification class.
		/// </summary>
		/// <param name="notification">The base notification</param>
		public CreateCardNotification(Notification notification)
			: base(notification.Svc, notification.Id)
		{
			VerifyNotExpired();
			_boardId = notification.Data.TryGetString("board","id");
			_boardName = notification.Data.TryGetString("board","name");
			_listId = notification.Data.TryGetString("list","id");
			_listName = notification.Data.TryGetString("list","name");
			_cardId = notification.Data.TryGetString("card","id");
			_cardName = notification.Data.TryGetString("card","name");
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
			return _stringFormat ?? (_stringFormat = string.Format("{0} created card '{1}' in list '{2}'.",
			                                                       MemberCreator.FullName,
			                                                       Card != null ? Card.Name : _cardName,
			                                                       List != null ? List.Name : _listName));
		}
	}
}
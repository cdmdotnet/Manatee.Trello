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
 
	File Name:		AddedMemberToCardNotification.cs
	Namespace:		Manatee.Trello
	Class Name:		AddedMemberToCardNotification
	Purpose:		Provides notification that another member was
					added to a card.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Provides notification that another member was added to a card.
	/// </summary>
	public class AddedMemberToCardNotification : Notification
	{
		private Board _board;
		private readonly string _boardId;
		private Card _card;
		private readonly string _cardId;
		private Member _member;
		private readonly string _memberId;
		private readonly string _cardName;

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
		/// Gets the member associated with the notification.
		/// </summary>
		public Member Member
		{
			get
			{
				VerifyNotExpired();
				return ((_member == null) || (_member.Id != _memberId)) && (Svc != null) ? (_member = Svc.Retrieve<Member>(_memberId)) : _member;
			}
		}

		/// <summary>
		/// Creates a new instance of the AddedMemberToCardNotification class.
		/// </summary>
		/// <param name="notification">The base notification</param>
		public AddedMemberToCardNotification(Notification notification)
			: base(notification.Svc, notification.Id)
		{
			VerifyNotExpired();
			_boardId = notification.Data.TryGetString("board","id");
			_cardId = notification.Data.TryGetString("card","id");
			_cardName = notification.Data.TryGetString("card","name");
			_memberId = notification.Data.TryGetString("member","id");
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
			return string.Format("{0} added {1} to card '{2}'.",
								 MemberCreator.FullName,
								 Member.FullName,
								 Card != null ? Card.Name : _cardName);
		}
	}
}
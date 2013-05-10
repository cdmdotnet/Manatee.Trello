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
 
	File Name:		AddAttachmentToCardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		AddAttachmentToCardAction
	Purpose:		Indicates an attachment was added to a card.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates an attachment was added to a card.
	/// </summary>
	public class AddAttachmentToCardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private Card _card;
		private readonly string _cardId;
		private readonly string _cardName;
		private Attachment _attachment;
		private readonly string _attachmentId;
		private readonly string _attachmentName;

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
		/// Gets the card associated with the action.
		/// </summary>
		public Card Card
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return ((_card == null) || (_card.Id != _cardId)) && (Svc != null) ? (_card = Svc.Retrieve<Card>(_cardId)) : _card;
			}
		}
		/// <summary>
		/// Gets the attachment associated with the action.
		/// </summary>
		public Attachment Attachment
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return _attachment;
			}
		}

		/// <summary>
		/// Creates a new instance of the AddAttachmentToCardAction class.
		/// </summary>
		/// <param name="action"></param>
		public AddAttachmentToCardAction(Action action)
			: base(action.Svc, action.Id)
		{
			VerifyNotExpired();
			_boardId = action.Data.TryGetString("board", "id");
			_cardId = action.Data.TryGetString("card", "id");
			_cardName = action.Data.TryGetString("card", "name");
			_attachmentId = action.Data.TryGetString("attachment", "id");
			_attachmentName = action.Data.TryGetString("attachment", "name");
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
			return string.Format("{0} added attachment '{1}' to card '{2}' on {3}",
								 MemberCreator.FullName,
								 Attachment != null ? Attachment.Name : _attachmentName,
								 Card != null ? Card.Name : _cardName,
								 Date);
		}
	}
}
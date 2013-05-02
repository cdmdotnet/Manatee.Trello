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
 
	File Name:		UpdateCardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		UpdateCardAction
	Purpose:		Indicates a card was moved or updated in some other
					fashion.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a card was moved or updated in some other fashion.
	/// </summary>
	public class UpdateCardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private List _listBefore;
		private readonly string _listBeforeId;
		private readonly string _listBeforeName;
		private List _listAfter;
		private readonly string _listAfterId;
		private readonly string _listAfterName;
		private Card _card;
		private readonly string _cardId;
		private readonly string _cardName;

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
		/// Gets the list associated with the action.
		/// </summary>
		public List ListBefore
		{
			get
			{
				VerifyNotExpired();
				if (_listBeforeId == null) return null;
				return ((_listBefore == null) || (_listBefore.Id != _listBeforeId)) && (Svc != null) ? (_listBefore = Svc.Retrieve<List>(_listBeforeId)) : _listBefore;
			}
		}
		/// <summary>
		/// Gets the list associated with the action.
		/// </summary>
		public List ListAfter
		{
			get
			{
				VerifyNotExpired();
				if (_listAfterId == null) return null;
				return ((_listAfter == null) || (_listAfter.Id != _listAfterId)) && (Svc != null) ? (_listAfter = Svc.Retrieve<List>(_listAfterId)) : _listAfter;
			}
		}
		/// <summary>
		/// Gets the card associated with the action.
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
		/// Creates a new instance of the UpdateCardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public UpdateCardAction(Action action)
			: base(action.Svc, action.Id)
		{
			VerifyNotExpired();
			_boardId = action.Data.TryGetString("board","id");
			_listBeforeId = action.Data.TryGetString("id");
			_listBeforeName = action.Data.TryGetString("listBefore","name");
			_listAfterId = action.Data.TryGetString("listAfter","id");
			_listAfterName = action.Data.TryGetString("listAfter","name");
			_cardId = action.Data.TryGetString("card","id");
			_cardName = action.Data.TryGetString("card","name");
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
			if (_listBeforeId != null)
				return string.Format("{0} moved card '{1}' from list '{2}' to list '{3}' on {4}",
				                     MemberCreator.FullName,
				                     Card != null ? Card.Name : _cardName,
				                     ListBefore != null ? ListBefore.Name : _listBeforeName,
				                     ListAfter != null ? ListAfter.Name : _listAfterName,
				                     Date);
			return string.Format("{0} updated card '{1}' on {2}",
									MemberCreator.FullName,
									Card != null ? Card.Name : _cardName,
									Date);
		}
	}
}

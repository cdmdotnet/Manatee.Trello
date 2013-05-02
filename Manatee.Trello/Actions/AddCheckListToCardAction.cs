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
 
	File Name:		AddCheckListToCardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		AddCheckListToCardAction
	Purpose:		Indicates a check list was added to a card.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a check list was added to a card.
	/// </summary>
	public class AddCheckListToCardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private CheckList _checkList;
		private readonly string _checkListId;
		private Card _card;
		private readonly string _cardId;
		private readonly string _checkListName;
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
		/// Gets the check list associated with the action.
		/// </summary>
		public CheckList CheckList
		{
			get
			{
				VerifyNotExpired();
				return ((_checkList == null) || (_checkList.Id != _checkListId)) && (Svc != null) ? (_checkList = Svc.Retrieve<CheckList>(_checkListId)) : _checkList;
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
		/// Creates a new instance of the AddCheckListToCardAction class.
		/// </summary>
		/// <param name="action"></param>
		public AddCheckListToCardAction(Action action)
		{
			VerifyNotExpired();
			_boardId = action.Data.TryGetString("board","id");
			_checkListId = action.Data.TryGetString("checklist","id");
			_checkListName = action.Data.TryGetString("checklist","name");
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
			return string.Format("{0} added check list '{1}' to card '{2}' on {3}",
								 MemberCreator.FullName,
								 CheckList != null ? CheckList.Name : _checkListName,
								 Card != null ? Card.Name : _cardName,
								 Date);
		}
	}
}
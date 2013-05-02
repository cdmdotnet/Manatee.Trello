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
 
	File Name:		UpdateCheckItemStateOnCardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		UpdateCheckItemStateOnCardAction
	Purpose:		Indicates a check item was either checked or unchecked.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a check item was either checked or unchecked.
	/// </summary>
	public class UpdateCheckItemStateOnCardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private CheckList _checkList;
		private readonly string _checkListId;
		private Card _card;
		private readonly string _cardId;
		private CheckItem _checkItem;
		private readonly string _checkItemId;
		private readonly string _cardName;
		private readonly string _checkItemName;

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
		/// Gets the check item associated with the action.
		/// </summary>
		public CheckItem CheckItem
		{
			get
			{
				VerifyNotExpired();
				return ((_checkItem == null) || (_checkItem.Id != _cardId)) && (Svc != null) ? (_checkItem = Svc.Retrieve<CheckItem>(_checkItemId)) : _checkItem;
			}
		}

		/// <summary>
		/// Creates a new instance of the UpdateCheckItemStateOnCardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public UpdateCheckItemStateOnCardAction(Action action)
			: base(action.Svc, action.Id)
		{
			VerifyNotExpired();
			_boardId = action.Data.TryGetString("board", "id");
			_checkListId = action.Data.TryGetString("checklist","id");
			_cardId = action.Data.TryGetString("card","id");
			_cardName = action.Data.TryGetString("card","name");
			_checkItemId = action.Data.TryGetString("checkItem","id");
			_checkItemName = action.Data.TryGetString("checkItem","name");
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
			return string.Format("{0} updated check item '{1}' on card '{2}' on {3}",
								 MemberCreator.FullName,
								 CheckItem!= null ? CheckItem.Name : _checkItemName,
								 Card != null ? Card.Name : _cardName,
								 Date);
		}
	}
}
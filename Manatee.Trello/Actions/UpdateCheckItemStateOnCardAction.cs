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
using Manatee.Json.Extensions;

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
		private readonly CheckItem _checkItem;

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
		public CheckItem CheckItem { get { return _checkItem; } }

		/// <summary>
		/// Creates a new instance of the UpdateCheckItemStateOnCardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public UpdateCheckItemStateOnCardAction(Action action)
			: base(action.Svc, action.Id)
		{
			_boardId = action.Data.Object.TryGetObject("board").TryGetString("id");
			_checkListId = action.Data.Object.TryGetObject("checklist").TryGetString("id");
			_cardId = action.Data.Object.TryGetObject("card").TryGetString("id");
			_checkItem = new CheckItem();
			_checkItem.FromJson(action.Data.Object.TryGetObject("checkItem"));
		}
	}
}
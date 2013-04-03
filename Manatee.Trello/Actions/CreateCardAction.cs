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
 
	File Name:		CreateCardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		CreateCardAction
	Purpose:		Indicates a card was added to a board.

***************************************************************************************/
using Manatee.Json.Extensions;

namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a card was added to a board.
	/// </summary>
	public class CreateCardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private List _list;
		private readonly string _listId;
		private Card _card;
		private readonly string _cardId;

		/// <summary>
		/// Gets the board associated with the action.
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
		/// Gets the list associated with the action.
		/// </summary>
		public List List
		{
			get
			{
				VerifyNotExpired();
				return ((_list == null) || (_list.Id != _listId)) && (Svc != null) ? (_list = Svc.Get(Svc.RequestProvider.Create<List>(_listId))) : _list;
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
				return ((_card == null) || (_card.Id != _cardId)) && (Svc != null) ? (_card = Svc.Get(Svc.RequestProvider.Create<Card>(_cardId))) : _card;
			}
		}

		/// <summary>
		/// Creates a new instance of the CreateCardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public CreateCardAction(Action action)
			: base(action.Svc, action.Id)
		{
			_boardId = action.Data.Object.TryGetObject("board").TryGetString("id");
			_listId = action.Data.Object.TryGetObject("list").TryGetString("id");
			_cardId = action.Data.Object.TryGetObject("card").TryGetString("id");
		}
	}
}
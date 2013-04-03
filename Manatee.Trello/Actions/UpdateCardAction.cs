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
using Manatee.Json.Extensions;

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
		private List _listAfter;
		private readonly string _listAfterId;
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
		public List ListBefore
		{
			get
			{
				VerifyNotExpired();
				if (_listBeforeId == null) return null;
				return ((_listBefore == null) || (_listBefore.Id != _listBeforeId)) && (Svc != null) ? (_listBefore = Svc.Get(Svc.RequestProvider.Create<List>(_listBeforeId))) : _listBefore;
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
				return ((_listAfter == null) || (_listAfter.Id != _listAfterId)) && (Svc != null) ? (_listAfter = Svc.Get(Svc.RequestProvider.Create<List>(_listAfterId))) : _listAfter;
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
		/// Creates a new instance of the UpdateCardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public UpdateCardAction(Action action)
			: base(action.Svc, action.Id)
		{
			_boardId = action.Data.Object.TryGetObject("board").TryGetString("id");
			_listBeforeId = action.Data.Object.TryGetObject("listBefore").TryGetString("id");
			_listAfterId = action.Data.Object.TryGetObject("listAfter").TryGetString("id");
			_cardId = action.Data.Object.TryGetObject("card").TryGetString("id");
		}
	}
}

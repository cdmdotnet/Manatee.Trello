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
 
	File Name:		MoveCardFromBoardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		MoveCardFromBoardAction
	Purpose:		Indicates a card was removed from a board.

***************************************************************************************/
using Manatee.Json.Extensions;

namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a card was removed from a board.
	/// </summary>
	public class MoveCardFromBoardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private Board _boardTarget;
		private readonly string _boardTargetId;
		private Card _card;
		private readonly string _cardId;
		private readonly string _boardName;
		private readonly string _boardTargetName;
		private readonly string _cardName;

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
		/// Gets the board associated with the action.
		/// </summary>
		public Board BoardTarget
		{
			get
			{
				VerifyNotExpired();
				return ((_boardTarget == null) || (_boardTarget.Id != _boardTargetId)) && (Svc != null) ? (_boardTarget = Svc.Get(Svc.RequestProvider.Create<Board>(_boardTargetId))) : _boardTarget;
			}
		}
		/// <summary>
		/// Gets the list associated with the action.
		/// </summary>
		public Card Card
		{
			get
			{
				VerifyNotExpired();
				if (_cardId == null) return null;
				return ((_card == null) || (_card.Id != _cardId)) && (Svc != null) ? (_card = Svc.Get(Svc.RequestProvider.Create<Card>(_cardId))) : _card;
			}
		}

		/// <summary>
		/// Creates a new instance of the MoveCardFromBoardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public MoveCardFromBoardAction(Action action)
			: base(action.Svc, action.Id)
		{
			Refresh(action);
			_boardId = action.Data.Object.TryGetObject("board").TryGetString("id");
			_boardName = action.Data.Object.TryGetObject("board").TryGetString("id");
			_boardTargetId = action.Data.Object.TryGetObject("boardTarget").TryGetString("id");
			_boardTargetName = action.Data.Object.TryGetObject("boardTarget").TryGetString("id");
			_cardId = action.Data.Object.TryGetObject("card").TryGetString("id");
			_cardName = action.Data.Object.TryGetObject("card").TryGetString("id");
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
			return string.Format("{0} moved list '{1}' from board '{2}' to board '{3}' on {4}",
								 MemberCreator.FullName,
								 Card != null ? Card.Name : _cardName,
								 Board != null ? Board.Name : _boardName,
								 BoardTarget != null ? BoardTarget.Name : _boardTargetName,
								 Date);
		}
	}
}
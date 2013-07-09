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
 
	File Name:		MoveCardToBoardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		MoveCardToBoardAction
	Purpose:		Indicates a card was moved to a board.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a card was moved to a board.
	/// </summary>
	public class MoveCardToBoardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private Board _boardSource;
		private readonly string _boardSourceId;
		private Card _card;
		private readonly string _cardId;
		private readonly string _boardName;
		private readonly string _boardSourceName;
		private readonly string _cardName;
		private string _stringFormat;

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
		/// Gets the board associated with the action.
		/// </summary>
		public Board BoardSource
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return ((_boardSource == null) || (_boardSource.Id != _boardSourceId)) && (Svc != null) ? (_boardSource = Svc.Retrieve<Board>(_boardSourceId)) : _boardSource;
			}
		}
		/// <summary>
		/// Gets the list associated with the action.
		/// </summary>
		public Card Card
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				if (_cardId == null) return null;
				return ((_card == null) || (_card.Id != _cardId)) && (Svc != null) ? (_card = Svc.Retrieve<Card>(_cardId)) : _card;
			}
		}

		/// <summary>
		/// Creates a new instance of the MoveCardToBoardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public MoveCardToBoardAction(Action action)
			: base(action.Svc, action.Id)
		{
			VerifyNotExpired();
			_boardId = action.Data.TryGetString("board", "id");
			_boardName = action.Data.TryGetString("board", "name");
			_boardSourceId = action.Data.TryGetString("boardSource", "id");
			_boardSourceName = action.Data.TryGetString("boardSource", "name");
			_cardId = action.Data.TryGetString("id");
			_cardName = action.Data.TryGetString("card", "name");
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
			return _stringFormat ?? (_stringFormat = string.Format("{0} moved card '{1}' from board '{2}' to board '{3}' on {4}",
			                                                       MemberCreator.FullName,
			                                                       Card != null ? Card.Name : _cardName,
			                                                       BoardSource != null ? BoardSource.Name : _boardSourceName,
			                                                       Board != null ? Board.Name : _boardName,
			                                                       Date));
		}
	}
}
/***************************************************************************************

	Copyright 2012 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		DeleteCardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		DeleteCardAction
	Purpose:		Indicates a card was deleted.

***************************************************************************************/

namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a card was deleted.
	/// </summary>
	public class DeleteCardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private List _list;
		private readonly string _listId;
		private readonly string _cardId;
		private readonly int? _cardShortId;
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
		/// Gets the list associated with the action.
		/// </summary>
		public List List
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return ((_list == null) || (_list.Id != _listId)) && (Svc != null) ? (_list = Svc.Retrieve<List>(_listId)) : _list;
			}
		}
		/// <summary>
		/// Gets the card associated with the action.
		/// </summary>
		public string CardId
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return _cardId;
			}
		}
		/// <summary>
		/// Gets the card number (ShortID) of the card associated with the action.
		/// </summary>
		public int? CardNumber
		{
			get
			{
				if (_isDeleted) return null;
				return _cardShortId;
			}
		}

		/// <summary>
		/// Creates a new instance of the AddAttachmentToCardAction class.
		/// </summary>
		/// <param name="action"></param>
		public DeleteCardAction(Action action)
			: base(action.Svc, action.Id)
		{
			VerifyNotExpired();
			_boardId = action.Data.TryGetString("board", "id");
			_listId = action.Data.TryGetString("list", "id");
			_cardId = action.Data.TryGetString("card", "id");
			_cardShortId = (int?) action.Data.TryGetNumber("card", "idShort");
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
			return _stringFormat ?? (_stringFormat = string.Format("{0} deleted card #{1} on {2}",
			                                                       MemberCreator.FullName,
			                                                       _cardShortId,
			                                                       Date));
		}
	}
}
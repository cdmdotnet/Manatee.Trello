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
 
	File Name:		RemoveMemberFromCardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		RemoveMemberFromCardAction
	Purpose:		Indicates a member was removed from a card.

***************************************************************************************/
using Manatee.Json.Extensions;

namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a member was removed from a card.
	/// </summary>
	public class RemoveMemberFromCardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private Card _card;
		private readonly string _cardId;
		private Member _member;
		private readonly string _memberId;
		private readonly bool? _isDeactivated;
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
		/// Indicates whether the action was caused by member deactivation.
		/// </summary>
		public bool? IsDeactivated { get { return _isDeactivated; } }
		/// <summary>
		/// Gets the member associated with the action.
		/// </summary>
		public Member Member
		{
			get
			{
				VerifyNotExpired();
				return ((_member == null) || (_member.Id != _memberId)) && (Svc != null) ? (_member = Svc.Get(Svc.RequestProvider.Create<Member>(_memberId))) : _member;
			}
		}

		/// <summary>
		/// Creates a new instance of the RemoveMemberFromCardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public RemoveMemberFromCardAction(Action action)
			: base(action.Svc, action.Id)
		{
			Refresh(action);
			_boardId = action.Data.Object.TryGetObject("board").TryGetString("id");
			_cardId = action.Data.Object.TryGetObject("card").TryGetString("id");
			_cardName = action.Data.Object.TryGetObject("card").TryGetString("name");
			_isDeactivated = action.Data.Object.TryGetBoolean("deactivated");
			_memberId = action.Data.Object.TryGetString("idMember");
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
			return string.Format("{0} removed {1} from card '{2}' on {3}",
			                     MemberCreator.FullName,
								 Member.FullName,
								 Card != null ? Card.Name : _cardName,
								 Date);
		}
	}
}
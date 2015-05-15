/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Action.cs
	Namespace:		Manatee.Trello
	Class Name:		Action
	Purpose:		Represents an action performed on Trello objects.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents an action performed on Trello objects.
	/// </summary>
	public class Action : ICacheable
	{
		private static readonly Dictionary<ActionType, Func<Action, string>> _stringDefinitions;

		private readonly Field<Member> _creator;
		private readonly Field<DateTime?> _date;
		private readonly Field<ActionType?> _type;
		private readonly ActionContext _context;
		private string _id;

		/// <summary>
		/// Gets the member who performed the action.
		/// </summary>
		public Member Creator { get { return _creator.Value; } }
		/// <summary>
		/// Gets any data associated with the action.
		/// </summary>
		public ActionData Data { get; private set; }
		/// <summary>
		/// Gets the date and time at which the action was performed.
		/// </summary>
		public DateTime? Date { get { return _date.Value; } }
		/// <summary>
		/// Gets the action's ID.
		/// </summary>
		public string Id
		{
			get
			{
				if (!_context.HasValidId)
					_context.Synchronize();
				return _id;
			}
			private set { _id = value; }
		}
		/// <summary>
		/// Gets the type of action.
		/// </summary>
		public ActionType? Type { get { return _type.Value; } }

		internal IJsonAction Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

#if IOS
		private Action<Action, IEnumerable<string>> _updatedInvoker;

		/// <summary>
		/// Raised when data on the action is updated.
		/// </summary>
		public event Action<Action, IEnumerable<string>> Updated
		{
			add { _updatedInvoker += value; }
			remove { _updatedInvoker -= value; }
		}
#else
		/// <summary>
		/// Raised when data on the action is updated.
		/// </summary>
		public event Action<Action, IEnumerable<string>> Updated;
#endif

		static Action()
		{
			_stringDefinitions = new Dictionary<ActionType, Func<Action, string>>
				{
					{ActionType.AddAttachmentToCard, a => string.Format("{0} attached {1} to card {2}.", a.Creator, a.Data.Attachment, a.Data.Card)},
					{ActionType.AddChecklistToCard, a => string.Format("{0} added checklist {1} to card {2}.", a.Creator, a.Data.CheckList, a.Data.Card)},
					{ActionType.AddMemberToBoard, a => string.Format("{0} added member {1} to board {2}.", a.Creator, a.Data.Member, a.Data.Board)},
					{ActionType.AddMemberToCard, a => string.Format("{0} assigned member {1} to card {2}.", a.Creator, a.Data.Member, a.Data.Card)},
					{ActionType.AddMemberToOrganization, a => string.Format("{0} added member {1} to organization {2}.", a.Creator, a.Data.Member, a.Data.Organization)},
					{ActionType.AddToOrganizationBoard, a => string.Format("{0} moved board {1} into organization {2}.", a.Creator, a.Data.Board, a.Data.Organization)},
					{ActionType.CommentCard, a => string.Format("{0} commented on card {1}: '{2}'.", a.Creator, a.Data.Card, a.Data.Text)},
					{ActionType.ConvertToCardFromCheckItem, a => string.Format("{0} converted checkitem {1} to a card.", a.Creator, a.Data.Card)},
					{ActionType.CopyBoard, a => string.Format("{0} copied board {1} from board {2}.", a.Creator, a.Data.Board, a.Data.BoardSource)},
					{ActionType.CopyCard, a => string.Format("{0} copied card {1} from card {2}.", a.Creator, a.Data.Card, a.Data.CardSource)},
					{ActionType.CopyCommentCard, a => string.Format("{0} copied a comment from {1}.", a.Creator, a.Data.Card)},
					{ActionType.CreateBoard, a => string.Format("{0} created board {1}.", a.Creator, a.Data.Board)},
					{ActionType.CreateCard, a => string.Format("{0} created card {1}.", a.Creator, a.Data.Card)},
					{ActionType.CreateList, a => string.Format("{0} created list {1}.", a.Creator, a.Data.List)},
					{ActionType.CreateOrganization, a => string.Format("{0} created organization {1}.", a.Creator, a.Data.Organization)},
					{ActionType.DeleteAttachmentFromCard, a => string.Format("{0} removed attachment {1} from card {2}.", a.Creator, a.Data.Attachment, a.Data.Card)},
					{ActionType.DeleteBoardInvitation, a => string.Format("{0} rescinded an invitation.", a.Creator)},
					{ActionType.DeleteCard, a => string.Format("{0} deleted card {1} from {2}.", a.Creator, a.Data.Card, a.Data.Board)},
					{ActionType.DeleteOrganizationInvitation, a => string.Format("{0} rescinded an invitation.", a.Creator)},
					{ActionType.DisablePowerUp, a => string.Format("{0} disabled power-up {1}.", a.Creator, a.Data.Value)},
					{ActionType.EmailCard, a => string.Format("{0} added card {1} by email.", a.Creator, a.Data.Card)},
					{ActionType.EnablePowerUp, a => string.Format("{0} enabled power-up {1}.", a.Creator, a.Data.Value)},
					{ActionType.MakeAdminOfBoard, a => string.Format("{0} made member {1} an admin of board {2}.", a.Creator, a.Data.Member, a.Data.Board)},
					{ActionType.MakeNormalMemberOfBoard, a => string.Format("{0} made member {1} a normal user of board {2}.", a.Creator, a.Data.Member, a.Data.Board)},
					{ActionType.MakeNormalMemberOfOrganization, a => string.Format("{0} made member {1} a normal user of organization {2}.", a.Creator, a.Data.Member, a.Data.Organization)},
					{ActionType.MakeObserverOfBoard, a => string.Format("{0} made member {1} an observer of board {2}.", a.Creator, a.Data.Member, a.Data.Board)},
					{ActionType.MemberJoinedTrello, a => string.Format("{0} joined Trello!.", a.Creator)},
					{ActionType.MoveCardFromBoard, a => string.Format("{0} moved card {1} from board {2} to board {3}.", a.Creator, a.Data.Card, a.Data.Board, a.Data.BoardTarget)},
					{ActionType.MoveCardToBoard, a => string.Format("{0} moved card {1} from board {2} to board {3}.", a.Creator, a.Data.Card, a.Data.BoardSource, a.Data.Board)},
					{ActionType.MoveListFromBoard, a => string.Format("{0} moved list {1} from board {2}.", a.Creator, a.Data.List, a.Data.Board)},
					{ActionType.MoveListToBoard, a => string.Format("{0} moved list {1} to board {2}.", a.Creator, a.Data.List, a.Data.Board)},
					{ActionType.RemoveChecklistFromCard, a => string.Format("{0} deleted checklist {1} from card {2}.", a.Creator, a.Data.CheckList, a.Data.Card)},
					{ActionType.RemoveFromOrganizationBoard, a => string.Format("{0} removed board {1} from organization {2}.", a.Creator, a.Data.Board, a.Data.Organization)},
					{ActionType.RemoveMemberFromCard, a => string.Format("{0} removed member {1} from card {2}.", a.Creator, a.Data.Member, a.Data.Card)},
					{ActionType.UnconfirmedBoardInvitation, a => string.Format("{0} invited {1} to board {2}.", a.Creator, a.Data.Member, a.Data.Board)},
					{ActionType.UnconfirmedOrganizationInvitation, a => string.Format("{0} invited {1} to organization {2}.", a.Creator, a.Data.Member, a.Data.Organization)},
					{ActionType.UpdateBoard, a => string.Format("{0} updated board {1}.", a.Creator, a.Data.Board)},
					{ActionType.UpdateCard, a => string.Format("{0} updated card {1}.", a.Creator, a.Data.Card)},
					{ActionType.UpdateCardIdList, a => string.Format("{0} moved card {1} from list {2} to list {3}.", a.Creator, a.Data.Card, a.Data.ListBefore, a.Data.ListAfter)},
					{ActionType.UpdateCardClosed, a => string.Format("{0} archived card {1}.", a.Creator, a.Data.Card)},
					{ActionType.UpdateCardDesc, a => string.Format("{0} changed the description of card {1}.", a.Creator, a.Data.Card)},
					{ActionType.UpdateCardName, a => string.Format("{0} changed the name of card {1}.", a.Creator, a.Data.Card)},
					{ActionType.UpdateCheckItemStateOnCard, a => string.Format("{0} updated checkitem {1}.", a.Creator, a.Data.CheckItem)},
					{ActionType.UpdateChecklist, a => string.Format("{0} updated checklist {1}.", a.Creator, a.Data.CheckList)},
					{ActionType.UpdateListClosed, a => string.Format("{0} archived list {1}.", a.Creator, a.Data.List)},
					{ActionType.UpdateListName, a => string.Format("{0} changed the name of list {1}.", a.Creator, a.Data.List)},
					{ActionType.UpdateMember, a => string.Format("{0} updated their profile.", a.Creator)},
					{ActionType.UpdateOrganization, a => string.Format("{0} updated organization {1}.", a.Creator, a.Data.Organization)},
				};
		}
		/// <summary>
		/// Creates a new <see cref="Action"/> object.
		/// </summary>
		/// <param name="id">The action's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public Action(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new ActionContext(id, auth);
			_context.Synchronized += Synchronized;

			_creator = new Field<Member>(_context, () => Creator);
			_date = new Field<DateTime?>(_context, () => Date);
			Data = new ActionData(_context.ActionDataContext);
			_type = new Field<ActionType?>(_context, () => Type);

			TrelloConfiguration.Cache.Add(this);
		}
		internal Action(IJsonAction json, TrelloAuthorization auth)
			: this(json.Id, auth)
		{
			_context.Merge(json);
		}

		/// <summary>
		/// Deletes the card.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the card from Trello's server, however, this object will
		/// remain in memory and all properties will remain accessible.
		/// </remarks>
		public void Delete()
		{
			_context.Delete();
			TrelloConfiguration.Cache.Remove(this);
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
			return Type.HasValue ? _stringDefinitions[Type.Value](this) : "Action type could not be determined.";
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
#if IOS
			var handler = _updatedInvoker;
#else
			var handler = Updated;
#endif
			if (handler != null)
				handler(this, properties);
		}
	}
}
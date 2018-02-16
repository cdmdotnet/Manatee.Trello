using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Documents all of the activities in Trello.
	/// </summary>
	public class Action : ICacheable
	{
		[Flags]
		public enum Fields
		{
			[Display(Description="data")]
			Data = 1,
			[Display(Description="date")]
			Date = 1 << 1,
			[Display(Description="idMemberCreator")]
			Creator = 1 << 2,
			[Display(Description="type")]
			Type = 1 << 3
		}

		private static readonly Dictionary<ActionType, Func<Action, string>> _stringDefinitions;

		private readonly Field<Member> _creator;
		private readonly Field<DateTime?> _date;
		private readonly Field<ActionType?> _type;
		private readonly ActionContext _context;
		private string _id;
		private DateTime? _creation;

		public static Fields DownloadedFields { get; set; } = (Fields)Enum.GetValues(typeof(Fields)).Cast<int>().Sum();

		/// <summary>
		/// Gets the creation date.
		/// </summary>
		public DateTime CreationDate
		{
			get
			{
				if (_creation == null)
					_creation = Id.ExtractCreationDate();
				return _creation.Value;
			}
		}
		/// <summary>
		/// Gets the member who performed the action.
		/// </summary>
		public Member Creator => _creator.Value;
		/// <summary>
		/// Gets any associated data.
		/// </summary>
		public ActionData Data { get; }
		/// <summary>
		/// Gets the date and time at which the action was performed.
		/// </summary>
		public DateTime? Date => _date.Value;
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
		public ActionType? Type => _type.Value;

		internal IJsonAction Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		/// <summary>
		/// Raised when any data on the <see cref="Action"/> instance is updated.
		/// </summary>
		public event Action<Action, IEnumerable<string>> Updated;

		static Action()
		{
			_stringDefinitions = new Dictionary<ActionType, Func<Action, string>>
				{
					{ActionType.AddAttachmentToCard, a => $"{a.Creator} attached {a.Data.Attachment} to card {a.Data.Card}."},
					{ActionType.AddChecklistToCard, a => $"{a.Creator} added checklist {a.Data.CheckList} to card {a.Data.Card}."},
					{ActionType.AddMemberToBoard, a => $"{a.Creator} added member {a.Data.Member} to board {a.Data.Board}."},
					{ActionType.AddMemberToCard, a => $"{a.Creator} assigned member {a.Data.Member} to card {a.Data.Card}."},
					{ActionType.AddMemberToOrganization, a => $"{a.Creator} added member {a.Data.Member} to organization {a.Data.Organization}."},
					{ActionType.AddToOrganizationBoard, a => $"{a.Creator} moved board {a.Data.Board} into organization {a.Data.Organization}."},
					{ActionType.CommentCard, a => $"{a.Creator} commented on card {a.Data.Card}: '{a.Data.Text}'."},
					{ActionType.ConvertToCardFromCheckItem, a => $"{a.Creator} converted checkitem {a.Data.Card} to a card."},
					{ActionType.CopyBoard, a => $"{a.Creator} copied board {a.Data.Board} from board {a.Data.BoardSource}."},
					{ActionType.CopyCard, a => $"{a.Creator} copied card {a.Data.Card} from card {a.Data.CardSource}."},
					{ActionType.CopyCommentCard, a => $"{a.Creator} copied a comment from {a.Data.Card}: '{a.Data.Text}'."},
					{ActionType.CreateBoard, a => $"{a.Creator} created board {a.Data.Board}."},
					{ActionType.CreateCard, a => $"{a.Creator} created card {a.Data.Card}."},
					{ActionType.CreateList, a => $"{a.Creator} created list {a.Data.List}."},
					{ActionType.CreateOrganization, a => $"{a.Creator} created organization {a.Data.Organization}."},
					{ActionType.DeleteAttachmentFromCard, a => $"{a.Creator} removed attachment {a.Data.Attachment} from card {a.Data.Card}."},
					{ActionType.DeleteBoardInvitation, a => $"{a.Creator} rescinded an invitation."},
					{ActionType.DeleteCard, a => $"{a.Creator} deleted card #{a.Data.Card.Json.IdShort} from {a.Data.Board}."},
					{ActionType.DeleteOrganizationInvitation, a => $"{a.Creator} rescinded an invitation."},
					{ActionType.DisablePowerUp, a => $"{a.Creator} disabled power-up {a.Data.Value}."},
					{ActionType.EmailCard, a => $"{a.Creator} added card {a.Data.Card} by email."},
					{ActionType.EnablePowerUp, a => $"{a.Creator} enabled power-up {a.Data.Value}."},
					{ActionType.MakeAdminOfBoard, a => $"{a.Creator} made member {a.Data.Member} an admin of board {a.Data.Board}."},
					{ActionType.MakeNormalMemberOfBoard, a => $"{a.Creator} made member {a.Data.Member} a normal user of board {a.Data.Board}."},
					{ActionType.MakeNormalMemberOfOrganization, a => $"{a.Creator} made member {a.Data.Member} a normal user of organization {a.Data.Organization}."},
					{ActionType.MakeObserverOfBoard, a => $"{a.Creator} made member {a.Data.Member} an observer of board {a.Data.Board}."},
					{ActionType.MemberJoinedTrello, a => $"{a.Creator} joined Trello!."},
					{ActionType.MoveCardFromBoard, a => $"{a.Creator} moved card {a.Data.Card} from board {a.Data.Board} to board {a.Data.BoardTarget}."},
					{ActionType.MoveCardToBoard, a => $"{a.Creator} moved card {a.Data.Card} from board {a.Data.BoardSource} to board {a.Data.Board}."},
					{ActionType.MoveListFromBoard, a => $"{a.Creator} moved list {a.Data.List} from board {a.Data.Board}."},
					{ActionType.MoveListToBoard, a => $"{a.Creator} moved list {a.Data.List} to board {a.Data.Board}."},
					{ActionType.RemoveChecklistFromCard, a => $"{a.Creator} deleted checklist {a.Data.CheckList} from card {a.Data.Card}."},
					{ActionType.RemoveFromOrganizationBoard, a => $"{a.Creator} removed board {a.Data.Board} from organization {a.Data.Organization}."},
					{ActionType.RemoveMemberFromCard, a => $"{a.Creator} removed member {a.Data.Member} from card {a.Data.Card}."},
					{ActionType.UnconfirmedBoardInvitation, a => $"{a.Creator} invited {a.Data.Member} to board {a.Data.Board}."},
					{ActionType.UnconfirmedOrganizationInvitation, a => $"{a.Creator} invited {a.Data.Member} to organization {a.Data.Organization}."},
					{ActionType.UpdateBoard, a => $"{a.Creator} updated board {a.Data.Board}."},
					{ActionType.UpdateCard, a => $"{a.Creator} updated card {a.Data.Card}."},
					{ActionType.UpdateCardIdList, a => $"{a.Creator} moved card {a.Data.Card} from list {a.Data.ListBefore} to list {a.Data.ListAfter}."},
					{ActionType.UpdateCardClosed, a => $"{a.Creator} archived card {a.Data.Card}."},
					{ActionType.UpdateCardDesc, a => $"{a.Creator} changed the description of card {a.Data.Card}."},
					{ActionType.UpdateCardName, a => $"{a.Creator} changed the name of card {a.Data.Card}."},
					{ActionType.UpdateCheckItemStateOnCard, a => $"{a.Creator} updated checkitem {a.Data.CheckItem}."},
					{ActionType.UpdateChecklist, a => $"{a.Creator} updated checklist {a.Data.CheckList}."},
					{ActionType.UpdateList, a => $"{a.Creator} updated list {a.Data.List}."},
					{ActionType.UpdateListClosed, a => $"{a.Creator} archived list {a.Data.List}."},
					{ActionType.UpdateListName, a => $"{a.Creator} changed the name of list {a.Data.List}."},
					{ActionType.UpdateMember, a => $"{a.Creator} updated their profile."},
					{ActionType.UpdateOrganization, a => $"{a.Creator} updated organization {a.Data.Organization}."},
					{ActionType.EnablePlugin, a => $"{a.Creator} enabled plugin {a.Data.PowerUp}."},
					{ActionType.DisablePlugin, a => $"{a.Creator} disabled plugin {a.Data.PowerUp}."},
				};
		}
		/// <summary>
		/// Creates a new <see cref="Action"/> instance.
		/// </summary>
		/// <param name="id">The action's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided, <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public Action(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new ActionContext(id, auth);
			_context.Synchronized += Synchronized;

			_creator = new Field<Member>(_context, nameof(Creator));
			_date = new Field<DateTime?>(_context, nameof(Date));
			Data = new ActionData(_context.ActionDataContext);
			_type = new Field<ActionType?>(_context, nameof(Type));

			TrelloConfiguration.Cache.Add(this);
		}
		internal Action(IJsonAction json, TrelloAuthorization auth)
			: this(json.Id, auth)
		{
			_context.Merge(json);
		}

		/// <summary>
		/// Permanently deletes the action from Trello.
		/// </summary>
		/// <remarks>
		/// This instance will remain in memory and all properties will remain accessible.
		/// </remarks>
		public void Delete()
		{
			_context.Delete();
			TrelloConfiguration.Cache.Remove(this);
		}
		/// <summary>
		/// Returns a string that represents the action.  The content will vary based on the value of <see cref="Type"/>.
		/// </summary>
		/// <returns>
		/// A string that represents the action.
		/// </returns>
		public override string ToString()
		{
			return Type.HasValue && Type != ActionType.Unknown ? _stringDefinitions[Type.Value](this) : "Action type could not be determined.";
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}
}
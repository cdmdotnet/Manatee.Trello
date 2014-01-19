using System.Collections.Generic;

namespace Manatee.Trello
{
	public class Me : Member
	{
		private readonly MemberPreferences _preferences;

		/// <summary>
		/// Gets or sets the bio of the member.
		/// </summary>
		public new string Bio
		{
			get { return base.Bio; }
			set { base.Bio = value; }
		}
		/// <summary>
		/// Gets the member's registered email address.
		/// </summary>
		public new string Email { get { return base.Email; } }
		/// <summary>
		/// Gets the member's full name.
		/// </summary>
		public new string FullName
		{
			get { return base.FullName; }
			set { base.FullName = value; }
		}
		/// <summary>
		/// Gets or sets the member's initials.
		/// </summary>
		public new string Initials
		{
			get { return base.Initials; }
			set { base.Initials = value; }
		}
		/// <summary>
		/// Enumerates the member's notifications.
		/// </summary>
		public IEnumerable<Notification> Notifications { get { return BuildList<Notification>(EntityRequestType.Member_Read_Notifications); } }
		/// <summary>
		/// Enumerates the types of messages automatically dismissed for the user.
		/// </summary>
		public new IEnumerable<string> OneTimeMessagesDismissed { get { return base.OneTimeMessagesDismissed; } }
		/// <summary>
		/// Enumerates the boards the member has starred in their boards menu.
		/// </summary>
		public IEnumerable<Board> StarredBoards { get { return BuildList<Board>(EntityRequestType.Member_Read_StarredBoards); } }
		///<summary>
		/// Gets the set of preferences for the member.
		///</summary>
		public MemberPreferences Preferences { get { return _preferences; } }
		/// <summary>
		/// Enumerates the active sessions with trello.com.
		/// </summary>
		internal IEnumerable<MemberSession> Sessions { get { return BuildList<MemberSession>(EntityRequestType.Member_Read_Sessions); } }
		/// <summary>
		/// Enumerates the tokens provided by the member.
		/// </summary>
		public IEnumerable<Token> Tokens { get { return BuildList<Token>(EntityRequestType.Member_Read_Tokens); } }
		/// <summary>
		/// Gets or sets the member's username.
		/// </summary>
		public new string Username
		{
			get { return base.Username; }
			set { base.Username = value; }
		}

		public Me()
		{
			_preferences = new MemberPreferences(this);
		}

		/// <summary>
		/// Marks all unread notifications for the member as read.
		/// </summary>
		public void ClearNotifications()
		{
			Validator.Writable();
			EntityRepository.Upload(EntityRequestType.Member_Write_ClearNotifications, Parameters);
		}
		/// <summary>
		/// Creates a personal board for the current member.
		/// </summary>
		/// <param name="name">The name of the board.</param>
		/// <returns>The newly-created Board object.</returns>
		public Board CreateBoard(string name)
		{
			Validator.Writable();
			Validator.NonEmptyString(name);
			Parameters.Add("name", name);
			var board = EntityRepository.Download<Board>(EntityRequestType.Member_Write_CreateBoard, Parameters);
			UpdateDependencies(board);
			return board;
		}
		/// <summary>
		/// Creates an organization administered by the current member.
		/// </summary>
		/// <param name="displayName">The display name of the organization.</param>
		/// <returns>The newly-created Organization object.</returns>
		public Organization CreateOrganization(string displayName)
		{
			Validator.Writable();
			Validator.NonEmptyString(displayName);
			Parameters.Add("displayName", displayName);
			var org = EntityRepository.Download<Organization>(EntityRequestType.Member_Write_CreateOrganization, Parameters);
			UpdateDependencies(org);
			return org;
		}
		/// <summary>
		/// Adds a board to the member's boards menu.
		/// </summary>
		/// <param name="board">The board to pin.</param>
		public void PinBoard(Board board)
		{
			Validator.Writable();
			Validator.Entity(board);
			Parameters["_id"] = Id;
			Parameters.Add("value", board.Id);
			EntityRepository.Upload(EntityRequestType.Member_Write_PinBoard, Parameters);
		}
		/// <summary>
		/// Removes the member's vote from a card.
		/// </summary>
		/// <param name="card"></param>
		public void RescindVoteForCard(Card card)
		{
			Validator.Writable();
			Validator.Entity(card);
			Parameters.Add("_cardId", card.Id);
			Parameters["_id"] = Id;
			EntityRepository.Upload(EntityRequestType.Member_Write_RescindVoteForCard, Parameters);
		}
		/// <summary>
		/// Removes a board from the member's boards menu.
		/// </summary>
		/// <param name="board"></param>
		public void UnpinBoard(Board board)
		{
			Validator.Writable();
			Validator.Entity(board);
			Parameters["_id"] = Id;
			Parameters.Add("_boardId", board.Id);
			EntityRepository.Upload(EntityRequestType.Member_Write_UnpinBoard, Parameters);
		}
		/// <summary>
		/// Applies the member's vote to a card.
		/// </summary>
		/// <param name="card"></param>
		public void VoteForCard(Card card)
		{
			Validator.Writable();
			Validator.Entity(card);
			Parameters.Add("_cardId", card.Id);
			Parameters["_id"] = Id;
			EntityRepository.Upload(EntityRequestType.Member_Write_VoteForCard, Parameters);
		}

		internal override void PropagateDependencies()
		{
			UpdateDependencies(_preferences);
		}
	}
}

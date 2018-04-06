using System;
using System.Collections.Generic;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the permission level a member has on a board.
	/// </summary>
	public class BoardMembership : IBoardMembership
	{
		private readonly Field<Member> _member;
		private readonly Field<BoardMembershipType?> _memberType;
		private readonly Field<bool?> _isDeactivated;
		private readonly BoardMembershipContext _context;
		private DateTime? _creation;

		/// <summary>
		/// Gets the creation date of the membership.
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
		/// Gets the membership definition's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets whether the member has accepted the invitation to join Trello.
		/// </summary>
		public bool? IsDeactivated => _isDeactivated.Value;
		/// <summary>
		/// Gets the member.
		/// </summary>
		public IMember Member => _member.Value;
		/// <summary>
		/// Gets the membership's permission level.
		/// </summary>
		public BoardMembershipType? MemberType
		{
			get { return _memberType.Value; }
			set { _memberType.Value = value; }
		}

		internal IJsonBoardMembership Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		/// <summary>
		/// Raised when data on the membership is updated.
		/// </summary>
		public event Action<IBoardMembership, IEnumerable<string>> Updated;

		internal BoardMembership(IJsonBoardMembership json, string ownerId, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new BoardMembershipContext(Id, ownerId, auth);
			_context.Synchronized += Synchronized;

			_member = new Field<Member>(_context, nameof(Member));
			_memberType = new Field<BoardMembershipType?>(_context, nameof(MemberType));
			_memberType.AddRule(NullableHasValueRule<BoardMembershipType>.Instance);
			_memberType.AddRule(EnumerationRule<BoardMembershipType?>.Instance);
			_isDeactivated = new Field<bool?>(_context, nameof(IsDeactivated));

			TrelloConfiguration.Cache.Add(this);

			_context.Merge(json);
		}

		/// <summary>
		/// Marks the board membership to be refreshed the next time data is accessed.
		/// </summary>
		public void Refresh()
		{
			_context.Expire();
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}
}
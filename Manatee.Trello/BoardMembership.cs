/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		BoardMembership.cs
	Namespace:		Manatee.Trello
	Class Name:		BoardMembership
	Purpose:		Represents the permission level a member has on a board.

***************************************************************************************/

using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class BoardMembership
	{
		private readonly string _id;
		private readonly Field<Member> _member;
		private readonly Field<BoardMembershipType> _memberType;
		private readonly Field<bool?> _isDeactivated;
		private readonly BoardMembershipContext _context;

		public string Id { get { return _id; } }
		public bool? IsDeactivated { get { return _isDeactivated.Value; } }
		public Member Member { get { return _member.Value; } }
		public BoardMembershipType MemberType
		{
			get { return _memberType.Value; }
			set { _memberType.Value = value; }
		}

		internal BoardMembership(IJsonBoardMembership json, string ownerId)
		{
			_id = json.Id;
			_context = new BoardMembershipContext(_id, ownerId);

			_member = new Field<Member>(_context, () => Member);
			_memberType = new Field<BoardMembershipType>(_context, () => MemberType);
			_isDeactivated = new Field<bool?>(_context, () => IsDeactivated);

			TrelloConfiguration.Cache.Add(this);

			_context.Merge(json);
		}
	}
}
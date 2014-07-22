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

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class BoardMembership : ICacheable
	{
		private readonly Field<Member> _member;
		private readonly Field<BoardMembershipType> _memberType;
		private readonly Field<bool?> _isDeactivated;
		private readonly BoardMembershipContext _context;

		public string Id { get; private set; }
		public bool? IsDeactivated { get { return _isDeactivated.Value; } }
		public Member Member { get { return _member.Value; } }
		public BoardMembershipType MemberType
		{
			get { return _memberType.Value; }
			set { _memberType.Value = value; }
		}

		public event Action<BoardMembership, IEnumerable<string>> Updated;

		internal BoardMembership(IJsonBoardMembership json, string ownerId)
		{
			Id = json.Id;
			_context = new BoardMembershipContext(Id, ownerId);
			_context.Synchronized += Synchronized;

			_member = new Field<Member>(_context, () => Member);
			_memberType = new Field<BoardMembershipType>(_context, () => MemberType);
			_memberType.AddRule(EnumerationRule<BoardMembershipType>.Instance);
			_isDeactivated = new Field<bool?>(_context, () => IsDeactivated);

			TrelloConfiguration.Cache.Add(this);

			_context.Merge(json);
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			if (handler != null)
				handler(this, properties);
		}
	}
}
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
 
	File Name:		Token.cs
	Namespace:		Manatee.Trello
	Class Name:		Token
	Purpose:		Represents a token.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;

namespace Manatee.Trello
{
	public class Token
	{
		private readonly Field<string> _appName;
		private readonly Field<DateTime?> _dateCreated;
		private readonly Field<DateTime?> _dateExpires;
		private readonly Field<Member> _member;
		private readonly TokenContext _context;

		public string AppName { get { return _appName.Value; } }
		public TokenPermission BoardPermissions { get; private set; }
		public DateTime? DateCreated { get { return _dateCreated.Value; } }
		public DateTime? DateExpires { get { return _dateExpires.Value; } }
		public string Id { get; private set; }
		public Member Member { get { return _member.Value; } }
		public TokenPermission MemberPermissions { get; private set; }
		public TokenPermission OrganizationPermissions { get; private set; }

		public event Action<Token, IEnumerable<string>> Updated;

		public Token(string id)
		{
			Id = id;
			_context = new TokenContext(id);
			_context.Synchronized += Synchronized;

			_appName = new Field<string>(_context, () => AppName);
			BoardPermissions = new TokenPermission(_context.BoardPermissions);
			_dateCreated = new Field<DateTime?>(_context, () => DateCreated);
			_dateExpires = new Field<DateTime?>(_context, () => DateExpires);
			_member = new Field<Member>(_context, () => Member);
			MemberPermissions = new TokenPermission(_context.MemberPermissions);
			OrganizationPermissions = new TokenPermission(_context.OrganizationPermissions);

			TrelloConfiguration.Cache.Add(this);
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
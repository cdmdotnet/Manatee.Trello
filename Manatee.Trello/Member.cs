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
 
	File Name:		Member2.cs
	Namespace:		Manatee.Trello
	Class Name:		Member2
	Purpose:		Represents a member.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class Member
	{
		private readonly Field<AvatarSource> _avatarSource;
		private readonly Field<string> _bio;
		private readonly ReadOnlyBoardCollection _boards;
		private readonly Field<string> _fullName;
		private readonly string _id;
		private readonly Field<string> _initials;
		private readonly Field<bool?> _isConfirmed;
		private readonly ReadOnlyOrganizationCollection _organizations;
		private readonly Field<MemberStatus> _status;
		private readonly Field<IEnumerable<string>> _trophies;
		private readonly Field<string> _url;
		private readonly Field<string> _userName;
		internal readonly MemberContext _context;

		public AvatarSource AvatarSource
		{
			get { return _avatarSource.Value; }
			internal set { _avatarSource.Value = value; }
		}
		public string Bio
		{
			get { return _bio.Value; }
			internal set { _bio.Value = value; }
		}
		public ReadOnlyBoardCollection Boards { get { return _boards; } }
		public string FullName
		{
			get { return _fullName.Value; }
			internal set { _fullName.Value = value; }
		}
		public string Id { get { return _id; } }
		public string Initials
		{
			get { return _initials.Value; }
			internal set { _initials.Value = value; }
		}
		public bool? IsConfirmed
		{
			get { return _isConfirmed.Value; }
		}
		public ReadOnlyOrganizationCollection Organizations { get { return _organizations; } }
		public MemberStatus Status { get { return _status.Value; } }
		public IEnumerable<string> Trophies { get { return _trophies.Value; } }
		public string Url { get { return _url.Value; } }
		public string UserName
		{
			get { return _userName.Value; }
			internal set { _userName.Value = value; }
		}

		internal IJsonMember Json { get { return _context.Data; } }

		public Member(string id)
			: this(id, false) {}
		internal Member(string id, bool isMe)
		{
			_context = new MemberContext(id);

			_avatarSource = new Field<AvatarSource>(_context, () => AvatarSource);
			_bio = new Field<string>(_context, () => Bio);
			_boards = isMe ? new BoardCollection(typeof(Member), id) : new ReadOnlyBoardCollection(typeof(Member), id);
			_fullName = new Field<string>(_context, () => FullName);
			_id = id;
			_initials = new Field<string>(_context, () => Initials);
			_initials.AddRule(MemberInitialsRule.Instance);
			_isConfirmed = new Field<bool?>(_context, () => IsConfirmed);
			_organizations = isMe ? new OrganizationCollection(id) : new ReadOnlyOrganizationCollection(id);
			_status = new Field<MemberStatus>(_context, () => Status);
			_trophies = new Field<IEnumerable<string>>(_context, () => Trophies);
			_url = new Field<string>(_context, () => Url);
			_userName = new Field<string>(_context, () => UserName);
			_userName.AddRule(UsernameRule.Instance);

			TrelloConfiguration.Cache.Add(this);
		}
		internal Member(IJsonMember json)
			: this(json.Id)
		{
			_context.Merge(json);
		}
	}
}
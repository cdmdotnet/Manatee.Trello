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
 
	File Name:		Board2.cs
	Namespace:		Manatee.Trello
	Class Name:		Board2
	Purpose:		Represents a board.

***************************************************************************************/

using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class Board
	{
		private readonly ReadOnlyCardCollection _cards;
		private readonly Field<string> _description;
		private readonly string _id;
		private readonly Field<bool?> _isClosed;
		private readonly Field<bool?> _isPinned;
		private readonly Field<bool?> _isSubscribed;
		private readonly ListCollection _lists;
		private readonly ReadOnlyMemberCollection _members;
		private readonly BoardMembershipCollection _memberships;
		private readonly Field<string> _name;
		private readonly Field<Organization> _organization;
		private readonly Field<string> _url;
		private readonly BoardContext _context;

		public ReadOnlyCardCollection Cards { get { return _cards; } }
		public string Description
		{
			get { return _description.Value; }
			set { _description.Value = value; }
		}
		public string Id { get { return _id; } }
		public bool? IsClosed
		{
			get { return _isClosed.Value; }
			set { _isClosed.Value = value; }
		}
		public bool? IsPinned { get { return _isPinned.Value; } }
		public bool? IsSubscribed
		{
			get { return _isSubscribed.Value; }
			set { _isSubscribed.Value = value; }
		}
		public LabelNames LabelNames { get; private set; }
		public ListCollection Lists { get { return _lists; } }
		public ReadOnlyMemberCollection Members { get { return _members; } }
		public BoardMembershipCollection Memberships { get { return _memberships; } }
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		public Organization Organization
		{
			get { return _organization.Value; }
			set { _organization.Value = value; }
		}
		public BoardPreferences Preferences { get; private set; }
		public string Url { get { return _url.Value; } }

		internal IJsonBoard Json { get { return _context.Data; } }

		public Board(string id)
		{
			_context = new BoardContext(id);

			_cards = new ReadOnlyCardCollection(typeof (Board), id);
			_description = new Field<string>(_context, () => Description);
			_id = id;
			_isClosed = new Field<bool?>(_context, () => IsClosed);
			_isClosed.AddRule(NullableHasValueRule<bool>.Instance);
			_isPinned = new Field<bool?>(_context, () => IsPinned);
			_isSubscribed = new Field<bool?>(_context, () => IsSubscribed);
			_isSubscribed.AddRule(NullableHasValueRule<bool>.Instance);
			LabelNames = new LabelNames(_context.LabelNamesContext);
			_lists = new ListCollection(id);
			_members = new ReadOnlyMemberCollection(typeof(Board), id);
			_memberships = new BoardMembershipCollection(id);
			_name = new Field<string>(_context, () => Name);
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_organization = new Field<Organization>(_context, () => Organization);
			Preferences = new BoardPreferences(_context.BoardPreferencesContext);
			_url = new Field<string>(_context, () => Url);

			TrelloConfiguration.Cache.Add(this);
		}
		internal Board(IJsonBoard json)
			: this(json.Id)
		{
			_context.Merge(json);
		}
	}
}
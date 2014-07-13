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
 
	File Name:		Organization2.cs
	Namespace:		Manatee.Trello
	Class Name:		Organization2
	Purpose:		Represents an organization.

***************************************************************************************/

using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class Organization
	{
		private readonly BoardCollection _boards;
		private readonly Field<string> _description;
		private readonly Field<string> _displayName;
		private readonly string _id;
		private readonly Field<bool> _isBusinessClass;
		private readonly ReadOnlyMemberCollection _members;
		private readonly OrganizationMembershipCollection _memberships;
		private readonly Field<string> _name;
		private readonly Field<string> _website;
		private readonly OrganizationContext _context;
		private bool _deleted;

		public BoardCollection Boards { get { return _boards; } }
		public string Description
		{
			get { return _description.Value; }
			set { _description.Value = value; }
		}
		public string DisplayName
		{
			get { return _displayName.Value; }
			set { _displayName.Value = value; }
		}
		public string Id { get { return _id; } }
		public bool IsBusinessClass
		{
			get { return _isBusinessClass.Value; }
		}
		public ReadOnlyMemberCollection Members { get { return _members; } }
		public OrganizationMembershipCollection Memberships { get { return _memberships; } }
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		public OrganizationPreferences Preferences { get; private set; }
		public string Website
		{
			get { return _website.Value; }
			set { _website.Value = value; }
		}

		internal IJsonOrganization Json { get { return _context.Data; } }

		public Organization(string id)
		{
			_context = new OrganizationContext(id);

			_boards = new BoardCollection(typeof (Organization), id);
			_description = new Field<string>(_context, () => Description);
			_displayName = new Field<string>(_context, () => DisplayName);
			_id = id;
			_isBusinessClass = new Field<bool>(_context, () => IsBusinessClass);
			_members = new ReadOnlyMemberCollection(typeof(Organization), id);
			_memberships = new OrganizationMembershipCollection(id);
			_name = new Field<string>(_context, () => Name);
			_name.AddRule(OrganizationNameRule.Instance);
			Preferences = new OrganizationPreferences(_context.OrganizationPreferencesContext);
			_website = new Field<string>(_context, () => Website);
			_website.AddRule(UriRule.Instance);

			TrelloConfiguration.Cache.Add(this);
		}
		internal Organization(IJsonOrganization json)
			: this(json.Id)
		{
			_context.Merge(json);
		}

		public void Delete()
		{
			if (_deleted) return;

			_context.Delete();
			_deleted = true;
			TrelloConfiguration.Cache.Remove(this);
		}
	}
}
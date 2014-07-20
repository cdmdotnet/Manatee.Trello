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

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class Organization : ICanWebhook
	{
		private readonly Field<string> _description;
		private readonly Field<string> _displayName;
		private readonly Field<bool> _isBusinessClass;
		private readonly Field<string> _name;
		private readonly Field<string> _website;
		private readonly OrganizationContext _context;

		private bool _deleted;

		public ReadOnlyActionCollection Actions { get; private set; }
		public BoardCollection Boards { get; private set; }
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
		public string Id { get; private set; }
		public bool IsBusinessClass { get { return _isBusinessClass.Value; } }
		public ReadOnlyMemberCollection Members { get; private set; }
		public OrganizationMembershipCollection Memberships { get; private set; }
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

		public event Action<Organization, IEnumerable<string>> Updated;

		public Organization(string id)
			: this(id, true) {}
		internal Organization(IJsonOrganization json, bool cache)
			: this(json.Id, cache)
		{
			_context.Merge(json);
		}
		private Organization(string id, bool cache)
		{
			Id = id;
			_context = new OrganizationContext(id);
			_context.Synchronized += Synchronized;

			Actions = new ReadOnlyActionCollection(typeof(Organization), id);
			Boards = new BoardCollection(typeof(Organization), id);
			_description = new Field<string>(_context, () => Description);
			_displayName = new Field<string>(_context, () => DisplayName);
			_isBusinessClass = new Field<bool>(_context, () => IsBusinessClass);
			Members = new ReadOnlyMemberCollection(typeof(Organization), id);
			Memberships = new OrganizationMembershipCollection(id);
			_name = new Field<string>(_context, () => Name);
			_name.AddRule(OrganizationNameRule.Instance);
			Preferences = new OrganizationPreferences(_context.OrganizationPreferencesContext);
			_website = new Field<string>(_context, () => Website);
			_website.AddRule(UriRule.Instance);

			if (cache)
				TrelloConfiguration.Cache.Add(this);
		}

		void ICanWebhook.ApplyAction(Action action)
		{
			if (action.Type != ActionType.UpdateOrganization || action.Data.Organization == null || action.Data.Organization.Id != Id)
				return;
			_context.Merge(action.Data.Organization.Json);
		}
		public void Delete()
		{
			if (_deleted) return;

			_context.Delete();
			_deleted = true;
			TrelloConfiguration.Cache.Remove(this);
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
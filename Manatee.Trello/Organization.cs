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
	/// <summary>
	/// Represents an organization.
	/// </summary>
	public class Organization : ICanWebhook, IQueryable
	{
		private readonly Field<string> _description;
		private readonly Field<string> _displayName;
		private readonly Field<bool> _isBusinessClass;
		private readonly Field<string> _name;
		private readonly Field<string> _url;
		private readonly Field<string> _website;
		private readonly OrganizationContext _context;

		private bool _deleted;

		/// <summary>
		/// Gets the collection of actions performed on the organization.
		/// </summary>
		public ReadOnlyActionCollection Actions { get; private set; }
		/// <summary>
		/// Gets the collection of boards owned by the organization.
		/// </summary>
		public BoardCollection Boards { get; private set; }
		/// <summary>
		/// Gets or sets the organization's description.
		/// </summary>
		public string Description
		{
			get { return _description.Value; }
			set { _description.Value = value; }
		}
		/// <summary>
		/// Gets or sets the organization's display name.
		/// </summary>
		public string DisplayName
		{
			get { return _displayName.Value; }
			set { _displayName.Value = value; }
		}
		/// <summary>
		/// Gets the organization's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets whether the organization has business class status.
		/// </summary>
		public bool IsBusinessClass { get { return _isBusinessClass.Value; } }
		/// <summary>
		/// Gets the collection of members who belong to the organization.
		/// </summary>
		public ReadOnlyMemberCollection Members { get; private set; }
		/// <summary>
		/// Gets the collection of members and their priveledges on this organization.
		/// </summary>
		public OrganizationMembershipCollection Memberships { get; private set; }
		/// <summary>
		/// Gets the organization's name.
		/// </summary>
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		/// <summary>
		/// Gets the set of preferences for the organization.
		/// </summary>
		public OrganizationPreferences Preferences { get; private set; }
		/// <summary>
		/// Gets the organization's URL.
		/// </summary>
		public string Url { get { return _url.Value; } }
		/// <summary>
		/// Gets or sets the organization's website.
		/// </summary>
		public string Website
		{
			get { return _website.Value; }
			set { _website.Value = value; }
		}

		internal IJsonOrganization Json { get { return _context.Data; } }

		/// <summary>
		/// Raised when data on the organization is updated.
		/// </summary>
		public event Action<Organization, IEnumerable<string>> Updated;

		/// <summary>
		/// Creates a new instance of the <see cref="Organization"/> object.
		/// </summary>
		/// <param name="id">The organization's ID.</param>
		/// <remarks>
		/// The supplied ID can be either the full ID or the organization's name.
		/// </remarks>
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
			_url = new Field<string>(_context, () => Url);
			_website = new Field<string>(_context, () => Website);
			_website.AddRule(UriRule.Instance);

			if (cache)
				TrelloConfiguration.Cache.Add(this);
		}

		/// <summary>
		/// Applies the changes an action represents.
		/// </summary>
		/// <param name="action">The action.</param>
		void ICanWebhook.ApplyAction(Action action)
		{
			if (action.Type != ActionType.UpdateOrganization || action.Data.Organization == null || action.Data.Organization.Id != Id)
				return;
			_context.Merge(action.Data.Organization.Json);
		}
		/// <summary>
		/// Deletes the organization.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the organization from Trello's server, however, this
		/// object will remain in memory and all properties will remain accessible.
		/// </remarks>
		public void Delete()
		{
			// TODO: Add a CanUpdate() method to the context which is based on whether an object has been deleted.
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
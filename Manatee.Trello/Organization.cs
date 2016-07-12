/***************************************************************************************

	Copyright 2015 Greg Dennis

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
using Manatee.Trello.Internal.DataAccess;
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

		private string _id;
		private DateTime? _creation;

		/// <summary>
		/// Gets the collection of actions performed on the organization.
		/// </summary>
		public ReadOnlyActionCollection Actions { get; }
		/// <summary>
		/// Gets the collection of boards owned by the organization.
		/// </summary>
		public BoardCollection Boards { get; }
		/// <summary>
		/// Gets the creation date of the organization.
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
		public string Id
		{
			get
			{
				if (!_context.HasValidId)
					_context.Synchronize();
				return _id;
			}
			private set { _id = value; }
		}
		/// <summary>
		/// Gets whether the organization has business class status.
		/// </summary>
		public bool IsBusinessClass => _isBusinessClass.Value;
		/// <summary>
		/// Gets the collection of members who belong to the organization.
		/// </summary>
		public ReadOnlyMemberCollection Members { get; }
		/// <summary>
		/// Gets the collection of members and their priveledges on this organization.
		/// </summary>
		public OrganizationMembershipCollection Memberships { get; }
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
		public OrganizationPreferences Preferences { get; }
		/// <summary>
		/// Gets the organization's URL.
		/// </summary>
		public string Url => _url.Value;
		/// <summary>
		/// Gets or sets the organization's website.
		/// </summary>
		public string Website
		{
			get { return _website.Value; }
			set { _website.Value = value; }
		}

		internal IJsonOrganization Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

#if IOS
		private Action<Organization, IEnumerable<string>> _updatedInvoker;

		/// <summary>
		/// Raised when data on the organization is updated.
		/// </summary>
		public event Action<Organization, IEnumerable<string>> Updated
		{
			add { _updatedInvoker += value; }
			remove { _updatedInvoker -= value; }
		}
#else
		/// <summary>
		/// Raised when data on the organization is updated.
		/// </summary>
		public event Action<Organization, IEnumerable<string>> Updated;
#endif

		/// <summary>
		/// Creates a new instance of the <see cref="Organization"/> object.
		/// </summary>
		/// <param name="id">The organization's ID.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided,
		/// <see cref="TrelloAuthorization.Default"/> will be used.</param>
		/// <remarks>
		/// The supplied ID can be either the full ID or the organization's name.
		/// </remarks>
		public Organization(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new OrganizationContext(id, auth);
			_context.Synchronized += Synchronized;

			Actions = new ReadOnlyActionCollection(typeof(Organization), () => Id, auth);
			Boards = new BoardCollection(typeof(Organization), () => Id, auth);
			_description = new Field<string>(_context, nameof(Description));
			_displayName = new Field<string>(_context, nameof(DisplayName));
			_isBusinessClass = new Field<bool>(_context, nameof(IsBusinessClass));
			Members = new ReadOnlyMemberCollection(EntityRequestType.Organization_Read_Members, () => Id, auth);
			Memberships = new OrganizationMembershipCollection(() => Id, auth);
			_name = new Field<string>(_context, nameof(Name));
			_name.AddRule(OrganizationNameRule.Instance);
			Preferences = new OrganizationPreferences(_context.OrganizationPreferencesContext);
			_url = new Field<string>(_context, nameof(Url));
			_website = new Field<string>(_context, nameof(Website));
			_website.AddRule(UriRule.Instance);

			TrelloConfiguration.Cache.Add(this);
		}
		internal Organization(IJsonOrganization json, TrelloAuthorization auth)
			: this(json.Id, auth)
		{
			_context.Merge(json);
		}

		/// <summary>
		/// Applies the changes an action represents.
		/// </summary>
		/// <param name="action">The action.</param>
		public void ApplyAction(Action action)
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
			_context.Delete();
			TrelloConfiguration.Cache.Remove(this);
		}
		/// <summary>
		/// Marks the organization to be refreshed the next time data is accessed.
		/// </summary>
		public void Refresh()
		{
			_context.Expire();
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return DisplayName;
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
#if IOS
			var handler = _updatedInvoker;
#else
			var handler = Updated;
#endif
			handler?.Invoke(this, properties);
		}
	}
}
﻿using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the preferences for an organization.
	/// </summary>
	public interface IOrganizationPreferences
	{
		/// <summary>
		/// Gets or sets the general visibility of the organization.
		/// </summary>
		OrganizationPermissionLevel? PermissionLevel { get; set; }

		/// <summary>
		/// Gets or sets whether external members are disabled.
		/// </summary>
		/// <remarks>
		/// Still researching what this means.
		/// </remarks>
		// TODO: What does ExternalMembersDisabled do?
		bool? ExternalMembersDisabled { get; set; }

		/// <summary>
		/// Gets or sets a domain to associate with the organization.
		/// </summary>
		/// <remarks>
		/// Still researching what this means.
		/// </remarks>
		// TODO: What does AssociatedDomain do?
		string AssociatedDomain { get; set; }

		/// <summary>
		/// Gets or sets the visibility of public-viewable boards owned by the organizations.
		/// </summary>
		OrganizationBoardVisibility? PublicBoardVisibility { get; set; }

		/// <summary>
		/// Gets or sets the visibility of organization-viewable boards owned by the organization.
		/// </summary>
		OrganizationBoardVisibility? OrganizationBoardVisibility { get; set; }

		/// <summary>
		/// Gets or sets the visibility of private-viewable boards owned by the organization.
		/// </summary>
		OrganizationBoardVisibility? PrivateBoardVisibility { get; set; }
	}

	/// <summary>
	/// Represents the preferences for an organization.
	/// </summary>
	public class OrganizationPreferences : IOrganizationPreferences
	{
		private readonly Field<OrganizationPermissionLevel?> _permissionLevel;
		private readonly Field<bool?> _externalMembersDisabled;
		private readonly Field<string> _assocatedDomain;
		private readonly Field<OrganizationBoardVisibility?> _publicBoardVisibility;
		private readonly Field<OrganizationBoardVisibility?> _organizationBoardVisibility;
		private readonly Field<OrganizationBoardVisibility?> _privateBoardVisibility;
		private OrganizationPreferencesContext _context;

		/// <summary>
		/// Gets or sets the general visibility of the organization.
		/// </summary>
		public OrganizationPermissionLevel? PermissionLevel
		{
			get { return _permissionLevel.Value; }
			set { _permissionLevel.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether external members are disabled.
		/// </summary>
		/// <remarks>
		/// Still researching what this means.
		/// </remarks>
		// TODO: What does ExternalMembersDisabled do?
		public bool? ExternalMembersDisabled
		{
			get { return _externalMembersDisabled.Value; }
			set { _externalMembersDisabled.Value = value; }
		}
		/// <summary>
		/// Gets or sets a domain to associate with the organization.
		/// </summary>
		/// <remarks>
		/// Still researching what this means.
		/// </remarks>
		// TODO: What does AssociatedDomain do?
		public string AssociatedDomain
		{
			get { return _assocatedDomain.Value; }
			set { _assocatedDomain.Value = value; }
		}
		/// <summary>
		/// Gets or sets the visibility of public-viewable boards owned by the organizations.
		/// </summary>
		public OrganizationBoardVisibility? PublicBoardVisibility
		{
			get { return _publicBoardVisibility.Value; }
			set { _publicBoardVisibility.Value = value; }
		}
		/// <summary>
		/// Gets or sets the visibility of organization-viewable boards owned by the organization.
		/// </summary>
		public OrganizationBoardVisibility? OrganizationBoardVisibility
		{
			get { return _organizationBoardVisibility.Value; }
			set { _organizationBoardVisibility.Value = value; }
		}
		/// <summary>
		/// Gets or sets the visibility of private-viewable boards owned by the organization.
		/// </summary>
		public OrganizationBoardVisibility? PrivateBoardVisibility
		{
			get { return _privateBoardVisibility.Value; }
			set { _privateBoardVisibility.Value = value; }
		}

		internal OrganizationPreferences(OrganizationPreferencesContext context)
		{
			_context = context;

			_permissionLevel = new Field<OrganizationPermissionLevel?>(_context, nameof(PermissionLevel));
			_permissionLevel.AddRule(NullableHasValueRule<OrganizationPermissionLevel>.Instance);
			_permissionLevel.AddRule(EnumerationRule<OrganizationPermissionLevel?>.Instance);
			_externalMembersDisabled = new Field<bool?>(_context, nameof(ExternalMembersDisabled));
			_externalMembersDisabled.AddRule(NullableHasValueRule<bool>.Instance);
			_assocatedDomain = new Field<string>(_context, nameof(AssociatedDomain));
			_publicBoardVisibility = new Field<OrganizationBoardVisibility?>(_context, nameof(PublicBoardVisibility));
			_publicBoardVisibility.AddRule(NullableHasValueRule<OrganizationBoardVisibility>.Instance);
			_publicBoardVisibility.AddRule(EnumerationRule<OrganizationBoardVisibility?>.Instance);
			_organizationBoardVisibility = new Field<OrganizationBoardVisibility?>(_context, nameof(OrganizationBoardVisibility));
			_organizationBoardVisibility.AddRule(NullableHasValueRule<OrganizationBoardVisibility>.Instance);
			_organizationBoardVisibility.AddRule(EnumerationRule<OrganizationBoardVisibility?>.Instance);
			_privateBoardVisibility = new Field<OrganizationBoardVisibility?>(_context, nameof(PrivateBoardVisibility));
			_privateBoardVisibility.AddRule(NullableHasValueRule<OrganizationBoardVisibility>.Instance);
			_privateBoardVisibility.AddRule(EnumerationRule<OrganizationBoardVisibility?>.Instance);
		}
	}
}
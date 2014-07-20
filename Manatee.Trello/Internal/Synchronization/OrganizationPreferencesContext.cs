using System.Collections.Generic;
using Manatee.Trello.Enumerations;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class OrganizationPreferencesContext : LinkedSynchronizationContext<IJsonOrganizationPreferences>
	{
		static OrganizationPreferencesContext()
		{
			_properties = new Dictionary<string, Property<IJsonOrganizationPreferences>>
				{
					{"PermissionLevel", new Property<IJsonOrganizationPreferences, OrganizationPermissionLevel>(d => d.PermissionLevel, (d, o) => d.PermissionLevel = o)},
					{"ExternalMembersDisabled", new Property<IJsonOrganizationPreferences, bool?>(d => d.ExternalMembersDisabled, (d, o) => d.ExternalMembersDisabled = o)},
					{"AssociatedDomain", new Property<IJsonOrganizationPreferences, string>(d => d.AssociatedDomain, (d, o) => d.AssociatedDomain = o)},
					{"PublicBoardVisibility", new Property<IJsonOrganizationPreferences, OrganizationBoardVisibility>(d => d.BoardVisibilityRestrict.Public, (d, o) => d.BoardVisibilityRestrict.Public = o)},
					{"OrganizationBoardVisibility", new Property<IJsonOrganizationPreferences, OrganizationBoardVisibility>(d => d.BoardVisibilityRestrict.Org, (d, o) => d.BoardVisibilityRestrict.Org = o)},
					{"PrivateBoardVisibility", new Property<IJsonOrganizationPreferences, OrganizationBoardVisibility>(d => d.BoardVisibilityRestrict.Private, (d, o) => d.BoardVisibilityRestrict.Private = o)}
				};
		}
		internal OrganizationPreferencesContext()
		{
			Data.BoardVisibilityRestrict = TrelloConfiguration.JsonFactory.Create<IJsonBoardVisibilityRestrict>();
		}
	}
}
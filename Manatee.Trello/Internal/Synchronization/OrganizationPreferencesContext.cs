using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class OrganizationPreferencesContext : LinkedSynchronizationContext<IJsonOrganizationPreferences>
	{
		static OrganizationPreferencesContext()
		{
			_properties = new Dictionary<string, Property<IJsonOrganizationPreferences>>
				{
					{"PermissionLevel", new Property<IJsonOrganizationPreferences, OrganizationPermissionLevel?>((d, a) => d.PermissionLevel, (d, o) => d.PermissionLevel = o)},
					{"ExternalMembersDisabled", new Property<IJsonOrganizationPreferences, bool?>((d, a) => d.ExternalMembersDisabled, (d, o) => d.ExternalMembersDisabled = o)},
					{"AssociatedDomain", new Property<IJsonOrganizationPreferences, string>((d, a) => d.AssociatedDomain, (d, o) => d.AssociatedDomain = o)},
					{"PublicBoardVisibility", new Property<IJsonOrganizationPreferences, OrganizationBoardVisibility?>((d, a) => d.BoardVisibilityRestrict.Public, (d, o) => d.BoardVisibilityRestrict.Public = o)},
					{"OrganizationBoardVisibility", new Property<IJsonOrganizationPreferences, OrganizationBoardVisibility?>((d, a) => d.BoardVisibilityRestrict.Org, (d, o) => d.BoardVisibilityRestrict.Org = o)},
					{"PrivateBoardVisibility", new Property<IJsonOrganizationPreferences, OrganizationBoardVisibility?>((d, a) => d.BoardVisibilityRestrict.Private, (d, o) => d.BoardVisibilityRestrict.Private = o)}
				};
		}
		internal OrganizationPreferencesContext(TrelloAuthorization auth)
			: base(auth)
		{
			Data.BoardVisibilityRestrict = TrelloConfiguration.JsonFactory.Create<IJsonBoardVisibilityRestrict>();
		}
	}
}
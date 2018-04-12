using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class OrganizationPreferencesContext : LinkedSynchronizationContext<IJsonOrganizationPreferences>
	{
		static OrganizationPreferencesContext()
		{
			Properties = new Dictionary<string, Property<IJsonOrganizationPreferences>>
				{
					{
						nameof(OrganizationPreferences.PermissionLevel),
						new Property<IJsonOrganizationPreferences, OrganizationPermissionLevel?>(
							(d, a) => d.PermissionLevel,
							(d, o) => d.PermissionLevel = o)
					},
					{
						nameof(OrganizationPreferences.ExternalMembersDisabled),
						new Property<IJsonOrganizationPreferences, bool?>((d, a) => d.ExternalMembersDisabled,
						                                                  (d, o) => d.ExternalMembersDisabled = o)
					},
					{
						nameof(OrganizationPreferences.AssociatedDomain),
						new Property<IJsonOrganizationPreferences, string>((d, a) => d.AssociatedDomain,
						                                                   (d, o) => d.AssociatedDomain = o)
					},
					{
						nameof(OrganizationPreferences.PublicBoardVisibility),
						new Property<IJsonOrganizationPreferences, OrganizationBoardVisibility?>(
							(d, a) => d.BoardVisibilityRestrict.Public,
							(d, o) => d.BoardVisibilityRestrict.Public = o)
					},
					{
						nameof(OrganizationPreferences.OrganizationBoardVisibility),
						new Property<IJsonOrganizationPreferences, OrganizationBoardVisibility?>(
							(d, a) => d.BoardVisibilityRestrict.Org,
							(d, o) => d.BoardVisibilityRestrict.Org = o)
					},
					{
						nameof(OrganizationPreferences.PrivateBoardVisibility),
						new Property<IJsonOrganizationPreferences, OrganizationBoardVisibility?>(
							(d, a) => d.BoardVisibilityRestrict.Private, 
							(d, o) => d.BoardVisibilityRestrict.Private = o)
					}
				};
		}
		internal OrganizationPreferencesContext(TrelloAuthorization auth)
			: base(auth)
		{
			Data.BoardVisibilityRestrict = TrelloConfiguration.JsonFactory.Create<IJsonBoardVisibilityRestrict>();
		}
	}
}
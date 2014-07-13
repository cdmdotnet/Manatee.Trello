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
					{
						"PermissionLevel", new Property<IJsonOrganizationPreferences>(d => d.PermissionLevel.ConvertEnum<OrganizationPermissionLevel>(),
						                                                              (d, o) => d.PermissionLevel = ((OrganizationPermissionLevel) o).ConvertEnum())
					},
					{"ExternalMembersDisabled", new Property<IJsonOrganizationPreferences>(d => d.ExternalMembersDisabled, (d, o) => d.ExternalMembersDisabled = (bool?) o)},
					{"AssociatedDomain", new Property<IJsonOrganizationPreferences>(d => d.AssociatedDomain, (d, o) => d.AssociatedDomain = (string) o)},
					{
						"PublicBoardVisibility", new Property<IJsonOrganizationPreferences>(d => d.BoardVisibilityRestrict.Public.ConvertEnum<OrganizationBoardVisibility>(),
						                                                                    (d, o) => d.BoardVisibilityRestrict.Public = ((OrganizationBoardVisibility) o).ConvertEnum())
					},
					{
						"OrganizationBoardVisibility", new Property<IJsonOrganizationPreferences>(d => d.BoardVisibilityRestrict.Org.ConvertEnum<OrganizationBoardVisibility>(),
						                                                                          (d, o) => d.BoardVisibilityRestrict.Org = ((OrganizationBoardVisibility) o).ConvertEnum())
					},
					{
						"PrivateBoardVisibility", new Property<IJsonOrganizationPreferences>(d => d.BoardVisibilityRestrict.Private.ConvertEnum<OrganizationBoardVisibility>(),
						                                                                     (d, o) => d.BoardVisibilityRestrict.Private = ((OrganizationBoardVisibility) o).ConvertEnum())
					}
				};
		}
		internal OrganizationPreferencesContext()
		{
			Data.BoardVisibilityRestrict = TrelloConfiguration.JsonFactory.Create<IJsonBoardVisibilityRestrict>();
		}
	}
}
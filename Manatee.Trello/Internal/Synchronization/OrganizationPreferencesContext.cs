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
 
	File Name:		OrganizationPreferencesContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		OrganizationPreferencesContext
	Purpose:		Provides a data context for an organization's preferences.

***************************************************************************************/
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
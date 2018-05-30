---
title: IJsonOrganizationPreferences
category: API
order: 115
---

Defines the JSON structure for the OrganizationPreferences object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonOrganizationPreferences

## Properties

### string AssociatedDomain { get; set; }

Gets or sets the Google Apps domain.

### [IJsonBoardVisibilityRestrict](../IJsonBoardVisibilityRestrict#ijsonboardvisibilityrestrict) BoardVisibilityRestrict { get; set; }

Gets or sets the visibility of boards owned by the organization.

### bool? ExternalMembersDisabled { get; set; }

Gets or sets whether external members are disabled.

### List&lt;Object&gt; OrgInviteRestrict { get; set; }

Gets or sets organization invitation restrictions.

### [OrganizationPermissionLevel](../OrganizationPermissionLevel#organizationpermissionlevel)? PermissionLevel { get; set; }

Gets or sets the permission level.


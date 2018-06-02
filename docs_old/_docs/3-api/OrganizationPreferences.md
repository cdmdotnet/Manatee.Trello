---
title: OrganizationPreferences
category: API
order: 215
---

Represents the preferences for an organization.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- OrganizationPreferences

## Properties

### string AssociatedDomain { get; set; }

Gets or sets a domain to associate with the organization.

#### Remarks

Still researching what this means.

### bool? ExternalMembersDisabled { get; set; }

Gets or sets whether external members are disabled.

#### Remarks

Still researching what this means.

### [OrganizationBoardVisibility](../OrganizationBoardVisibility#organizationboardvisibility)? OrganizationBoardVisibility { get; set; }

Gets or sets the visibility of organization-viewable boards owned by the organization.

### [OrganizationPermissionLevel](../OrganizationPermissionLevel#organizationpermissionlevel)? PermissionLevel { get; set; }

Gets or sets the general visibility of the organization.

### [OrganizationBoardVisibility](../OrganizationBoardVisibility#organizationboardvisibility)? PrivateBoardVisibility { get; set; }

Gets or sets the visibility of private-viewable boards owned by the organization.

### [OrganizationBoardVisibility](../OrganizationBoardVisibility#organizationboardvisibility)? PublicBoardVisibility { get; set; }

Gets or sets the visibility of public-viewable boards owned by the organizations.


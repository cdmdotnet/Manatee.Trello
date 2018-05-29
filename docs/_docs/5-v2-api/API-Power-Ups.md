---
title: Power-Ups
category: Version 2.x - API
order: 12
---

> **NOTICE** In migrating to this new documentation, many (if not all) of the links are broken.  Please use the sidebar on the left for navigation.

# PowerUpBase

Provides a base implementation for Trello Power-Ups.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- PowerUpBase

## Properties

### string AdditionalInfo { get; }

Gets a URI that provides JSON-formatted data about the plugin.

### string Id { get; }

Gets the power-up&#39;s ID.

### string Name { get; }

Gets the name of the power-up.

### bool? Public { get; }

Gets or sets whether this power-up is closed.

## Methods

### string ToString()

Returns the [Name](/API-Power-Ups#string-name--get-)

**Returns:** A string that represents the current object.

# UnknownPowerUp

Provides an implementation for an unknown power-up.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- PowerUpBase
- UnknownPowerUp

# ReadOnlyPowerUpCollection

A read-only collection of power-ups.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;IPowerUp&gt;
- ReadOnlyPowerUpCollection

#### Remarks

If a power-up hasn&#39;t been registered, it will be instantiated using [UnknownPowerUp](/API-Power-Ups#unknownpowerup).

# PowerUpData

Represents the data associated with a power-up.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- PowerUpData

## Properties

### string Id { get; }

Gets the ID associated with this particular data instance.

### string PluginId { get; }

Gets the ID for the power-up with which this data is associated.

### string Value { get; }

Gets the data as a string. This data will be JSON-encoded.

# ReadOnlyPowerUpDataCollection

A read-only collection of power-up data.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;PowerUpData&gt;
- ReadOnlyPowerUpDataCollection

# OrganizationMembershipCollection

A collection of organization memberships.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;OrganizationMembership&gt;
- ReadOnlyOrganizationMembershipCollection
- OrganizationMembershipCollection

## Methods

### void Add(Member member, OrganizationMembershipType membership)

Adds a member to an organization with specified privileges.

**Parameter:** member

The member to add.

**Parameter:** membership

The membership type.

### void Remove(Member member)

Removes a member from an organization.

**Parameter:** member

The member to remove.

# OrganizationPreferences

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

### [OrganizationBoardVisibility](/API-Organizations#organizationboardvisibility)? OrganizationBoardVisibility { get; set; }

Gets or sets the visibility of organization-viewable boards owned by the organization.

### [OrganizationPermissionLevel](/API-Organizations#organizationpermissionlevel)? PermissionLevel { get; set; }

Gets or sets the general visibility of the organization.

### [OrganizationBoardVisibility](/API-Organizations#organizationboardvisibility)? PrivateBoardVisibility { get; set; }

Gets or sets the visibility of private-viewable boards owned by the organization.

### [OrganizationBoardVisibility](/API-Organizations#organizationboardvisibility)? PublicBoardVisibility { get; set; }

Gets or sets the visibility of public-viewable boards owned by the organizations.


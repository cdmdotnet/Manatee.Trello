---
title: IJsonOrganization
category: API
order: 113
---

Defines the JSON structure for the Organization object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonOrganization

## Properties

### List&lt;IJsonAction&gt; Actions { get; set; }

Gets or sets a collection of actions.

### List&lt;IJsonBoard&gt; Boards { get; set; }

Gets or sets a collection of boards.

### string Desc { get; set; }

Gets or sets the description for the organization.

### string DisplayName { get; set; }

Gets or sets the name to be displayed for the organization.

### string LogoHash { get; set; }

Gets or sets the organization&#39;s logo hash.

### List&lt;IJsonMember&gt; Members { get; set; }

Gets or sets a collection of members.

### List&lt;IJsonOrganizationMembership&gt; Memberships { get; set; }

Gets or sets a collection of memberships.

### string Name { get; set; }

Gets or sets the name of the organization.

### bool? PaidAccount { get; set; }

Gets or sets whether the organization is a paid account.

### List&lt;IJsonPowerUpData&gt; PowerUpData { get; set; }

Gets or sets a collection of power-up data.

### List&lt;int&gt; PowerUps { get; set; }

Enumerates the powerups obtained by the organization.

### [IJsonOrganizationPreferences](../IJsonOrganizationPreferences#ijsonorganizationpreferences) Prefs { get; set; }

Gets or sets a set of preferences for the organization.

### List&lt;string&gt; PremiumFeatures { get; set; }

Gets or sets a collection of premium features available to the organization.

### string Url { get; set; }

Gets or sets the URL to the organization&#39;s profile.

### string Website { get; set; }

Gets or sets the organization&#39;s website.


---
title: IReadOnlyOrganizationCollection
category: API
order: 103
---

# IReadOnlyOrganizationCollection

A read-only collection of organizations.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IReadOnlyOrganizationCollection

## Properties

### [IOrganization](IOrganization#iorganization) this[string key] { get; }

Retrieves a organization which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching organization, or null if none found.

#### Remarks

Matches on Organization.Id, Organization.Name, and Organization.DisplayName. Comparison is case-sensitive.

## Methods

### void Filter(OrganizationFilter filter)

Adds a filter to the collection.

**Parameter:** filter

The filter value.


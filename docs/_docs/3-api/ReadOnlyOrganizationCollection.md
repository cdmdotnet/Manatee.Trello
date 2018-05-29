---
title: ReadOnlyOrganizationCollection
category: API
order: 229
---

A read-only collection of organizations.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;IOrganization&gt;
- ReadOnlyOrganizationCollection

## Properties

### [IOrganization](../IOrganization#iorganization) this[string key] { get; }

Retrieves a organization which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching organization, or null if none found.

#### Remarks

Matches on organization ID, name, and display name. Comparison is case-sensitive.

## Methods

### void Filter(OrganizationFilter filter)

Adds a filter to the collection.

**Parameter:** filter

The filter value.


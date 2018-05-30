---
title: IReadOnlyOrganizationMembershipCollection
category: API
order: 160
---

A read-only collection of organization memberships.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IReadOnlyOrganizationMembershipCollection

## Properties

### [IOrganizationMembership](../IOrganizationMembership#iorganizationmembership) this[string key] { get; }

Retrieves a membership which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching list, or null if none found.

#### Remarks

Matches on OrganizationMembership.Id, OrganizationMembership.Member.Id, OrganizationMembership.Member.FullName, and OrganizationMembership.Member.Username. Comparison is case-sensitive.

## Methods

### void Filter(MembershipFilter filter)

Adds a filter to the collection.

**Parameter:** filter

The filter value.


---
title: ReadOnlyOrganizationMembershipCollection
category: API
order: 230
---

A read-only collection of organization memberships.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;IOrganizationMembership&gt;
- ReadOnlyOrganizationMembershipCollection

## Properties

### [IOrganizationMembership](../IOrganizationMembership#iorganizationmembership) this[string key] { get; }

Retrieves a membership which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching list, or null if none found.

#### Remarks

Matches on membership ID, member ID, member full name, and member username. Comparison is case-sensitive.

## Methods

### void Filter(MembershipFilter filter)

Adds a filter to the collection.

**Parameter:** filter

The filter value.

### void Filter(IEnumerable&lt;MembershipFilter&gt; filters)

Adds a set of filters to the collection.

**Parameter:** filters

The filter values.


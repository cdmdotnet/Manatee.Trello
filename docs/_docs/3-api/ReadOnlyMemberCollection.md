---
title: ReadOnlyMemberCollection
category: API
order: 227
---

A read-only collection of members.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;IMember&gt;
- ReadOnlyMemberCollection

## Properties

### [IMember](../IMember#imember) this[string key] { get; }

Retrieves a member which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching member, or null if none found.

#### Remarks

Matches on member ID, full name, and username. Comparison is case-sensitive.

## Methods

### void Filter(MemberFilter filter)

Adds a filter to the collection.

**Parameter:** filter

The filter value.

### void Filter(IEnumerable&lt;MemberFilter&gt; filters)

Adds a set of filters to the collection.

**Parameter:** filters

The filter values.


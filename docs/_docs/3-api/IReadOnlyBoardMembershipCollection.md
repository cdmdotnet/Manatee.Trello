---
title: IReadOnlyBoardMembershipCollection
category: API
order: 95
---

# IReadOnlyBoardMembershipCollection

A read-only collection of board memberships.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IReadOnlyBoardMembershipCollection

## Properties

### [IBoardMembership](IBoardMembership#iboardmembership) this[string key] { get; }

Retrieves a membership which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching membership, or null if none found.

#### Remarks

Matches on BoardMembership.Id, BoardMembership.Member.Id, BoardMembership.Member.Name, and BoardMembership.Usernamee. Comparison is case-sensitive.

## Methods

### void Filter(MembershipFilter membership)

Adds a filter to the collection.

**Parameter:** membership

The filter value.


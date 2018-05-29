---
title: MemberCollection
category: API
order: 132
---

# MemberCollection

A collection of members.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;IMember&gt;
- ReadOnlyMemberCollection
- MemberCollection

## Methods

### Task Add(IMember member, CancellationToken ct)

Adds a member to the collection.

**Parameter:** member

The member to add.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### Task Remove(IMember member, CancellationToken ct)

Removes a member from the collection.

**Parameter:** member

The member to remove.

**Parameter:** ct

(Optional) A cancellation token for async processing.


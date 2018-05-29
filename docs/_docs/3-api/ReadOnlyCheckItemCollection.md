---
title: ReadOnlyCheckItemCollection
category: API
order: 164
---

# ReadOnlyCheckItemCollection

A read-only collection of checklist items.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;ICheckItem&gt;
- ReadOnlyCheckItemCollection

## Properties

### [ICheckItem](ICheckItem#icheckitem) this[string key] { get; }

Retrieves a check list item which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching check list item, or null if none found.

#### Remarks

Matches on check item ID and name. Comparison is case-sensitive.


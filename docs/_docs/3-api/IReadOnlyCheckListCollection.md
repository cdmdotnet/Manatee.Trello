---
title: IReadOnlyCheckListCollection
category: API
order: 159
---

A read-only collection of checklists.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IReadOnlyCheckListCollection

## Properties

### [ICheckList](../ICheckList#ichecklist) this[string key] { get; }

Retrieves a check list which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching check list, or null if none found.

#### Remarks

Matches on CheckList.Id and CheckList.Name. Comparison is case-sensitive.


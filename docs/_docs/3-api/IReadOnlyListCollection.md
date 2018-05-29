---
title: IReadOnlyListCollection
category: API
order: 156
---

A read-only collection of lists.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IReadOnlyListCollection

## Properties

### [IList](../IList#ilist) this[string key] { get; }

Retrieves a list which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching list, or null if none found.

#### Remarks

Matches on List.Id and List.Name. Comparison is case-sensitive.

## Methods

### void Filter(ListFilter filter)

Adds a filter to the collection.

**Parameter:** filter

The filter value.


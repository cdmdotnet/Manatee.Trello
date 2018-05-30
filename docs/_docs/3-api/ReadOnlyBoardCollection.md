---
title: ReadOnlyBoardCollection
category: API
order: 217
---

A read-only collectin of boards.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;IBoard&gt;
- ReadOnlyBoardCollection

## Properties

### [IBoard](../IBoard#iboard) this[string key] { get; }

Retrieves a board which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching board, or null if none found.

#### Remarks

Matches on board ID and name. Comparison is case-sensitive.

## Methods

### void Filter(BoardFilter filter)

Adds a filter to the collection.

**Parameter:** filter

The filter value.


---
title: IReadOnlyBoardCollection
category: API
order: 155
---

A read-only collectin of boards.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IReadOnlyBoardCollection

## Properties

### [IBoard](../IBoard#iboard) this[string key] { get; }

Retrieves a board which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching board, or null if none found.

#### Remarks

Matches on Board.Id and Board.Name. Comparison is case-sensitive.

## Methods

### void Filter(BoardFilter filter)

Adds a filter to the collection.

**Parameter:** filter

The filter value.

